using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class BattlerClass : _ClassOperations
    {
        private ComboBoxInputData UpgradedClass1Data, UpgradedClass2Data, UsableWeaponType1Data, UsableWeaponType2Data, PassiveSkill1Data, PassiveSkill2Data;

        public BattlerClass()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            Skills.Setup("BattlerClass", "Skill", "Skill Set", new List<string> { "Skill", "Level" });
            UpgradedClass1Data = new ComboBoxInputData("BattlerClass_ID", "Name", "BaseObject JOIN BattlerClass", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            UpgradedClass2Data = new ComboBoxInputData("BattlerClass_ID", "Name", "BaseObject JOIN BattlerClass", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            UsableWeaponType1Data = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Weapon Types'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            UsableWeaponType2Data = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Weapon Types'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            PassiveSkill1Data = new ComboBoxInputData("PassiveSkill_ID", "Name", "BaseObject JOIN PassiveSkill", "BaseObject_ID = BaseObjectID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            PassiveSkill2Data = new ComboBoxInputData("PassiveSkill_ID", "Name", "BaseObject JOIN PassiveSkill", "BaseObject_ID = BaseObjectID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            Skills.AttributeName = "LevelRequired";
            UpgradedClass1Input.ItemsSource = UpgradedClass1Data.OptionsListNames;
            UpgradedClass2Input.ItemsSource = UpgradedClass2Data.OptionsListNames;
            UsableWeaponType1Input.ItemsSource = UsableWeaponType1Data.OptionsListNames;
            UsableWeaponType2Input.ItemsSource = UsableWeaponType2Data.OptionsListNames;
            PassiveSkill1Input.ItemsSource = PassiveSkill1Data.OptionsListNames;
            PassiveSkill2Input.ItemsSource = PassiveSkill2Data.OptionsListNames;
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            ScaledStats.InitializeNew(3);
            ScaledStats.HostTableAttributeName = "ScaledStats";
            UpgradedClass1Input.SelectedIndex = 0;
            UpgradedClass2Input.SelectedIndex = 0;
            UsableWeaponType1Input.SelectedIndex = 0;
            UsableWeaponType2Input.SelectedIndex = 0;
            PassiveSkill1Input.SelectedIndex = 0;
            PassiveSkill2Input.SelectedIndex = 0;
            PSkillLvlRequired1Input.Text = "1";
            PSkillLvlRequired2Input.Text = "1";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += ScaledStats.ValidateInputs(0, 8.5);
            err += Skills.ValidateInputs();
            if (UpgradedClass1Input.SelectedIndex == UpgradedClass2Input.SelectedIndex && UpgradedClass1Input.SelectedIndex != 0) err += "Upgraded Classes cannot be the same\n";
            if (UsableWeaponType1Input.SelectedIndex == UsableWeaponType2Input.SelectedIndex && UsableWeaponType1Input.SelectedIndex != 0) err += "Usable Weapon Type cannot be the same\n";
            if (PassiveSkill1Input.SelectedIndex == PassiveSkill2Input.SelectedIndex && PassiveSkill1Input.SelectedIndex != 0) err += "Passive Skills cannot be the same\n";
            if (!Utils.PosInt(PSkillLvlRequired1Input.Text)) err += "Level Required for Passive Skill 1 must be a positive integer\n";
            if (!Utils.PosInt(PSkillLvlRequired2Input.Text)) err += "Level Required for Passive Skill 2 must be a positive integer\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@ScaledStats", ScaledStats.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@UpgradedClass1", UpgradedClass1Data.SelectedInput(UpgradedClass1Input));
            SQLDB.ParameterizeAttribute("@UpgradedClass2", UpgradedClass2Data.SelectedInput(UpgradedClass2Input));
            SQLDB.ParameterizeAttribute("@UsableWeaponType1", UsableWeaponType1Data.SelectedInput(UsableWeaponType1Input));
            SQLDB.ParameterizeAttribute("@UsableWeaponType2", UsableWeaponType2Data.SelectedInput(UsableWeaponType2Input));
            SQLDB.ParameterizeAttribute("@PassiveSkill1", PassiveSkill1Data.SelectedInput(PassiveSkill1Input));
            SQLDB.ParameterizeAttribute("@PassiveSkill2", PassiveSkill2Data.SelectedInput(PassiveSkill2Input));
            SQLDB.ParameterizeAttribute("@PSkillLvlRequired1", PSkillLvlRequired1Input.Text);
            SQLDB.ParameterizeAttribute("@PSkillLvlRequired2", PSkillLvlRequired2Input.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            ScaledStats.Create(conn);
            SQLCreate(conn, "BaseObjectID, ScaledStats, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, " +
                "PassiveSkill1, PassiveSkill2, PSkillLvlRequired1, PSkillLvlRequired2",
                "@BaseObjectID, @ScaledStats, @UpgradedClass1, @UpgradedClass2, @UsableWeaponType1, @UsableWeaponType2" +
                "@PassiveSkill1, @PassiveSkill2, @PSkillLvlRequired1, @PSkillLvlRequired2");
            Skills.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            ScaledStats.Read(reader);
            Skills.Read();
            UpgradedClass1Input.SelectedIndex = UpgradedClass1Data.FindIndex(reader["UpgradedClass1"]);
            UpgradedClass2Input.SelectedIndex = UpgradedClass2Data.FindIndex(reader["UpgradedClass2"]);
            UsableWeaponType1Input.SelectedIndex = UsableWeaponType1Data.FindIndex(reader["UsableWeaponType1"]);
            UsableWeaponType2Input.SelectedIndex = UsableWeaponType2Data.FindIndex(reader["UsableWeaponType2"]);
            PassiveSkill1Input.SelectedIndex = PassiveSkill1Data.FindIndex(reader["PassiveSkill1"]);
            PassiveSkill2Input.SelectedIndex = PassiveSkill2Data.FindIndex(reader["PassiveSkill2"]);
            PSkillLvlRequired1Input.Text = reader["PSkillLvlRequired1"].ToString();
            PSkillLvlRequired2Input.Text = reader["PSkillLvlRequired2"].ToString();
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            ScaledStats.Update(conn);
            Skills.Update(conn);
            SQLUpdate(conn, "UpgradedClass1=@UpgradedClass1, UpgradedClass2=@UpgradedClass2, UsableWeaponType1=@UsableWeaponType1, UsableWeaponType2=@UsableWeaponType2, " +
                "PassiveSkill1=@PassiveSkill1, PassiveSkill2=@PassiveSkill2, PSkillLvlRequired1=@PSkillLvlRequired1, PSkillLvlRequired2=@PSkillLvlRequired2");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            ScaledStats.Delete(conn);
            Skills.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            ScaledStats.Clone(conn);
            Skills.Clone(conn);
        }
    }
}