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

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for TableList.xaml
    /// </summary>
    public partial class TableList : UserControl
    {
        //public List<B> TheList { get; private set; }

        public TableList()
        {
            InitializeComponent();
            CreateTable();
        }

        public void CreateTable()
        {
            for (int i = 0; i < 200; i++)
            {
                CreateButton(i);
            }
        }

        public void CreateButton(int ListID)
        {

        }
    }
}