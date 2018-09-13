using System;
using System.Windows;
using System.Windows.Controls;
using Database.Classes;
using System.Data.SQLite;
using Database.Utilities;

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

        private void SetUpFromClick<P>() where P : Page, new()
        {
            if (SQLDB.CurrentTable == typeof(P).Name) return;
            SQLDB.CurrentTable = typeof(P).Name;
            Application.Current.MainWindow.Content = new P();
        }

        private void GoToAchievements(object sender, EventArgs e)
        {
            SetUpFromClick<Achievement>();
        }

        private void GoToBattlerClasses(object sender, EventArgs e)
        {
            SetUpFromClick<BattlerClass>();
        }

        private void GoToEnemies(object sender, EventArgs e)
        {
            SetUpFromClick<Enemy>();
        }
        
        private void GoToEnvironments(object sender, EventArgs e)
        {
            //SetUpFromClick<Environment>();
        }

        private void GoToItems(object sender, EventArgs e)
        {
            SetUpFromClick<Item>();
        }

        private void GoToOtherLists(object sender, EventArgs e)
        {
            if (SQLDB.CurrentTable == "") return;
            SQLDB.CurrentTable = "";
            Application.Current.MainWindow.Content = new ClassesUnstructured.OtherLists();
        }

        private void GoToPassiveSkills(object sender, EventArgs e)
        {
            SetUpFromClick<PassiveSkill>();
        }

        private void GoToPlatforms(object sender, EventArgs e)
        {
            //SetUpFromClick<Platforms>();
        }

        private void GoToPlayers(object sender, EventArgs e)
        {
            SetUpFromClick<Player>();
        }

        private void GoToProjectiles(object sender, EventArgs e)
        {
            //SetUpFromClick<Projectile>();
        }

        private void GoToSkills(object sender, EventArgs e)
        {
            SetUpFromClick<Skill>();
        }

        private void GoToStates(object sender, EventArgs e)
        {
            SetUpFromClick<State>();
        }

        private void GoToWeapons(object sender, EventArgs e)
        {
            //SetUpFromClick<Weapon>();
        }

    }
}
