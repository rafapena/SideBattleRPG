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
    /// <summary>
    /// The main static class directly managing the data from the SQLite database.
    /// 
    /// The most important rule: When connecting the database ALWAYS use the statement:
    /// using (var conn = SQLDB.DB()) { ... }
    /// 
    /// When reading and writing data, it is highly advised to use SQLDB.Read(conn, ...) and SQLDB.Write(conn, ...), respectively.
    /// Putting functions like command.ExecuteCommand() and command.Parameters.AddRange() anywhere in the project outside of this class,
    /// will be consufing and redundant for this application. They are already provided in the following functions mentioned above
    /// </summary>
    public static class SQLDB
    {
        // The only global variables: should indicate the current table list and item the user is viewing
        public static string CurrentTable { get; set; }
        public static int CurrentId { get; set; }

        // For handling input parameters: deals with injection attacks
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
            return new SQLiteConnection(@"data source=C:\Users\User\SideBattleRPG.db; Version=3; foreign keys=true;");
        }


        // Deals with sanitizing across the object operations Create(), Update(), and Clone()
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


        // Only reads the SQL data: Assumes that sqlCommand is a SELECT statement
        public static SQLiteDataReader Read(SQLiteConnection conn, string sqlCommand)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(sqlCommand, conn);
                return command.ExecuteReader();
            }
            catch (Exception e)
            {
                string endMsg = "The program will probably crash or misbehave from this point";
                MessageBox.Show(e.Message + "\n" + endMsg, "Error while writing to the database");
                return null;
            }
        }

        // Assume this doesn't do anything else besides: creating, updating, deleting, and cloning
        public static void Write(SQLiteConnection conn, string sqlCommand)
        {
            try
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
            catch (Exception e)
            {
                string endMsg = "The program will probably crash or misbehave from this point";
                MessageBox.Show(e.Message + "\n" + endMsg, "Error while writing to the database");
            }
        }

        
        // Returns a scalar value, such as COUNT(*): This function is rarely ever used, if not at all
        public static int Scalar(string sqlCommand)
        {
            int val = 0;
            using (var conn = DB())
            {
                conn.Open();
                using (var comm = new SQLiteCommand(sqlCommand, conn))
                {
                    try { val = (int)((long)comm.ExecuteScalar()); }
                    catch (Exception e) { MessageBox.Show(e.Message + "\nReturning default value 0", "Could not convert into a scalar"); }
                }
                conn.Close();
            }
            return val;
        }

        // When a new item is going to be created, the currentId gets set to a value +1 value higher than the max Id of the table
        public static int MaxIdPlusOne(string table)
        {
            int maxIdPlusOne;
            using (var conn = DB())
            {
                conn.Open();
                using (var comm = new SQLiteCommand("SELECT MAX(" + table + "_ID) FROM " + table, conn))
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