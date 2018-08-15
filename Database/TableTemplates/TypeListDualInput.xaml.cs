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
    public partial class TypeListDualInput : _TableTemplateOperations
    {
        protected List<string> SelectedIds { get; set; }
        protected List<string> OptionsListIds { get; set; }
        protected List<string> OptionsListNames { get; set; }


        public TypeListDualInput()
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
            SelectedIds = new List<string>();
            OptionsListIds = getFromQuery(TargetDBTable, "BaseObject_ID");
            OptionsListNames = getFromQuery(TargetDBTable, "Name");
        }


        protected override void OnAutomate(int i)
        {
            if (isDual()) ((TextBox)Elements[i][2]).Text = (i * 2).ToString();
        }

        protected override string OnValidateInputs(int i)
        {
            string err = "";
            if (isDual() && !Utils.PosInt(((TextBox)Elements[i][2]).Text))
                err += "Row " + i + " on " + TableTitle + " must be a positive integer";
            for (int j = i + 1; j < Count; j++)
            {
                if (SelectedIds[i] != SelectedIds[j]) continue;
                err += "All rows in " + TableTitle + " must be unique";
                break;
            }
            return err;
        }

        protected override void OnParameterizeInputs(int i)
        {
            if (isDual()) ParameterizeInput("@" + AdditionalAttribute + "" + i, ((TextBox)Elements[i][2]).Text);
        }


        protected override string[] OnCreate(int i)
        {
            string attributes = SQLDB.CurrentClass + "ID, " + TargetClass + "ID";
            string values = SQLDB.CurrentId.ToString() + ", " + SelectedIds[i];
            if (isDual())
            {
                attributes += ", " + AdditionalAttribute;
                values += ", @" + AdditionalAttribute + "" + i;
            }
            return new string[] { attributes, values };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            int landingIndex = Convert.ToInt32(OptionsListIds.FindIndex(a => a == reader["BaseObject_ID"].ToString()));
            Elements[Count - 1].Add(ComboBox("CB_" + SelectedIds.Count, OptionsListNames, landingIndex, Count, 1, UpdateSelectedIds));
            if (isDual()) AddSecondInput(reader[AdditionalAttribute].ToString());
            SelectedIds.Add(OptionsListIds[landingIndex]);
        }


        private void UpdateSelectedIds(object sender, EventArgs e)
        {
            int getIdThroughName = Convert.ToInt32(((ComboBox)sender).Name.Split('_').Last());
            SelectedIds[getIdThroughName] = OptionsListIds[((ComboBox)sender).SelectedIndex];
        }
    }
}
