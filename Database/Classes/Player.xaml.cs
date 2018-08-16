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
            SkillChoices.SetupTableData("Skill", "Players_To_Skills", "Skill Set", new List<string> { "Skill", "Level" });
            StateRates.SetupTableData("State", "Players_To_States", "State Rates", new List<string> { "State", "%" });
            ElementRates.SetupTableData("Elements", "TypesLists", "Element Rates", new List<string> { "Element", "%" });
            SkillChoices.InputAttributeName = "LevelRequired";
            StateRates.InputAttributeName = "Vulnerability";
            ElementRates.InputAttributeName = "ElementRates";
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
            SkillChoices.Automate();
            StateRates.Automate();
            ElementRates.Automate();
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += NatStats.ValidateInputs();
            err += ClassChoices.ValidateInputs();
            err += SkillChoices.ValidateInputs();
            err += StateRates.ValidateInputs();
            err += ElementRates.ValidateInputs();
            return err;
        }

        public override void ParameterizeInputs() { }

        protected override void OnCreate()
        {
            Base.Create();
            NatStats.Create();
            ElementRates.Create();
            SQLCreate(
                "BaseObjectID, NaturalStats, ElementRates",
                Base.ClassTemplateId.ToString() + ", " + NatStats.ClassTemplateId.ToString() + ", '" + ElementRates.StringList + "'");
            ClassChoices.Create();
            SkillChoices.Create();
            StateRates.Create();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            SetupTables();
            Base.Read(reader);
            NatStats.Read(reader);
            ClassChoices.Read();
            SkillChoices.Read();
            StateRates.Read();
            ElementRates.Read();
        }

        protected override void OnUpdate()
        {
            Base.Update();
            NatStats.Update();
            ClassChoices.Update();
            SkillChoices.Update();
            StateRates.Update();
            ElementRates.Update();
        }

        protected override void OnDelete()
        {
            Base.Delete();
            NatStats.Delete();
            ClassChoices.Delete();
            SkillChoices.Delete();
            StateRates.Delete();
            ElementRates.Delete();
        }

        protected override void OnClone()
        {
            Base.Clone();
            NatStats.Clone();
            ClassChoices.Clone();
            SkillChoices.Clone();
            StateRates.Clone();
            ElementRates.Clone();
        }
    }
}