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

        public void ApplyInitializeNewSettings()
        {
            DeleteButton.Visibility = Visibility.Collapsed;
            AutoButton.Visibility = Visibility.Visible;
            CloneButton.Visibility = Visibility.Collapsed;
            CreateButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Collapsed;
        }

        public void ApplyReadSettings()
        {
            DeleteButton.Visibility = Visibility.Visible;
            AutoButton.Visibility = Visibility.Collapsed;
            CloneButton.Visibility = Visibility.Visible;
            CreateButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Visible;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Trigger Setup and Trigger Page Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Deleted(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentClass)
            {
                case "Achievement": Deleted<Achievement>(); break;
                case "Player": Deleted<Player>(); break;
            }
        }

        public void Automated(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentClass)
            {
                case "Achievement": Automated<Achievement>(); break;
                case "Player": Automated<Player>(); break;
            }
        }

        public void Cloned(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentClass)
            {
                case "Achievement": Cloned<Achievement>(); break;
                case "Player": Cloned<Player>(); break;
            }
        }

        public void Created(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentClass)
            {
                case "Achievement": Created<Achievement>(); break;
                case "Player": Created<Player>(); break;
            }
        }

        public void Updated(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentClass)
            {
                case "Achievement": Updated<Achievement>(); break;
                case "Player": Updated<Player>(); break;
            }
        }
        
        private void Deleted<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).Delete(); }
        private void Cloned<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).Clone(); }
        private void Automated<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).Automate(); }
        private void Created<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).Create(); }
        private void Updated<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).Update(); }
    }
}