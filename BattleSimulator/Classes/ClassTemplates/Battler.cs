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
        // Retrieved from DB (Also PassiveEffect dependent)
        public int[] ElementRates { get; protected set; }
        public int[] StateRates { get; protected set; }

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
        public Stats StatModifiers { get; protected set; }
        public bool IsConscious { get; private set; }
        public int CannotMove { get; private set; }
        public int SPConsumeRate { get; private set; }
        public int ComboDifficulty { get; private set; }
        public int Counter { get; private set; }
        public int Reflect { get; private set; }
        public int ExtraTurns { get; private set; }
        public List<int> DisabledToolTypes { get; private set; }
        public List<int> RemoveByHit { get; private set; }
        public List<int> ContactSpread { get; private set; }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Initializers --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Battler() : base()
        {
            StatBoosts = new Stats();
            Skills = new List<Skill>();
            Items = new List<Item>();
            Weapons = new List<Weapon>();
            PassiveSkills = new List<PassiveSkill>();
            States = new List<State>();
            ComboPartners = new List<Battler>();
            SelectedTargets = new List<Battler>();
            TargetResults = new List<List<int>>();
            StatModifiers = new Stats();
            DisabledToolTypes = new List<int>();
            RemoveByHit = new List<int>();
            ContactSpread = new List<int>();
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
            StatModifiers = Clone(original.StatModifiers, o => new Stats(o));
            IsConscious = original.IsConscious;
            CannotMove = original.CannotMove;
            SPConsumeRate = original.SPConsumeRate;
            ComboDifficulty = original.ComboDifficulty;
            Counter = original.Counter;
            Reflect = original.Reflect;
            ExtraTurns = original.ExtraTurns;
            DisabledToolTypes = Clone(original.DisabledToolTypes, o => new List<int>(o));
            ContactSpread = Clone(original.ContactSpread, o => new List<int>(o));
            RemoveByHit = Clone(original.RemoveByHit, o => new List<int>(o));
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
            IsConscious = true;
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
            newS.TurnsLeft = newS.TurnEnd2 > 0 ? RandInt(newS.TurnEnd1, newS.TurnEnd2) : -1;
            if (newS.KO || newS.Petrify) IsConscious = false;
            if (newS.Stun) CannotMove++;
            if (newS.ContactSpreadRate > 0) ContactSpread.AddRange(new int[] { newS.Id, newS.ContactSpreadRate });
            AddPassiveEffects(newS);
            States.Add(newS);
            return newS.Id;
        }
        public int RemoveState(int listIndex)
        {
            if (!ValidListInput(States, listIndex)) return -1;
            State toRemove = States[listIndex];
            if (toRemove.KO || toRemove.Petrify) IsConscious = true;
            if (toRemove.Stun) CannotMove--;
            for (int i = 0; i < ContactSpread.Count; i += 2)
                if (toRemove.Id == ContactSpread[i] && toRemove.ContactSpreadRate == ContactSpread[i + 1]) ContactSpread.RemoveRange(i, 2);
            RemoveStateStackEffect(toRemove);
            RemovePassiveEffects(toRemove);
            States.RemoveAt(listIndex);
            return toRemove.Id;
        }

        public void AddPassiveEffects(PassiveEffect pe)
        {
            for (int i = 0; i < ElementRates.Length; i++) ElementRates[i] += pe.ElementRates[i];
            for (int i = 0; i < StateRates.Length; i++) StateRates[i] += pe.StateRates[i];
            StatModifiers.Add(pe.StatModifiers);
            SPConsumeRate += pe.SPConsumeRate;
            ComboDifficulty += pe.ComboDifficulty;
            Counter += pe.Counter;
            Reflect += pe.Reflect;
            ExtraTurns += pe.ExtraTurns;
            if (pe.DisabledToolType1 > 0) DisabledToolTypes.Add(pe.DisabledToolType1);
            if (pe.DisabledToolType2 > 0) DisabledToolTypes.Add(pe.DisabledToolType2);
            if (pe.RemoveByHit > 0) RemoveByHit.AddRange(new int[] { pe.Id, pe.RemoveByHit });
        }
        public void RemovePassiveEffects(PassiveEffect pe)
        {
            for (int i = 0; i < ElementRates.Length; i++) ElementRates[i] -= pe.ElementRates[i];
            for (int i = 0; i < StateRates.Length; i++) StateRates[i] -= pe.StateRates[i];
            if (pe.StatModifiers != null) StatModifiers.Subtract(pe.StatModifiers);
            SPConsumeRate -= pe.SPConsumeRate;
            ComboDifficulty -= pe.ComboDifficulty;
            Counter -= pe.Counter;
            Reflect -= pe.Reflect;
            ExtraTurns -= pe.ExtraTurns;
            bool d1 = false;
            bool d2 = false;
            for (int i = 0; i < DisabledToolTypes.Count; i += 2)
            {
                if (pe.DisabledToolType1 == DisabledToolTypes[i] && !d1) { DisabledToolTypes.RemoveAt(i); d1 = true; }
                if (pe.DisabledToolType2 == DisabledToolTypes[i] && !d2) { DisabledToolTypes.RemoveAt(i); d2 = true; }
            }
            List<int> rbh = RemoveByHit;
            for (int i = 0; i < rbh.Count; i += 2) if (pe.Id == rbh[i] && pe.RemoveByHit == rbh[i + 1]) rbh.RemoveRange(i, 2);
        }

        private void AddStateStackEffect(State s)
        {
            //for (int i = 0; i < ElementRates.Count; i++) ElementRates[i] += s.ElementRates[i] / 2;
            //for (int i = 0; i < StateRates.Count; i++) StateRates[i] += s.StateRates[i] / 2;
            ExtraTurns += s.ExtraTurns;
        }
        private void RemoveStateStackEffect(State s)
        {
            ExtraTurns -= s.ExtraTurns;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Tool Actions --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ExecuteTool()
        {
            TargetResults.Clear();
            if (SelectedSkill != null) ExecuteSkill();
            else if (SelectedItem != null) ExecuteItem();
        }

        private void ExecuteSkill()
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
                ApplyToolEffects(SelectedTargets[0], sk);
                for (int i = 1; i < SelectedTargets.Count; i++)
                {
                    ApplyToolEffects(SelectedTargets[i], sk, 0.5);
                    //TargetResults.Last().Add(sk.Steal ? 1 : 0);
                }
                return;
            }
            foreach (Battler b in SelectedTargets)
            {
                ApplyToolEffects(b, sk);
                //TargetResults.Last().Add(sk.Steal ? 1 : 0);
            }
        }

        private void ExecuteItem()
        {
            Item it = SelectedItem;
            if (it.Scope == 2)
            {
                ApplyToolEffects(SelectedTargets[0], it);
                for (int i = 1; i < SelectedTargets.Count; i++) ApplyToolEffects(SelectedTargets[i], it, 0.5);
            }
            else foreach (Battler b in SelectedTargets) ApplyToolEffects(b, it);
            Stats.Add(it.PermantentStatChanges);
            if (it.TurnsInto != null) Items[Items.FindIndex(x => x.Id == it.Id)] = new Item(it.TurnsInto);
            else if (it.Consumable) Items.Remove(it);
        }
        
        private void ApplyToolEffects(Battler b, Tool t, double effect=1.0)
        {
            List<int> resultForTarget = new List<int>();
            if (!t.Hit(this, b))
            {
                resultForTarget.Add(-t.Type);
                return;
            }
            CriticalHitRatio = t.CriticalHitRatio(this, b);
            resultForTarget.Add(CriticalHitRatio);
            resultForTarget.Add(t.GetToolFormula(this, b));
            List<int>[] states = t.TriggeredStates(this, b);
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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Applying Passive Effects --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool CanMove()
        {
            return CannotMove <= 0;
        }

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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Stat Management --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public int MaxHP() { return NaturalNumber((Stats.MaxHP + StatBoosts.MaxHP) * (StatModifiers.MaxHP + 100) / 100); }
        public int Atk() { return NaturalNumber((Stats.Atk + StatBoosts.Atk) * (StatModifiers.Atk + 100) / 100); }
        public int Def() { return NaturalNumber((Stats.Def + StatBoosts.Def) * (StatModifiers.Def + 100) / 100); }
        public int Map() { return NaturalNumber((Stats.Map + StatBoosts.Map) * (StatModifiers.Map + 100) / 100); }
        public int Mar() { return NaturalNumber((Stats.Mar + StatBoosts.Mar) * (StatModifiers.Mar + 100) / 100); }
        public int Spd() { return NaturalNumber((Stats.Spd + StatBoosts.Spd) * (StatModifiers.Spd + 100) / 100); }
        public int Tec() { return NaturalNumber((Stats.Tec + StatBoosts.Tec) * (StatModifiers.Tec + 100) / 100); }
        public int Luk() { return NaturalNumber((Stats.Luk + StatBoosts.Luk) * (StatModifiers.Luk + 100) / 100); }

        public int Acc() { return NaturalNumber((Stats.Acc + StatBoosts.Acc) * (StatModifiers.Acc + 100) / 100); }
        public int Eva() { return NaturalNumber((Stats.Eva + StatBoosts.Eva) * (StatModifiers.Eva + 100) / 100); }
        public int Crt() { return NaturalNumber((Stats.Crt + StatBoosts.Crt) * (StatModifiers.Crt + 100) / 100); }
        public int Cev() { return NaturalNumber((Stats.Cev + StatBoosts.Cev) * (StatModifiers.Cev + 100) / 100); }
    }
}