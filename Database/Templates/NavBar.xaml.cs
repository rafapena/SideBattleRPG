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
using Database.Superclasses;

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        private void SetupFromClick()
        {
            /*SQLDB<BaseObjectControl>.db.Open();
            string tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Text_Entry NVARCHAR(2048) NULL);"; //+
                                                    //"DROP TABLE IF EXISTS MyTable;";
            SQLiteCommand createTable = new SQLiteCommand(tableCommand, db);
            createTable.ExecuteReader();
            SQLDB<BaseObjectControl>.Close();
            DBList = new List<B>();*/
            //MessageBox.Show("Hello World");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Click Functions --
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void GoToAchievements(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToAnimations(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToClasses(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToEnemies(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToEnemyGroups(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToEnvironments(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToEvents(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToItems(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToPassiveSkills(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToPlatforms(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToPlayers(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToProjectiles(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoTos(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToSkills(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToStates(object sender, EventArgs e)
        {
            SetupFromClick();
        }

        private void GoToWeapons(object sender, EventArgs e)
        {
            SetupFromClick();
        }

    }
}
