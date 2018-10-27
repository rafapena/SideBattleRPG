using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using Database.Utilities;
using System.Windows;
using System;

namespace Database.ClassTemplates
{
    public partial class BattleEnemy : _ClassTemplateOperations
    {
        ComboBoxInputData EnemyData, PassiveSkillData;
        private List<string> GridPositionZOptions = new List<string> { "Front", "Center", "Back" };
        private List<string> GridPositionXOptions = new List<string> { "Left", "Center", "Right" };

        public BattleEnemy()
        {
            InitializeComponent();
            ClassTemplateTable = "BattleEnemy";
        }


        public string Position()
        {
            return GridPositionZInput.SelectedIndex + "" + GridPositionXInput.SelectedIndex;
        }
        public bool InSamePositionAs(BattleEnemy other)
        {
            return Position() == other.Position();
        }
        public void SetClassTemplateId(int id)
        {
            ClassTemplateId = id;
        }

        private void CBChangedEnemy(object sender, SelectionChangedEventArgs e)
        {
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                using (var reader = SQLDB.Read(conn, "SELECT Image FROM Enemy JOIN BaseObject " +
                    "WHERE BaseObject_ID = BaseObjectID AND Enemy_ID = " + EnemyData.SelectedInput(EnemyInput) + ";"))
                {
                    reader.Read();
                    try { EnemyImage.Source = ImageManager.BytesToImage(ImageManager.BlobToBytes(reader, 0)); }
                    catch (Exception) { EnemyImage.Source = null; }
                }
                conn.Close();
            }
        }


        protected override void SetupTableData()
        {
            EnemyData = new ComboBoxInputData("Enemy_ID", "Name", "BaseObject JOIN Enemy", "BaseObjectID = BaseObject_ID", "Name");
            EnemyInput.SelectionChanged += new SelectionChangedEventHandler(CBChangedEnemy);
            EnemyInput.ItemsSource = EnemyData.OptionsListNames;
            GridPositionZInput.ItemsSource = GridPositionZOptions;
            GridPositionXInput.ItemsSource = GridPositionXOptions;
            EnemySkills.Setup(ClassTemplateId, "Skill", "Skills", new List<string> { "Name", "" });
            EnemyWeapons.Setup(ClassTemplateId, "Weapon", "Weapons", new List<string> { "Name", "" });
            EnemyItems.Setup(ClassTemplateId, "Item", "Items", new List<string> { "Name", "" });
            PassiveSkillData = new ComboBoxInputData("PassiveSkill_ID", "Name", "BaseObject JOIN PassiveSkill", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            PassiveSkill1Input.ItemsSource = PassiveSkillData.OptionsListNames;
            PassiveSkill2Input.ItemsSource = PassiveSkillData.OptionsListNames;
        }
        
        protected override void OnInitializeNew()
        {
            EnemyInput.SelectedIndex = 0;
            LevelInput.Text = "1";
            GridPositionZInput.SelectedIndex = 0;
            GridPositionXInput.SelectedIndex = 1;
            HPMultiplierInput.Text = "1";
            PassiveSkill1Input.SelectedIndex = 0;
            PassiveSkill2Input.SelectedIndex = 0;
        }
        public new void InitializeNew(string hostTableAttributeName)
        {
            HostTableAttributeName = hostTableAttributeName;
            SetupTableData();
            OnInitializeNew();
        }

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.PosInt(LevelInput.Text) || int.Parse(LevelInput.Text) < 1 || int.Parse(LevelInput.Text) > 100) err += "Level must be a positive integer between 1 and 100\n";
            if (!Utils.NumberBetween(HPMultiplierInput.Text, 0.1, 100)) err += "HP x must be a number between 0.1 and 100\n";
            err += EnemySkills.ValidateInputs();
            err += EnemyWeapons.ValidateInputs();
            err += EnemyItems.ValidateInputs();
            if (PassiveSkill1Input.SelectedIndex == PassiveSkill2Input.SelectedIndex && PassiveSkill1Input.SelectedIndex > 0) err += "Passive Skills must be unique\n";
            return err;
        }
        
        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@EnemyID", EnemyData.SelectedInput(EnemyInput));
            SQLDB.ParameterizeAttribute("@Level", LevelInput.Text);
            SQLDB.ParameterizeAttribute("@GridPositionZ", GridPositionZInput.SelectedIndex);
            SQLDB.ParameterizeAttribute("@GridPositionX", GridPositionXInput.SelectedIndex);
            SQLDB.ParameterizeAttribute("@HPMultiplier", HPMultiplierInput.Text);
            SQLDB.ParameterizeAttribute("@PassiveSkill1", PassiveSkillData.SelectedInput(PassiveSkill1Input));
            SQLDB.ParameterizeAttribute("@PassiveSkill2", PassiveSkillData.SelectedInput(PassiveSkill2Input));
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            return new string[] { "EnemyID, Level, GridPositionZ, GridPositionX, HPMultiplier, PassiveSkill1, PassiveSkill2",
                "@EnemyID, @Level, @GridPositionZ, @GridPositionX, @HPMultiplier, @PassiveSkill1, @PassiveSkill2" };
        }
        public new void Create(SQLiteConnection conn)
        {
            base.Create(conn);
            EnemySkills.Create(conn);
            EnemyWeapons.Create(conn);
            EnemyItems.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            EnemyInput.SelectedIndex = EnemyData.FindIndex(reader["EnemyID"]);
            LevelInput.Text = reader["Level"].ToString();
            GridPositionZInput.SelectedIndex = int.Parse(reader["GridPositionZ"].ToString());
            GridPositionXInput.SelectedIndex = int.Parse(reader["GridPositionX"].ToString());
            HPMultiplierInput.Text = reader["HPMultiplier"].ToString();
            EnemySkills.Read();
            EnemyWeapons.Read();
            EnemyItems.Read();
            PassiveSkill1Input.SelectedIndex = PassiveSkillData.FindIndex(reader["PassiveSkill1"]);
            PassiveSkill2Input.SelectedIndex = PassiveSkillData.FindIndex(reader["PassiveSkill2"]);
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            return "EnemyID = @EnemyID, Level = @Level, GridPositionZ = @GridPositionZ, GridPositionX = @GridPositionX, " +
                "HPMultiplier = @HPMultiplier, PassiveSkill1 = @PassiveSkill1, PassiveSkill2 = @PassiveSkill2";
        }
        public new void Update(SQLiteConnection conn)
        {
            base.Update(conn);
            EnemySkills.Update(conn);
            EnemyWeapons.Update(conn);
            EnemyItems.Update(conn);
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            EnemySkills.Delete(conn);
            EnemyWeapons.Delete(conn);
            EnemyItems.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            EnemySkills.Clone(conn, ClassTemplateId);
            EnemyWeapons.Clone(conn, ClassTemplateId);
            EnemyItems.Clone(conn, ClassTemplateId);
        }
        public new void Clone(SQLiteConnection conn)
        {
            OnClone(conn);
        }
    }
}