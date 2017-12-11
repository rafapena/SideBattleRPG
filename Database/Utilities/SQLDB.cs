using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Collections;

namespace Database.Utilities
{
    public static class SQLDB
    {
        public static SQLiteConnection db = new SQLiteConnection("URI=file://C:/Users/User/GC_RPG_DB.db");
        public static string CurrentTable; 
        public static int CurrentId;

        public static void Command(string sqlCommand)
        {
            db.Open();
            SQLiteCommand command = new SQLiteCommand(sqlCommand, db);
            command.ExecuteReader();
            db.Close();
        }
    }
}