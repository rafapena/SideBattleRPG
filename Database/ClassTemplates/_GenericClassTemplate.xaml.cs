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

namespace Database.ClassTemplates
{
    public partial class _GenericClassTemplate : _ClassTemplateOperations
    {
        public _GenericClassTemplate()
        {
            InitializeComponent();
            ClassTemplateTable = "_GenericClassTemplate";    // PLURAL For Type
            ClassTemplateType = "_GenericClassTemplate";
        }

        protected override void SetupTableData()
        {
            //table1.Setup(hostType, hostDBTable, targetType, targetDBTable, title, new List<string> {});
            //table2.Setup(hostType, hostDBTable, targetType, targetDBTable, title, new List<string> {});
        }

        protected override void OnInitializeNew()
        {
            //attr1Input.Text = "0";
            //attr2Image.Source = null;
        }

        public override string ValidateInputs()
        {
            string err = "";
            //if (!Utils.InRequiredLength(Util.CutSpaces(attr1Input.Text))) err += "attr1 needs to have 1 to 16 characters";
            return err;
        }

        public override void ParameterizeInputs()
        {
            //ParameterizeInput("@attr1", attr1Input.Text);
            //ParameterizeInput("@attr2", attr2Input.Text);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            return new string[] { "attr1, attr2", "@attr1, @attr2" };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            //attr1Input.Text = reader.GetInt32(N).ToString();
            //attr2Input.Text = reader.GetString(N);
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            return "attr1 = @attr1, attr2 = @attr2";
        }
    }
}