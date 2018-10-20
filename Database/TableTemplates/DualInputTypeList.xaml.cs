using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using Database.Utilities;
using static Database.Utilities.TableBuilder;
using System.Windows;

namespace Database.TableTemplates
{
    public partial class DualInputTypeList : _TableTemplateOperations
    {
        private ComboBoxInputData CBInputs;
        public string ListType { get; private set; }
        public string AttributeName { get; set; }
        public string StringList { get; private set; }

        public DualInputTypeList()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Same functions as DualInputDBTable below
        /// </summary>

        // Checks if a table has that extra textbox input
        public bool isDual() { return Table.ColumnDefinitions.Count == 3; }

        // Adds that extra textbox input to the table, as another column
        private void AddSecondInput(string startingText)
        {
            TextBox tb = TextBox("TB_" + Count, startingText, Count, 2);
            tb.Width = 30;
            Elements[Count - 1].Add(tb);
        }

        protected override string CheckAddability()
        {
            return CBInputs.NoOptions() ? "The table '" + TargetDBTable + "' is currently empty" : "";
        }
        protected override void OnAddRow()
        {
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, 0));
            if (isDual()) AddSecondInput("100");
        }
        protected override void OnRemoveRow()
        {
            CBInputs.RemoveFromSelectedIds();
        }

        /// <summary>
        /// Same functions as DualInputDBTable above
        /// </summary>


        public new void Setup(string hostDBTable, string targetDBTable, string title, List<string> columnNames, int scrollerHeight = 100)
        {
            throw new InvalidOperationException("Use the other Setup(...) table function for DualInputTypeList templates instead.");
        }
        public void Setup(string hostDBTable, string targetDBTable, string listType, string title, List<string> columnNames, int scrollerHeight = 100)
        {
            ListType = listType;
            base.Setup(hostDBTable, targetDBTable, title, columnNames, scrollerHeight);
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            CBInputs = new ComboBoxInputData("List_ID", "Name", TargetDBTable, "List_Type = '" + ListType + "'", "List_ID");
            AttributeName = "";
            StringList = "";
        }

        // Same as DualInputDBTable
        protected override string OnValidateInputs(int i)
        {
            string err = "";
            if (isDual() && !Utils.PosInt(((TextBox)Elements[i][2]).Text))
                err += "Input on row " + (i+1) + " for " + TableTitle + " must be a positive integer\n";
            for (int j = i + 1; j < Count; j++)
            {
                if (CBInputs.SelectedIds[i] != CBInputs.SelectedIds[j]) continue;
                err += "All rows in " + TableTitle + " must be unique\n";
                break;
            }
            return err;
        }

        // Same as DualInputDBTable
        protected override void OnParameterizeInputs(int i)
        {
            if (isDual()) SQLDB.ParameterizeAttribute("@" + AttributeName + i.ToString(), ((TextBox)Elements[i][2]).Text);
        }

        
        protected override string[] OnCreate() { return null; }     // DO NOT USE: Only here because they're abstract functions
        protected override string OnCreateValues(int i) { return ""; }
        public new void Create(SQLiteConnection conn)
        {
            SQLDB.ResetParameterizedAttributes();
            ParameterizeAttributes();
            StringList = "";
            for (int i = 0; i < Count; i++)
            {
                string textInput = isDual() ? ((TextBox)Elements[i][2]).Text + "_" : "";
                StringList += CBInputs.SelectedIds[i] + "_" + textInput;
            }
        }

        
        protected override void OnRead(SQLiteDataReader reader) { } // DO NOT USE: Only here because it's an abstract function
        public new void Read()
        {
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                using (var reader = SQLDB.Read(conn,
                    "SELECT " + AttributeName + " FROM " + HostDBTable + " " +
                    "WHERE " + HostDBTable + "_ID = " + HostId + ";"))
                {
                    reader.Read();
                    StringList = reader[AttributeName].ToString();
                }
                conn.Close();  
            }
            if (StringList == "") return;
            string[] items = StringList.Split('_');
            int inc = isDual() ? 2 : 1;
            for (int i = 1; i < items.Length; i += inc)
            {
                int listId = int.Parse(items[i - 1]);
                if (listId >= CBInputs.OptionsListNames.Count) continue;
                AddRow(null, null);
                Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, listId));
                if (isDual()) AddSecondInput(int.Parse(items[i]).ToString());
                AddRangeToTable();
            }
        }
        

        public new void Update(SQLiteConnection conn)
        {
            Create(conn);
            SQLDB.Write(conn, "UPDATE " + HostDBTable + " SET " + AttributeName + " = '" + StringList + "' WHERE " + HostDBTable + "_ID = " + HostId + ";");
        }

        // DO NOT USE: ClassOperation handles this - Only here to override base function
        public new void Delete(SQLiteConnection conn) { }
    }
}
