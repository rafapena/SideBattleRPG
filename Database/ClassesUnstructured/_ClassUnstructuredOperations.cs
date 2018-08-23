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

/*
 * OtherLists
 * Skill/Tool Animations
 * Skill/Tools Animation Combos
 * State Animations
 * Event Control Flow
*/

namespace Database.ClassesUnstructured
{
    public abstract class _ClassUnstructuredOperations : Page, ObjectPageOperations
    {
        protected virtual void SetupTableData() { }
        protected virtual void OnInitializeNew() { }
        public void InitializeNew()
        {
            SetupTableData();
            OnInitializeNew();
        }

        public abstract string ValidateInputs();
        public abstract void ParameterizeInputs();


        protected virtual void OnCreate(SQLiteConnection conn) { }
        public void Create()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not update due to the following:\n\n" + err);
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
                MessageBox.Show("Creating successful");
            }
        }
        protected void SQLCreate(SQLiteConnection conn, string[] text)
        {
            SQLDB.ResetParameterizedInputs();
            ParameterizeInputs();
            SQLDB.Write(conn, "INSERT INTO " + SQLDB.CurrentTable + " (" + text[0] + ") VALUES (" + text[1] + ");");
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Read(conn, "SELECT * FROM " + SQLDB.CurrentTable + " WHERE " + SQLDB.CurrentTable + "_ID = " + SQLDB.CurrentId.ToString()))
                {
                    SetupTableData();
                    reader.Read();
                    OnRead(reader);
                }
                conn.Close();
            }
        }


        protected virtual void OnUpdate(SQLiteConnection conn) { }
        public void Update()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not update due to the following:\n\n" + err);
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
                MessageBox.Show("Updating successful");
            }
        }
        protected void SQLUpdate(SQLiteConnection conn, string input)
        {
            SQLDB.ResetParameterizedInputs();
            ParameterizeInputs();
            SQLDB.Write(conn, "UPDATE " + SQLDB.CurrentTable + " SET " + input + " WHERE " + SQLDB.CurrentTable + "_ID = " + SQLDB.CurrentId.ToString() + ";");
        }


        protected virtual void OnDelete(SQLiteConnection conn) { }
        public void Delete()
        {
            if (!Utils.Confirm("Are you sure?", "Deleting " + SQLDB.CurrentTable)) return;
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
            MessageBox.Show("Deleting successful");
            InitializeNew();
        }


        protected virtual void OnClone(SQLiteConnection conn) { }
        public void Clone()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not clone " + SQLDB.CurrentTable + ":\n\n" + err);
            else
            {
                SQLDB.CurrentId = SQLDB.MaxIdPlusOne(SQLDB.CurrentTable);
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
                MessageBox.Show(SQLDB.CurrentTable + " cloned");
            }
        }
    }
}
