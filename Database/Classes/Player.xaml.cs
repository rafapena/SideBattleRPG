﻿using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    // PLAYER MUST HAVE AT LEAST ONE BATTLERCLASS

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
            ClassChoices.Setup("Player", "BattlerClass", "Possible Classes", new List<string> { "Class" });
            Skills.Setup("Player", "Skill", "Skill Set", new List<string> { "Skill", "Level" });
            StateRates.Setup("Player", "State", "State Rates", new List<string> { "State", "%" });
            ElementRates.Setup("Player", "TypesLists", "Elements", "Element Rates", new List<string> { "Element", "%" });
            Relations.Setup("Player", "Player", "Compatibilities", new List<string> { "Player", "Comp." });
            Skills.AttributeName = "LevelRequired";
            StateRates.AttributeName = "Vulnerability";
            ElementRates.AttributeName = "ElementRates";
            Relations.AttributeName = "CompanionshipTo";
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            NatStats.InitializeNew("NaturalStats");
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
            if (ClassChoices.Count <= 0) err += "Player must have at least one possible class\n";
            err += Skills.ValidateInputs();
            err += StateRates.ValidateInputs();
            err += ElementRates.ValidateInputs();
            err += Relations.ValidateInputs();
            bool relationTabErrored = false;
            if (!Utils.PosInt(CompanionshipInput.Text, 1000)) relationTabErrored = true;
            if (!Utils.PosInt(SavePartnerRateInput.Text, 1000)) relationTabErrored = true;
            if (!Utils.PosInt(CounterattackRateInput.Text, 1000)) relationTabErrored = true;
            if (!Utils.PosInt(AssistDamageRateInput.Text, 1000)) relationTabErrored = true;
            if (relationTabErrored) err += "All fillable inputs in 'Relations' must be integers between 0 and 1000\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@NaturalStats", NatStats.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@ElementRates", ElementRates.StringList);
            SQLDB.ParameterizeAttribute("@Companionship", CompanionshipInput.Text);
            SQLDB.ParameterizeAttribute("@SavePartnerRate", SavePartnerRateInput.Text);
            SQLDB.ParameterizeAttribute("@CounterattackRate", CounterattackRateInput.Text);
            SQLDB.ParameterizeAttribute("@AssistDamageRate", AssistDamageRateInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            NatStats.Create(conn);
            ElementRates.Create(conn);
            SQLCreate(conn, "BaseObjectID, NaturalStats, ElementRates, Companionship, SavePartnerRate, CounterattackRate, AssistDamageRate",
                "@BaseObjectID, @NaturalStats, @ElementRates, @Companionship, @SavePartnerRate, @CounterattackRate, @AssistDamageRate");
            ClassChoices.Create(conn);
            Skills.Create(conn);
            StateRates.Create(conn);
            Relations.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            NatStats.Read(reader);
            ClassChoices.Read();
            Skills.Read();
            StateRates.Read();
            ElementRates.Read();
            Relations.Read();
            CompanionshipInput.Text = reader["Companionship"].ToString();
            SavePartnerRateInput.Text = reader["SavePartnerRate"].ToString();
            CounterattackRateInput.Text = reader["CounterattackRate"].ToString();
            AssistDamageRateInput.Text = reader["AssistDamageRate"].ToString();
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            NatStats.Update(conn);
            ClassChoices.Update(conn);
            Skills.Update(conn);
            StateRates.Update(conn);
            ElementRates.Update(conn);
            Relations.Update(conn);
            SQLUpdate(conn, "Companionship=@Companionship, SavePartnerRate=@SavePartnerRate, CounterattackRate=@CounterattackRate, AssistDamageRate=@AssistDamageRate");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            NatStats.Delete(conn);
            ClassChoices.Delete(conn);
            Skills.Delete(conn);
            StateRates.Delete(conn);
            ElementRates.Delete(conn);
            Relations.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            NatStats.Clone(conn);
            ClassChoices.Clone(conn);
            Skills.Clone(conn);
            StateRates.Clone(conn);
            ElementRates.Clone(conn);
            Relations.Clone(conn);
        }
    }
}