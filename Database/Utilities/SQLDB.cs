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

        public static List<SQLiteParameter> Inputs { get; set; }
        public static List<ImageInput> ImageInputs { get; set; }
        public class ImageInput
        {
            public string Name { get; private set; }
            public byte[] Data { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public ImageInput(string name, byte[] data, int width, int height)
            {
                Name = name;
                Data = data;
                Width = width;
                Height = height;
            }
        }


        public static SQLiteConnection DB()
        {
            return new SQLiteConnection(@"data source=C:\Users\User\GC_RPG_DB.db; Version=3; foreign keys=true;");
        }


        public static void ParameterizeImageInput(string name, byte[] value,  int width, int height)
        {
            ImageInputs.Add(new ImageInput(name, value, width, height));
        }

        public static SQLiteDataReader Retrieve(string sqlCommand, SQLiteConnection currentTransaction)
        {
            SQLiteCommand command = new SQLiteCommand(sqlCommand, currentTransaction);
            return command.ExecuteReader();
        }

        public static void Command(SQLiteConnection conn, string sqlCommand)
        {
            using (var comm = new SQLiteCommand(sqlCommand, conn))
            {
                if (Inputs != null && Inputs.Count > 0) comm.Parameters.AddRange(Inputs.ToArray());
                if (ImageInputs != null)
                {
                    for (int i = 0; i < ImageInputs.Count; i++)
                    {
                        int size = 4 * ImageInputs[i].Width * ImageInputs[i].Height;
                        comm.Parameters.Add(ImageInputs[i].Name, DbType.Binary, size).Value = ImageInputs[i].Data;
                    }
                }
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
        }

        public static void OneCommand(string sqlCommand)
        {
            using (var conn = DB())
            {
                conn.Open();
                Command(conn, sqlCommand);
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