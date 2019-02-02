using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using static Database.AccessDB;
using static Database.Utilities.SQLDB;
using BattleSimulator.Classes;
using BattleSimulator.Classes.ClassTemplates;
using BattleSimulator.Utilities;
using static BattleSimulator.Utilities.DataManager;

namespace BattleSimulator.Simulator
{
    public static class AllData
    {
        public static List<string> Elements { get; private set; }
        public static List<string> WeaponTypes { get; private set; }
        public static List<string> ToolFormulas { get; private set; }
        public static List<string> ToolTypes { get; private set; }
        public static List<BattlerClass> Classes { get; private set; }
        public static List<Enemy> Enemies { get; private set; }
        public static List<Classes.Environment> Environments { get; private set; }
        public static List<Item> Items { get; private set; }
        public static List<PassiveSkill> PassiveSkills { get; private set; }
        public static List<Player> Players { get; private set; }
        public static List<Skill> Skills { get; private set; }
        public static List<State> States { get; private set; }
        public static List<Weapon> Weapons { get; private set; }
        public static Battle Battle { get; private set; }

        private delegate void SetupList(SQLiteDataReader data);
        private static readonly SetupList[] TblFuncsC = { BattlerClassC, EnemyC, EnvironmentC, ItemC, PassiveSkillC, PlayerC, SkillC, StateC, WeaponC };
        private static readonly SetupList[] TblFuncsU = { BattlerClassU, EnemyU, EnvironmentU, ItemU, PassiveSkillU, PlayerU, SkillU, StateU, WeaponU };
        private static readonly string[] Tables = { "BattlerClass", "Enemy", "Environment", "Item", "PassiveSkill", "Player", "Skill", "State", "Weapon" };


        public static void Setup()
        {
            Classes = new List<BattlerClass>(new BattlerClass[MaxIdPlusOne(Tables[0])]);
            Enemies = new List<Enemy>(new Enemy[MaxIdPlusOne(Tables[1])]);
            Environments = new List<Classes.Environment>(new Classes.Environment[MaxIdPlusOne(Tables[2])]);
            Items = new List<Item>(new Item[MaxIdPlusOne(Tables[3])]);
            PassiveSkills = new List<PassiveSkill>(new PassiveSkill[MaxIdPlusOne(Tables[4])]);
            Players = new List<Player>(new Player[MaxIdPlusOne(Tables[5])]);
            Skills = new List<Skill>(new Skill[MaxIdPlusOne(Tables[6])]);
            States = new List<State>(new State[MaxIdPlusOne(Tables[7])]);
            Weapons = new List<Weapon>(new Weapon[MaxIdPlusOne(Tables[8])]);
            Battle = new Battle();
            using (var conn = Connect())
            {
                conn.Open();
                SetupTypesLists(conn);
                for (int i = 0; i < TblFuncsC.Length; i++)
                    using (var data = Read(conn, "SELECT " + Tables[i] + "_ID FROM " + Tables[i]))
                        while (data.Read()) TblFuncsC[i](data);
                for (int i = 0; i < TblFuncsU.Length; i++)
                    using (var data = Read(conn, "SELECT * FROM BaseObject JOIN " + Tables[i] + " WHERE BaseObject_ID = BaseObjectID"))
                        while (data.Read()) TblFuncsU[i](data);
                conn.Close();
            }
        }

        public static void SetupBattle(int id)
        {
            using (var conn = Connect())
            {
                conn.Open();
                using (var data = Read(conn, "SELECT * FROM BaseObject JOIN Battle WHERE BaseObject_ID = BaseObjectID AND Battle_ID = " + id))
                {
                    data.Read();
                    Battle.Initialize(data, Environments, Enemies, PassiveSkills, Skills, Items, Weapons, States);
                }
                conn.Close();
            }
        }

        private static void SetupTypesLists(SQLiteConnection conn)
        {
            Elements = new List<string> { "None" };
            WeaponTypes = new List<string> { "None" };
            ToolFormulas = new List<string> { "None" };
            ToolTypes = new List<string> { "None" };
            using (var data = Read(conn, "SELECT * FROM TypesLists"))
            {
                while (data.Read())
                {
                    switch (data["List_Type"].ToString())
                    {
                        case "Elements": Elements.Add(data["Name"].ToString()); break;
                        case "Weapon Types": WeaponTypes.Add(data["Name"].ToString()); break;
                        case "Tool Formulas": ToolFormulas.Add(data["Name"].ToString()); break;
                        case "Tool Types": ToolTypes.Add(data["Name"].ToString()); break;
                    }
                }
            }
        }


        private static void BattlerClassC(SQLiteDataReader data) { Classes[Int(data["BattlerClass_ID"])] = new BattlerClass(); }
        private static void BattlerClassU(SQLiteDataReader data) { Classes[Int(data["BattlerClass_ID"])].Initialize(data, Classes, PassiveSkills, Skills); }

        private static void EnemyC(SQLiteDataReader data) { Enemies[Int(data["Enemy_ID"])] = new Enemy(); }
        private static void EnemyU(SQLiteDataReader data) { Enemies[Int(data["Enemy_ID"])].Initialize(data, Elements, States, Classes); }

        private static void EnvironmentC(SQLiteDataReader data) { Environments[Int(data["Environment_ID"])] = new Classes.Environment(); }
        private static void EnvironmentU(SQLiteDataReader data) { Environments[Int(data["Environment_ID"])].Initialize(data, Elements, States); }

        private static void ItemC(SQLiteDataReader data) { Items[Int(data["Item_ID"])] = new Item(); }
        private static void ItemU(SQLiteDataReader data) { Items[Int(data["Item_ID"])].Initialize(data, Classes, States, Items); }

        private static void PassiveSkillC(SQLiteDataReader data) { PassiveSkills[Int(data["PassiveSkill_ID"])] = new PassiveSkill(); }
        private static void PassiveSkillU(SQLiteDataReader data) { PassiveSkills[Int(data["PassiveSkill_ID"])].Initialize(data, Elements, States); }

        private static void PlayerC(SQLiteDataReader data) { Players[Int(data["Player_ID"])] = new Player(); }
        private static void PlayerU(SQLiteDataReader data) { Players[Int(data["Player_ID"])].Initialize(data, Elements, States, Classes, Players, Skills); }

        private static void SkillC(SQLiteDataReader data) { Skills[Int(data["Skill_ID"])] = new Skill(); }
        private static void SkillU(SQLiteDataReader data) { Skills[Int(data["Skill_ID"])].Initialize(data, Classes, States, Players, Enemies); }

        private static void StateC(SQLiteDataReader data) { States[Int(data["State_ID"])] = new State(); }
        private static void StateU(SQLiteDataReader data) { States[Int(data["State_ID"])].Initialize(data, Elements, States); }

        private static void WeaponC(SQLiteDataReader data) { Weapons[Int(data["Weapon_ID"])] = new Weapon(); }
        private static void WeaponU(SQLiteDataReader data) { Weapons[Int(data["Weapon_ID"])].Initialize(data, Classes, States); }
    }
}