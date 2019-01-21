using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static BattleSimulator.Utilities.Utils;
using System.Windows.Forms;

namespace BattleSimulator.Classes.ClassTemplates
{
    public abstract class Battler : BaseObject
    {
        public List<int> ElementRates { get; protected set; }
        public List<int> StateRates { get; protected set; }

        public BattlerClass Class { get; protected set; }
        public int HP { get; protected set; }
        public int SP { get; protected set; }
        public int Level { get; protected set; }
        public int ZPosition { get; protected set; }
        public int XPosition { get; protected set; }
        public Stats Stats { get; protected set; }
        public List<Skill> Skills { get; protected set; }
        public List<Item> Items { get; protected set; }
        public List<Weapon> Weapons { get; protected set; }
        public List<PassiveSkill> PassiveSkills { get; protected set; }
        public List<State> States { get; protected set; }

        public List<Battler> ComboPartners { get; set; }
        public Skill SelectedSkill { get; set; }
        public Item SelectedItem { get; set; }
        public Weapon SelectedWeapon { get; set; }
        public List<Battler> SelectedTargets { get; set; }
        public int MovingLocation { get; set; }

        public bool ExecutedAction { get; set; }


        public Battler() : base()
        {
            ElementRates = new List<int>();
            StateRates = new List<int>();
            Skills = new List<Skill>();
            Items = new List<Item>();
            Weapons = new List<Weapon>();
            PassiveSkills = new List<PassiveSkill>();
            States = new List<State>();
            ComboPartners = new List<Battler>();
            SelectedTargets = new List<Battler>();
        }

        public Battler(Battler original) : base(original)
        {
            Class = Clone(original.Class, o => new BattlerClass(o));
            HP = original.HP;
            SP = original.SP;
            Level = original.Level;
            ZPosition = original.ZPosition;
            XPosition = original.XPosition;
            Stats = Clone(original.Stats, o => new Stats(o));
            ElementRates = Clone(original.ElementRates);
            StateRates = Clone(original.StateRates);
            Skills = Clone(original.Skills, o => new Skill(o));
            Items = Clone(original.Items, o => new Item(o));
            Weapons = Clone(original.Weapons, o => new Weapon(o));
            PassiveSkills = Clone(original.PassiveSkills, o => new PassiveSkill(o));
            States = Clone(original.States, o => new State(o));
            SelectedSkill = Clone(original.SelectedSkill, o => new Skill(o));
            SelectedItem = Clone(original.SelectedItem, o => new Item(o));
            SelectedWeapon = Clone(original.SelectedWeapon, o => new Weapon(o));
            ComboPartners = new List<Battler>();   //Clone(original.ComboPartners, o => new Battler(o));
            SelectedTargets = new List<Battler>(); //Clone(original.SelectedTargets, o => new Battler(o));
        }
        
        public void ClarifyName(int asciiChar)
        {
            Name += " " + (char)asciiChar;
        }

        public void SetAllStats(int level)
        {
            Level = level;
        }

        public void MaxHPSP()
        {
            HP = Stats.MaxHP;
            SP = 100;
        }
        
        public void MoveToPosition(int z, int x)
        {
            int zNew = ZPosition + z;
            int xNew = XPosition + x;
            if (zNew < 0 || zNew > 2 || xNew < 0 || xNew > 2) return;
            ZPosition = zNew;
            XPosition = xNew;
        }

        public void AddSkill(List<Skill> skillsList, int id)
        {
            if (id > 0 && id < skillsList.Count) Skills.Add(new Skill(skillsList[id]));
        }

        public void RemoveSkill(int id)
        {

        }

        public void AddItem(List<Item> itemsList, int id)
        {
            if (id > 0 && id < itemsList.Count) Items.Add(new Item(itemsList[id]));
        }
        public void RemoveItem(int index)
        {

        }

        public void AddWeapon(List<Weapon> weaponsList, int id)
        {
            if (id > 0 && id < weaponsList.Count) Weapons.Add(new Weapon(weaponsList[id]));
        }
        public void RemoveWeapon(int id)
        {

        }

        public void AddPassiveSkill(List<PassiveSkill> pSkillsList, int id)
        {
            if (pSkillsList[id] != null && id > 0 && id < pSkillsList.Count) PassiveSkills.Add(new PassiveSkill(pSkillsList[id]));
        }
        public void RemovePassiveSkill(int id)
        {

        }

        public void AddState(List<State> statesList, int id)
        {
            if (id > 0 && id < statesList.Count) States.Add(new State(statesList[id]));
        }
        public void RemoveState(int id)
        {

        }

        public bool IsConscious()
        {
            foreach (State s in States) if (s.KO || s.Petrify) return false;
            return HP > 0;
        }
    }
}
