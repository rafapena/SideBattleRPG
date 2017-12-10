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

namespace Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public static class SQLDB<B> where B : Superclasses.BaseObject
    {
        public static SQLiteConnection db = new SQLiteConnection("URI=file://C:/Users/User/GC_RPG_DB.db");
        public static List<B> DBList;

        public static void RetrieveContents()
        {
            db.Open();
            DBList = new List<B>();
            string tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Text_Entry NVARCHAR(2048) NULL);"; //+
                                                    //"DROP TABLE IF EXISTS MyTable;";
            SQLiteCommand createTable = new SQLiteCommand(tableCommand, db);
            createTable.ExecuteReader();
            db.Close();
        }

        public static void Command(string sqlCommand)
        {
            db.Open();
            SQLiteCommand command = new SQLiteCommand(sqlCommand, db);
            command.ExecuteReader();
            db.Close();
        }
    }
}
