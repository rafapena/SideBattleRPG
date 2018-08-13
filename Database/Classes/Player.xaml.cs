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
    public partial class Player : _ClassOperations
    {
        public Player()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected void SetupTables()
        {
            ClassChoices.SetupTableData("Class", "Players_To_Classes", "Possible Classes", new List<string> { "Class" });
            //Rates.InitializeNew();
        }

        protected override void OnInitializeNew()
        {
            SetupTables();
            Base.InitializeNew();
            NatStats.InitializeNew();
            NatStats.CustomName = "NaturalStats";
        }

        public override void Automate()
        {
            Base.Automate();
            NatStats.Automate();
            ClassChoices.Automate();
            //Rates.Automate();
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += NatStats.ValidateInputs();
            err += ClassChoices.ValidateInputs();
            //err += Rates.ValidateInputs();
            return err;
        }

        public override void ParameterizeInputs() { }

        protected override void OnCreate()
        {
            Base.Create();
            NatStats.Create();
            SQLCreate("BaseObjectID, NaturalStats", Base.ClassTemplateId.ToString() + ", " + NatStats.ClassTemplateId.ToString());
            ClassChoices.Create();
            //Rates.Create();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            SetupTables();
            Base.Read(reader);
            NatStats.Read(reader);
            ClassChoices.Read();
            //Rates.Read(reader);
        }

        protected override void OnUpdate()
        {
            Base.Update();
            NatStats.Update();
            ClassChoices.Update();
            //Rates.Update();
        }

        protected override void OnDelete()
        {
            Base.Delete();
            NatStats.Delete();
            ClassChoices.Delete();
            //Rates.Delete();
        }

        protected override void OnClone()
        {
            Base.Clone();
            NatStats.Clone();
            ClassChoices.Clone();
            //Rates.Clone();
        }
    }
}