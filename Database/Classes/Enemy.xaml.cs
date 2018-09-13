using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class Enemy : _ClassOperations
    {
        private ComboBoxInputData EnemyClassData;
        private List<string> SizeOptions = new List<string> { "1", "2", "3" };
        private List<string> BossTypeOptions = new List<string> { "None", "Mini", "Standard", "Final" };

        public Enemy()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            EnemyClassData = new ComboBoxInputData("BattlerClass_ID", "Name", "BaseObject JOIN BattlerClass", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            StateRates.Setup("Enemy", "State", "State Rates", new List<string> { "State", "%" });
            ElementRates.Setup("Enemy", "TypesLists", "Elements", "Element Rates", new List<string> { "Element", "%" });
            WidthInput.ItemsSource = SizeOptions;
            HeightInput.ItemsSource = SizeOptions;
            BossTypeInput.ItemsSource = BossTypeOptions;
            EnemyClassInput.ItemsSource = EnemyClassData.OptionsListNames;
            StateRates.AttributeName = "Vulnerability";
            ElementRates.AttributeName = "ElementRates";
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            CustomStats.InitializeNew(0);
            CustomStats.HostTableAttributeName = "ScaledStats";
            WidthInput.SelectedIndex = 0;
            HeightInput.SelectedIndex = 0;
            BossTypeInput.SelectedIndex = 0;
            FlyingInput.IsChecked = false;
            ExpInput.Text = "50";
            GoldInput.Text = "10";
            EnemyClassInput.SelectedIndex = 0;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            string customStats = "";
            if (EnemyClassInput.SelectedIndex == 0)
            {
                customStats = CustomStats.ValidateInputs(0, 8.5);
                if (customStats != "") customStats = "Class is set to 'None': " + customStats;
            } else {
                customStats = CustomStats.ValidateInputs(-3, 3);
                if (customStats != "") customStats = "Enemy has a class: " + customStats;
            }
            err += customStats;
            err += ElementRates.ValidateInputs();
            err += StateRates.ValidateInputs();
            if (!Utils.PosInt(ExpInput.Text)) err += "EXP Gain must be a positive integer";
            if (!Utils.PosInt(GoldInput.Text)) err += "Gold Gain must be a positive integer";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.ParameterizeInput("@Width", SizeOptions[WidthInput.SelectedIndex].ToString());
            SQLDB.ParameterizeInput("@Height", SizeOptions[HeightInput.SelectedIndex].ToString());
            SQLDB.ParameterizeInput("@BossType", BossTypeInput.SelectedIndex.ToString());
            SQLDB.ParameterizeInput("@Exp", ExpInput.Text);
            SQLDB.ParameterizeInput("@Gold", GoldInput.Text);
            SQLDB.ParameterizeInput("@EnemyClass", EnemyClassData.SelectedInput(EnemyClassInput));
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            CustomStats.Create(conn);
            ElementRates.Create(conn);
            SQLCreate(conn, "ElementRates, Width, Height, BossType, Flying, Exp, Gold, BaseObjectID, ScaledStats, EnemyClass",
                "'" + ElementRates.StringList + "', @Width, @Height, @BossType, " + ((bool)FlyingInput.IsChecked ? 1:0) + ", @Exp, @Gold, " +
                Base.ClassTemplateId + ", " + CustomStats.ClassTemplateId + ", @EnemyClass");
            StateRates.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            CustomStats.Read(reader);
            ElementRates.Read();
            StateRates.Read();
            WidthInput.SelectedIndex = SizeOptions.FindIndex(a => a == reader["Width"].ToString());
            HeightInput.SelectedIndex = SizeOptions.FindIndex(a => a == reader["Height"].ToString());
            BossTypeInput.SelectedIndex = int.Parse(reader["BossType"].ToString());
            FlyingInput.IsChecked = reader["Flying"].ToString() == "True" ? true : false;
            ExpInput.Text = reader["Exp"].ToString();
            GoldInput.Text = reader["Gold"].ToString();
            EnemyClassInput.SelectedIndex = EnemyClassData.FindIndex(reader["EnemyClass"]);
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            CustomStats.Update(conn);
            ElementRates.Update(conn);
            StateRates.Update(conn);
            SQLUpdate(conn, "ElementRates='" + ElementRates.StringList + "', Width=@Width, Height=@Height, BossType=@BossType, " +
                "Flying=" + ((bool)FlyingInput.IsChecked ? 1:0) + ", Exp=@Exp, Gold=@Gold, EnemyClass=@EnemyClass");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            CustomStats.Delete(conn);
            ElementRates.Delete(conn);
            StateRates.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            CustomStats.Clone(conn);
            ElementRates.Clone(conn);
            StateRates.Clone(conn);
        }
    }
}