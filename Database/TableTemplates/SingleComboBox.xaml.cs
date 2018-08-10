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
    public partial class SingleComboBox : _TableTemplateOperations
    {
        private List<String> OptionsList { get; set; }

        public SingleComboBox()
        {
            InitializeComponent();
        }

        protected override void OnAddRow()
        {
            InputElements[Count - 1].Add(TextBox(Inputs[0] + Count, "", Count, 1));
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
        }

        protected override void OnAutomate(int i) { }
        protected override string OnValidateInputs(int i)
        {
            string err = "";
            for (int j = i; j < Count; j++)
            {
                //if () continue;
                //err += ;
                break;
            }
            return err;
        }
        protected override void OnParameterizeInputs(int i) { }

        protected override void OnRead(SQLiteDataReader reader)
        {
            InputElements[Count - 1].Add(TextBox(Inputs[0]+Count, reader.GetString(3), Count, 1));
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
