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
    public partial class _GenericTable : _TableTemplateOperations
    {
        public _GenericTable()
        {
            InitializeComponent();
        }

        protected override void OnAddRow()
        {
            int latest = Count - 1;
            //InputElements[latest].Add(TextBox(Inputs[0] + Count, "", Count, 1));
            //InputElements[latest].Add(TextBox(Inputs[1] + Count, "", Count, 1));
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
        }

        protected override void OnAutomate(int i)
        {
            //((TextBox)InputElements[i][0]).Text = (i + Count * 2).ToString();
            //((TextBox)InputElements[i][1]).Text = (i + Count * 2).ToString();
        }

        protected override string OnValidateInputs(int i)
        {
            string err = "";
            if (Utils.CutSpaces(((TextBox)InputElements[i][0]).Text) == "") err += "Inputs in " + TableTitle + " cannot be empty\n";
            return err;
        }

        protected override void OnParameterizeInputs(int i)
        {
            //ParameterizeInputs("@attr1" + i, ((TextBox)InputElements[i][0]).Text);
            //ParameterizeInputs("@attr2" + i, ((TextBox)InputElements[i][1]).Text);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            int latest = Count - 1;
            //InputElements[latest].Add(TextBox(Inputs[0]+Count, reader.GetString(3), Count, 1));
            //InputElements[latest].Add(TextBox(Inputs[1]+Count, reader.GetString(4), Count, 2));
        }

        protected override string[] OnUpdateAddRow(int i)
        {
            return new string[] { SQLDB.CurrentClass + "ID", SQLDB.CurrentId.ToString() };
        }
        protected override string OnUpdateRemovedRowCondition()
        {
            return SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId.ToString();
        }
        protected override string[] OnUpdateRow(int i)
        {
            return new string[] { "attr1 = @attr1" + i.ToString(),
                SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId.ToString() };
        }
    }
}
