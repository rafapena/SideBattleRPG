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
using Database.Utilities;

namespace Database.Classes
{
    public abstract class _ClassOperations : Page, ObjectOperations
    {
        public abstract void InitializeNew();
        public abstract void Automate();
        public abstract string ValidateInputs();


        protected abstract void OnCreate();
        public void Create()
        {
            OnCreate();
            MessageBox.Show("Creating successful");
            InitializeNew();
        }
        protected void SQLCreate(string[] text)
        {
            SQLDB.Command("INSERT INTO " + SQLDB.CurrentTable + " (" + text[0] + ") VALUES (" + text[1] + ");");
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + SQLDB.CurrentTable, conn))
                {
                    reader.Read();
                    OnRead(reader);
                }
                conn.Close();
            }
        }


        protected abstract void OnUpdate();
        public void Update()
        {
            string err = ValidateInputs();
            if (err != "") { MessageBox.Show("Could not update due to the following:\n\n" + err); return; }
            OnUpdate();
            MessageBox.Show("Updating successful");
        }
        protected void SQLUpdate(string input)
        {
            SQLDB.Command("UPDATE " + SQLDB.CurrentTable + " SET " + input + " WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString() + ";");
        }


        protected abstract void OnDelete();
        public void Delete()
        {
            if (!Utils.Confirm("Are you sure?", "Deleting " + SQLDB.CurrentClass)) return;
            OnDelete();
            SQLDB.Command("DELETE FROM " + SQLDB.CurrentTable + " WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString());
            MessageBox.Show("Deleting successful");
            InitializeNew();
        }


        protected abstract void OnClone();
        public void Clone()
        {
            string err = ValidateInputs();
            if (err != "") { MessageBox.Show("Could not copy due to the following:\n\n" + err); return; }
            if (!Utils.Confirm("Are you sure?", "Cloning " + SQLDB.CurrentClass)) return;
            OnClone();
            MessageBox.Show("Cloning successful");
        }
    }
}
