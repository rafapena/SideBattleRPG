using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.ClassTemplates
{
    public partial class _GenericClassTemplate : _ClassTemplateOperations
    {
        public _GenericClassTemplate()
        {
            InitializeComponent();
            ClassTemplateTable = "_GenericClassTemplate";   // DO NOT FORGET TO SET THIS
        }

        protected override void SetupTableData()
        {
            //table1.Setup(hostDBTable, targetDBTable, title, new List<string> { "colunmName1", "columnName2" });   // For DBTables
            //table2.Setup(hostDBTable, targetDBTable, List_Type, title, new List<string> { "colunmName1", "columnName2" }); // For TypesLists
        }

        protected override void OnInitializeNew()
        {   
            //attr1Input.Text = "0";
            //attr2Image.Source = null;
        }

        public override string ValidateInputs()
        {
            string err = "";
            //if (!Utils.InRequiredLength(Utils.CutSpaces(attr1Input.Text))) err += "attr1 must have 1 to 16 characters";
            //if (!Utils.PosInt(attr2Input.Text)) err += "attr2 must be a positive integer";
            //if (!Utils.NumberBetween(attr3Input.Text, 1, 100)) err += "attr3 must be a number between 1 and 100";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            //SQLDB.ParameterizeInput("@attr1", attr1Input.Text);
            //SQLDB.ParameterizeInput("@attr2", attr2Input.Text);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            return new string[] { "attr1, attr2", "@attr1, @attr2" };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            //attr1Input.Text = int.Parse(reader["IntegerAttr"].ToString());
            //attr2Input.Text = reader["StringAttr"].ToString();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            return "attr1 = @attr1, attr2 = @attr2";
        }
    }
}