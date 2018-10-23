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
        private static BattleEnemyAttributes[] EnemiesA;
        private static BattleEnemyUtilities[] EnemiesU;
        private int PreviousNumberOfEnemies, NumberOfEnemies;
        private int LatestFreeBEId;

        public Battle()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        private void UpdateLatestFreeBEId()
        {
            BattleEnemyAttributes dummy = new BattleEnemyAttributes();
            dummy.InitializeNew();
            LatestFreeBEId = dummy.ClassTemplateId;
        }

        protected override void SetupTableData()
        {
            EnemiesA = new BattleEnemyAttributes[] { EnemyA1, EnemyA2, EnemyA3, EnemyA4, EnemyA5, EnemyA6 };
            EnemiesU = new BattleEnemyUtilities[] { EnemyU1, EnemyU2, EnemyU3, EnemyU4, EnemyU5, EnemyU6 };
            for (int i = 1; i < EnemiesA.Length; i++)
            {
                EnemiesA[i].Visibility = Visibility.Collapsed;
                EnemiesU[i].Visibility = Visibility.Collapsed;
            }
            UpdateLatestFreeBEId();
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            PreviousNumberOfEnemies = 0;
            NumberOfEnemies = 1;
            for (int i = 0; i < EnemiesA.Length; i++)
            {
                string htan = "BattleEnemy" + (i + 1);
                EnemiesA[i].HostTableAttributeName = htan;
                EnemiesA[i].InitializeNew();
                EnemiesA[i].SetClassTemplateID(LatestFreeBEId + i);
                EnemiesU[i].HostTableAttributeName = htan;
                EnemiesU[i].InitializeNew();
                EnemiesU[i].SetClassTemplateID(LatestFreeBEId + i);
            }
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            for (int i = 0; i < NumberOfEnemies; i++)
            {
                err += EnemiesA[i].ValidateInputs();
                err += EnemiesU[i].ValidateInputs();
                for (int j = i + 1; j < NumberOfEnemies; j++)
                {
                    if (!EnemiesA[i].InSamePositionAs(EnemiesA[j])) continue;
                    err += "Enemies cannot share the same X and Z positions\n";
                    break;
                }
            }
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            string attrs = "";
            string vals = "";
            for (int i = 0; i < NumberOfEnemies; i++)
            {
                EnemiesA[i].Create(conn);
                attrs += ", BattleEnemy" + (i + 1);
                vals += ", " + (LatestFreeBEId + i);
            }
            MessageBox.Show(EnemiesA[0].ClassTemplateId + " " + EnemiesA[1].ClassTemplateId + " " + EnemiesA[2].ClassTemplateId + " " + EnemiesA[3].ClassTemplateId);
            LatestFreeBEId = EnemiesA[NumberOfEnemies - 1].ClassTemplateId + 1;
            SQLCreate(conn, "BaseObjectID, NumberOfEnemies" + attrs, "@BaseObjectID, " + NumberOfEnemies + vals);
            for (int i = 0; i < NumberOfEnemies; i++) EnemiesU[i].Create(conn);
            PreviousNumberOfEnemies = NumberOfEnemies;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            PreviousNumberOfEnemies = int.Parse(reader["NumberOfEnemies"].ToString());
            NumberOfEnemies = PreviousNumberOfEnemies;
            for (int i = 0; i < NumberOfEnemies; i++)
            {
                string htan = "BattleEnemy" + (i + 1);
                EnemiesA[i].HostTableAttributeName = htan;
                EnemiesA[i].Read(reader);
                EnemiesA[i].Visibility = Visibility.Visible;
                EnemiesU[i].HostTableAttributeName = htan;
                EnemiesU[i].Read(reader);
                EnemiesU[i].Visibility = Visibility.Visible;
            }
            for (int i = NumberOfEnemies; i < EnemiesA.Length; i++)
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            int UnchangedIds = NumberOfEnemies <= PreviousNumberOfEnemies ? NumberOfEnemies : PreviousNumberOfEnemies;
            for (int i = 0; i < UnchangedIds; i++)
            {
                EnemiesA[i].Update(conn);
                EnemiesU[i].Update(conn);
            }
            string updateForNewEnemies = "";
            int inc = 0;
            for (int i = PreviousNumberOfEnemies; i < NumberOfEnemies; i++) // Add new enemies
            {
                EnemiesA[i].Create(conn);
                EnemiesU[i].Create(conn);
                updateForNewEnemies += ", BattleEnemy" + (i + 1) + " = " + (LatestFreeBEId + inc++);
            }
            for (int i = NumberOfEnemies; i < PreviousNumberOfEnemies; i++) // Remove old enemies
            {
                EnemiesA[i].Delete(conn);
                EnemiesU[i].Delete(conn);
            }
            SQLUpdate(conn, "NumberOfEnemies = " + NumberOfEnemies + updateForNewEnemies);
            UpdateLatestFreeBEId();
            PreviousNumberOfEnemies = NumberOfEnemies;
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            for (int i = 0; i < PreviousNumberOfEnemies; i++)
            {
                EnemiesA[i].Delete(conn);
                EnemiesU[i].Delete(conn);
            }
            UpdateLatestFreeBEId();
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            for (int i = 0; i < NumberOfEnemies; i++)
            {
                EnemiesA[i].Clone(conn);
                EnemiesU[i].Clone(conn);
            }
            UpdateLatestFreeBEId();
        }


        private void AddBattleEnemy(object sender, RoutedEventArgs e)
        {
            if (NumberOfEnemies >= 6)
            {
                MessageBox.Show("Battle must have at no more than 6 enemies");
                return;
            }
            EnemiesA[NumberOfEnemies].Visibility = Visibility.Visible;
            EnemiesU[NumberOfEnemies].Visibility = Visibility.Visible;
            NumberOfEnemies++;
        }

        private void RemoveBattleEnemy(object sender, RoutedEventArgs e)
        {
            if (NumberOfEnemies <= 1)
            {
                MessageBox.Show("Battle must have at least 1 enemy");
                return;
            }
            NumberOfEnemies--;
            EnemiesA[NumberOfEnemies].Visibility = Visibility.Collapsed;
            EnemiesU[NumberOfEnemies].Visibility = Visibility.Collapsed;
        }
    }
}