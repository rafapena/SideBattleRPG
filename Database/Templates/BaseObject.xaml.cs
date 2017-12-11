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

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for BaseObject.xaml
    /// </summary>
    public partial class BaseObject : UserControl, ObjectOperations
    {
        public int BaseObjectId { get; set; }

        public BaseObject()
        {
            InitializeComponent();
        }

        public void InitializeNew()
        {
            SQLDB.CurrentId = 0;
            BaseObjectId = 0;
            NameInput.Text = "";
            DescriptionInput.Text = "";
            CreatedText.Text = "";
            UpdatedText.Text = "";
            ImageInput.Source = null;
        }

        public void Automate()
        {
            NameInput.Text = "Generic Name";
            DescriptionInput.Text = "Description";
        }

        public string GetErrors()
        {
            string err = "";
            if (Utils.InRequiredLength(Utils.CutSpaces(NameInput.Text))) err += "Name must have 1 to 16 characters";
            if (DescriptionInput.Text == "") DescriptionInput.Text = "N/A";
            return err;
        }

        public void Create()
        {
            SQLDB.Command(
                "INSERT INTO BasObjects (Name, Description) VALUES (" +
                 NameInput.Text + ", " +
                 DescriptionInput.Text + ");"
            );
        }

        public void Read()
        {
            SQLDB.db.Open();
            SQLiteCommand command = new SQLiteCommand(
                "SELECT * FROM BaseObjects WHERE " + SQLDB.CurrentTable + "ID = " + SQLDB.CurrentId.ToString(),
                SQLDB.db);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            BaseObjectId = reader.GetInt32(0);
            NameInput.Text = reader.GetString(1);
            DescriptionInput.Text = reader.GetString(2);
            ImageInput.Source = null; //reader.GetBlob(3, false);
            CreatedText.Text = string.Format("{0:d}", reader.GetDateTime(4));
            UpdatedText.Text = string.Format("{0:d}", reader.GetDateTime(5));
            SQLDB.db.Close();
        }

        public void Update()
        {
            SQLDB.Command(
                "UPDATE BaseObjects SET " +
                "Name = " + NameInput.Text + ", " +
                "Description = " + DescriptionInput.Text + " " +
                "WHERE BaseObject_ID = " + BaseObjectId + ")"
            );
        }

        public void Delete()
        {
            SQLDB.Command(
                "DELETE FROM BaseObjects " +
                "WHERE BaseObject_ID = " + BaseObjectId + ")"
            );
        }

        public void Copy()
        {
            Create();
        }
    }
}
