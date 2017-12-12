using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Collections;
using System.Windows;

namespace Database.Utilities
{
    public static class SQLDB
    {
        public static SQLiteParameter[] Inputs { get; private set; }
        public static string CurrentTable = "";
        public static int CurrentId = 0;

        public static SQLiteConnection DB()
        {
            return new SQLiteConnection("URI=file://C:/Users/User/GC_RPG_DB.db");
        }

        public static SQLiteDataReader Retrieve(string sqlCommand, SQLiteConnection currentTransaction)
        {
            SQLiteCommand command = new SQLiteCommand(sqlCommand, currentTransaction);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader;
        }

        public static void AddParameters(SQLiteParameter[] InputsList)
        {
            Inputs = null;
            Inputs = InputsList;
        }

        public static void Command(string sqlCommand)
        {
            using (var conn = DB())
            {
                conn.Open();
                using (var comm = new SQLiteCommand(sqlCommand, conn))
                {
                    try
                    {
                        if (Inputs != null)
                        {
                            comm.Parameters.AddRange(Inputs);
                            Inputs = null;
                        }
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception e) { MessageBox.Show("Something went wrong:\n" + e.Message); }
                }
                conn.Close();
            }
        }
    }
}