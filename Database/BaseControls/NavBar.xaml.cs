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
using Database.Classes;

namespace Database.BaseControls
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

        private void SetUpFromClick<P>(string currentTable) where P : Page, new()
        {
            if (SQLDB.CurrentTable == currentTable) return;
            SQLDB.CurrentTable = currentTable;
            SQLDB.CurrentClass = typeof(P).Name;
            Application.Current.MainWindow.Content = new P();
        }

        private void GoToAchievements(object sender, EventArgs e)
        {
            SetUpFromClick<Achievement>("Achievements");
        }

        private void GoToClasses(object sender, EventArgs e)
        {
            //SetUpFromClick(); BattlerClass();
        }

        private void GoToEnemies(object sender, EventArgs e)
        {
            //SetUpFromClick(); Enemies();
        }

        private void GoToEnemyGroups(object sender, EventArgs e)
        {
            //SetUpFromClick(); EnemyGroups();
        }

        private void GoToEnvironments(object sender, EventArgs e)
        {
            //SetUpFromClick(); Environments();
        }

        private void GoToEvents(object sender, EventArgs e)
        {
            //SetUpFromClick(); Events();
        }

        private void GoToItems(object sender, EventArgs e)
        {
            //SetUpFromClick(); Items();
        }

        private void GoToOtherLists(object sender, EventArgs e)
        {
            //SetUpFromClick(); ();
        }

        private void GoToPassiveSkills(object sender, EventArgs e)
        {
            //SetUpFromClick(); PassiveSkill();
        }

        private void GoToPlatforms(object sender, EventArgs e)
        {
            //SetUpFromClick<Platforms>("Platforms", "Players");
        }

        private void GoToPlayers(object sender, EventArgs e)
        {
            SetUpFromClick<Player>("Players");
        }

        private void GoToProjectiles(object sender, EventArgs e)
        {
            //SetUpFromClick(); Projectiles();
        }

        private void GoToSkills(object sender, EventArgs e)
        {
            //SetUpFromClick(); Skill();
        }

        private void GoToStates(object sender, EventArgs e)
        {
            //SetUpFromClick(); State();
        }

        private void GoToWeapons(object sender, EventArgs e)
        {
            //SetUpFromClick(); Weapon();
        }

    }
}
