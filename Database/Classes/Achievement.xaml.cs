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
    public partial class Achievement : _ClassOperations
    {
        public Achievement()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            LevelInput.Text = "";
            HintInput.Text = "";
        }

        public override void Automate()
        {
            Base.Automate();
            LevelInput.Text = "1";
            HintInput.Text = "Insert Text Here";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            if (!Utils.PosInt(LevelInput.Text)) err += "Level must be a positive integer\n";
            if (Utils.CutSpaces(HintInput.Text) == "") HintInput.Text = "N/A";
            return err;
        }

        public override void ParameterizeInputs()
        {
            ParameterizeInput("@Level", LevelInput.Text);
            ParameterizeInput("@Hint", HintInput.Text);
        }

        protected override void OnCreate()
        {
            Base.Create();
            SQLCreate("Level, Hint, BaseObjectID", "@Level, @Hint, " + Base.ClassTemplateId);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            LevelInput.Text = reader.GetInt32(1).ToString();
            HintInput.Text = reader.GetString(2);
        }

        protected override void OnUpdate()
        {
            Base.Update();
            SQLUpdate("Level = @Level, Hint = @Hint");
        }

        protected override void OnDelete()
        {
            Base.Delete();
        }

        protected override void OnClone()
        {
            Base.Clone();
        }
    }
}