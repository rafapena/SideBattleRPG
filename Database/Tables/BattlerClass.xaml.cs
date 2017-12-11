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
            ObjectList.SetupTable("Class", "Classes");
            BottomButtons.SetupFooter("Class");
        }

        public void Automate()
        {
            throw new NotImplementedException();
        }

        public void Copy()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            MessageBox.Show("CREATED BattlerClass");
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public string GetErrors()
        {
            throw new NotImplementedException();
        }

        public void InitializeNew()
        {
            throw new NotImplementedException();
        }

        public void Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
