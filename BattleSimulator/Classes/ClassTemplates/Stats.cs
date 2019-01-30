using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.ListManager;

namespace BattleSimulator.Classes.ClassTemplates
{
    public class Stats
    {
        public static class StatTable
        {
            public static int[] MinMHP = { 10, 19, 22, 25, 28, 31, 37, 40, 49 };
            public static int[] MaxMHPLow = { 45, 275, 375, 475, 600, 750, 950, 1100, 1500 };
            public static int[] MaxMHPHigh = { 55, 325, 425, 525, 700, 850, 1050, 1300, 1700 };
            public static int[] NatMHPMods = { -120, -80, -40, 0, 40, 80, 120 };

            public static int[] MinStat = { 2, 4, 4, 5, 6, 6, 8, 8, 10 };
            public static int[] MaxStatLow = { 45, 90, 115, 140, 175, 210, 240, 270, 320 };
            public static int[] MaxStatHigh = { 55, 110, 135, 160, 195, 230, 260, 290, 340 };
            public static int[] NatStatMods = { -60, -40, -20, 0, 20, 40, 60 };

            public static int SetMHPNorms(int level, int baseStat, int natMod)
            {
                double actualBase = baseStat / 10.0;
                int flooredBase = (int)actualBase;
                int min = MinMHP[flooredBase] - natMod * 4;
                int maxMhpLow = MaxMHPLow[flooredBase];
                int maxMhpHigh = MaxMHPHigh[flooredBase];
                double max = maxMhpLow + (actualBase - flooredBase) * (maxMhpHigh - maxMhpLow) + natMod * 40;
                double result = min + (level - 1) * ((int)max - min) / 99;
                return (int)(Math.Round(result));
            }
            public static int SetStatNorms(int level, int baseStat, int natMod)
            {
                double actualBase = baseStat / 10.0;
                int flooredBase = (int)actualBase;
                int min = MinStat[flooredBase] - natMod * 2;
                int maxStatLow = MaxStatLow[flooredBase];
                int maxStatHigh = MaxStatHigh[flooredBase];
                double max = maxStatLow + (actualBase - flooredBase) * (maxStatHigh - maxStatLow) + natMod * 20;
                double result = min + (level - 1) * ((int)max - min) / 99;
                return (int)(Math.Round(result));
            }
            public static int SetOtherStatNorms(int baseStat, int natMod)
            {
                return baseStat * natMod / 10000;
            }
        }


        private const int NUMBER_OF_REGULAR_STATS = 8;
        private const int NUMBER_OF_STATS = 12;

        private int[] StatsList;
        public int MaxHP { get { return StatsList[0]; } }
        public int Atk { get { return StatsList[1]; } }
        public int Def { get { return StatsList[2]; } }
        public int Map { get { return StatsList[3]; } }
        public int Mar { get { return StatsList[4]; } }
        public int Spd { get { return StatsList[5]; } }
        public int Tec { get { return StatsList[6]; } }
        public int Luk { get { return StatsList[7]; } }
        public int Acc { get { return StatsList[8]; } }
        public int Eva { get { return StatsList[9]; } }
        public int Crt { get { return StatsList[10]; } }
        public int Cev { get { return StatsList[11]; } }
        

        public Stats()
        {
            StatsList = new int[NUMBER_OF_STATS];
            for (int i = 0; i < NUMBER_OF_REGULAR_STATS; i++) StatsList[i] = 0;
            for (int i = NUMBER_OF_REGULAR_STATS; i < NUMBER_OF_STATS; i++) StatsList[i] = 100;
        }

        public Stats(System.Data.SQLite.SQLiteDataReader data)
        {
            StatsList = new int[NUMBER_OF_STATS];
            StatsList[0] = Int(data["HP"]);
            StatsList[1] = Int(data["Atk"]);
            StatsList[2] = Int(data["Def"]);
            StatsList[3] = Int(data["Map"]);
            StatsList[4] = Int(data["Mar"]);
            StatsList[5] = Int(data["Spd"]);
            StatsList[6] = Int(data["Tec"]);
            StatsList[7] = Int(data["Luk"]);
            StatsList[8] = Int(data["Acc"]);
            StatsList[9] = Int(data["Eva"]);
            StatsList[10] = Int(data["Crt"]);
            StatsList[11] = Int(data["Cev"]);
        }

        public Stats(Stats original)
        {
            StatsList = new int[NUMBER_OF_STATS];
            StatsList[0] = original.MaxHP;
            StatsList[1] = original.Atk;
            StatsList[2] = original.Def;
            StatsList[3] = original.Map;
            StatsList[4] = original.Mar;
            StatsList[5] = original.Spd;
            StatsList[6] = original.Tec;
            StatsList[7] = original.Luk;
            StatsList[8] = original.Acc;
            StatsList[9] = original.Eva;
            StatsList[10] = original.Crt;
            StatsList[11] = original.Cev;
        }

        public Stats(int level, Stats baseStats, Stats natMods=null)
        {
            StatsList = new int[NUMBER_OF_STATS];
            if (baseStats == null) return;
            Stats natStats = natMods == null ? new Stats() : natMods;
            StatsList[0] = StatTable.SetMHPNorms(level, baseStats.MaxHP, natStats.MaxHP);
            StatsList[1] = StatTable.SetStatNorms(level, baseStats.Atk, natStats.Atk);
            StatsList[2] = StatTable.SetStatNorms(level, baseStats.Def, natStats.Def);
            StatsList[3] = StatTable.SetStatNorms(level, baseStats.Map, natStats.Map);
            StatsList[4] = StatTable.SetStatNorms(level, baseStats.Mar, natStats.Mar);
            StatsList[5] = StatTable.SetStatNorms(level, baseStats.Spd, natStats.Spd);
            StatsList[6] = StatTable.SetStatNorms(level, baseStats.Tec, natStats.Tec);
            StatsList[7] = StatTable.SetStatNorms(level, baseStats.Luk, natStats.Luk);
            StatsList[8] = StatTable.SetOtherStatNorms(baseStats.Acc, natStats.Acc);
            StatsList[9] = StatTable.SetOtherStatNorms(baseStats.Eva, natStats.Eva);
            StatsList[10] = StatTable.SetOtherStatNorms(baseStats.Crt, natStats.Crt);
            StatsList[11] = StatTable.SetOtherStatNorms(baseStats.Cev, natStats.Cev);
        }
        
        
        public void NormalizeRateTo0()
        {
            for (int i = 0; i < NUMBER_OF_STATS; i++) StatsList[i] -= 100;
        }

        private delegate int DoOperation(int a, int b);
        private List<int> ExecuteOperation(Stats other, DoOperation opFunc)
        {
            List<int> statsThatChanged = new List<int>();
            int[] statChanges = new int[] { other.MaxHP, other.Atk, other.Def, other.Map, other.Mar,
                    other.Spd, other.Tec, other.Luk, other.Acc, other.Crt, other.Eva, other.Cev };
            for (int i = 0; i<statChanges.Length; i++)
            {
                if (statChanges[i] == 0) continue;
                StatsList[i] = opFunc(StatsList[i], statChanges[i]);
                statsThatChanged.Add(i);
            }
            return statsThatChanged;
        }

        private int Add(int a, int b) { return a + b; }
        public List<int> Add(Stats other) { return ExecuteOperation(other, Add); }

        private int Subtract(int a, int b) { return a - b; }
        public List<int> Subtract(Stats other) { return ExecuteOperation(other, Subtract); }

        private int Multiply(int a, int b) { return a * b; }
        public List<int> Multiply(Stats other) { return ExecuteOperation(other, Multiply); }

        private int Divide(int a, int b) { return a / b; }
        public List<int> Divide(Stats other) { return ExecuteOperation(other, Divide); }

        public void Add(int index, double multiplier) { StatsList[index] = (int)(StatsList[index] * multiplier); }
        public void Subtract(int index, double multiplier) { StatsList[index] = (int)(StatsList[index] * multiplier); }
        public void Multiply(int index, double multiplier) { StatsList[index] = (int)(StatsList[index] * multiplier); }
        public void Divide(int index, double multiplier) { StatsList[index] = (int)(StatsList[index] * multiplier); }
    }
}