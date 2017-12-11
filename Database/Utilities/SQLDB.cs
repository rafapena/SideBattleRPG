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
        public static string CurrentTableName;

        private static DataTable GetDataTable(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                db.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, db);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static ArrayList GetTables(string tableType)
        {
            ArrayList list = new ArrayList();
            /*string query =
                    "SELECT Name FROM sqlite_master " +
                    "WHERE type = 'table'" +
                    "ORDER BY 1";*/
            string query =
                    "SELECT * FROM " + tableType + " " +
                    "WHERE type = 'table'" +
                    "ORDER BY 1";
            try
            {
                DataTable table = GetDataTable(query);
                //foreach (DataRow row in table.Rows) list.Add(row.ItemArray[0].ToString());
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return list;
        }

        public static void RetrieveContents()
        {
            //DataSet d = new DataSet();
            //foreach (string tableName in GetTables()) d.Tables.Add(GetDataTable("select * from " + tableName));
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
