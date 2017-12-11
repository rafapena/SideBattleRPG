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
using static Database.Utilities.Utils;
using static Database.Utilities.SQLDB;
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

        private void GoToAchievements(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Achievements");
        }

        private void GoToAnimations(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Animations");
        }

        private void GoToClasses(object sender, EventArgs e)
        {
            //Application.Current.MainWindow.Content = new BattlerClass();
        }

        private void GoToEnemies(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Enemies");
        }

        private void GoToEnemyGroups(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "EnemyGroups");
        }

        private void GoToEnvironments(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Environments");
        }

        private void GoToEvents(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Events");
        }

        private void GoToItems(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Items");
        }

        private void GoToOtherLists(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, );
        }

        private void GoToPassiveSkills(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "PassiveSkills");
        }

        private void GoToPlatforms(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Platforms");
        }

        private void GoToPlayers(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Content = new Player();
        }

        private void GoToProjectiles(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Projectiles");
        }

        private void GoToSkills(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Skills");
        }

        private void GoToStates(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "States");
        }

        private void GoToWeapons(object sender, EventArgs e)
        {
            //SetupFromClick(sender as Button, "Weapons");
        }

    }
}
