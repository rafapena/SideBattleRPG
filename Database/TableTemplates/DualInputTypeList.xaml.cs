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

namespace Database.TableTemplates
{
    public partial class DualInputTypeList : _TableTemplateOperations
    {
        private ComboBoxInputData CBInputs;
        public string AttributeName { get; set; }
        public string StringList { get; private set; }

        
        public bool isDual() { return Table.ColumnDefinitions.Count == 3; }

        public DualInputTypeList()
        {
            InitializeComponent();
        }

        private void AddSecondInput(string startingText)
        {
            TextBox tb = TextBox("TB_" + Count, startingText, Count, 2);
            tb.Width = 30;
            Elements[Count - 1].Add(tb);
        }

        protected override string CheckAddability()
        {
            return CBInputs.NoOptions() ? "No " + TargetDBTable + " have been created yet." : "";
        }
        protected override void OnAddRow()
        {
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, 0));
            if (isDual()) AddSecondInput("");
            CBInputs.AddToSelectedIds(0);
        }
        protected override void OnRemoveRow()
        {
            CBInputs.RemoveFromSelectedIds();
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            CBInputs = new ComboBoxInputData("List_ID", "Name", TargetDBTable, "ListType = '" + TargetType + "' ORDER BY List_ID ASC");
            AttributeName = "";
            StringList = "";
        }


        protected override void OnAutomate(int i)
        {
            if (isDual()) ((TextBox)Elements[i][2]).Text = (i * 2).ToString();
        }

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

        protected override void OnParameterizeInputs(int i)
        {
            if (isDual()) ParameterizeInput("@" + AttributeName + "" + i, ((TextBox)Elements[i][2]).Text);
        }


        // DO NOT USE: Only here because they're abstract functions
        protected override string[] OnCreate() { return null; }
        protected override string OnCreateValues(int i) { return ""; }
         
        public new void Create()
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            StringList = "";
            for (int i = 0; i < Count; i++)
            {
                string textInput = isDual() ? ((TextBox)Elements[i][2]).Text + "_" : "";
                StringList += CBInputs.SelectedIds[i] + "_" + textInput;
            }
            SQLDB.Inputs = null;
        }

        
        protected override void OnRead(SQLiteDataReader reader) { } // DO NOT USE: Only here because it's an abstract function
        public new void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + HostDBTable + " WHERE " + HostType + "_ID = " + HostId + ";", conn))
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
                CBInputs.AddToSelectedIds(listId);
                AddRangeToTable();
            }
        }
        

        public new void Update()
        {
            Create();
            SQLDB.Command("UPDATE " + HostDBTable + " SET " + AttributeName + " = '" + StringList + "' WHERE " + HostType + "_ID = " + HostId + ";");
        }

        public new void Delete() { }    // DO NOT USE: ClassOperation handles this - Only here to override base function
    }
}
