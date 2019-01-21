using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;
using BattleSimulator.Utilities;

namespace BattleSimulator.Classes
{
    public class Enemy : Battler
    {
        public Stats ScaledStats { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Flying { get; private set; }
        public int BossType { get; private set; }
        public int Exp { get; private set; }
        public int Gold { get; private set; }

        public List<EnemyTool<Skill>> SkillAI { get; private set; }
        public List<EnemyTool<Item>> ItemAI { get; private set; }
        public List<EnemyTool<Weapon>> WeaponAI { get; private set; }
        

        public Enemy() : base()
        {
            SkillAI = new List<EnemyTool<Skill>>();
            ItemAI = new List<EnemyTool<Item>>();
            WeaponAI = new List<EnemyTool<Weapon>>();
        }
        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<string> elementsData, List<State> statesData, List<BattlerClass> classesData)
        {
            Initialize(data);
            Id = Int(data["Enemy_ID"]);
            ElementRates = ReadRatesList(data, "Enemy", elementsData, "ElementRates");
            StateRates = ReadRatesList(data, "Enemy", statesData, "Vulnerability");
            ScaledStats = ReadStats(data["ScaledStats"]);
            Class = ReadObj(classesData, data["EnemyClass"]);
        }

        public Enemy(Enemy original) : base(original)
        {
            ScaledStats = Clone(original.ScaledStats, o => new Stats(o));
            Width = original.Width;
            Height = original.Height;
            Flying = original.Flying;
            BossType = original.BossType;
            Exp = original.Exp;
            Gold = original.Gold;
            SkillAI = Clone(original.SkillAI, o => new EnemyTool<Skill>(o));
            ItemAI = Clone(original.ItemAI, o => new EnemyTool<Item>(o));
            WeaponAI = Clone(original.WeaponAI, o => new EnemyTool<Weapon>(o));
        }


        public void SetAllStats(int level, double hpMultiplier)
        {
            SetAllStats(level);
            if (Class != null) Stats = new Stats(level, Class.BaseStats, ScaledStats);
            else Stats = new Stats(level, ScaledStats);
            Stats.Multiply(0, hpMultiplier);
        }

        public void AddSkillAI(EnemyTool<Skill> ai)
        {

        }
        public void AddItemAI(EnemyTool<Item> ai)
        {

        }
        public void AddWeaponAI(EnemyTool<Item> ai)
        {

        }

        public void DecideMove(List<Player> players, List<Enemy> enemies)
        {
            SelectedTargets.Clear();
            SelectedSkill = Skills.Count > 0 ? Skills[0] : null;
            SelectedItem = null;
            SelectedWeapon = Weapons.Count > 0 ? Weapons[0] : null;
            SelectedTargets.Add(players[RandInt(0, players.Count - 1)]);
        }
    }
}
