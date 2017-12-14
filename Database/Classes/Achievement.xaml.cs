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
    /// Interaction logic for Achievement.xaml
    /// </summary>
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
            HiddenMessageInput.Text = "";
        }

        public override void Automate()
        {
            Base.Automate();
            LevelInput.Text = "1";
            HiddenMessageInput.Text = "Insert Text Here";
        }

        public override string ValidateInputs()
        {
            SQLDB.AddParameters(new SQLiteParameter[] {
                new SQLiteParameter("@Level", LevelInput.Text),
                new SQLiteParameter("@HiddenMessage", HiddenMessageInput.Text)
            });
            string err = Base.ValidateInputs();
            if (!Utils.PosInt(LevelInput.Text)) err += "Level must be a positive integer\n";
            if (Utils.CutSpaces(HiddenMessageInput.Text) == "") HiddenMessageInput.Text = "N/A";
            return err;
        }

        protected override void OnCreate()
        {
            SQLCreate(new string[]{ "Level, HiddenMessage", "@Level, @HiddenMessage" });
            Base.Create();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read();
            LevelInput.Text = reader.GetString(1);
            HiddenMessageInput.Text = reader.GetString(2);
        }

        protected override void OnUpdate()
        {
            SQLUpdate("Level = @Level, HiddenMessage = @HiddenMessage");
            Base.Update();
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