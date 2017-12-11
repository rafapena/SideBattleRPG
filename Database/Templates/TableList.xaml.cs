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
using static Database.Utilities.TableBuilder;
using static Database.Utilities.SQLDB;

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for TableList.xaml
    /// </summary>
    public partial class TableList : UserControl
    {
        public string CurrentTableName { get; private set; }
        public string CurrentTableNamePl { get; private set; }

        public TableList()
        {
            InitializeComponent();
        }

        public void SetupTable(string tableType, string tableTypePlural)
        {
            CurrentTableName = tableType;
            CurrentTableNamePl = tableTypePlural;
            Title.Text = CurrentTableNamePl;
            TableSetup(Objects);
            int iterations = 0;
            db.Open();
            SQLiteCommand command = new SQLiteCommand(
                "SELECT * FROM BaseObjects JOIN " + CurrentTableNamePl + " WHERE " + CurrentTableName + "ID = " + CurrentTableName + "_ID", db);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) CreateRow(Objects, iterations++, reader);
            db.Close();
        }

        public void CreateRow(Grid table, int rowNum, SQLiteDataReader reader)
        {
            table.RowDefinitions.Add(new RowDefinition());
            table.Children.Add(TextBlock(rowNum+1, rowNum, 0));
            Button b = Button((string)reader["Name"], Read, "#dddddd", int.Parse(reader[CurrentTableName + "_ID"].ToString()), rowNum, 1);
            b.Margin = Margin(0,0,5,0);
            table.Children.Add(b);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Trigger Setup and Trigger Page Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Read(object sender, EventArgs e)
        {
            int id = (int)(sender as Button).Tag;
            switch (CurrentTableName)
            {
                case "Class": Read<BattlerClass>(sender, e, id); break;
                case "Player": Read<Player>(sender, e, id); break;
            }
        }

        public void InitializeNew(object sender, EventArgs e)
        {
            switch (CurrentTableName)
            {
                case "Class": InitializeNew<BattlerClass>(sender, e); break;
                case "Player": InitializeNew<Player>(sender, e); break;
            }
        }

        private void Read<P>(object sender, EventArgs e, int id) where P : Page, Utilities.ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).Read(id);
        }
        private void InitializeNew<P>(object sender, EventArgs e) where P : Page, Utilities.ObjectOperations
        {
            (Application.Current.MainWindow.Content as P).InitializeNew();
        }
    }
}