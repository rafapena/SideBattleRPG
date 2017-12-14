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
    public abstract class _ClassOperations : Page, ObjectOperations
    {
        public TableList LinkedTableList { get; set; }
        public Footer LinkedFooter { get; set; }

        public abstract void Automate();
        public abstract string ValidateInputs();


        protected abstract void OnInitializeNew();
        public void InitializeNew()
        {
            LinkedTableList.SetupTable(false);
            SQLDB.CurrentId = SQLDB.GetMaxIdFromTable(SQLDB.CurrentTable, SQLDB.CurrentClass);
            LinkedFooter.ApplyInitializeNewSettings();
            LinkedTableList.RemoveButtonHighlight();
            OnInitializeNew();
        }
        

        protected abstract void OnCreate();
        public void Create()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not update due to the following:\n\n" + err);
            else
            {
                OnCreate();
                MessageBox.Show("Creating successful");
                LinkedTableList.SetupTable(true);
                LinkedFooter.ApplyReadSettings();
            }
            SQLDB.ClearParameters();
        }
        protected void SQLCreate(string[] text)
        {
            SQLDB.Command("INSERT INTO " + SQLDB.CurrentTable + " (" + text[0] + ") VALUES (" + text[1] + ");");
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            LinkedFooter.ApplyReadSettings();
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + SQLDB.CurrentTable + " WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString(), conn))
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
            if (err != "") MessageBox.Show("Could not update due to the following:\n\n" + err);
            else
            {
                OnUpdate();
                MessageBox.Show("Updating successful");
                LinkedTableList.SetupTable(true);
            }
            SQLDB.ClearParameters();
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
            MessageBox.Show("Deleting successful");
            InitializeNew();
        }


        protected abstract void OnClone();
        public void Clone()
        {
            if (!Utils.Confirm("Are you sure?\nThe un-updated changes will be discarded", "Cloning " + SQLDB.CurrentClass)) return;
            LinkedFooter.ApplyInitializeNewSettings();
            LinkedTableList.RemoveButtonHighlight();
            SQLDB.CurrentId = SQLDB.GetMaxIdFromTable(SQLDB.CurrentTable, SQLDB.CurrentClass);
            OnClone();
            MessageBox.Show("Cloning successful\nThe cloned " + SQLDB.CurrentClass + " can now be created");
        }
    }
}
