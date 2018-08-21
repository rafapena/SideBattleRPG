using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace Database.Utilities
{
    public static class SQLDB
    {
        public static string CurrentClass { get; set; }
        public static string CurrentTable { get; set; }
        public static int CurrentId { get; set; }

        public static List<SQLiteParameter> Inputs { get; private set; }
        public static List<BlobInput> BlobInputs { get; private set; }
        public class BlobInput
        {
            public string Name { get; private set; }
            public byte[] Data { get; private set; }
            public int Size { get; private set; }
            public BlobInput(string name, byte[] data, int size)
            {
                Name = name;
                Data = data;
                Size = size;
            }
        }


        // Indicates the file where the database is located
        public static SQLiteConnection DB()
        {
            return new SQLiteConnection(@"data source=C:\Users\User\GC_RPG_DB.db; Version=3; foreign keys=true;");
        }


        public static void ResetParameterizedInputs()
        {
            Inputs = new List<SQLiteParameter>();
            BlobInputs = new List<BlobInput>();
        }
        public static void ParameterizeInput(string name, string value)
        {
            Inputs.Add(new SQLiteParameter(name, value));
        }
        public static void ParameterizeBlobInput(string name, byte[] value, int size)
        {
            BlobInputs.Add(new BlobInput(name, value, size));
        }


        public static SQLiteDataReader Read(SQLiteConnection conn, string sqlCommand)
        {
            SQLiteCommand command = new SQLiteCommand(sqlCommand, conn);
            return command.ExecuteReader();
        }

        public static void Write(SQLiteConnection conn, string sqlCommand)
        {
            using (var comm = new SQLiteCommand(sqlCommand, conn))
            {
                if (Inputs != null && Inputs.Count > 0) comm.Parameters.AddRange(Inputs.ToArray());
                if (BlobInputs != null)
                    for (int i = 0; i < BlobInputs.Count; i++)
                        comm.Parameters.Add(BlobInputs[i].Name, DbType.Binary, 4 * BlobInputs[i].Size).Value = BlobInputs[i].Data;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
        }


        public static int Scalar(string sqlCommand)
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

        public static int MaxIdPlusOne(string table, string type)
        {
            int maxIdPlusOne;
            using (var conn = DB())
            {
                conn.Open();
                using (var comm = new SQLiteCommand("SELECT MAX(" + type + "_ID) FROM " + table, conn))
                {
                    try { maxIdPlusOne = (int)((long)comm.ExecuteScalar()) + 1; }
                    catch (InvalidCastException) { maxIdPlusOne = 1; }
                }
                conn.Close();
            }
            return maxIdPlusOne;
        }
    }
}