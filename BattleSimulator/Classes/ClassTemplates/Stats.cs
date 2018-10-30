using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSimulator.Classes.ClassTemplates
{
    public class Stats
    {
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


        private Stats() { }
        public Stats(int maxhp, int atk, int def, int map, int mar, int spd, int tec, int luk)
        {
            StatsList = new int[12];
            StatsList[0] = maxhp;
            StatsList[1] = atk;
            StatsList[2] = def;
            StatsList[3] = map;
            StatsList[4] = mar;
            StatsList[5] = spd;
            StatsList[6] = tec;
            StatsList[7] = luk;
            StatsList[8] = 100;
            StatsList[9] = 100;
            StatsList[10] = 100;
            StatsList[11] = 100;
        }
        public void SetModifiables(int acc, int eva, int crt, int cev)
        {
            StatsList[8] = acc;
            StatsList[9] = eva;
            StatsList[10] = crt;
            StatsList[11] = cev;
        }

        public Stats(Stats original)
        {
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

        public Stats TranslatedBaseStat(Stats baseStats, Stats naturalStats)
        {
            return null;
        }

        private int TranslateHP()
        {
            return 0;
        }

        private int TranslateStats(int statIndex)
        {
            return 0;
        }
    }
}