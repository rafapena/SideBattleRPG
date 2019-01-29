using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.ClassTemplates
{
    public partial class PassiveEffectAttributes: _ClassTemplateOperations
    {
        private ComboBoxInputData DisabledToolType1Data, DisabledToolType2Data;
        private List<string> TurnSequenceOptions = new List<string> { "Before Action", "After Action", "After Turn" };

        public PassiveEffectAttributes()
        {
            InitializeComponent();
            ClassTemplateTable = "PassiveEffect";
        }

        protected override void SetupTableData()
        {
            DisabledToolType1Data = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Tool Types'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            DisabledToolType2Data = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Tool Types'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            DisabledToolType1Input.ItemsSource = DisabledToolType1Data.OptionsListNames;
            DisabledToolType2Input.ItemsSource = DisabledToolType2Data.OptionsListNames;
            TurnSequenceInput.ItemsSource = TurnSequenceOptions;
        }

        protected override void OnInitializeNew()
        {
            HPRegenInput.Text = "0";
            SPRegenInput.Text = "0";
            SPConsumeRateInput.Text = "100";
            ComboDifficultyInput.Text = "100";
            CounterInput.Text = "0";
            ReflectInput.Text = "0";
            DisabledToolType1Input.SelectedIndex = 0;
            DisabledToolType2Input.SelectedIndex = 0;
            ExtraTurnsInput.Text = "0";
            TurnEnd1Input.Text = "0";
            TurnEnd2Input.Text = "0";
            TurnSequenceInput.SelectedIndex = 0;
            RemoveByHitInput.Text = "0";
        }

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.NumberBetween(HPRegenInput.Text, -5000, 5000)) err += "HP Regenerate must be a number between -5000 and 5000\n";
            if (!Utils.NumberBetween(SPRegenInput.Text, -100, 100)) err += "SP Regenerate must be a number between -100 and 100\n";
            if (!Utils.PosInt(SPConsumeRateInput.Text, 1000)) err += "SP Consume Rate must be an integer between 0 and 1000";
            if (!Utils.PosInt(ComboDifficultyInput.Text, 1000)) err += "Combo Difficulty must be an integer between 0 and 1000";
            if (!Utils.PosInt(CounterInput.Text, 1000)) err += "Counter Rate must be an integer between 0 and 1000";
            if (!Utils.PosInt(ReflectInput.Text, 1000)) err += "ReflectRate Difficulty must be an integer between 0 and 1000";
            if (DisabledToolType1Input.SelectedIndex == DisabledToolType2Input.SelectedIndex && DisabledToolType1Input.SelectedIndex != 0) err += "Disabled Tool Types cannot be the same\n";
            if (!Utils.PosInt(ExtraTurnsInput.Text, 10)) err += "Extra Turns must be an integer between 0 and 10";
            if (!Utils.PosInt(TurnEnd1Input.Text)) err += "Turn End 1 must be a positive integer";
            if (!Utils.PosInt(TurnEnd2Input.Text)) err += "Turn End 2 must be a positive integer";
            if (!Utils.PosInt(RemoveByHitInput.Text, 100)) err += "Remove by Hit % must be a positive integer between 0 and 100";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@HPRegen", HPRegenInput.Text);
            SQLDB.ParameterizeAttribute("@SPRegen", SPRegenInput.Text);
            SQLDB.ParameterizeAttribute("@SPConsumeRate", SPConsumeRateInput.Text);
            SQLDB.ParameterizeAttribute("@ComboDifficulty", ComboDifficultyInput.Text);
            SQLDB.ParameterizeAttribute("@Counter", CounterInput.Text);
            SQLDB.ParameterizeAttribute("@Reflect", ReflectInput.Text);
            SQLDB.ParameterizeAttribute("@DisabledToolType1", DisabledToolType1Data.SelectedInput(DisabledToolType1Input));
            SQLDB.ParameterizeAttribute("@DisabledToolType2", DisabledToolType2Data.SelectedInput(DisabledToolType2Input));
            SQLDB.ParameterizeAttribute("@ExtraTurns", ExtraTurnsInput.Text);
            SQLDB.ParameterizeAttribute("@TurnEnd1", TurnEnd1Input.Text);
            SQLDB.ParameterizeAttribute("@TurnEnd2", TurnEnd2Input.Text);
            SQLDB.ParameterizeAttribute("@TurnSequence", TurnSequenceInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@RemoveByHit", RemoveByHitInput.Text);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            return new string[] {
                "HPRegen, SPRegen, SPConsumeRate, ComboDifficulty, Counter, Reflect, DisabledToolType1, DisabledToolType2, ExtraTurns, " +
                "TurnEnd1, TurnEnd2, TurnSequence, RemoveByHit",
                "@HPRegen, @SPRegen, @SPConsumeRate, @ComboDifficulty, @Counter, @Reflect, @DisabledToolType1, @DisabledToolType2, @ExtraTurns, " +
                "@TurnEnd1, @TurnEnd2, @TurnSequence, @RemoveByHit"
            };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            HPRegenInput.Text = reader["HPRegen"].ToString();
            SPRegenInput.Text = reader["SPRegen"].ToString();
            SPConsumeRateInput.Text = reader["SPConsumeRate"].ToString();
            ComboDifficultyInput.Text = reader["ComboDifficulty"].ToString();
            CounterInput.Text = reader["Counter"].ToString();
            ReflectInput.Text = reader["Reflect"].ToString();
            DisabledToolType1Input.SelectedIndex = DisabledToolType1Data.FindIndex(reader["DisabledToolType1"]);
            DisabledToolType2Input.SelectedIndex = DisabledToolType2Data.FindIndex(reader["DisabledToolType2"]);
            ExtraTurnsInput.Text = reader["ExtraTurns"].ToString();
            TurnEnd1Input.Text = reader["TurnEnd1"].ToString();
            TurnEnd2Input.Text = reader["TurnEnd2"].ToString();
            TurnSequenceInput.SelectedIndex = int.Parse(reader["TurnSequence"].ToString());
            RemoveByHitInput.Text = reader["RemoveByHit"].ToString();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            return "HPRegen=@HPRegen, SPRegen=@SPRegen, SPConsumeRate=@SPConsumeRate, ComboDifficulty=@ComboDifficulty, Counter=@Counter, Reflect=@Reflect, " +
                "DisabledToolType1=@DisabledToolType1, DisabledToolType2=@DisabledToolType2, ExtraTurns=@ExtraTurns, TurnEnd1=@TurnEnd1, TurnEnd2=@TurnEnd2, " +
                "TurnSequence=@TurnSequence, RemoveByHit=@RemoveByHit";
        }
    }
}