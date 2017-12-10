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
using System.Collections;
using Database.Utilities;
using Database.Tables;

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

        private void SetupFromClick(string tableType)
        {
            //SQLDB.Command("SELECT Name FROM THIS");
            ArrayList list = SQLDB.GetTables(tableType);
            string msg = "";
            foreach (string i in list)
            {
                msg += i + "\n";
            }
            //MessageBox.Show("Selected: " + tableType);
            MessageBox.Show(msg);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Click Functions --
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void GoToAchievements(object sender, EventArgs e)
        {
            SetupFromClick("Achievements");
            //Application.Current.MainWindow.Navigate(typeof(Player));
        }

        private void GoToAnimations(object sender, EventArgs e)
        {
            SetupFromClick("Animations");
        }

        private void GoToClasses(object sender, EventArgs e)
        {
            SetupFromClick("Classes");
        }

        private void GoToEnemies(object sender, EventArgs e)
        {
            SetupFromClick("Enemies");
        }

        private void GoToEnemyGroups(object sender, EventArgs e)
        {
            SetupFromClick("EnemyGroups");
        }

        private void GoToEnvironments(object sender, EventArgs e)
        {
            SetupFromClick("Environments");
        }

        private void GoToEvents(object sender, EventArgs e)
        {
            SetupFromClick("Events");
        }

        private void GoToItems(object sender, EventArgs e)
        {
            SetupFromClick("Items");
        }

        private void GoToOtherLists(object sender, EventArgs e)
        {
            //SetupFromClick();
        }

        private void GoToPassiveSkills(object sender, EventArgs e)
        {
            SetupFromClick("PassiveSkills");
        }

        private void GoToPlatforms(object sender, EventArgs e)
        {
            SetupFromClick("Platforms");
        }

        private void GoToPlayers(object sender, EventArgs e)
        {
            SetupFromClick("Players");
        }

        private void GoToProjectiles(object sender, EventArgs e)
        {
            SetupFromClick("Projectiles");
        }

        private void GoToSkills(object sender, EventArgs e)
        {
            SetupFromClick("Skills");
        }

        private void GoToStates(object sender, EventArgs e)
        {
            SetupFromClick("States");
        }

        private void GoToWeapons(object sender, EventArgs e)
        {
            SetupFromClick("Weapons");
        }

    }
}
