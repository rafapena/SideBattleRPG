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
        // Retrieved from DB
        public List<int> ElementRates { get; protected set; }
        public List<int> StateRates { get; protected set; }

        // Retrieved from battle simulator
        public BattlerClass Class { get; protected set; }
        public int HP { get; protected set; }
        public int SP { get; protected set; }
        public int Level { get; protected set; }
        public int ZPosition { get; protected set; }
        public int XPosition { get; protected set; }
        public Stats Stats { get; protected set; }
        public Stats StatBoosts { get; private set; }
        public List<Skill> Skills { get; protected set; }
        public List<Item> Items { get; protected set; }
        public List<Weapon> Weapons { get; protected set; }
        public List<PassiveSkill> PassiveSkills { get; protected set; }

        // Overall battle info
        public List<Battler> ComboPartners { get; set; }
        public Skill SelectedSkill { get; set; }
        public Item SelectedItem { get; set; }
        public Weapon SelectedWeapon { get; set; }
        public List<Battler> SelectedTargets { get; set; }
        public List<State> States { get; protected set; }
        public int MovingLocation { get; set; }

        // Action execution info
        public bool ExecutedAction { get; set; }
        public int CriticalHitRatio { get; set; }
        public List<List<int>> TargetResults { get; private set; }

        // PassiveEffect dependent info
        public PassiveEffect Effect { get; private set; }
        public List<int>[] ContactSpread { get; private set; }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Initializers --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            TargetResults = new List<List<int>>();
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
            StatBoosts = Clone(original.StatBoosts, o => new Stats(o));
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
            ExecutedAction = original.ExecutedAction;
            CriticalHitRatio = original.CriticalHitRatio;
            TargetResults = Clone(original.TargetResults, o => new List<int>(o));
            Effect = Clone(original.Effect, o => new PassiveEffect(o));
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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Add/Remove List Components --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public int AddSkill(List<Skill> skillsList, int id)
        {
            if (!ValidListInput(skillsList, id)) return -1;
            Skills.Add(new Skill(skillsList[id]));
            return Skills.Last().Id;
        }
        public int RemoveSkill(int listIndex)
        {
            if (!ValidListInput(Skills, listIndex)) return -1;
            Skill toRemove = Skills[listIndex];
            Skills.RemoveAt(listIndex);
            return toRemove.Id;
        }

        public int AddItem(List<Item> itemsList, int id)
        {
            if (!ValidListInput(itemsList, id)) return -1;
            Items.Add(new Item(itemsList[id]));
            return Items.Last().Id;
        }
        public int RemoveItem(int listIndex)
        {
            if (!ValidListInput(Items, listIndex)) return -1;
            Item toRemove = Items[listIndex];
            Items.RemoveAt(listIndex);
            return toRemove.Id;
        }

        public int AddWeapon(List<Weapon> weaponsList, int id)
        {
            if (!ValidListInput(weaponsList, id)) return -1;
            Weapons.Add(new Weapon(weaponsList[id]));
            return Weapons.Last().Id;
        }
        public int RemoveWeapon(int listIndex)
        {
            if (!ValidListInput(Weapons, listIndex)) return -1;
            Weapon toRemove = Weapons[listIndex];
            Weapons.RemoveAt(listIndex);
            return toRemove.Id;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Add/Remove Passive Effect Components --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public int AddPassiveSkill(List<PassiveSkill> pSkillsList, int id)
        {
            if (!ValidListInput(pSkillsList, id)) return -1;
            PassiveSkills.Add(new PassiveSkill(pSkillsList[id]));
            AddPassiveEffects(PassiveSkills.Last());
            return PassiveSkills.Last().Id;
        }
        public int RemovePassiveSkill(int listIndex)
        {
            if (!ValidListInput(PassiveSkills, listIndex)) return -1;
            PassiveSkill toRemove = PassiveSkills[listIndex];
            RemovePassiveEffects(toRemove);
            PassiveSkills.RemoveAt(listIndex);
            return toRemove.Id;
        }

        public int AddState(List<State> statesList, int id)
        {
            if (!ValidListInput(statesList, id)) return -1;
            State newS = new State(statesList[id]);
            foreach (State existingState in States)
            {
                if (newS.Id != existingState.Id) continue;
                if (existingState.Stack >= existingState.MaxStack) return -1;
                existingState.Stack++;
                existingState.TurnsLeft++;
                AddStateStackEffect(existingState);
                return existingState.Id;
            }
            newS.TurnsLeft = RandInt(newS.TurnEnd1, newS.TurnEnd2);
            AddPassiveEffects(newS);
            States.Add(newS);
            return newS.Id;
        }
        public int RemoveState(int listIndex)
        {
            if (!ValidListInput(States, listIndex)) return -1;
            State toRemove = States[listIndex];
            RemoveStateStackEffect(toRemove);
            RemovePassiveEffects(toRemove);
            States.RemoveAt(listIndex);
            return toRemove.Id;
        }

        private void AddPassiveEffects(PassiveEffect pe)
        {

        }
        private void RemovePassiveEffects(PassiveEffect pe)
        {

        }

        private void AddStateStackEffect(State s)
        {

        }
        private void RemoveStateStackEffect(State s)
        {

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Tool Actions --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ExecuteTool(Environment e)
        {
            TargetResults.Clear();
            if (SelectedSkill != null) ExecuteSkill(e);
            else if (SelectedItem != null) ExecuteItem(e);
        }

        private void ExecuteSkill(Environment e)
        {
            Skill sk = SelectedSkill;
            sk.StartCharge();
            if (sk.ChargeCount > 0)
            {
                sk.Charge1Turn();
                return;
            }
            sk.DisableForCooldown();
            foreach (Battler p in ComboPartners)
            {
                p.ChangeSP(sk.SPConsume);
                if (sk.ShareTurns) p.ExecutedAction = true;
            }
            sk.SummonPlayers();
            sk.SummonEnemies();
            if (sk.Scope == 2)
            {
                ApplyToolEffects(SelectedTargets[0], sk, e);
                for (int i = 1; i < SelectedTargets.Count; i++)
                {
                    ApplyToolEffects(SelectedTargets[i], sk, e, 0.5);
                    TargetResults.Last().Add(sk.Steal ? 1 : 0);
                }
                return;
            }
            foreach (Battler b in SelectedTargets)
            {
                ApplyToolEffects(b, sk, e);
                TargetResults.Last().Add(sk.Steal ? 1 : 0);
            }
        }

        private void ExecuteItem(Environment e)
        {
            Item it = SelectedItem;
            if (it.Scope == 2)
            {
                ApplyToolEffects(SelectedTargets[0], it, e);
                for (int i = 1; i < SelectedTargets.Count; i++) ApplyToolEffects(SelectedTargets[i], it, e, 0.5);
            }
            else foreach (Battler b in SelectedTargets) ApplyToolEffects(b, it, e);
            Stats.Add(it.PermantentStatChanges);
            if (it.TurnsInto != null) Items[Items.FindIndex(x => x.Id == it.Id)] = new Item(it.TurnsInto);
            else if (it.Consumable) Items.Remove(it);
        }
        
        private void ApplyToolEffects(Battler b, Tool t, Environment e, double effect=1.0)
        {
            List<int> resultForTarget = new List<int>();
            if (!t.Hit(this, b, e))
            {
                resultForTarget.Add(-t.Type);
                return;
            }
            CriticalHitRatio = t.CriticalHitRatio(this, b, e);
            resultForTarget.Add(CriticalHitRatio);
            resultForTarget.Add(t.GetToolFormula(this, b, e));
            List<int>[] states = t.TriggeredStates(this, b, e);
            resultForTarget.Add(states[0].Count);
            foreach (int stateGiveId in states[0]) resultForTarget.Add(stateGiveId);
            resultForTarget.Add(states[1].Count);
            foreach (int stateReceiveId in states[1]) resultForTarget.Add(stateReceiveId);
            TargetResults.Add(resultForTarget);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- General HP/SP Management --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ChangeHP(int val)
        {
            HP += val;
            if (HP < 0) HP = 0;
            else if (HP > Stats.MaxHP) HP = Stats.MaxHP;
        }

        public void ChangeSP(int val)
        {
            SP += val;
            if (SP < 0) SP = 0;
            else if (SP > 100) SP = 100;
        }

        public bool IsConscious()
        {
            foreach (State s in States) if (s.KO || s.Petrify) return false;
            return HP > 0;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Applying Passive Effects --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ApplyStartActionEffects()
        {
            //foreach (State s in States) if (s.) ;
            //foreach (PassiveSkill p in PassiveSkills) if () ;
        }
        public void ApplyEndActionEffects()
        {

        }
        public void ApplyEndTurnEffects()
        {

        }

        private void ApplyEffects()
        {

        }

        /*public double ElementRate(int elementId)
        {
            double eRate = ElementRates[elementId];
            foreach (State s in States) eRate *= s.ElementRates[elementId] / 100.0;
            foreach (PassiveSkill p in PassiveSkills) eRate *= p.ElementRates[elementId] / 100.0;
            return eRate;
        }

        public double StateRate(int stateId)
        {
            double sRate = StateRates[stateId];
            foreach (State s in States) sRate *= s.StateRates[stateId] / 100.0;
            foreach (PassiveSkill p in PassiveSkills) sRate *= p.StateRates[stateId] / 100.0;
            return sRate;
        }*/


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Stat Management --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public int MaxHP() { return (Stats.MaxHP + StatBoosts.MaxHP) * Effect.StatModifiers.MaxHP / 100; }
        public int Atk() { return (Stats.Atk + StatBoosts.Atk) * Effect.StatModifiers.Atk / 100; }
        public int Def() { return (Stats.Def + StatBoosts.Def) * Effect.StatModifiers.Def / 100; }
        public int Map() { return (Stats.Map + StatBoosts.Map) * Effect.StatModifiers.Map / 100; }
        public int Mar() { return (Stats.Mar + StatBoosts.Mar) * Effect.StatModifiers.Mar / 100; }
        public int Spd() { return (Stats.Spd + StatBoosts.Spd) * Effect.StatModifiers.Spd / 100; }
        public int Tec() { return (Stats.Tec + StatBoosts.Tec) * Effect.StatModifiers.Tec / 100; }
        public int Luk() { return (Stats.Luk + StatBoosts.Luk) * Effect.StatModifiers.Luk / 100; }

        public int Acc() { return (Stats.Acc + StatBoosts.Acc) * Effect.StatModifiers.Acc / 100; }
        public int Eva() { return (Stats.Eva + StatBoosts.Eva) * Effect.StatModifiers.Eva / 100; }
        public int Crt() { return (Stats.Crt + StatBoosts.Crt) * Effect.StatModifiers.Crt / 100; }
        public int Cev() { return (Stats.Cev + StatBoosts.Cev) * Effect.StatModifiers.Cev / 100; }
    }
}
