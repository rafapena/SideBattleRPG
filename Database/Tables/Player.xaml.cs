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
using Database.Utilities;

namespace Database.Tables
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : Page, ObjectOperations
    {
        public Player()
        {
            InitializeComponent();
            SQLDB.CurrentTable = "Player";
            ObjectList.SetupTable("Players");
            InitializeNew();
        }

        public void Automate()
        {
            Base.Automate();
        }

        public void Copy()
        {
            Base.Copy();
        }

        public void Create()
        {
            string err = GetErrors();
            if (err != "") { MessageBox.Show("Could Not Update due to the following:\n\n"); return; }
            Base.Create();
        }

        public void Delete()
        {
            Base.Delete();
            SQLDB.Command("DELETE FROM Players WHERE Player_ID = " + SQLDB.CurrentId.ToString());
        }

        public string GetErrors()
        {
            string err = Base.GetErrors();
            return err;
        }

        public void InitializeNew()
        {
            Base.InitializeNew();
            BottomButtons.InitializeNewSettings();
        }

        public void Read()
        {
            Base.Read();
            BottomButtons.ReadSettings();
        }

        public void Update()
        {
            Base.Update();
        }
    }
}
