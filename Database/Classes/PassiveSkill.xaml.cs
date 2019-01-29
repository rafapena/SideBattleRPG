using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class PassiveSkill : _ClassOperations
    {
        private ComboBoxInputData StateActive1Data, StateActive2Data, StateInactive1Data, StateInactive2Data;
        private List<string> AllyConditionOptions = new List<string> { "None", "No allies", "One ally", "All allies" };
        private List<string> FoeConditionOptions = new List<string> { "None", "No foes", "One foe", "All foes" };
        private List<string> UserConditionOptions = new List<string> { "None", "Cannot meet", "Must meet" };

        public PassiveSkill()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            StateActive1Data = new ComboBoxInputData("State_ID", "Name", "BaseObject JOIN State", "BaseObject_ID = BaseObjectID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            StateActive2Data = new ComboBoxInputData("State_ID", "Name", "BaseObject JOIN State", "BaseObject_ID = BaseObjectID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            StateInactive1Data = new ComboBoxInputData("State_ID", "Name", "BaseObject JOIN State", "BaseObject_ID = BaseObjectID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            StateInactive2Data = new ComboBoxInputData("State_ID", "Name", "BaseObject JOIN State", "BaseObject_ID = BaseObjectID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            StateActive1Input.ItemsSource = StateActive1Data.OptionsListNames;
            StateActive2Input.ItemsSource = StateActive2Data.OptionsListNames;
            StateInactive1Input.ItemsSource = StateInactive1Data.OptionsListNames;
            StateInactive2Input.ItemsSource = StateInactive2Data.OptionsListNames;
            AllyConditionInput.ItemsSource = AllyConditionOptions;
            FoeConditionInput.ItemsSource = FoeConditionOptions;
            UserConditionInput.ItemsSource = UserConditionOptions;
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            PassiveEffectAttributes.InitializeNew();
            PassiveEffectRates.InitializeNew();
            StatMods.InitializeNew(100, "StatModifiers");
            HPMinInput.Text = "0";
            HPMaxInput.Text = "100";
            SPMinInput.Text = "0";
            SPMaxInput.Text = "100";
            AnyStateInput.IsChecked = false;
            NoStateInput.IsChecked = false;
            StateActive1Input.SelectedIndex = 0;
            StateActive2Input.SelectedIndex = 0;
            StateInactive1Input.SelectedIndex = 0;
            StateInactive2Input.SelectedIndex = 0;
            ExpGainRateInput.Text = "100";
            GoldGainRateInput.Text = "100";
            AllyConditionInput.SelectedIndex = 0;
            FoeConditionInput.SelectedIndex = 0;
            UserConditionInput.SelectedIndex = 2;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += PassiveEffectAttributes.ValidateInputs();
            err += PassiveEffectRates.ValidateInputs();
            err += StatMods.ValidateInputs(0, 1000);
            if (!Utils.PosInt(HPMinInput.Text, 100)) err += "HP Min % must be an integer between 0 and 100\n";
            if (!Utils.PosInt(HPMaxInput.Text, 100)) err += "HP Max % must be an integer between 0 and 100\n";
            if (!Utils.PosInt(SPMinInput.Text, 100)) err += "SP Min % must be an integer between 0 and 100\n";
            if (!Utils.PosInt(SPMaxInput.Text, 100)) err += "SP Max % must be an integer between 0 and 100\n";
            bool identical = false;
            int s1 = StateActive1Input.SelectedIndex;
            int s2 = StateActive2Input.SelectedIndex;
            int s3 = StateInactive1Input.SelectedIndex;
            int s4 = StateInactive2Input.SelectedIndex;
            if (s1 != 0 && (s1 == s2 || s1 == s3 || s1 == s4)) identical = true;
            if (s2 != 0 && (s2 == s3 || s2 == s4)) identical = true;
            if (s3 != 0 && (s3 == s4)) identical = true;
            if (identical) err += "All State Active/Inactive inputs must be unique from each other\n";
            if (!Utils.PosInt(ExpGainRateInput.Text)) err += "EXP Gain % must be a positive integer\n";
            if (!Utils.PosInt(GoldGainRateInput.Text)) err += "Gold Gain % must be a positive integer\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@PassiveEffectID", PassiveEffectAttributes.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@StatModifiers", StatMods.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@HPMin", HPMinInput.Text);
            SQLDB.ParameterizeAttribute("@HPMax", HPMaxInput.Text);
            SQLDB.ParameterizeAttribute("@SPMin", SPMinInput.Text);
            SQLDB.ParameterizeAttribute("@SPMax", SPMaxInput.Text);
            SQLDB.ParameterizeAttribute("@AnyState", (bool)AnyStateInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@NoState", (bool)NoStateInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@StateActive1", StateActive1Data.SelectedInput(StateActive1Input));
            SQLDB.ParameterizeAttribute("@StateActive2", StateActive2Data.SelectedInput(StateActive2Input));
            SQLDB.ParameterizeAttribute("@StateInactive1", StateInactive1Data.SelectedInput(StateInactive1Input));
            SQLDB.ParameterizeAttribute("@StateInactive2", StateInactive2Data.SelectedInput(StateInactive2Input));
            SQLDB.ParameterizeAttribute("@ExpGainRate", ExpGainRateInput.Text);
            SQLDB.ParameterizeAttribute("@GoldGainRate", GoldGainRateInput.Text);
            SQLDB.ParameterizeAttribute("@AllyCondition", AllyConditionInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@FoeCondition", FoeConditionInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@UserCondition", UserConditionInput.SelectedIndex.ToString());
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            PassiveEffectAttributes.Create(conn);
            PassiveEffectRates.Create(conn);
            StatMods.Create(conn);
            SQLCreate(conn, "BaseObjectID, PassiveEffectID, StatModifiers, HPMin, HPMax, SPMin, SPMax, AnyState, NoState, " +
                "StateActive1, StateActive2, StateInactive1, StateInactive2, ExpGainRate, GoldGainRate, AllyCondition, FoeCondition, UserCondition",
                "@BaseObjectID, @PassiveEffectID, @StatModifiers, @HPMin, @HPMax, @SPMin, @SPMax, @AnyState, @NoState, " +
                "@StateActive1, @StateActive2, @StateInactive1, @StateInactive2, @ExpGainRate, @GoldGainRate, @AllyCondition, @FoeCondition, @UserCondition");
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            PassiveEffectAttributes.Read(reader);
            PassiveEffectRates.Read(reader);
            StatMods.Read(reader);
            HPMinInput.Text = reader["HPMin"].ToString();
            HPMaxInput.Text = reader["HPMax"].ToString();
            SPMinInput.Text = reader["SPMin"].ToString();
            SPMaxInput.Text = reader["SPMax"].ToString();
            AnyStateInput.IsChecked = reader["AnyState"].ToString() == "True" ? true : false;
            NoStateInput.IsChecked = reader["NoState"].ToString() == "True" ? true : false;
            StateActive1Input.SelectedIndex = StateActive1Data.FindIndex(reader["StateActive1"]);
            StateActive2Input.SelectedIndex = StateActive2Data.FindIndex(reader["StateActive2"]);
            StateInactive1Input.SelectedIndex = StateInactive1Data.FindIndex(reader["StateInactive1"]);
            StateInactive2Input.SelectedIndex = StateInactive2Data.FindIndex(reader["StateInactive2"]);
            ExpGainRateInput.Text = reader["ExpGainRate"].ToString();
            GoldGainRateInput.Text = reader["GoldGainRate"].ToString();
            AllyConditionInput.SelectedIndex = int.Parse(reader["AllyCondition"].ToString());
            FoeConditionInput.SelectedIndex = int.Parse(reader["FoeCondition"].ToString());
            UserConditionInput.SelectedIndex = int.Parse(reader["UserCondition"].ToString());
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            PassiveEffectAttributes.Update(conn);
            PassiveEffectRates.Update(conn);
            StatMods.Update(conn);
            SQLUpdate(conn, "BaseObjectID=@BaseObjectID, PassiveEffectID=@PassiveEffectID, StatModifiers=@StatModifiers, HPMin=@HPMin, HPMax=@HPMax, " +
                "SPMin=@SPMin, SPMax=@SPMax, AnyState=@AnyState, NoState=@NoState, StateActive1=@StateActive1, StateActive2=@StateActive2, " +
                "StateInactive1=@StateInactive1, StateInactive2=@StateInactive2, ExpGainRate=@ExpGainRate, GoldGainRate=@GoldGainRate, " +
                "AllyCondition=@AllyCondition, FoeCondition=@FoeCondition, UserCondition=@UserCondition");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            PassiveEffectAttributes.Delete(conn);
            PassiveEffectRates.Delete(conn);
            StatMods.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            PassiveEffectAttributes.Clone(conn);
            PassiveEffectRates.Clone(conn);
            StatMods.Clone(conn);
        }
    }
}