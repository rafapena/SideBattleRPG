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
using static Database.Utilities.SQLDB;

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
            ObjectList.SetupTable("Player", "Players");
            BottomButtons.SetupFooter("Player");
        }

        public void Automate()
        {
            throw new NotImplementedException();
        }

        public void Copy()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            MessageBox.Show("CREATE Player");
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public string GetErrors()
        {
            throw new NotImplementedException();
        }

        public void InitializeNew()
        {
            MessageBox.Show("init Player");
        }

        public void Read(int id)
        {
            /*db.Open();
            SQLiteCommand command = new SQLiteCommand(
                "SELECT * FROM " + CurrentTableNamePl + " WHERE " + CurrentTableName + "_ID = " + (int)(sender as Button).Tag, db);
            SQLiteDataReader reader = command.ExecuteReader();
            db.Close();*/
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
