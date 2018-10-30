using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes;
using BattleSimulator.Classes.ClassTemplates;
using BattleSimulator.Utilities;
using static Database.AccessDB;
using static Database.Utilities.SQLDB;
using System.IO;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BattleSimulator.Simulator
{
    public static class DataManager
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

        private delegate void SetupListC(SQLiteDataReader data);
        private delegate void SetupListU(SQLiteDataReader data, int i);
        private static readonly SetupListC[] TblFuncsC = { BattlerClassC, EnemyC, EnvironmentC, ItemC, PassiveSkillC, PlayerC, SkillC, StateC, WeaponC };
        private static readonly SetupListU[] TblFuncsU = { BattlerClassU, EnemyU, EnvironmentU, ItemU, PassiveSkillU, PlayerU, SkillU, StateU, WeaponU };
        private static readonly string[] Tables = { "BattlerClass", "Enemy", "Environment", "Item", "PassiveSkill", "Player", "Skill", "State", "Weapon" };
        private static int[][] IdsList;
        private const int BC = 0, EM = 1, EV = 2, IT = 3, PS = 4, PL = 5, SK = 6, ST = 7, WP = 8;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// --- Setup ---
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void Setup()
        {
            IdsList = new int[Tables.Length][];
            for (int i = 0; i < Tables.Length; i++) IdsList[i] = new int[MaxIdPlusOne(Tables[i])];
            Classes = new List<BattlerClass>();
            Enemies = new List<Enemy>();
            Environments = new List<Classes.Environment>();
            Items = new List<Item>();
            PassiveSkills = new List<PassiveSkill>();
            Players = new List<Player>();
            Skills = new List<Skill>();
            States = new List<State>();
            Weapons = new List<Weapon>();
            using (var conn = Connect())
            {
                conn.Open();
                SetupTypesLists(conn);
                string readText = "SELECT * FROM BaseObject JOIN " + Tables[0] + " WHERE BaseObject_ID = BaseObjectID";
                int i = 0;
                foreach (var tfc in TblFuncsC) using (var data = Read(conn, readText)) while (data.Read()) tfc(data);
                foreach (var tfu in TblFuncsU) using (var data = Read(conn, readText)) while (data.Read()) tfu(data, i++);
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
                    switch (Text(data["List_Type"]))
                    {
                        case "Elements": Elements.Add(Text(data["Name"])); break;
                        case "Weapon Types": WeaponTypes.Add(Text(data["Name"])); break;
                        case "Tool Formulas": ToolFormulas.Add(Text(data["Name"])); break;
                        case "Tool Types": ToolTypes.Add(Text(data["Name"])); break;
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// --- Local Utilities ---
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static T GetObj<T>(List<T> list, int tableNum, object element) where T : BaseObject
        {
            return element == null ? null : list[IdsList[tableNum][Int(element)]];
        }
        private static int Int(object o)
        {
            string res = o.ToString();
            return res == "" ? 0 : int.Parse(res);
        }
        private static double Dbl(object o) {  return o.ToString() == "" ? 0 : (double)o; }
        private static string Text(object o) { return o.ToString(); }
        
        private static List<int> ReadList(string hostTable, string targetTable, int hostId, string extraAttribute="")
        {
            List<int> list = new List<int>();
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data = Read(conn0, "SELECT * FROM " + hostTable + "_To_" + targetTable + " WHERE " + hostTable + "ID = " + hostId))
                {
                    while (data.Read())
                    {
                        list.Add(Int(data[targetTable + "ID"]));
                        if (extraAttribute != "") list.Add(Int(data[extraAttribute]));
                    }
                }
                conn0.Close();
            }
            return list;
        }

        private static Stats ReadStats(object statsId, int mul = 1)
        {
            Stats stats = null;
            if (statsId == null) return stats;
            else statsId = Int(statsId);
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data = Read(conn0, "SELECT * FROM Stats WHERE Stats_ID = " + statsId))
                {
                    data.Read();
                    int maxhp = (int)(Dbl(data["HP"]) * mul);
                    int atk = (int)(Dbl(data["Atk"]) * mul);
                    int def = (int)(Dbl(data["Def"]) * mul);
                    int map = (int)(Dbl(data["Map"]) * mul);
                    int mar = (int)(Dbl(data["Mar"]) * mul);
                    int spd = (int)(Dbl(data["Spd"]) * mul);
                    int tec = (int)(Dbl(data["Tec"]) * mul);
                    int luk = (int)(Dbl(data["Luk"]) * mul);
                    stats = new Stats(maxhp, atk, def, map, mar, spd, tec, luk);
                    stats.SetModifiables(Int(data["Acc"]), Int(data["Eva"]), Int(data["Crt"]), Int(data["Cev"]));
                }
                conn0.Close();
            }
            return stats;
        }

        private static T ReadTool<T>(T tool, object toolId) where T : Tool
        {
            if (toolId == null) return tool;
            toolId = Int(toolId);
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data = Read(conn0, "SELECT * FROM Tool WHERE Tool_ID = " + toolId))
                {
                    data.Read();
                    int tp = Int(data[""]);
                    int fr = Int(data[""]);
                    int hpspa = Int(data[""]);
                    int hpa = Int(data[""]);
                    int spa = Int(data[""]);
                    int hpp = Int(data[""]);
                    int spp = Int(data[""]);
                    int hpr = Int(data[""]);
                    int sc = Int(data[""]);
                    int ca = Int(data[""]);
                    int ra = Int(data[""]);
                    int el = Int(data[""]);
                    int pw = Int(data[""]);
                    int acc = Int(data[""]);
                    int crt = Int(data[""]);
                    int pr = Int(data[""]);
                    tool.SetTypes(tp, fr);
                    tool.SetHPSPValues(hpspa, hpa, spa, hpp, spp, hpr);
                    tool.SetTargets(sc, ca, ra);
                    tool.SetAmplifiers(el, pw, acc, crt, pr);
                    //tool.SetExclusiveClasses();
                    //tool.AddStateRateGive();
                    //tool.AddStateRateReceive();
                }
                conn0.Close();
            }
            return tool;
        }

        private static P ReadPassiveEffect<P>(P pEffect, object pEffectId) where P : PassiveEffect
        {
            if (pEffectId == null) return pEffect;
            pEffectId = Int(pEffectId);
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data = Read(conn0, "SELECT * FROM PassiveEffect WHERE PassiveEffect_ID = " + pEffectId))
                {
                    data.Read();
                }
                conn0.Close();
            }
            return pEffect;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// --- Create and Update Functions for each db table ---
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void BattlerClassC(SQLiteDataReader data)
        {
            int id = Int(data["BattlerClass_ID"]);
            int wt1 = Int(data["UsableWeaponType1"]);
            int wt2 = Int(data["UsableWeaponType2"]);
            Stats stats = ReadStats(data["ScaledStats"], 2);
            int psl1 = Int(data["PSkillLvlRequired1"]);
            int psl2 = Int(data["PSkillLvlRequired2"]);
            BattlerClass newObj = new BattlerClass(id, Text(data["Name"]), Text(data["Description"]), Utils.BytesToImage(data, 3));
            newObj.SetWeaponTypes(wt1, wt2);
            newObj.SetBaseStats(stats);
            newObj.SetPassiveSkills(null, null, psl1, psl2);
            IdsList[BC][id] = Classes.Count;
            Classes.Add(newObj);
        }
        private static void BattlerClassU(SQLiteDataReader data, int i)
        {
            BattlerClass uc1 = GetObj(Classes, BC, data["UpgradedClass1"]);
            BattlerClass uc2 = GetObj(Classes, BC, data["UpgradedClass2"]);
            PassiveSkill ps1 = GetObj(PassiveSkills, PS, data["PassiveSkill1"]);
            PassiveSkill ps2 = GetObj(PassiveSkills, PS, data["PassiveSkill2"]);
            int psl1 = Classes[i].PassiveSkillLvlRequired1;
            int psl2 = Classes[i].PassiveSkillLvlrequired2;
            List<int> skills = ReadList("BattlerClass", "Skill", Int(data["BattlerClass_ID"]), "LevelRequired");
            Classes[i].SetUpgradedClasses(uc1, uc2);
            Classes[i].SetPassiveSkills(ps1, ps2, psl1, psl2);
            for (int j = 0; j < skills.Count;) Classes[i].AddSkill(GetObj(Skills, SK, skills[j++]), skills[j++]);
        }

        private static void EnemyC(SQLiteDataReader data)
        {

        }
        private static void EnemyU(SQLiteDataReader data, int i)
        {

        }

        private static void EnvironmentC(SQLiteDataReader data)
        {

        }
        private static void EnvironmentU(SQLiteDataReader data, int i)
        {

        }

        private static void ItemC(SQLiteDataReader data)
        {

        }
        private static void ItemU(SQLiteDataReader data, int i)
        {

        }

        private static void PassiveSkillC(SQLiteDataReader data)
        {

        }
        private static void PassiveSkillU(SQLiteDataReader data, int i)
        {

        }

        private static void PlayerC(SQLiteDataReader data)
        {

        }
        private static void PlayerU(SQLiteDataReader data, int i)
        {

        }

        private static void SkillC(SQLiteDataReader data)
        {

        }
        private static void SkillU(SQLiteDataReader data, int i)
        {

        }

        private static void StateC(SQLiteDataReader data)
        {

        }
        private static void StateU(SQLiteDataReader data, int i)
        {

        }

        private static void WeaponC(SQLiteDataReader data)
        {

        }
        private static void WeaponU(SQLiteDataReader data, int i)
        {

        }
    }
}