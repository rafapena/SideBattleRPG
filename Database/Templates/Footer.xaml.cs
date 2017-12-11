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
using Database.Tables;
using Database.Utilities;

namespace Database.Templates
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

        public void InitializeNewSettings()
        {
            DeleteButton.Visibility = Visibility.Collapsed;
            AutoButton.Visibility = Visibility.Visible;
            CopyButton.Visibility = Visibility.Collapsed;
            CreateButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Collapsed;
        }

        public void ReadSettings()
        {
            DeleteButton.Visibility = Visibility.Visible;
            AutoButton.Visibility = Visibility.Collapsed;
            CopyButton.Visibility = Visibility.Visible;
            CreateButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Visible;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Trigger Setup and Trigger Page Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Deleted(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTable)
            {
                case "Class": Deleted<BattlerClass>(); break;
                case "Player": Deleted<Player>(); break;
            }
        }

        public void Auto(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTable)
            {
                case "Class": Auto<BattlerClass>(); break;
                case "Player": Auto<Player>(); break;
            }
        }

        public void Copied(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTable)
            {
                case "Class": Copied<BattlerClass>(); break;
                case "Player": Copied<Player>(); break;
            }
        }

        public void Created(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTable)
            {
                case "Class": Created<BattlerClass>(); break;
                case "Player": Created<Player>(); break;
            }
        }

        public void Updated(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTable)
            {
                case "Class": Updated<BattlerClass>(); break;
                case "Player": Updated<Player>(); break;
            }
        }
        
        private void Deleted<P>() where P : Page, ObjectOperations { (Application.Current.MainWindow.Content as P).Delete(); }
        private void Copied<P>() where P : Page, ObjectOperations { (Application.Current.MainWindow.Content as P).Copy(); }
        private void Auto<P>() where P : Page, ObjectOperations { (Application.Current.MainWindow.Content as P).Automate(); }
        private void Created<P>() where P : Page, ObjectOperations { (Application.Current.MainWindow.Content as P).Create(); }
        private void Updated<P>() where P : Page, ObjectOperations { (Application.Current.MainWindow.Content as P).Update(); }
    }
}