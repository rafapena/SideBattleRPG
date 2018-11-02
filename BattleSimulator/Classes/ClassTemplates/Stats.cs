using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;

namespace BattleSimulator.Classes.ClassTemplates
{
    public class Stats
    {
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
            for (int i = 0; i < NUMBER_OF_STATS; i++) StatsList[i] = 0;
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
            StatsList[7] = original.Acc;
            StatsList[8] = original.Luk;
            StatsList[9] = original.Eva;
            StatsList[10] = original.Crt;
            StatsList[11] = original.Cev;
        }
        

        public Stats(int level, Stats baseStats, Stats NatMods)
        {
            StatsList = new int[NUMBER_OF_STATS];
            if (baseStats == null || NatMods == null) return;
            //return 0;
        }

        public void Multiply(int index, double multiplier)
        {
            StatsList[index] = (int)(StatsList[index] * multiplier);
        }
    }
}