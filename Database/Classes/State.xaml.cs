using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class State : _ClassOperations
    {
        public State()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData() { }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            PassiveEffectAttributes.InitializeNew();
            PassiveEffectRates.InitializeNew();
            StatMods.InitializeNew(100);
            StatMods.HostTableAttributeName = "StatModifiers";
            MaxStackInput.Text = "1";
            StepsToRemoveInput.Text = "0";
            SpreadOnContactInput.IsChecked = false;
            StunInput.IsChecked = false;
            PetrifyInput.IsChecked = false;
            KOInput.IsChecked = false;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += PassiveEffectAttributes.ValidateInputs();
            err += PassiveEffectRates.ValidateInputs();
            err += StatMods.ValidateInputs(0, 1000);
            if (!Utils.PosInt(MaxStackInput.Text)) err += "Max Stack must be a positive integer\n";
            if (!Utils.PosInt(StepsToRemoveInput.Text)) err += "Steps to Remove must be a positive integer\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@PassiveEffectID", PassiveEffectAttributes.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@StatModifiers", StatMods.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@MaxStack", MaxStackInput.Text);
            SQLDB.ParameterizeAttribute("@StepsToRemove", StepsToRemoveInput.Text);
            SQLDB.ParameterizeAttribute("@SpreadOnContact", (bool)SpreadOnContactInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@Stun", (bool)StunInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@Petrify", (bool)PetrifyInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@KO", (bool)KOInput.IsChecked ? 1 : 0);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            PassiveEffectAttributes.Create(conn);
            PassiveEffectRates.Create(conn);
            StatMods.Create(conn);
            SQLCreate(conn, "BaseObjectID, PassiveEffectID, StatModifiers, MaxStack, StepsToRemove, SpreadOnContact, Stun, Petrify, KO",
                "@BaseObjectID, @PassiveEffectID, @StatModifiers, @MaxStack, @StepsToRemove, @SpreadOnContact, @Stun, @Petrify, @KO");
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            PassiveEffectAttributes.Read(reader);
            PassiveEffectRates.Read(reader);
            StatMods.Read(reader);
            MaxStackInput.Text = reader["MaxStack"].ToString();
            StepsToRemoveInput.Text = reader["StepsToRemove"].ToString();
            SpreadOnContactInput.IsChecked = reader["SpreadOnContact"].ToString() == "True" ? true : false; ;
            StunInput.IsChecked = reader["Stun"].ToString() == "True" ? true : false; ;
            PetrifyInput.IsChecked = reader["Petrify"].ToString() == "True" ? true : false; ;
            KOInput.IsChecked = reader["KO"].ToString() == "True" ? true : false; ;
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            PassiveEffectAttributes.Update(conn);
            PassiveEffectRates.Update(conn);
            StatMods.Update(conn);
            SQLUpdate(conn, "MaxStack = @MaxStack, StepsToRemove = @StepsToRemove, SpreadOnContact = @SpreadOnContact, Stun = @Stun, Petrify = @Petrify, KO = @KO");
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