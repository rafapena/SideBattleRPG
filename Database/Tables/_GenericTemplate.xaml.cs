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

namespace Database.Tables
{
    /// <summary>
    /// Interaction logic for _GenericTemplate.xaml
    /// </summary>
    public partial class _GenericTemplate : Page, ObjectOperations
    {
        public _GenericTemplate()
        {
            InitializeComponent();
            SQLDB.CurrentTable = "InsertTableName";
            ObjectList.SetupTable(SQLDB.CurrentTable + "s");
            InitializeNew();
        }

        public void Automate()
        {
            Base.Automate();
            //Insert Here
        }

        public void Copy()
        {
            if (!Utils.Confirm("Are you sure?", "Cloning " + SQLDB.CurrentTable)) return;
            Base.Copy();
            SQLDB.db.Open();
            //Insert Here
            SQLDB.db.Close();
        }

        public void Create()
        {
            string err = GetErrors();
            if (err != "") { MessageBox.Show("Could Not Update due to the following:\n\n"); return; }
            Base.Create();
            SQLDB.db.Open();
            //Insert Here
            SQLDB.db.Close();
        }

        public void Delete()
        {
            if (!Utils.Confirm("Are you sure?", "Deleting " + SQLDB.CurrentTable)) return;
            Base.Delete();
            SQLDB.Command("DELETE FROM InsertTableName WHERE InsertTableName_ID = " + SQLDB.CurrentId.ToString());
            SQLDB.db.Open();
            //Insert Here
            SQLDB.db.Close();
        }

        public string GetErrors()
        {
            string err = Base.GetErrors();
            //Insert Here
            return err;
        }

        public void InitializeNew()
        {
            Base.InitializeNew();
            //Insert Here
            BottomButtons.InitializeNewSettings();
        }

        public void Read()
        {
            Base.Read();
            SQLDB.db.Open();
            //Insert Here
            SQLDB.db.Close();
            BottomButtons.ReadSettings();
        }

        public void Update()
        {
            Base.Update();
            SQLDB.db.Open();
            //Insert Here
            SQLDB.db.Close();
        }
    }
}