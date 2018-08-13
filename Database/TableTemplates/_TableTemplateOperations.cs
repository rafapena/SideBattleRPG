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
        public List<string> ColumnNames { get; private set; }
        public List<List<UIElement>> Elements { get; private set; }

        public string TableTitle { get; private set; }
        public string TargetClass { get; private set; }
        public string TargetDBTable { get; private set; }
        public string TableTemplateDBTable { get; private set; }
        public int ScrollerHeight { get; private set; }
        public int Count { get; private set; }


        protected abstract void OnAddRow();
        public void AddRow(object sender, RoutedEventArgs e)
        {
            Count++;
            Table.RowDefinitions.Add(new RowDefinition());
            TextBlock t = TextBlock(Count, Count, 0);
            t.HorizontalAlignment = HorizontalAlignment.Center;
            Elements.Add(new List<UIElement>() { t });
            Table.Children.Add(t);
            if (sender == null) return;
            OnAddRow();
            AddRangeToTable();
        }

        protected void RemoveRow(object sender, RoutedEventArgs e)
        {
            if (Count <= 0) return;
            List<UIElement> elmts = Elements[Count - 1];
            for (int i = 0; i < elmts.Count; i++) Table.Children.Remove(elmts[i]);
            Elements.RemoveAt(Count - 1);
            Table.RowDefinitions.RemoveAt(Count);
            Count--;
        }

        protected void AddRangeToTable()
        {
            List<UIElement> elmts = Elements[Count - 1];
            for (int i = 1; i < elmts.Count; i++) Table.Children.Add(elmts[i]);
        }

        public void SetupTableData(string targetClass, string hostAndTargetTable, string title,
            List<string> columnNames = null, int scrollerHeight = 100)
        {
            string[] toGetTable = hostAndTargetTable.Split('_');
            TargetClass = targetClass;
            TableTemplateDBTable = hostAndTargetTable;
            TargetDBTable = toGetTable.Length > 2 ? toGetTable[2] : "";
            TableTitle = title;
            ScrollerHeight = scrollerHeight;
            if (columnNames != null && columnNames.Count > 0)
            {
                ColumnNames = new List<string>();
                ColumnNames.Add("#");
                ColumnNames.AddRange(columnNames);
            }
            InitializeNew();
        }

        protected abstract void OnInitializeNew();
        public void InitializeNew() // Not too useful: SetupTableData does all of the work, due to naming conventions
        {
            Elements = new List<List<UIElement>>();
            Count = 0;
            OnInitializeNew();
            TableSetup(Table, ColumnNames);
        }


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
            for (int i = 0; i < Count; i++) OnParameterizeInputs(i);
        }
        public void ParameterizeInput(string parameterized, string input)
        {
            SQLDB.Inputs.Add(new SQLiteParameter(parameterized, input));
        }


        protected abstract string[] OnCreate(int i);
        public void Create()
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            for (int i = 0; i < Count; i++)
            {
                string[] str = OnCreate(i);
                SQLDB.Command("INSERT INTO " + TableTemplateDBTable + " (" + str[0] + ") VALUES (" + str[1] + ");");
            }
            SQLDB.Inputs = null;
        }


        protected virtual string[] OnReadCommands()
        {
            return new string[] {
                TableTemplateDBTable + " JOIN  BaseObjects JOIN " + TargetDBTable,
                "BaseObject_ID = BaseObjectID AND " + TargetClass + "_ID = " + TargetClass + "ID AND " +
                    SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId + " ORDER BY Name;"
            };
        }
        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            if (Table.ColumnDefinitions.Count <= 0) return;
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


        public void Update()
        {
            Delete();
            Create();
        }


        protected virtual string DeleteCondition()
        {
            return SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId.ToString();
        }
        public void Delete()
        {
            SQLDB.Command("DELETE FROM " + TableTemplateDBTable + " WHERE " + DeleteCondition() + ";");
        }
        
        // Not needed: No ID to keep track of and is only implemented here (Create() handles everything)
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
                    "WHERE BaseObject_ID = BaseObjectID ORDER BY Name ASC;",
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