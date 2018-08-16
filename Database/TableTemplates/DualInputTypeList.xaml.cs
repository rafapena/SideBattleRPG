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
        protected List<int> SelectedIds { get; set; }
        protected List<string> OptionsListNames { get; set; }
        public string StringList { get; private set; }


        public DualInputTypeList()
        {
            InitializeComponent();
        }

        private void AddSecondInput(string startingText)
        {
            TextBox tb = TextBox("TB_" + SelectedIds.Count, startingText, Count, 2);
            tb.Width = 30;
            Elements[Count - 1].Add(tb);
        }

        protected override string CheckAddability()
        {
            return OptionsListNames.Count > 0 ? "" : "No " + TargetDBTable + " have been created yet.";
        }
        protected override void OnAddRow()
        {
            Elements[Count - 1].Add(ComboBox("CB_" + SelectedIds.Count, OptionsListNames, 0, Count, 1, UpdateSelectedIds));
            if (isDual()) AddSecondInput("");
            SelectedIds.Add(0);
        }

        protected override void OnRemoveRow()
        {
            SelectedIds.RemoveAt(Count - 1);
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            SelectedIds = new List<int>();
            OptionsListNames = getFromQuery("Name");
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
                err += "Row " + i + " on " + TableTitle + " must be a positive integer\n";
            for (int j = i + 1; j < Count; j++)
            {
                if (SelectedIds[i] != SelectedIds[j]) continue;
                err += "All rows in " + TableTitle + " must be unique\n";
                break;
            }
            return err;
        }

        protected override void OnParameterizeInputs(int i)
        {
            if (isDual()) ParameterizeInput("@" + AdditionalAttribute + i.ToString(), ((TextBox)Elements[i][2]).Text);
        }


        protected override string[] OnCreate(int i) { return null; }    // DO NOT USE: Only here because it's an abstract function
        public new void Create()
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            StringList = "";
            for (int i = 0; i < Count; i++)
            {
                string textInput = isDual() ? ((TextBox)Elements[i][2]).Text + "_" : "";
                StringList += SelectedIds[i] + "_" + textInput;
            }
            SQLDB.Inputs = null;
        }

        
        protected override void OnRead(SQLiteDataReader reader) { } // DO NOT USE: Only here because it's an abstract function
        public new void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + SQLDB.CurrentTable + " " +
                    "WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString(), conn))
                {
                    reader.Read();
                    StringList = reader[CustomName].ToString();
                }
                conn.Close();  
            }
            if (StringList == "") return;
            string[] items = StringList.Split('_');
            int inc = isDual() ? 2 : 1;
            int length = items.Length - 1;
            for (int i = 0; i < length; i += inc)
            {
                int listId = int.Parse(items[i]);
                if (listId >= OptionsListNames.Count)
                {
                    i--;
                    continue;
                }
                AddRow(null, null);
                Elements[Count - 1].Add(ComboBox("CB_" + SelectedIds.Count, OptionsListNames, listId, Count, 1, UpdateSelectedIds));
                if (isDual()) AddSecondInput(int.Parse(items[i + 1]).ToString());
                SelectedIds.Add(listId);
                AddRangeToTable();
            }
        }
        

        public new void Update()
        {
            Create();
            SQLDB.Command(
                "UPDATE " + SQLDB.CurrentTable + " SET " + CustomName + " = '" + StringList + "' " +
                "WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId + ";");
        }

        public new void Delete() { }    // DO NOT USE: ClassOperation handles this - Only here to override base function


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Other functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void UpdateSelectedIds(object sender, EventArgs e)
        {
            int getIdThroughName = Convert.ToInt32(((ComboBox)sender).Name.Split('_').Last());
            SelectedIds[getIdThroughName] = ((ComboBox)sender).SelectedIndex;
        }

        private List<string> getFromQuery(string attribute)
        {
            List<string> list = new List<string>();
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + TargetDBTable + " WHERE ListType = '" + TargetType + "' ORDER BY List_ID ASC;", conn))
                    while (reader.Read()) list.Add(reader[attribute].ToString());
                conn.Close();
            }
            return list;
        }
    }
}
