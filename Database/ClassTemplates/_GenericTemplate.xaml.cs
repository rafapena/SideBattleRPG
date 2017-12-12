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

namespace Database.ClassTemplates
{
    /// <summary>
    /// Interaction logic for _GenericTemplate.xaml
    /// </summary>
    public partial class _GenericTemplate : UserControl
    {
        public SQLiteParameter[] Inputs;

        public int _GenericTemplateId { get; set; }

        public _GenericTemplate()
        {
            InitializeComponent();
        }

        public void InitializeNew()
        {
            SQLDB.CurrentId = 0;
            _GenericTemplateId = 0;
            // Insert Here
        }

        public void Automate()
        {
            // Insert Here
        }

        public string ValidateInputs()
        {
            SQLDB.AddParameters(new SQLiteParameter[] { });
            string err = "";
            // Insert Here
            return err;
        }

        public void Create()
        {
            SQLDB.Command(
                "INSERT INTO _GenericTemplates () VALUES ();"
                // Insert Here
            );
        }

        public void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM _GenericTemplates WHERE " + SQLDB.CurrentTable + "ID = " + SQLDB.CurrentId.ToString(), conn))
                {
                    _GenericTemplateId = reader.GetInt32(0);
                    // Insert Here
                }
                conn.Close();
            }
        }

        public void Update()
        {
            SQLDB.Command(
                "UPDATE _GenericTemplates SET WHERE _GenericTemplate_ID = " + _GenericTemplateId + ";"
                // Insert Here
            );
        }

        public void Delete()
        {
            SQLDB.Command("DELETE FROM _GenericTemplates WHERE _GenericTemplate_ID = " + _GenericTemplateId + ";");
        }

        public void Copy()
        {
            Create();
        }
    }
}
