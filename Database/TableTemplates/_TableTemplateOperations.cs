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
        public string TableTitle { get; protected set; }
        public string[] Columns { get; set; }
        public Grid Table { get; set; }

        protected string ClassTemplateTable { get; set; }
        protected string ClassTemplateType { get; set; }
        public int ClassTemplateId { get; protected set; }
        
        public void AddRow(object sender, RoutedEventArgs e)
        {
            Table.RowDefinitions.Add(new RowDefinition());
        }

        public void RemoveRow(object sender, RoutedEventArgs e)
        {
            //Table.RowDefinitions.Remove(RowNumberByRowDefinition);
        }


        protected abstract void OnInitializeNew();
        public void InitializeNew(string title, string[] columns=null)
        {
            TableTitle = title;
            Columns = columns;
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