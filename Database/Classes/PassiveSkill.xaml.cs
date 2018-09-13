using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class PassiveSkill : _ClassOperations
    {
        private ComboBoxInputData StateActive1Data, StateActive2Data, StateInactive1Data, StateInactive2Data;

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
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            PassiveEffectAttributes.InitializeNew();
            PassiveEffectRates.InitializeNew();
            StatMods.InitializeNew(100);
            StatMods.HostTableAttributeName = "StatModifiers";
            HPMinInput.Text = "0";
            HPMaxInput.Text = "100";
            SPMinInput.Text = "0";
            SPMaxInput.Text = "100";
            AnyStateInput.IsChecked = false;
            NoStateInput.IsChecked = false;
            StateActive1Input.ItemsSource = StateActive1Data.OptionsListNames;
            StateActive2Input.ItemsSource = StateActive2Data.OptionsListNames;
            StateInactive1Input.ItemsSource = StateInactive1Data.OptionsListNames;
            StateInactive2Input.ItemsSource = StateInactive2Data.OptionsListNames;
            ExpGainRateInput.Text = "100";
            GoldGainRateInput.Text = "100";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += PassiveEffectAttributes.ValidateInputs();
            err += PassiveEffectRates.ValidateInputs();
            err += StatMods.ValidateInputs(0, 1000);
            if (!Utils.PosInt(HPMinInput.Text)) err += "HP Min % must be a positive integer\n";
            if (!Utils.PosInt(HPMaxInput.Text)) err += "HP Max % must be a positive integer\n";
            if (!Utils.PosInt(SPMinInput.Text)) err += "SP Min % must be a positive integer\n";
            if (!Utils.PosInt(SPMaxInput.Text)) err += "SP Max % must be a positive integer\n";
            if (!Utils.PosInt(ExpGainRateInput.Text)) err += "EXP Gain % must be a positive integer\n";
            if (!Utils.PosInt(GoldGainRateInput.Text)) err += "Gold Gain % must be a positive integer\n";
            return err;
        }

        public override void ParameterizeInputs()
        {
            //SQLDB.ParameterizeInput("@MaxStack", MaxStackInput.Text);
            //SQLDB.ParameterizeInput("@StepsToRemove", StepsToRemoveInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            PassiveEffectAttributes.Create(conn);
            PassiveEffectRates.Create(conn);
            StatMods.Create(conn);
            //SQLCreate(conn, "MaxStack, StepsToRemove, Stun, Petrify, KO, PassiveEffectID, StatModifiers, BaseObjectID",
              //  "@MaxStack, @StepsToRemove, " + ((bool)StunInput.IsChecked ? 1:0) + ", " + ((bool)PetrifyInput.IsChecked ? 1:0) + ", " + ((bool)KOInput.IsChecked ? 1:0) + ", " +
                //PassiveEffectAttributes.ClassTemplateId + ", " + StatMods.ClassTemplateId + ", " + Base.ClassTemplateId);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            PassiveEffectAttributes.Read(reader);
            PassiveEffectRates.Read(reader);
            StatMods.Read(reader);
            //MaxStackInput.Text = reader["MaxStack"].ToString();
            //StepsToRemoveInput.Text = reader["StepsToRemove"].ToString();
            //StunInput.IsChecked = reader["Stun"].ToString() == "True" ? true : false; ;
            //PetrifyInput.IsChecked = reader["Petrify"].ToString() == "True" ? true : false; ;
            //KOInput.IsChecked = reader["KO"].ToString() == "True" ? true : false; ;
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            PassiveEffectAttributes.Update(conn);
            PassiveEffectRates.Update(conn);
            StatMods.Update(conn);
            //SQLUpdate(conn, "MaxStack = @MaxStack, StepsToRemove = @StepsToRemove, " +
              //  "Stun = " + ((bool)StunInput.IsChecked ? 1:0) + ", Petrify = " + ((bool)PetrifyInput.IsChecked ? 1:0) + ", KO = " + ((bool)KOInput.IsChecked ? 1:0));
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