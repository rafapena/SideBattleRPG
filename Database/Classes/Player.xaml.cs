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

        protected override void SetupTableData()
        {
            ClassChoices.Setup("Class", "Classes", "Possible Classes", new List<string> { "Class" });
            SkillChoices.Setup("Skill", "Skills", "Skill Set", new List<string> { "Skill", "Level" });
            StateRates.Setup("State", "States", "State Rates", new List<string> { "State", "%" });
            ElementRates.Setup("Elements", "TypesLists", "Element Rates", new List<string> { "Element", "%" });
            Relations.Setup("Player", "Players", "Compatibilities", new List<string> { "Player", "Comp." });
            SkillChoices.AttributeName = "LevelRequired";
            StateRates.AttributeName = "Vulnerability";
            ElementRates.AttributeName = "ElementRates";
            Relations.AttributeName = "CompanionshipTo";
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            NatStats.InitializeNew();
            NatStats.HostTableAttributeName = "NaturalStats";
            CompanionshipInput.Text = "100";
            SavePartnerRateInput.Text = "100";
            CounterattackRateInput.Text = "100";
            AssistDamageRateInput.Text = "100";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += NatStats.ValidateInputs(-3, 3);
            err += ClassChoices.ValidateInputs();
            err += SkillChoices.ValidateInputs();
            err += StateRates.ValidateInputs();
            err += ElementRates.ValidateInputs();
            err += Relations.ValidateInputs();
            bool relationTabErrored = false;
            if (!Utils.PosInt(CompanionshipInput.Text)) relationTabErrored = true;
            if (!Utils.PosInt(SavePartnerRateInput.Text)) relationTabErrored = true;
            if (!Utils.PosInt(CounterattackRateInput.Text)) relationTabErrored = true;
            if (!Utils.PosInt(AssistDamageRateInput.Text)) relationTabErrored = true;
            if (relationTabErrored) err += "All fillable inputs in 'Relations' must be positive integers\n";
            return err;
        }

        public override void ParameterizeInputs()
        {
            ParameterizeInput("@Companionship", CompanionshipInput.Text);
            ParameterizeInput("@SavePartnerRate", SavePartnerRateInput.Text);
            ParameterizeInput("@CounterattackRate", CounterattackRateInput.Text);
            ParameterizeInput("@AssistDamageRate", AssistDamageRateInput.Text);
        }

        protected override void OnCreate()
        {
            Base.Create();
            NatStats.Create();
            ElementRates.Create();
            SQLCreate(
                "BaseObjectID, NaturalStats, ElementRates, Companionship, SavePartnerRate, CounterattackRate, AssistDamageRate",
                Base.ClassTemplateId.ToString() + ", " + NatStats.ClassTemplateId.ToString() + ", '" + ElementRates.StringList + "', " +
                    "@Companionship, @SavePartnerRate, @CounterattackRate, @AssistDamageRate");
            ClassChoices.Create();
            SkillChoices.Create();
            StateRates.Create();
            Relations.Create();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            NatStats.Read(reader);
            ClassChoices.Read();
            SkillChoices.Read();
            StateRates.Read();
            ElementRates.Read();
            Relations.Read();
            CompanionshipInput.Text = reader["Companionship"].ToString();
            SavePartnerRateInput.Text = reader["SavePartnerRate"].ToString();
            CounterattackRateInput.Text = reader["CounterattackRate"].ToString();
            AssistDamageRateInput.Text = reader["AssistDamageRate"].ToString();
        }

        protected override void OnUpdate()
        {
            Base.Update();
            NatStats.Update();
            ClassChoices.Update();
            SkillChoices.Update();
            StateRates.Update();
            ElementRates.Update();
            Relations.Update();
            SQLUpdate("Companionship = @Companionship, SavePartnerRate = @SavePartnerRate, " +
                "CounterattackRate = @CounterattackRate, AssistDamageRate = @AssistDamageRate");
        }

        protected override void OnDelete()
        {
            Base.Delete();
            NatStats.Delete();
            ClassChoices.Delete();
            SkillChoices.Delete();
            StateRates.Delete();
            ElementRates.Delete();
            Relations.Delete();
        }

        protected override void OnClone()
        {
            Base.Clone();
            NatStats.Clone();
            ClassChoices.Clone();
            SkillChoices.Clone();
            StateRates.Clone();
            ElementRates.Clone();
            Relations.Clone();
        }
    }
}