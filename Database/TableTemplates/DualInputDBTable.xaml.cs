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
        private ComboBoxInputData CBInputs;

        public DualInputDBTable()
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
            CBInputs = new ComboBoxInputData("BaseObjectID", "Name", "BaseObjects JOIN " + TargetDBTable, "BaseObject_ID = BaseObjectID ORDER BY Name ASC");
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
                if (CBInputs.SelectedIds[i] != CBInputs.SelectedIds[j]) continue;
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
            string values = SQLDB.CurrentId + ", " + CBInputs.SelectedIds[i] + ", " + i;
            if (isDual())
            {
                attributes += ", " + InputAttributeName;
                values += ", @" + InputAttributeName + i.ToString();
            }
            return new string[] { attributes, values };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            int landingIndex = Convert.ToInt32( CBInputs.OptionsListIds.FindIndex( a => a == int.Parse(reader["BaseObject_ID"].ToString())) );
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, landingIndex));
            if (isDual()) AddSecondInput(reader[InputAttributeName].ToString());
            CBInputs.AddToSelectedIds(landingIndex);
        }
    }
}
