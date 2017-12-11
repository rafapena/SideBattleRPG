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
using Database.Tables;
using Database.Utilities;
using static Database.Utilities.TableBuilder;

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for TableList.xaml
    /// </summary>
    public partial class TableList : UserControl
    {
        public TableList()
        {
            InitializeComponent();
        }

        public void SetupTable(string currentTablePl)
        {
            Title.Text = currentTablePl;
            TableSetup(Objects);
            int iterations = 0;
            SQLDB.db.Open();
            SQLiteCommand command = new SQLiteCommand(
                "SELECT * FROM BaseObjects JOIN " + currentTablePl + " " +
                "WHERE " + SQLDB.CurrentTable + "ID = " + SQLDB.CurrentTable + "_ID " +
                "ORDER BY Name ASC", SQLDB.db);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) CreateRow(Objects, iterations++, reader);
            SQLDB.db.Close();
        }

        public void CreateRow(Grid table, int rowNum, SQLiteDataReader reader)
        {
            table.RowDefinitions.Add(new RowDefinition());
            Button b = Button((string)reader["Name"], Read, "#dddddd", int.Parse(reader[SQLDB.CurrentTable + "_ID"].ToString()), rowNum, 0);
            b.HorizontalAlignment = HorizontalAlignment.Left;
            b.Margin = Margin(0,0,5,0);
            table.Children.Add(b);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Trigger Setup and Trigger Page Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Read(object sender, EventArgs e)
        {
            SQLDB.CurrentId = (int)(sender as Button).Tag;
            switch (SQLDB.CurrentTable)
            {
                case "Player": Read<Player>(); break;
            }
        }

        public void InitializeNew(object sender, EventArgs e)
        {
            SQLDB.CurrentId = 0;
            switch (SQLDB.CurrentTable)
            {
                case "Player": InitializeNew<Player>(); break;
            }
        }

        private void Read<P>() where P : Page, Utilities.ObjectOperations { (Application.Current.MainWindow.Content as P).Read(); }
        private void InitializeNew<P>() where P : Page, Utilities.ObjectOperations { (Application.Current.MainWindow.Content as P).InitializeNew(); }
    }
}