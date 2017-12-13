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

namespace Database.Classes
{
    /// <summary>
    /// Interaction logic for _GenericTemplate.xaml
    /// </summary>
    public partial class _GenericTemplate : _ClassOperations
    {
        public _GenericTemplate()
        {
            InitializeComponent();
            ObjectList.SetupTable(FooterButtons);
            InitializeNew();
        }

        public override void InitializeNew()
        {
            Base.InitializeNew();
            //attr1Input.Text = "";
            //attr2Image.Source = null;
        }

        public override void Automate()
        {
            Base.Automate();
            //attr1Input.Text = "This";
            //attr2Input.Text = "0";
        }

        public override string ValidateInputs()
        {
            SQLDB.AddParameters(new SQLiteParameter[] {
                //new SQLiteParameter("@attr1", attr1Input.Text),
                //new SQLiteParameter("@attr2", attr2Input.Text)
            });
            string err = Base.ValidateInputs();
            //if (!Util.InRequiredLength(Util.CutSpaces(attr1))) err += "attr1 needs to have 1 to 16 characters";
            return err;
        }

        protected override void OnCreate()
        {
            Base.Create();
            SQLCreate(new string[]{ "attr1, attr2", "@attr1, @attr2" });
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read();
            //attr1Input.Text = reader.GetInt32(N);
            //attr2Input.Text = reader.GetString(N);
        }

        protected override void OnUpdate()
        {
            Base.Update();
            SQLUpdate("attr1 = @attr1, attr2 = @attr2");
        }

        protected override void OnDelete()
        {
            Base.Delete();
        }

        protected override void OnClone()
        {
            Base.Clone();
        }
    }
}