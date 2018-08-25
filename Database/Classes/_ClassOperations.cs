using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            LinkedTableList.RemoveButtonHighlight();
            SetupTableData();
            OnInitializeNew();
        }

        public abstract string ValidateInputs();
        public abstract void ParameterizeInputs();


        protected abstract void OnCreate(SQLiteConnection conn);
        public void Create()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show(err, "Could not create " + SQLDB.CurrentTable);
            else
            {
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
                MessageBox.Show(SQLDB.CurrentTable + " created");
            }
        }
        protected void SQLCreate(SQLiteConnection conn, string attributes, string inputs)
        {
            SQLDB.ResetParameterizedInputs();
            ParameterizeInputs();
            SQLDB.Write(conn, "INSERT INTO " + SQLDB.CurrentTable + " (" + attributes + ") VALUES (" + inputs + ");");
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            LinkedFooter.ApplyReadSettings();
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
                MessageBox.Show(SQLDB.CurrentTable + " updated");
            }
        }
        protected void SQLUpdate(SQLiteConnection conn, string input)
        {
            SQLDB.ResetParameterizedInputs();
            ParameterizeInputs();
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
            MessageBox.Show(SQLDB.CurrentTable + " deleted");
            InitializeNew();
        }


        protected abstract void OnClone(SQLiteConnection conn);
        public void Clone()
        {
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
                MessageBox.Show(SQLDB.CurrentTable + " cloned");
            }
        }
    }
}