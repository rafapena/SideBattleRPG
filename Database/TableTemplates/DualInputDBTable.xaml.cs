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
    public partial class DualInputDBTable : _TableTemplateOperations
    {
        protected List<int> SelectedIds { get; set; }
        protected List<int> OptionsListIds { get; set; }
        protected List<string> OptionsListNames { get; set; }


        public DualInputDBTable()
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
            return OptionsListIds.Count > 0 ? "" : "No " + TargetDBTable + " have been created yet.";
        }
        protected override void OnAddRow()
        {
            Elements[Count - 1].Add(ComboBox("CB_" + SelectedIds.Count, OptionsListNames, 0, Count, 1, UpdateSelectedIds));
            if (isDual()) AddSecondInput("");
            SelectedIds.Add(OptionsListIds[0]);
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
            getFromQuery();
        }


        protected override void OnAutomate(int i)
        {
            if (isDual()) ((TextBox)Elements[i][2]).Text = (i*2).ToString();
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
            if (isDual()) ParameterizeInput("@" + InputAttributeName + "" + i, ((TextBox)Elements[i][2]).Text);
        }


        protected override string[] OnCreate(int i)
        {
            string attributes = SQLDB.CurrentClass + "ID, " + TargetType + "ID, TableIndex";
            string values = SQLDB.CurrentId + ", " + SelectedIds[i] + ", " + i;
            if (isDual())
            {
                attributes += ", " + InputAttributeName;
                values += ", @" + InputAttributeName + i.ToString();
            }
            return new string[] { attributes, values };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            int savedId = int.Parse(reader["BaseObject_ID"].ToString());
            int landingIndex = Convert.ToInt32( OptionsListIds.FindIndex(a => a ==  savedId));
            Elements[Count - 1].Add(ComboBox("CB_" + SelectedIds.Count, OptionsListNames, landingIndex, Count, 1, UpdateSelectedIds));
            if (isDual()) AddSecondInput(reader[InputAttributeName].ToString());
            SelectedIds.Add(OptionsListIds[landingIndex]);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Other functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void UpdateSelectedIds(object sender, EventArgs e)
        {
            int getIdThroughName = Convert.ToInt32(((ComboBox)sender).Name.Split('_').Last());
            SelectedIds[getIdThroughName] = OptionsListIds[((ComboBox)sender).SelectedIndex];
        }

        private void getFromQuery()
        {
            OptionsListIds = new List<int>();
            OptionsListNames = new List<string>();
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve(
                    "SELECT * FROM BaseObjects JOIN " + TargetDBTable + " " +
                    "WHERE BaseObject_ID = BaseObjectID ORDER BY Name ASC;",
                    conn))
                {
                    while (reader.Read())
                    {
                        OptionsListIds.Add(int.Parse(reader["BaseObjectID"].ToString()));
                        OptionsListNames.Add(reader["Name"].ToString());
                    }
                }
                conn.Close();
            }
        }
    }
}
