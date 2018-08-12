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
        public static List<SQLiteParameter> Inputs { get; set; }
        public static string CurrentClass { get; set; }
        public static string CurrentTable { get; set; }
        public static int CurrentId { get; set; }


        public static SQLiteConnection DB()
        {
            return new SQLiteConnection(@"data source=C:\Users\User\GC_RPG_DB.db; Version=3; foreign keys=true;");
        }


        public static SQLiteDataReader Retrieve(string sqlCommand, SQLiteConnection currentTransaction)
        {
            SQLiteCommand command = new SQLiteCommand(sqlCommand, currentTransaction);
            return command.ExecuteReader();
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
                        if (Inputs != null && Inputs.Count > 0) comm.Parameters.AddRange(Inputs.ToArray());
                        comm.CommandType = CommandType.Text;
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception e) { MessageBox.Show("Something went wrong:\n" + e.Message); }
                }
                conn.Close();
            }
        }


        public static int GetScalar(string sqlCommand)
        {
            int val = 0;
            using (var conn = DB())
            {
                conn.Open();
                using (var comm = new SQLiteCommand(sqlCommand, conn))
                {
                    try { val = (int)((long)comm.ExecuteScalar()); }
                    catch (Exception e) { MessageBox.Show("Could not convert into a scalar:\n" + e.Message); }
                }
                conn.Close();
            }
            return val;
        }


        public static int GetMaxIdFromTable(string table, string type)
        {
            int maxId;
            using (var conn = DB())
            {
                conn.Open();
                using (var comm = new SQLiteCommand("SELECT MAX(" + type + "_ID) FROM " + table, conn))
                {
                    try { maxId = (int)((long)comm.ExecuteScalar()) + 1; }
                    catch (InvalidCastException) { maxId = 1; }
                }
                conn.Close();
            }
            return maxId;
        }
    }
}