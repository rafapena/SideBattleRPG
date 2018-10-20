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
            ScaledStats.InitializeNew(0);
            ScaledStats.HostTableAttributeName = "ScaledStats";
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
            string scaledStats = "";
            if (EnemyClassInput.SelectedIndex == 0)
            {
                scaledStats = ScaledStats.ValidateInputs(0, 8.5);
                if (scaledStats != "") scaledStats = "Class is set to 'None': " + scaledStats;
            } else {
                scaledStats = ScaledStats.ValidateInputs(-3, 3);
                if (scaledStats != "") scaledStats = "Enemy has a class: " + scaledStats;
            }
            err += scaledStats;
            err += ElementRates.ValidateInputs();
            err += StateRates.ValidateInputs();
            if (!Utils.PosInt(ExpInput.Text)) err += "EXP Gain must be a positive integer";
            if (!Utils.PosInt(GoldInput.Text)) err += "Gold Gain must be a positive integer";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@ScaledStats", ScaledStats.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@EnemyClass", EnemyClassData.SelectedInput(EnemyClassInput));
            SQLDB.ParameterizeAttribute("@ElementRates", ElementRates.StringList);
            SQLDB.ParameterizeAttribute("@Width", WidthInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@Height", HeightInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@BossType", BossTypeInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@Flying", (bool)FlyingInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@Exp", ExpInput.Text);
            SQLDB.ParameterizeAttribute("@Gold", GoldInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            ScaledStats.Create(conn);
            ElementRates.Create(conn);
            SQLCreate(conn, "BaseObjectID, ScaledStats, EnemyClass, ElementRates, Width, Height, BossType, Flying, Exp, Gold",
                "@BaseObjectID, @ScaledStats, @EnemyClass, '@ElementRates', @Width, @Height, @BossType, @Flying, @Exp, @Gold");
            StateRates.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            ScaledStats.Read(reader);
            EnemyClassInput.SelectedIndex = EnemyClassData.FindIndex(reader["EnemyClass"]);
            ElementRates.Read();
            StateRates.Read();
            WidthInput.SelectedIndex = int.Parse(reader["Width"].ToString());
            HeightInput.SelectedIndex = int.Parse(reader["Height"].ToString());
            BossTypeInput.SelectedIndex = int.Parse(reader["BossType"].ToString());
            FlyingInput.IsChecked = reader["Flying"].ToString() == "True" ? true : false;
            ExpInput.Text = reader["Exp"].ToString();
            GoldInput.Text = reader["Gold"].ToString();
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            ScaledStats.Update(conn);
            ElementRates.Update(conn);
            StateRates.Update(conn);
            SQLUpdate(conn, "EnemyClass=@EnemyClass, ElementRates='@ElementRates', Width=@Width, Height=@Height, BossType=@BossType, Flying=@Flying, Exp=@Exp, Gold=@Gold");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            ScaledStats.Delete(conn);
            ElementRates.Delete(conn);
            StateRates.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            ScaledStats.Clone(conn);
            ElementRates.Clone(conn);
            StateRates.Clone(conn);
        }
    }
}