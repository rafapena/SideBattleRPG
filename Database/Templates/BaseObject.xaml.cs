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
using Database.Utilities;

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for BaseObject.xaml
    /// </summary>
    public partial class BaseObject : UserControl, ObjectOperations
    {
        public int Id { get; set; }

        public BaseObject()
        {
            InitializeComponent();
        }

        public void InitializeNew()
        {
            Id = 0;
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
            Utilities.SQLDB.Command(
                "INSERT INTO BasObjects (Name, Description) VALUES (" +
                 NameInput.Text + ", " +
                 DescriptionInput.Text + ");"
            );
        }

        public void Read(int id)
        {
            Id = 0;
            NameInput.Text = "";
            DescriptionInput.Text = "";
            CreatedText.Text = "";
            UpdatedText.Text = string.Format("{0:d}", UpdatedText.Text);
            ImageInput.Source = null;
        }

        public void Update()
        {
            Utilities.SQLDB.Command(
                "UPDATE BaseObjects SET " +
                "Name = " + NameInput.Text + ", " +
                "Description = " + DescriptionInput.Text + " " +
                "WHERE BaseObject_ID = " + Id + ")"
            );
        }

        public void Delete()
        {

        }

        public void Copy()
        {

        }
    }
}
