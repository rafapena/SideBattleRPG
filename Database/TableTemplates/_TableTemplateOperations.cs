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
        public List<string> Columns { get; set; }
        public List<string> Inputs { get; set; }
        public List<List<UIElement>> InputElements { get; set; }

        public string TableTitle { get; protected set; }
        public int ScrollerHeight { get; protected set; }
        public int Count { get; set; }

        protected string TableTemplateTable { get; set; }
        protected string TableTemplateType { get; set; }
        public int TableTemplateId { get; protected set; }

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
        public void InitializeNew(string title, List<string> columns=null, List<string> inputs = null, int scrollerHeight=100)
        {
            TableTitle = title;
            ScrollerHeight = scrollerHeight;
            if (columns != null && columns.Count > 0)
            {
                Columns = new List<string>();
                Columns.Add("#");
                Columns.AddRange(columns);
            }
            Inputs = inputs;
            InputElements = new List<List<UIElement>>();
            Count = 0;
            InitializeNew();
            Read();
        }
        public virtual void InitializeNew()
        {
            TableTemplateId = SQLDB.GetMaxIdFromTable(TableTemplateTable, TableTemplateType);
            OnInitializeNew();
            TableSetup(Table, Columns);
        }


        public abstract void Automate();
        public abstract string ValidateInputs();
        public abstract void ParameterizeInputs();


        protected abstract void OnCreate();
        public void Create()
        {
            ParameterizeInputs();
            OnCreate();
            SQLDB.Inputs = null;
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read(SQLiteDataReader reader)
        {
            if (Inputs == null) return;
            TableTemplateId = int.Parse(reader[TableTemplateType + "ID"].ToString());
            Read();
        }
        public virtual void Read()
        {
            AddRow(null, null);
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + TableTemplateTable + " WHERE " + SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId.ToString(), conn))
                {
                    while (reader.Read()) OnRead(reader);
                }
                conn.Close();
            }
            AddRangeToTable();
        }


        protected abstract void OnUpdate();
        public void Update()
        {
            ParameterizeInputs();
            OnUpdate();
            SQLDB.Inputs = null;
        }


        public void Delete()
        {
            SQLDB.Command("DELETE FROM " + TableTemplateTable + " WHERE " + TableTemplateType + "_ID = " + TableTemplateId.ToString() + ";");
        }


        public void Clone()
        {
            TableTemplateId = SQLDB.GetMaxIdFromTable(TableTemplateTable, TableTemplateType);
        }
    }
}