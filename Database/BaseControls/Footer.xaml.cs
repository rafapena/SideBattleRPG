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
using Database.Classes;
using Database.Utilities;

namespace Database.BaseControls
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {   
        public Footer()
        {
            InitializeComponent();
        }

        // Sets the visibility of the buttons when the user selects 'New'
        public void ApplyInitializeNewSettings()
        {
            DeleteButton.Visibility = Visibility.Collapsed;
            CloneButton.Visibility = Visibility.Collapsed;
            CreateButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Collapsed;
        }

        // Sets the visibility of the buttons when reading the table
        public void ApplyReadSettings()
        {
            DeleteButton.Visibility = Visibility.Visible;
            CloneButton.Visibility = Visibility.Visible;
            CreateButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Visible;
        }

        // Trigger page functions
        private void Deleted(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Delete();
        }
        private void Cloned(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Clone();
        }
        private void Created(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Create();
        }
        private void Updated(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Update();
        }
    }
}