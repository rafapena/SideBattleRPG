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
        ComboBoxInputData EnemyData;
        private List<string> GridPositionZOptions = new List<string> { "Front", "Center", "Back" };
        private List<string> GridPositionXOptions = new List<string> { "Left", "Center", "Right" };

        public BattleEnemy()
        {
            InitializeComponent();
            ClassTemplateTable = "BattleEnemy";   // DO NOT FORGET TO SET THIS
        }


        public bool Exists()
        {
            return EnemyInput.SelectedIndex > 0;
        }
        public string Position()
        {
            return GridPositionZInput.SelectedIndex + "" + GridPositionXInput.SelectedIndex;
        }
        public void SetPosition(int z, int x)
        {
            GridPositionZInput.SelectedIndex = z;
            GridPositionXInput.SelectedIndex = x;
        }


        protected override void SetupTableData()
        {
            EnemyData = new ComboBoxInputData("Enemy_ID", "Name", "BaseObject JOIN Enemy", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            EnemyInput.SelectionChanged += new SelectionChangedEventHandler(CBChangedEnemy);
            EnemyInput.ItemsSource = EnemyData.OptionsListNames;
            GridPositionZInput.ItemsSource = GridPositionZOptions;
            GridPositionXInput.ItemsSource = GridPositionXOptions;
            EnemySkills.Setup(HostTableAttributeName, "BattleEnemy", "Skill", "Skills", new List<string> { "Name", "" });
            EnemyWeapons.Setup(HostTableAttributeName, "BattleEnemy", "Weapon", "Weapons", new List<string> { "Name", "" });
            EnemyItems.Setup(HostTableAttributeName, "BattleEnemy", "Item", "Items", new List<string> { "Name", "" });
            EnemyPassiveSkills.Setup(HostTableAttributeName, "BattleEnemy", "PassiveSkill", "Passive Skills", new List<string> { "Name" });
        }
        private void CBChangedEnemy(object sender, SelectionChangedEventArgs e)
        {
            using (var conn = AccessDB.Connect())
            {
                string enemyId = EnemyData.SelectedInput(EnemyInput);
                if (enemyId == null)
                {
                    EnemyImage.Source = null;
                    return;
                }
                conn.Open();
                using (var reader = SQLDB.Read(conn, "SELECT Image FROM Enemy JOIN BaseObject WHERE BaseObject_ID = BaseObjectID AND Enemy_ID = " + enemyId + ";"))
                {
                    reader.Read();
                    try { EnemyImage.Source = ImageManager.BytesToImage(ImageManager.BlobToBytes(reader, 0)); }
                    catch (Exception) { EnemyImage.Source = null;  }
                }
                conn.Close();
            }
        }

        protected override void OnInitializeNew()
        {
            EnemyInput.SelectedIndex = 0;
            GridPositionZInput.SelectedIndex = 0;
            GridPositionXInput.SelectedIndex = 1;
            HPMultiplierInput.Text = "1";
            LevelInput.Text = "1";
        }

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.NumberBetween(HPMultiplierInput.Text, 0.1, 100)) err += "HP x must be a number between 0.1 and 100";
            if (!Utils.PosInt(LevelInput.Text) || int.Parse(LevelInput.Text) < 1 || int.Parse(LevelInput.Text) > 100) err += "Level must be a positive integer between 1 and 100";
            err += EnemySkills.ValidateInputs();
            err += EnemyWeapons.ValidateInputs();
            err += EnemyItems.ValidateInputs();
            err += EnemyPassiveSkills.ValidateInputs();
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@EnemyID", EnemyData.SelectedInput(EnemyInput));
            SQLDB.ParameterizeAttribute("@GridPositionZ", GridPositionZInput.SelectedIndex);
            SQLDB.ParameterizeAttribute("@GridPositionX", GridPositionXInput.SelectedIndex);
            SQLDB.ParameterizeAttribute("@HPMultiplier", HPMultiplierInput.Text);
            SQLDB.ParameterizeAttribute("@Level", LevelInput.Text);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            EnemySkills.Create(conn);
            EnemyWeapons.Create(conn);
            EnemyItems.Create(conn);
            EnemyPassiveSkills.Create(conn);
            return new string[] { "EnemyID, GridPositionZ, GridPositionX, HPMultiplier, Level", "@EnemyID, @GridPositionZ, @GridPositionX, @HPMultiplier, @Level" };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            EnemyInput.SelectedIndex = EnemyData.FindIndex(reader["EnemyID"]);
            GridPositionZInput.SelectedIndex = int.Parse(reader["GridPositionZ"].ToString());
            GridPositionXInput.SelectedIndex = int.Parse(reader["GridPositionX"].ToString());
            HPMultiplierInput.Text = reader["HPMultiplier"].ToString();
            LevelInput.Text = reader["Level"].ToString();
            EnemySkills.Read();
            EnemyWeapons.Read();
            EnemyItems.Read();
            EnemyPassiveSkills.Read();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            EnemySkills.Update(conn);
            EnemyWeapons.Update(conn);
            EnemyItems.Update(conn);
            EnemyPassiveSkills.Update(conn);
            return "EnemyID = @EnemyID, GridPositionZ = @GridPositionZ, GridPositionX = @GridPositionX, HPMultiplier = @HPMultiplier, Level = @Level";
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            EnemySkills.Delete(conn);
            EnemyWeapons.Delete(conn);
            EnemyItems.Delete(conn);
            EnemyPassiveSkills.Delete(conn);
        }
    }
}