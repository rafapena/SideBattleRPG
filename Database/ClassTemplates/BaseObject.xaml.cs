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
            DescriptionInput.Text = "N/A";
            CreatedText.Text = "";
            UpdatedText.Text = "";
            ImageInput.Source = null;
        }

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.InRequiredLength(Utils.CutSpaces(NameInput.Text))) err += "Name must have 1 to 16 characters\n";
            if (Utils.CutSpaces(DescriptionInput.Text) == "") DescriptionInput.Text = "N/A";
            int dLen = DescriptionInput.Text.Length;
            if (dLen > 2500) err += "Description length must be 2500 characters or less (Input has " + dLen + " characters)";
            return err;
        }

        public override void ParameterizeInputs()
        {
            ParameterizeInput("@Name", NameInput.Text);
            ParameterizeInput("@Description", DescriptionInput.Text);
            SQLDB.ParameterizeImageInput("@Image", ImageManager.ImageToBytes(ImageInput.Source), (int)ImageInput.Width, (int)ImageInput.Height);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            CreatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", DateTime.Now);
            UpdatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", DateTime.Now);
            return new string[] { "Name, Description, Image", "@Name, @Description, @Image" };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            ClassTemplateId = reader.GetInt32(0);
            NameInput.Text = reader.GetString(1);
            DescriptionInput.Text = reader.GetString(2);
            ImageInput.Source = ImageManager.BytesToImage(ImageManager.BlobToBytes(reader, 3));
            CreatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", reader.GetDateTime(4));
            UpdatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", reader.GetDateTime(5));
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            UpdatedText.Text = string.Format("{0:MM-dd-yyyy H:mm}", DateTime.Now);
            return "Name = @Name, Description = @Description, Image = @Image";
        }

        private void SelectImage(object sender, EventArgs e)
        {
            ImageInput.Source = ImageManager.SelectImage((int)ImageInput.Width, (int)ImageInput.Height);
        }
    }
}
