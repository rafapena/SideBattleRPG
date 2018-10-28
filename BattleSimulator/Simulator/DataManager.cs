using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes;

namespace BattleSimulator.Simulator
{
    public static class DataManager
    {
        public static List<BattlerClass> BattlerClasses { get; private set; }
        public static List<Enemy> Enemies { get; private set; }
        public static List<Classes.Environment> Environments { get; private set; }
        public static List<Item> Items { get; private set; }
        public static List<PassiveSkill> PassiveSkills { get; private set; }
        public static List<Player> Players { get; private set; }
        public static List<Skill> Skills { get; private set; }
        public static List<State> States { get; private set; }
        public static List<Weapon> Weapons { get; private set; }

        public static void Setup()
        {

        }
    }
}
