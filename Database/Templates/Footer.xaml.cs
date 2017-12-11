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
        public string UpdateVisibility { get; set; }
        public string CreateVisibility { get; set; }
        public string DeleteVisibiliy { get; set; }
        public string CopyVisibility { get; set; }

        public Footer()
        {
            InitializeComponent();
        }

        public void Deleted(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTableName)
            {
                case "Classes": Deleted<BattlerClass>(sender, e); break;
                case "Players": Deleted<Player>(sender, e); break;
            }
        }

        public void Copied(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTableName)
            {
                case "Classes": Copied<BattlerClass>(sender, e); break;
                case "Players": Copied<Player>(sender, e); break;
            }
        }
        
        public void Created(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTableName)
            {
                case "Classes": Created<BattlerClass>(sender, e); break;
                case "Players": Created<Player>(sender, e); break;
            }
        }

        public void Updated(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentTableName)
            {
                case "Classes": Updated<BattlerClass>(sender, e); break;
                case "Players": Updated<Player>(sender, e); break;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Click Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Deleted<P>(object sender, EventArgs e) where P : Page, ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Delete();
        }
        private void Copied<P>(object sender, EventArgs e) where P : Page, ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Copy();
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
