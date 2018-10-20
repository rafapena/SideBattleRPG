using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;
using System.Windows;

namespace Database.Classes
{
    public partial class Battle : _ClassOperations
    {
        private static ClassTemplates.BattleEnemy[] Enemies;

        public Battle()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            Enemies = new ClassTemplates.BattleEnemy[] { Enemy1, Enemy2, Enemy3, Enemy4, Enemy5, Enemy6, Enemy7, Enemy8, Enemy9 };
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            int[] Zs = { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            int[] Xs = { 1, 0, 2, 1, 0, 2, 1, 0, 2 };
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].HostTableAttributeName = "BattleEnemy" + (i + 1);
                Enemies[i].InitializeNew();
                Enemies[i].SetPosition(Zs[i], Xs[i]);
            }
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            for (int i = 0; i < Enemies.Length; i++)
            {
                err += Enemies[i].ValidateInputs();
                for (int j = i+1; j < Enemies.Length; j++)
                {
                    if ((!Enemies[i].Exists() || !Enemies[j].Exists()) && Enemies[i].Position() != Enemies[j].Position()) continue;
                    err += "Enemies cannot share the same X and Z positions\n";
                    break;
                }
            }
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            for (int i = 0; i < Enemies.Length; i++) SQLDB.ParameterizeAttribute("@BattleEnemy" + (i + 1), Enemies[i].Exists() ? Enemies[i].ClassTemplateId : 0);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            for (int i = 0; i < Enemies.Length; i++) Enemies[i].Create(conn);
            SQLCreate(conn, "BaseObjectID, BattleEnemy1, BattleEnemy2, BattleEnemy3, BattleEnemy4, BattleEnemy5, BattleEnemy6, BattleEnemy7, BattleEnemy8, BattleEnemy9",
                "@BaseObjectID, @BattleEnemy1, @BattleEnemy2, @BattleEnemy3, @BattleEnemy4, @BattleEnemy5, @BattleEnemy6, @BattleEnemy7, @BattleEnemy8, @BattleEnemy9");
            // Create Dual Input Classes
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            for (int i = 0; i < Enemies.Length; i++) Enemies[i].Read(reader);
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            for (int i = 0; i < Enemies.Length; i++) Enemies[i].Update(conn);
            //SQLUpdate(conn, "");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            for (int i = 0; i < Enemies.Length; i++) Enemies[i].Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            for (int i = 0; i < Enemies.Length; i++) Enemies[i].Clone(conn);
        }
    }
}