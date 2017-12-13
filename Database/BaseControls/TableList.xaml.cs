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
using Database.Classes;
using Database.Utilities;
using static Database.Utilities.TableBuilder;

namespace Database.BaseControls
{
    /// <summary>
    /// Interaction logic for TableList.xaml
    /// </summary>
    public partial class TableList : UserControl
    {
        private Footer LinkedFooter { get; set; }

        public TableList()
        {
            InitializeComponent();
        }

        public void SetupTable(Footer link)
        {
            LinkedFooter = link;
            LinkedFooter.ApplyInitializeNewSettings();
            Title.Text = SQLDB.CurrentTable;
            TableSetup(RowsTable);
            int iterations = 0;
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                string query =
                    "SELECT * FROM BaseObjects JOIN " + SQLDB.CurrentTable + " " +
                    "WHERE " + SQLDB.CurrentClass + "ID = " + SQLDB.CurrentClass + "_ID " +
                    "ORDER BY Name ASC";
                using (SQLiteDataReader reader = SQLDB.Retrieve(query, conn))
                {
                    while (reader.Read()) CreateRow(RowsTable, iterations++, reader);
                }
                conn.Close();
            }
        }

        public void CreateRow(Grid table, int rowNum, SQLiteDataReader reader)
        {
            table.RowDefinitions.Add(new RowDefinition());
            Button b = Button((string)reader["Name"], Read, "#dddddd", int.Parse(reader[SQLDB.CurrentClass + "_ID"].ToString()), rowNum, 0);
            b.Margin = Margin(0, 0, 5, 0);
            b.Width = 70;
            table.Children.Add(b);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Trigger Setup and Trigger Page Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Read(object sender, EventArgs e)
        {
            SQLDB.CurrentId = (int)(sender as Button).Tag;
            switch (SQLDB.CurrentClass)
            {
                case "Player": Read<Player>(); break;
            }
            LinkedFooter.ApplyReadSettings();
        }

        public void InitializeNew(object sender, EventArgs e)
        {
            SQLDB.CurrentId = 0;
            switch (SQLDB.CurrentClass)
            {
                case "Player": InitializeNew<Player>(); break;
            }
            LinkedFooter.ApplyInitializeNewSettings();
        }

        private void Read<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).Read(); }
        private void InitializeNew<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).InitializeNew(); }
    }
}