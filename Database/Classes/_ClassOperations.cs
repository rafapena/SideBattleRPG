using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.SQLite;
using Database.BaseControls;
using Database.Utilities;

namespace Database.Classes
{
    public abstract class _ClassOperations : Page, ObjectPageOperations
    {
        public TableList LinkedTableList { get; set; }
        public Footer LinkedFooter { get; set; }


        protected abstract void SetupTableData();
        protected abstract void OnInitializeNew();
        public void InitializeNew()
        {
            LinkedTableList.SetupTable(false);
            SQLDB.CurrentId = SQLDB.MaxIdPlusOne(SQLDB.CurrentTable);
            LinkedFooter.ApplyInitializeNewSettings();
            LinkedFooter.RemoveOperationMessage();
            LinkedTableList.RemoveButtonHighlight();
            SetupTableData();
            OnInitializeNew();
        }

        public abstract string ValidateInputs();
        public abstract void ParameterizeAttributes();


        protected abstract void OnCreate(SQLiteConnection conn);
        public void Create()
        {
            LinkedFooter.RemoveOperationMessage();
            string err = ValidateInputs();
            if (err != "")  MessageBox.Show(err, "Could not create " + SQLDB.CurrentTable);
            else {
                using (var conn = AccessDB.Connect())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        OnCreate(conn);
                        transaction.Commit();
                    }
                    conn.Close();
                }
                LinkedTableList.SetupTable(true);
                LinkedFooter.ApplyReadSettings();
                LinkedFooter.SetOperationMessage(SQLDB.CurrentTable + " Created", Colors.LightGreen);
            }
        }
        protected void SQLCreate(SQLiteConnection conn, string attributes, string inputs)
        {
            SQLDB.ResetParameterizedAttributes();
            ParameterizeAttributes();
            SQLDB.Write(conn, "INSERT INTO " + SQLDB.CurrentTable + " (" + attributes + ") VALUES (" + inputs + ");");
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            LinkedFooter.ApplyReadSettings();
            LinkedFooter.RemoveOperationMessage();
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                using (var reader = SQLDB.Read(conn, "SELECT * FROM " + SQLDB.CurrentTable + " WHERE " + SQLDB.CurrentTable + "_ID = " + SQLDB.CurrentId + ";"))
                {
                    reader.Read();
                    SetupTableData();
                    OnRead(reader);
                }
                conn.Close();
            }
        }


        protected abstract void OnUpdate(SQLiteConnection conn);
        public void Update()
        {
            LinkedFooter.RemoveOperationMessage();
            string err = ValidateInputs();
            if (err != "") MessageBox.Show(err, "Could not update " + SQLDB.CurrentTable);
            else
            {
                using (var conn = AccessDB.Connect())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        OnUpdate(conn);
                        transaction.Commit();
                    }
                    conn.Close();
                }
                LinkedTableList.SetupTable(true);
                LinkedFooter.SetOperationMessage(SQLDB.CurrentTable + " Updated", Colors.SkyBlue);
            }
        }
        protected void SQLUpdate(SQLiteConnection conn, string input)
        {
            SQLDB.ResetParameterizedAttributes();
            ParameterizeAttributes();
            SQLDB.Write(conn, "UPDATE " + SQLDB.CurrentTable + " SET " + input + " WHERE " + SQLDB.CurrentTable + "_ID = " + SQLDB.CurrentId.ToString() + ";");
        }


        protected abstract void OnDelete(SQLiteConnection conn);
        public void Delete()
        {
            if (!Utils.Confirm("Are you sure?", "Deleting " + SQLDB.CurrentTable)) return;
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    OnDelete(conn);
                    transaction.Commit();
                }
                conn.Close();
            }
            InitializeNew();
            LinkedFooter.SetOperationMessage(SQLDB.CurrentTable + " Deleted", Colors.PaleVioletRed);
        }


        protected abstract void OnClone(SQLiteConnection conn);
        public void Clone()
        {
            LinkedFooter.RemoveOperationMessage();
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not clone " + SQLDB.CurrentTable + ":\n\n" + err);
            else
            {
                SQLDB.CurrentId = SQLDB.MaxIdPlusOne(SQLDB.CurrentTable);
                using (var conn = AccessDB.Connect())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        OnClone(conn);
                        OnCreate(conn);
                        transaction.Commit();
                    }
                    conn.Close();
                }
                LinkedTableList.SetupTable(true);
                LinkedFooter.SetOperationMessage(SQLDB.CurrentTable + " Cloned", Colors.DarkTurquoise);
            }
        }
    }
}