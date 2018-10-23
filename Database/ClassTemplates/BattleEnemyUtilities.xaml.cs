using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using Database.Utilities;
using System.Windows;
using System;

namespace Database.ClassTemplates
{
    public partial class BattleEnemyUtilities : _ClassTemplateOperations
    {
        ComboBoxInputData PassiveSkillData;

        public void SetClassTemplateID(int id)
        {
            ClassTemplateId = id;
        }

        public BattleEnemyUtilities()
        {
            InitializeComponent();
            ClassTemplateTable = "BattleEnemy";
        }

        protected override void SetupTableData()
        {
            EnemySkills.Setup(HostTableAttributeName, "BattleEnemy", "Skill", "Skills", new List<string> { "Name", "" });
            EnemyWeapons.Setup(HostTableAttributeName, "BattleEnemy", "Weapon", "Weapons", new List<string> { "Name", "" });
            EnemyItems.Setup(HostTableAttributeName, "BattleEnemy", "Item", "Items", new List<string> { "Name", "" });
            PassiveSkillData = new ComboBoxInputData("PassiveSkill_ID", "Name", "BaseObject JOIN PassiveSkill", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            PassiveSkill1Input.ItemsSource = PassiveSkillData.OptionsListNames;
            PassiveSkill2Input.ItemsSource = PassiveSkillData.OptionsListNames;
        }

        protected override void OnInitializeNew()
        {
            PassiveSkill1Input.SelectedIndex = 0;
            PassiveSkill2Input.SelectedIndex = 0;
        }

        public override string ValidateInputs()
        {
            string err = "";
            err += EnemySkills.ValidateInputs();
            err += EnemyWeapons.ValidateInputs();
            err += EnemyItems.ValidateInputs();
            if (PassiveSkill1Input.SelectedIndex == PassiveSkill2Input.SelectedIndex && PassiveSkill1Input.SelectedIndex > 0) err += "Passive Skills must be unique\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            EnemySkills.ParameterizeAttributes();
            EnemyWeapons.ParameterizeAttributes();
            EnemyItems.ParameterizeAttributes();
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            EnemySkills.Create(conn);
            EnemyWeapons.Create(conn);
            EnemyItems.Create(conn);
            UpdatePassiveSkills(conn);
            return null;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            EnemySkills.Read();
            EnemyWeapons.Read();
            EnemyItems.Read();
            PassiveSkill1Input.SelectedIndex = PassiveSkillData.FindIndex(reader["PassiveSkill1"]);
            PassiveSkill2Input.SelectedIndex = PassiveSkillData.FindIndex(reader["PassiveSkill2"]);
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            EnemySkills.Update(conn);
            EnemyWeapons.Update(conn);
            EnemyItems.Update(conn);
            UpdatePassiveSkills(conn);
            return "";
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            EnemySkills.Delete(conn);
            EnemyWeapons.Delete(conn);
            EnemyItems.Delete(conn);
        }

        private void UpdatePassiveSkills(SQLiteConnection conn)
        {
            string p1 = PassiveSkillData.SelectedInput(PassiveSkill1Input);
            string p2 = PassiveSkillData.SelectedInput(PassiveSkill2Input);
            if (p1 == null) p1 = "NULL";
            if (p2 == null) p2 = "NULL";
            SQLDB.Write(conn, "UPDATE BattleEnemy SET PassiveSkill1 = " + p1 + ", PassiveSkill2 = " + p2 + " WHERE BattleEnemy_ID = " + ClassTemplateId + ";");
        }
    }
}