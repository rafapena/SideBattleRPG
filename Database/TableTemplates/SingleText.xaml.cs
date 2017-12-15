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
    /// <summary>
    /// Interaction logic for GenericTable.xaml
    /// </summary>
    public partial class SingleText : _TableTemplateOperations
    {
        public SingleText()
        {
            InitializeComponent();
        }

        public new void AddRow(object sender, RoutedEventArgs e)
        {
            base.AddRow(sender, e);
            // Insert here
        }

        public new void RemoveRow(object sender, RoutedEventArgs e)
        {
            // Insert here
            base.RemoveRow(sender, e);
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
        }

        public override void Automate()
        {
            //attr1Input.Text = "This";
            //attr2Input.Text = "0";
        }

        public override string ValidateInputs()
        {
            string err = "";
            //if (!Util.InRequiredLength(Util.CutSpaces(attr1Input.Text))) err += "attr1 needs to have 1 to 16 characters";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.Inputs = new SQLiteParameter[] {
                //new SQLiteParameter("@attr1", attr1Input.Text),
                //new SQLiteParameter("@attr2", attr2Input.Text)
            };
        }

        protected override string[] OnCreate()
        {
            return new string[] { "attr1, attr2", "@attr1, @attr2" };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            //attr1Input.Text = reader.GetInt32(N).ToString();
            //attr2Input.Text = reader.GetString(N);
        }

        protected override string OnUpdate()
        {
            return "attr1 = @attr1, attr2 = @attr2";
        }
    }
}
