﻿using System.Collections.Generic;
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
            PhysicalDamageRateInput.Text = "100";
            MagicalDamageRateInput.Text = "100";
        }

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.NumberBetween(HPRegenInput.Text, -5000, 5000)) err += "HP Regenerate must be a number within -5000 and 5000\n";
            if (!Utils.NumberBetween(SPRegenInput.Text, -100, 100)) err += "SP Regenerate must be a number within -100 and 100\n";
            if (!Utils.PosInt(SPConsumeRateInput.Text)) err += "SP Consume Rate must be a positive integer";
            if (!Utils.PosInt(ComboDifficultyInput.Text)) err += "Combo Difficulty must be a positive integer";
            if (!Utils.PosInt(CounterInput.Text)) err += "Counter Rate must be a positive integer";
            if (!Utils.PosInt(ReflectInput.Text)) err += "ReflectRate Difficulty must be a positive integer";
            if (DisabledToolType1Input.SelectedIndex == DisabledToolType2Input.SelectedIndex && DisabledToolType1Input.SelectedIndex != 0) err += "Disabled Tool Types cannot be the same\n";
            if (!Utils.PosInt(ExtraTurnsInput.Text)) err += "Extra Turns must be a positive integer";
            if (!Utils.PosInt(TurnEnd1Input.Text)) err += "Turn End 1 must be a positive integer";
            if (!Utils.PosInt(TurnEnd2Input.Text)) err += "Turn End 2 must be a positive integer";
            if (!Utils.PosInt(RemoveByHitInput.Text)) err += "Remove by Hit % must be a positive integer";
            if (!Utils.PosInt(PhysicalDamageRateInput.Text)) err += "Physical Damage Rate must be a positive integer";
            if (!Utils.PosInt(MagicalDamageRateInput.Text)) err += "Magical Damage Rate must be a positive integer";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.ParameterizeInput("@HPRegen", HPRegenInput.Text);
            SQLDB.ParameterizeInput("@SPRegen", SPRegenInput.Text);
            SQLDB.ParameterizeInput("@SPConsumeRate", SPConsumeRateInput.Text);
            SQLDB.ParameterizeInput("@ComboDifficulty", ComboDifficultyInput.Text);
            SQLDB.ParameterizeInput("@Counter", CounterInput.Text);
            SQLDB.ParameterizeInput("@Reflect", ReflectInput.Text);
            SQLDB.ParameterizeInput("@DisabledToolType1", DisabledToolType1Data.SelectedInput(DisabledToolType1Input));
            SQLDB.ParameterizeInput("@DisabledToolType2", DisabledToolType2Data.SelectedInput(DisabledToolType2Input));
            SQLDB.ParameterizeInput("@ExtraTurns", ExtraTurnsInput.Text);
            SQLDB.ParameterizeInput("@TurnEnd1", TurnEnd1Input.Text);
            SQLDB.ParameterizeInput("@TurnEnd2", TurnEnd2Input.Text);
            SQLDB.ParameterizeInput("@TurnSequence", TurnSequenceInput.SelectedIndex.ToString());
            SQLDB.ParameterizeInput("@RemoveByHit", RemoveByHitInput.Text);
            SQLDB.ParameterizeInput("@PhysicalDamageRate", PhysicalDamageRateInput.Text);
            SQLDB.ParameterizeInput("@MagicalDamageRate", MagicalDamageRateInput.Text);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            return new string[] {
                "HPRegen, SPRegen, SPConsumeRate, ComboDifficulty, Counter, Reflect, DisabledToolType1, DisabledToolType2, ExtraTurns, " +
                "TurnEnd1, TurnEnd2, TurnSequence, RemoveByHit, PhysicalDamageRate, MagicalDamageRate",
                "@HPRegen, @SPRegen, @SPConsumeRate, @ComboDifficulty, @Counter, @Reflect, @DisabledToolType1, @DisabledToolType2, @ExtraTurns, " +
                "@TurnEnd1, @TurnEnd2, @TurnSequence, @RemoveByHit, @PhysicalDamageRate, @MagicalDamageRate"
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
            PhysicalDamageRateInput.Text = reader["PhysicalDamageRate"].ToString();
            MagicalDamageRateInput.Text = reader["MagicalDamageRate"].ToString();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            return "HPRegen=@HPRegen, SPRegen=@SPRegen, SPConsumeRate=@SPConsumeRate, ComboDifficulty=@ComboDifficulty, Counter=@Counter, Reflect=@Reflect, " +
                "DisabledToolType1=@DisabledToolType1, DisabledToolType2=@DisabledToolType2, ExtraTurns=@ExtraTurns, TurnEnd1=@TurnEnd1, TurnEnd2=@TurnEnd2, " +
                "TurnSequence=@TurnSequence, RemoveByHit=@RemoveByHit, PhysicalDamageRate=@PhysicalDamageRate, MagicalDamageRate=@MagicalDamageRate";
        }
    }
}