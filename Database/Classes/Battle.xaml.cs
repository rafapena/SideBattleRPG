using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;
using System.Windows;
using System;
using Database.ClassTemplates;

namespace Database.Classes
{
    public partial class Battle : _ClassOperations
    {
        private static BattleEnemy[] Enemies;
        private int PreviousNumberOfEnemies, NumberOfEnemies;

        public Battle()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            Enemies = new BattleEnemy[] { Enemy1, Enemy2, Enemy3, Enemy4, Enemy5, Enemy6 };
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            int latestId = SQLDB.MaxIdPlusOne("BattleEnemy");
            for (int i = 0; i < Enemies.Length;)
            {
                Enemies[i].Visibility = Visibility.Collapsed;
                Enemies[i].SetClassTemplateId(latestId + i);
                Enemies[i].InitializeNew("BattleEnemy" + ++i);
            }
            PreviousNumberOfEnemies = 0;
            NumberOfEnemies = 1;
            Enemies[0].Visibility = Visibility.Visible;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            bool flag = false;
            for (int i = 0; i < NumberOfEnemies; i++)
            {
                err += Enemies[i].ValidateInputs();
                if (flag) continue;
                for (int j = i + 1; j < NumberOfEnemies; j++)
                {
                    if (!Enemies[i].InSamePositionAs(Enemies[j])) continue;
                    err += "Enemies cannot share the same X and Z positions\n";
                    flag = true;
                    break;
                }
            }
            return err;
        }

        public override void ParameterizeAttributes() { }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            SQLCreate(conn, "BaseObjectID, NumberOfEnemies", Base.ClassTemplateId + ", " + NumberOfEnemies);
            CreateNewEnemies(conn, 0);
            PreviousNumberOfEnemies = NumberOfEnemies;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            PreviousNumberOfEnemies = int.Parse(reader["NumberOfEnemies"].ToString());
            NumberOfEnemies = PreviousNumberOfEnemies;
            for (int i = 0; i < NumberOfEnemies; i++)
            {
                Enemies[i].Visibility = Visibility.Visible;
                Enemies[i].Read(reader);
            }
            int latestId = SQLDB.MaxIdPlusOne("BattleEnemy");
            int inc = 0;
            for (int i = NumberOfEnemies; i < Enemies.Length;)
            {
                Enemies[i].Visibility = Visibility.Collapsed;
                Enemies[i].SetClassTemplateId(latestId + inc++);
                Enemies[i].InitializeNew("BattleEnemy" + ++i);
            }
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            int UnchangedIds = NumberOfEnemies <= PreviousNumberOfEnemies ? NumberOfEnemies : PreviousNumberOfEnemies;
            for (int i = 0; i < UnchangedIds; i++) Enemies[i].Update(conn);
            CreateNewEnemies(conn, PreviousNumberOfEnemies);                    // Add new enemies
            int latestId = SQLDB.MaxIdPlusOne("BattleEnemy");
            int inc = 0;
            for (int i = NumberOfEnemies; i < PreviousNumberOfEnemies; i++)     // Remove old enemies
            {
                Enemies[i].Delete(conn);
                Enemies[i].SetClassTemplateId(latestId + inc++);
                Enemies[i].InitializeNew();
            }
            PreviousNumberOfEnemies = NumberOfEnemies;
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            for (int i = 0; i < PreviousNumberOfEnemies; i++) Enemies[i].Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            int latestId = SQLDB.MaxIdPlusOne("BattleEnemy");
            for (int i = 0; i < NumberOfEnemies; i++)
            {
                Enemies[i].SetClassTemplateId(latestId + i);
                Enemies[i].Clone(conn);
            }
        }


        private void AddBattleEnemy(object sender, RoutedEventArgs e)
        {
            if (NumberOfEnemies >= 6) MessageBox.Show("6 enemies per battle is the maximum limit.", "Could not add enemy");
            else Enemies[NumberOfEnemies++].Visibility = Visibility.Visible;
        }

        private void RemoveBattleEnemy(object sender, RoutedEventArgs e)
        {
            if (NumberOfEnemies <= 1) MessageBox.Show("1 enemy per battle is the minimum limit", "Could not remove enemy");
            else Enemies[--NumberOfEnemies].Visibility = Visibility.Collapsed;
        }

        private void CreateNewEnemies(SQLiteConnection conn, int initialIndex)
        {
            string textForNewEnemies = "";
            int latestId = SQLDB.MaxIdPlusOne("BattleEnemy");
            int inc = 0;
            for (int i = initialIndex; i < NumberOfEnemies;)
            {
                Enemies[i++].Create(conn);
                textForNewEnemies += ", BattleEnemy" + i + " = " + (latestId + inc++);
            }
            SQLUpdate(conn, "NumberOfEnemies = " + NumberOfEnemies + textForNewEnemies);
        }
    }
}