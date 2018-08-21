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
using Database.BaseControls;
using Database.Utilities;

namespace Database.Classes
{
    public abstract class _ClassOperations : Page, ObjectClassOperations
    {
        public TableList LinkedTableList { get; set; }
        public Footer LinkedFooter { get; set; }


        protected abstract void SetupTableData();
        protected abstract void OnInitializeNew();
        public void InitializeNew()
        {
            LinkedTableList.SetupTable(false);
            SQLDB.CurrentId = SQLDB.MaxIdPlusOne(SQLDB.CurrentTable, SQLDB.CurrentClass);
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
            if (err != "") MessageBox.Show("Could not create " + SQLDB.CurrentClass + ":\n\n" + err);
            else
            {
                using (var conn = SQLDB.DB())
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
                MessageBox.Show(SQLDB.CurrentClass + " created");
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
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Read(conn,
                    "SELECT * FROM " + SQLDB.CurrentTable + " WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString()))
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
            if (err != "") MessageBox.Show("Could not update " + SQLDB.CurrentClass + ":\n\n" + err);
            else
            {
                using (var conn = SQLDB.DB())
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
                MessageBox.Show(SQLDB.CurrentClass + " updated");
            }
        }
        protected void SQLUpdate(SQLiteConnection conn, string input)
        {
            SQLDB.ResetParameterizedInputs();
            ParameterizeInputs();
            SQLDB.Write(conn, "UPDATE " + SQLDB.CurrentTable + " SET " + input + " WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString() + ";");
        }


        protected abstract void OnDelete(SQLiteConnection conn);
        public void Delete()
        {
            if (!Utils.Confirm("Are you sure?", "Deleting " + SQLDB.CurrentClass)) return;
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    OnDelete(conn);
                    transaction.Commit();
                }
                conn.Close();
            }
            MessageBox.Show(SQLDB.CurrentClass + " deleted");
            InitializeNew();
        }


        protected abstract void OnClone(SQLiteConnection conn);
        public void Clone()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not clone " + SQLDB.CurrentClass + ":\n\n" + err);
            else
            {
                SQLDB.CurrentId = SQLDB.MaxIdPlusOne(SQLDB.CurrentTable, SQLDB.CurrentClass);
                using (var conn = SQLDB.DB())
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
                MessageBox.Show(SQLDB.CurrentClass + " cloned");
            }
        }
    }
}