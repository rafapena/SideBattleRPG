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
        public string TableTitle { get; protected set; }
        public int ScrollerHeight { get; protected set; }
        public int Count { get; set; }

        protected string ClassTemplateTable { get; set; }
        protected string ClassTemplateType { get; set; }
        public int ClassTemplateId { get; protected set; }
        
        public void AddRow(object sender, RoutedEventArgs e)
        {
            Count++;
            Table.RowDefinitions.Add(new RowDefinition());
            TextBlock t = TextBlock(Count, Count, 0);
            t.Name = "INDEX" + Count;
            RegisterName(t.Name, t);
            t.HorizontalAlignment = HorizontalAlignment.Center;
            Table.Children.Add(t);
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
            Table.RowDefinitions.RemoveAt(Count);
            Count--;
        }

        protected abstract void OnInitializeNew();
        public void InitializeNew(string title, List<string> columns=null, List<string> inputs=null, int scrollerHeight=100)
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
            Count = 0;
            InitializeNew();
        }
        public void InitializeNew()
        {
            //ClassTemplateId = SQLDB.GetMaxIdFromTable(ClassTemplateTable, ClassTemplateType);
            OnInitializeNew();
            TableSetup(Table, Columns);
        }


        public abstract void Automate();
        public abstract string ValidateInputs();
        public abstract void ParameterizeInputs();


        protected abstract string[] OnCreate();
        public void Create()
        {
            string err = ValidateInputs();
            if (err != "") 
            ParameterizeInputs();
            string[] text = OnCreate();
            SQLDB.Command("INSERT INTO " + ClassTemplateTable + " (" + text[0] + ") VALUES (" + text[1] + ");");
            SQLDB.Inputs = null;
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read(SQLiteDataReader reader)
        {
            //ClassTemplateId = int.Parse(reader[ClassTemplateType + "ID"].ToString());
            Read();
        }
        public void Read()
        {
            /*using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + ClassTemplateTable + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString(), conn))
                {
                    reader.Read();
                    ClassTemplateId = reader.GetInt32(0);
                    OnRead(reader);
                }
                conn.Close();
            }*/
            //Count = Table.RowDefinitions.Count - 1;
        }


        protected abstract string OnUpdate();
        public void Update()
        {
            ParameterizeInputs();
            SQLDB.Command("UPDATE " + ClassTemplateTable + " SET " + OnUpdate() + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString() + ";");
            SQLDB.Inputs = null;
        }


        public void Delete()
        {
            SQLDB.Command("DELETE FROM " + ClassTemplateTable + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString() + ";");
        }


        public void Clone()
        {
            ClassTemplateId = SQLDB.GetMaxIdFromTable(ClassTemplateTable, ClassTemplateType);
        }
    }
}