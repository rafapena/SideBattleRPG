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
using Database.Utilities;

namespace Database.Tables
{
    /// <summary>
    /// Interaction logic for BattlerClass.xaml
    /// </summary>
    public partial class BattlerClass : Page, ObjectOperations
    {
        public BattlerClass()
        {
            InitializeComponent();
            SQLDB.CurrentTable = "Class";
            ObjectList.SetupTable("Classes");
            InitializeNew();
        }

        public void Automate()
        {
            Base.Automate();
        }

        public void Copy()
        {
            Base.Copy();
        }

        public void Create()
        {
            string err = GetErrors();
            if (err != "") { MessageBox.Show("Could Not Update due to the following:\n\n"); return; }
            Base.Create();
        }

        public void Delete()
        {
            Base.Delete();
        }

        public string GetErrors()
        {
            string err = Base.GetErrors();
            return err;
        }

        public void InitializeNew()
        {
            Base.InitializeNew();
            BottomButtons.InitializeNewSettings();
        }

        public void Read()
        {
            Base.Read();
            BottomButtons.ReadSettings();
        }

        public void Update()
        {
            Base.Update();
        }
    }
}