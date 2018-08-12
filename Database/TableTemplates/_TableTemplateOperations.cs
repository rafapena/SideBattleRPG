using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using Database.Utilities;
using static Database.Utilities.TableBuilder;

/*
 * Single string column
 * Single ComboBox
 * TwoNums: ComboBox of class objects + Positive integer input
 * Single Enemies for Enemy Groups (LOTS of content)
*/

namespace Database.TableTemplates
{
    public abstract class _TableTemplateOperations : UserControl, ObjectOperations
    {
        public Grid Table { get; set; }
        public List<string> ColumnNames { get; set; }
        public List<string> Inputs { get; set; }
        public List<List<UIElement>> InputElements { get; set; }

        public string TableTitle { get; private set; }
        public string TargetClass { get; private set; }
        public string TargetTable { get; private set; }
        public string TableTemplateTable { get; private set; }
        public int ScrollerHeight { get; private set; }
        public int Count { get; private set; }


        protected abstract void OnAddRow();
        public void AddRow(object sender, RoutedEventArgs e)
        {
            Count++;
            Table.RowDefinitions.Add(new RowDefinition());
            TextBlock t = TextBlock(Count, Count, 0);
            t.Name = "INDEX" + Count;
            RegisterName(t.Name, t);
            t.HorizontalAlignment = HorizontalAlignment.Center;
            Table.Children.Add(t);
            InputElements.Add(new List<UIElement>());
            if (sender == null || Inputs == null) return;
            OnAddRow();
            AddRangeToTable();
        }

        protected void RemoveRow(object sender, RoutedEventArgs e)
        {
            if (Count <= 0) return;
            TextBlock t = (TextBlock)FindName("INDEX" + Count);
            Table.Children.Remove(t);
            UnregisterName(t.Name);
            for (int i = 0; i < Inputs.Count; i++)
            {
                string name = Inputs[i] + Count;
                UIElement uie = (UIElement)FindName(name);
                Table.Children.Remove(uie);
                UnregisterName(name);
            }
            InputElements.RemoveAt(InputElements.Count-1);
            Table.RowDefinitions.RemoveAt(Count);
            Count--;
        }

        protected void AddRangeToTable()
        {
            int row = Count - 1;
            for (int i = 0; i < Inputs.Count; i++)
            {
                RegisterName(Inputs[i] + Count, InputElements[row][i]);
                Table.Children.Add(InputElements[row][i]);
            }
        }

        protected abstract void OnInitializeNew();
        public void InitializeNew(string targetClass, string hostAndTargetTable, string title,
            List<string> columnNames=null, int scrollerHeight=100)
        {
            TargetClass = targetClass;
            TableTemplateTable = hostAndTargetTable;
            string[] toGetTable = hostAndTargetTable.Split('_');
            TargetTable = toGetTable.Length > 2 ? toGetTable[2] : "";
            TableTitle = title;
            ScrollerHeight = scrollerHeight;
            if (columnNames != null && columnNames.Count > 0)
            {
                ColumnNames = new List<string>();
                ColumnNames.Add("#");
                ColumnNames.AddRange(columnNames);
            }
            Inputs = columnNames;
            InputElements = new List<List<UIElement>>();
            Count = 0;
            OnInitializeNew();
            TableSetup(Table, ColumnNames);
            Read();
        }
        public virtual void InitializeNew() { }     // Use the function above instead


        protected abstract void OnAutomate(int i);
        public void Automate()
        {
            for (int i = 0; i < Count; i++) OnAutomate(i);
        }

        protected abstract string OnValidateInputs(int i);
        public string ValidateInputs()
        {
            string err = "";
            for (int i = 0; i < Count; i++)
            {
                err += OnValidateInputs(i);
                if (err != "") break;
            }
            return err;
        }

        protected abstract void OnParameterizeInputs(int i);
        public void ParameterizeInputs()
        {
            int size = Count * Inputs.Count;
            for (int i = 0; i < size; i += Inputs.Count) OnParameterizeInputs(i);
        }
        public void ParameterizeInput(string parameterized, string input)
        {
            SQLDB.Inputs.Add(new SQLiteParameter(parameterized, input));
        }


        public void Create()
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            CreateRows(0);
            SQLDB.Inputs = null;
        }
        private void CreateRows(int startingI)
        {
            for (int i = startingI; i < Count; i++)
            {
                string[] str = OnUpdateAddRow(i);
                SQLDB.Command("INSERT INTO " + TableTemplateTable + " (" + str[0] + ") VALUES (" + str[1] + ");");
            }
        }


        protected virtual string[] OnReadCommands()
        {
            return new string[] {
                TableTemplateTable + " JOIN  BaseObjects JOIN " + TargetTable,
                TargetClass + "_ID = " + TargetClass + "ID AND BaseObject_ID = BaseObjectID ORDER BY BaseObject_ID"
            };
        }
        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            if (Inputs == null) return;
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                string[] readStr = OnReadCommands();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + readStr[0] + " WHERE " + readStr[1] + ";", conn))
                {
                    while (reader.Read())
                    {
                        AddRow(null, null);
                        OnRead(reader);
                        AddRangeToTable();
                    }
                }
                conn.Close();
            }
        }


        protected virtual string OnUpdateCountCondition()
        {
            return SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId;
        }
        protected abstract string[] OnUpdateAddRow(int i);
        protected abstract string OnUpdateRemovedRowCondition();
        protected abstract string[] OnUpdateRow(int i);
        public void Update()
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            /*int prevCount = SQLDB.GetScalar("SELECT COUNT(*) FROM " + TableTemplateTable + " WHERE " + OnUpdateCountCondition());
            if (Count > prevCount) CreateRows(prevCount);
            if (Count < prevCount) SQLDB.Command("DELETE FROM " + TableTemplateTable + " WHERE " + OnUpdateRemovedRowCondition());
            for (int i = 0; i < Count; i++)
            {
                string[] str = OnUpdateRow(i);
                SQLDB.Command("UPDATE " + TableTemplateTable + " SET " + str[0] + " WHERE " + str[1]);
            }*/
            SQLDB.Command("DELETE FROM " + TableTemplateTable + " WHERE " + OnUpdateRemovedRowCondition());
            CreateRows(0);
            SQLDB.Inputs = null;
        }


        public void Delete()
        {
            SQLDB.Command("DELETE FROM " + TableTemplateTable + " WHERE " + SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId + ";");
        }
        
        // Not needed: No ID to keep track of and is only implemented here (Create() and OnUpdateAddRow handle everything)
        public void Clone() { }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Other functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static List<string> getFromQuery(string table, string attribute)
        {
            List<string> list = new List<string>();
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve(
                    "SELECT * FROM BaseObjects JOIN " + table + " " +
                    "WHERE BaseObject_ID = BaseObjectID ORDER BY BaseObject_ID ASC;",
                    conn))
                {
                    while (reader.Read()) list.Add(reader[attribute].ToString());
                }
                conn.Close();
            }
            return list;
        }
    }
}