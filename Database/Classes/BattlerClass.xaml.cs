using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class BattlerClass : _ClassOperations
    {
        private ComboBoxInputData UpgradedClass1Data, UpgradedClass2Data, UsableWeaponType1Data, UsableWeaponType2Data;

        public BattlerClass()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            UpgradedClass1Data = new ComboBoxInputData("BattlerClass_ID", "Name", "BaseObject JOIN BattlerClass", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            UpgradedClass2Data = new ComboBoxInputData("BattlerClass_ID", "Name", "BaseObject JOIN BattlerClass", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            UsableWeaponType1Data = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Weapon Types'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            UsableWeaponType2Data = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Weapon Types'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            UpgradedClass1Input.ItemsSource = UpgradedClass1Data.OptionsListNames;
            UpgradedClass2Input.ItemsSource = UpgradedClass2Data.OptionsListNames;
            UsableWeaponType1Input.ItemsSource = UsableWeaponType1Data.OptionsListNames;
            UsableWeaponType2Input.ItemsSource = UsableWeaponType2Data.OptionsListNames;
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
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += ScaledStats.ValidateInputs(0, 8.5);
            if (UpgradedClass1Input.SelectedIndex == UpgradedClass2Input.SelectedIndex && UpgradedClass1Input.SelectedIndex != 0) err += "Upgraded Classes cannot be the same\n";
            if (UsableWeaponType1Input.SelectedIndex == UsableWeaponType2Input.SelectedIndex && UsableWeaponType1Input.SelectedIndex != 0) err += "Usable Weapon Type cannot be the same\n";
            return err;
        }

        public override void ParameterizeInputs()
        {
            int c1 = UpgradedClass1Data.OptionsListIds[UpgradedClass1Input.SelectedIndex];
            int c2 = UpgradedClass2Data.OptionsListIds[UpgradedClass2Input.SelectedIndex];
            SQLDB.ParameterizeInput("@UpgradedClass1", c1 < 0 ? null : c1.ToString());
            SQLDB.ParameterizeInput("@UpgradedClass2", c2 < 0 ? null : c2.ToString());
            SQLDB.ParameterizeInput("@UsableWeaponType1", UsableWeaponType1Data.OptionsListIds[UsableWeaponType1Input.SelectedIndex].ToString());
            SQLDB.ParameterizeInput("@UsableWeaponType2", UsableWeaponType2Data.OptionsListIds[UsableWeaponType2Input.SelectedIndex].ToString());
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            ScaledStats.Create(conn);
            SQLCreate(conn, "UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, ScaledStats, BaseObjectID",
                "@UpgradedClass1, @UpgradedClass2, @UsableWeaponType1, @UsableWeaponType2, " + ScaledStats.ClassTemplateId + ", " + Base.ClassTemplateId);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            ScaledStats.Read(reader);
            UpgradedClass1Input.SelectedIndex = UpgradedClass1Data.FindIndex(reader["UpgradedClass1"]);
            UpgradedClass2Input.SelectedIndex = UpgradedClass2Data.FindIndex(reader["UpgradedClass2"]);
            UsableWeaponType1Input.SelectedIndex = UsableWeaponType1Data.FindIndex(reader["UsableWeaponType1"]);
            UsableWeaponType2Input.SelectedIndex = UsableWeaponType2Data.FindIndex(reader["UsableWeaponType2"]);
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            ScaledStats.Update(conn);
            SQLUpdate(conn, "UpgradedClass1=@UpgradedClass1, UpgradedClass2=@UpgradedClass2, UsableWeaponType1=@UsableWeaponType1, UsableWeaponType2=@UsableWeaponType2");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            ScaledStats.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            ScaledStats.Clone(conn);
        }
    }
}