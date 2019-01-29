using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.ClassTemplates
{
    public partial class BaseObject : _ClassTemplateOperations
    {
        public BaseObject()
        {
            InitializeComponent();
            ClassTemplateTable = "BaseObject";
        }

        protected override void SetupTableData() { }

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
            if (!Utils.InRequiredLength(Utils.CutSpaces(NameInput.Text), 30)) err += "Name must have between 1 and 30 characters\n";
            if (Utils.CutSpaces(DescriptionInput.Text) == "") DescriptionInput.Text = "N/A";
            int dLen = DescriptionInput.Text.Length;
            if (dLen > 2500) err += "Description length must have 2500 characters or less (Input has " + dLen + " characters)";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@Name", NameInput.Text);
            SQLDB.ParameterizeAttribute("@Description", DescriptionInput.Text);
            SQLDB.ParameterizeBlobAttribute("@Image", ImageManager.ImageToBytes(ImageInput.Source), (int)ImageInput.Width * (int)ImageInput.Height);
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
            BitmapImage img = ImageManager.SelectImage((int)ImageInput.Width, (int)ImageInput.Height);
            if (img != null) ImageInput.Source = img;
        }
    }
}
