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
    /// <summary>
    /// Interaction logic for _GenericTemplate.xaml
    /// </summary>
    public partial class _GenericTemplate : Page, ObjectOperations
    {
        public _GenericTemplate()
        {
            InitializeComponent();
            ObjectList.SetupTable(SQLDB.CurrentTable + "s", FooterButtons);
            InitializeNew();
        }

        public void Automate()
        {
            Base.Automate();
            //Insert Here
        }

        public void Copy()
        {
            string err = ValidateInputs();
            if (err != "") { MessageBox.Show("Could not copy due to the following:\n\n" + err); return; }
            if (!Utils.Confirm("Are you sure?", "Cloning " + SQLDB.CurrentTable)) return;
            Base.Copy();
            //Insert Here
            MessageBox.Show("Cloning successful");
        }

        public void Create()
        {
            string err = ValidateInputs();
            if (err != "") { MessageBox.Show("Could not create due to the following:\n\n" + err); return; }
            Base.Create();
            //Insert Here
            MessageBox.Show("Creating successful");
            InitializeNew();
        }

        public void Delete()
        {
            if (!Utils.Confirm("Are you sure?", "Deleting " + SQLDB.CurrentTable)) return;
            Base.Delete();
            SQLDB.Command("DELETE FROM " + SQLDB.CurrentTable + "s WHERE " + SQLDB.CurrentTable + "_ID = " + SQLDB.CurrentId.ToString());
            MessageBox.Show("Deleting successful");
            InitializeNew();
        }

        public string ValidateInputs()
        {
            SQLDB.AddParameters(new SQLiteParameter[] { });
            string err = Base.ValidateInputs();
            //Insert Here
            return err;
        }

        public void InitializeNew()
        {
            Base.InitializeNew();
            //Insert Here
        }

        public void Read()
        {
            Base.Read();
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("", conn))
                {
                    // Insert Here
                }
                conn.Close();
            }
        }

        public void Update()
        {
            string err = ValidateInputs();
            if (err != "") { MessageBox.Show("Could not update due to the following:\n\n" + err); return; }
            Base.Update();
            //Insert Here
            MessageBox.Show("Updating successful");
        }
    }
}