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
    /// Interaction logic for BaseObject.xaml
    /// </summary>
    public partial class BaseObject : _ClassTemplateOperations
    {
        public BaseObject()
        {
            InitializeComponent();
            ClassTemplateTable = "BaseObjects";
            ClassTemplateType = "BaseObject";
        }

        protected override void OnInitializeNew()
        {
            NameInput.Text = "";
            DescriptionInput.Text = "";
            CreatedText.Text = "";
            UpdatedText.Text = "";
            ImageInput.Source = null;
        }

        public override void Automate()
        {
            NameInput.Text = "Generic Name";
            DescriptionInput.Text = "Description";
        }

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.InRequiredLength(Utils.CutSpaces(NameInput.Text))) err += "Name must have 1 to 16 characters\n";
            if (Utils.CutSpaces(DescriptionInput.Text) == "") DescriptionInput.Text = "N/A";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.Inputs = new SQLiteParameter[]
            {
                new SQLiteParameter("@Name", NameInput.Text),
                new SQLiteParameter("@Description", DescriptionInput.Text)
            };
        }

        protected override string[] OnCreate()
        {
            CreatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", DateTime.Now);
            UpdatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", DateTime.Now);
            return new string[] { "Name, Description", "@Name, @Description" };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            ClassTemplateId = reader.GetInt32(0);
            NameInput.Text = reader.GetString(1);
            DescriptionInput.Text = reader.GetString(2);
            ImageInput.Source = null; //reader.GetBlob(3, false);
            CreatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", reader.GetDateTime(4));
            UpdatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", reader.GetDateTime(5));
        }

        protected override string OnUpdate()
        {
            UpdatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", DateTime.Now);
            return "Name = @Name, Description = @Description";
        }

        public new void Clone()
        {
            base.Clone();
            CreatedText.Text = "";
            UpdatedText.Text = "";
        }
    }
}
