using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes;
using BattleSimulator.Classes.ClassTemplates;
using static Database.AccessDB;
using static Database.Utilities.SQLDB;
using System.Data.SQLite;

namespace BattleSimulator.Utilities
{
    public static class DataManager
    {
        public static int Int(object o)
        {
            string res = o.ToString();
            return res == "" ? 0 : int.Parse(res);
        }
        public static double Dbl(object o)
        {
            return o.ToString() == "" ? 0 : (double)o;
        }

        public static T ReadObj<T>(List<T> list, object id) where T : BaseObject
        {
            return list == null || id == null ? null : list[Int(id)];
        }

        public static int[] ReadRatesList<T>(SQLiteDataReader data, string hostTable, List<T> targetTableList, string attribute, int normValue = 100, string tableNameExt = "")
        {
            if (targetTableList == null) return null;
            int[] result = new int[targetTableList.Count];
            string type = typeof(T).Name;
            if (type == "String")   
            {
                string[] typeList = data[attribute].ToString().Split('_');
                if (typeList[0] == "") return result;
                for (int i = 0; i < typeList.Length; i += 2)
                {
                    int typeListNumber = int.Parse(typeList[i]);
                    if (typeListNumber >= result.Length) continue;
                    result[typeListNumber] = int.Parse(typeList[i + 1]) - normValue;
                }
                return result;
            }
            List<int> listFromDB = ReadDBList(data, hostTable, type, attribute, tableNameExt);
            for (int i = 0; i < listFromDB.Count;) result[listFromDB[i++]] = listFromDB[i++] - normValue;
            return result;
        }

        public static List<int> ReadDBList(SQLiteDataReader data, string hostTable, string targetTable, string extraAttribute = "", string tableNameExt = "")
        {
            List<int> list = new List<int>();
            using (var conn0 = Connect())
            {
                conn0.Open();
                if (tableNameExt != "") tableNameExt = "_" + tableNameExt;
                string readText = "SELECT * FROM " + hostTable + "_To_" + targetTable + tableNameExt + " WHERE " + hostTable + "ID = " + data[hostTable + "_ID"].ToString();
                using (var data0 = Read(conn0, readText))
                {
                    while (data0.Read())
                    {
                        list.Add(Int(data0[targetTable + "ID"]));
                        if (extraAttribute != "") list.Add(Int(data0[extraAttribute]));
                    }
                }
                conn0.Close();
            }
            return list;
        }


        public static T ReadTool<T>(T tool, object toolId, List<BattlerClass> classesData, List<State> statesData) where T : Tool
        {
            if (toolId == null) return tool;
            toolId = Int(toolId);
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data0 = Read(conn0, "SELECT * FROM Tool WHERE Tool_ID = " + toolId))
                {
                    data0.Read();
                    tool.Setup(data0, classesData, statesData);
                }
                conn0.Close();
            }
            return tool;
        }

        public static P ReadPassiveEffect<P>(P pEffect, object pEffectId, List<string> elementsData, List<State> statesData) where P : PassiveEffect
        {
            if (pEffectId == null) return pEffect;
            pEffectId = Int(pEffectId);
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data0 = Read(conn0, "SELECT * FROM PassiveEffect WHERE PassiveEffect_ID = " + pEffectId))
                {
                    data0.Read();
                    pEffect.Setup(data0, elementsData, statesData);
                }
                conn0.Close();
            }
            return pEffect;
        }


        public static Stats ReadStats(object statsId, bool normalizeToZero)
        {
            Stats stats = null;
            if (statsId == null) return stats;
            else statsId = Int(statsId);
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data0 = Read(conn0, "SELECT * FROM Stats WHERE Stats_ID = " + statsId))
                {
                    data0.Read();
                    stats = new Stats(data0);
                    if (normalizeToZero) stats.NormalizeRateTo0();
                }
                conn0.Close();
            }
            return stats;
        }

        public static BattleEnemy ReadBattleEnemy(object id, List<Enemy> enemiesData, List<PassiveSkill> pSkillsData)
        {
            BattleEnemy battleEnemy;
            using (var conn0 = Connect())
            {
                conn0.Open();
                using (var data0 = Read(conn0, "SELECT * FROM BattleEnemy WHERE BattleEnemy_ID = " + id))
                {
                    data0.Read();
                    battleEnemy = new BattleEnemy(data0, enemiesData, pSkillsData);
                }
                conn0.Close();
            }
            return battleEnemy;
        }

        public static List<EnemyTool<T>> ReadEnemyTools<T>(object id, List<T> toolsData, List<State> statesData) where T : Tool
        {
            List<EnemyTool<T>> enemyTools = new List<EnemyTool<T>>();
            using (var conn0 = Connect())
            {
                conn0.Open();
                string tbl = typeof(T).Name;
                using (var data0 = Read(conn0, "SELECT * FROM BattleEnemy_To_Tool JOIN " + tbl + " WHERE BattleEnemyID = " + id + " AND " + tbl + ".ToolID = BattleEnemy_To_Tool.ToolID"))
                    while (data0.Read()) enemyTools.Add(new EnemyTool<T>(data0, toolsData, statesData));
                conn0.Close();
            }
            return enemyTools;
        }

        public static T ReadToolObj<T>(List<T> toolList, object toolId) where T : Tool
        {
            T tool = null;
            using (var conn1 = Connect())
            {
                conn1.Open();
                string tbl = typeof(T).Name;
                using (var data1 = Read(conn1, "SELECT * FROM Tool JOIN " + tbl + " WHERE Tool_ID = ToolID AND Tool_ID = " + toolId))
                {
                    data1.Read();
                    tool = ReadObj(toolList, data1[tbl + "_ID"]);
                }
                conn1.Close();
            }
            return tool;
        }
    }
}