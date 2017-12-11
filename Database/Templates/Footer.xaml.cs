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
using Database.Tables;

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public string CurrentTableName { get; private set; }
        public string UpdateVisibility { get; set; }
        public string CreateVisibility { get; set; }
        public string DeleteVisibiliy { get; set; }
        public string CopyVisibility { get; set; }

        public Footer()
        {
            InitializeComponent();
        }

        public void SetupFooter(string tableType)
        {
            CurrentTableName = tableType;
        }

        public void Deleted(object sender, EventArgs e)
        {
            switch (CurrentTableName)
            {
                case "Class": Deleted<BattlerClass>(sender, e); break;
                case "Player": Deleted<Player>(sender, e); break;
            }
        }

        public void Auto(object sender, EventArgs e)
        {
            switch (CurrentTableName)
            {
                case "Class": Auto<BattlerClass>(sender, e); break;
                case "Player": Auto<Player>(sender, e); break;
            }
        }

        public void Copied(object sender, EventArgs e)
        {
            switch (CurrentTableName)
            {
                case "Class": Copied<BattlerClass>(sender, e); break;
                case "Player": Copied<Player>(sender, e); break;
            }
        }

        public void Created(object sender, EventArgs e)
        {
            switch (CurrentTableName)
            {
                case "Class": Created<BattlerClass>(sender, e); break;
                case "Player": Created<Player>(sender, e); break;
            }
        }

        public void Updated(object sender, EventArgs e)
        {
            switch (CurrentTableName)
            {
                case "Class": Updated<BattlerClass>(sender, e); break;
                case "Player": Updated<Player>(sender, e); break;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Trigger Page Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Deleted<P>(object sender, EventArgs e) where P : Page, ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Delete();
        }
        private void Copied<P>(object sender, EventArgs e) where P : Page, ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Copy();
        }
        private void Auto<P>(object sender, EventArgs e) where P : Page, ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Automate();
        }
        private void Created<P>(object sender, EventArgs e) where P : Page, ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Create();
        }
        private void Updated<P>(object sender, EventArgs e) where P : Page, ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Update();
        }
    }
}
