using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleSimulator.Classes.ClassTemplates
{
    public abstract class Battler : BaseObject
    {
        public int HP { get; private set; }
        public int SP { get; private set; }
        public int Level { get; private set; }
        public int ZPosition { get; private set; }
        public int XPosition { get; private set; }
        public Stats Stats { get; private set; }
        public List<int> ElementRates { get; private set; }
        public List<int> StateRates { get; private set; }
        public List<Skill> Skills { get; private set; }
        public List<Item> Items { get; private set; }
        public List<Weapon> Weapons { get; private set; }
        public List<PassiveSkill> PassiveSkills { get; private set; }

        public List<State> States { get; private set; }

        protected Battler() { }
        public Battler(int id, string name, string description, Bitmap image=null) : base(id, name, description, image)
        {
            ZPosition = 0;
            XPosition = 0;
            ElementRates = new List<int>();
            Skills = new List<Skill>();
            Items = new List<Item>();
            Weapons = new List<Weapon>();
            States = new List<State>();
            PassiveSkills = new List<PassiveSkill>();
        }
        public Battler(Battler original) : base(original)
        {
            HP = original.HP;
            SP = original.SP;
            Level = original.Level;
            ZPosition = original.ZPosition;
            XPosition = original.XPosition;
            Stats = new Stats(original.Stats);
            ElementRates = ClonePrimitiveList(original.ElementRates);
            Skills = CloneObjectList(original.Skills, o => new Skill(o));
            Items = CloneObjectList(original.Items, o => new Item(o));
            Weapons = CloneObjectList(original.Weapons, o => new Weapon(o));
            States = CloneObjectList(original.States, o => new State(o));
            PassiveSkills = CloneObjectList(original.PassiveSkills, o => new PassiveSkill(o));
        }
        public void SetStats(int level, Stats stats)
        {
            Level = level;
            Stats = stats;
            HP = Stats.MaxHP;
            SP = 100;
        }

        public string Position()
        {
            return ZPosition + "" + XPosition;
        }
        public void MoveToPosition(int z, int x)
        {
            int zNew = ZPosition + z;
            int xNew = XPosition + x;
            if (zNew > 0 || zNew > 2 || xNew < 0 || xNew > 2) return;
            ZPosition = zNew;
            XPosition = xNew;
        }

        public void SetElementRate(int element, int rate)
        {
            ElementRates[element] = rate;
        }

        public void AddSkill(int id)
        {

        }

        public void RemoveSkill(int id)
        {

        }

        public void AddItem(int id)
        {

        }

        public void RemoveItem(int id)
        {

        }

        public void AddWeapon(int id)
        {

        }

        public void RemoveWeapon(int id)
        {

        }

        public void AddState(int id)
        {

        }

        public void RemoveState(int id)
        {

        }

        public void AddPassiveSkill(int id)
        {

        }

        public void RemovePassiveSkill(int id)
        {

        }
    }
}
