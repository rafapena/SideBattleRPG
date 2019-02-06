using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static BattleSimulator.Utilities.ListManager;
using static BattleSimulator.Utilities.Conversion;
using static BattleSimulator.Utilities.RNG;
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
        public int Type { get; private set; }             // 0=Player, 1=Enemy, 2=Ally, 3=PlayerSummon
        public List<Battler> ComboPartners { get; set; }
        public Skill SelectedSkill { get; set; }
        public Item SelectedItem { get; set; }
        public Weapon SelectedWeapon { get; set; }
        public List<Battler> SelectedTargets { get; set; }
        public List<State> States { get; protected set; }
        public int MovingLocation { get; set; }

        // Action execution info
        public bool ExecutedAction { get; set; }
        public int CriticalHitRatio { get; private set; }
        public List<List<List<int>>> TargetResults { get; private set; }    // For every consecutive act, for every target, list traits
        public List<List<int>> EffectResults { get; private set; }          // For every target, list traits

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
            TargetResults = new List<List<List<int>>>();
            EffectResults = new List<List<int>>();
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
            Type = original.Type;
            ComboPartners = original.ComboPartners;
            SelectedSkill = Clone(original.SelectedSkill, o => new Skill(o));
            SelectedItem = Clone(original.SelectedItem, o => new Item(o));
            SelectedWeapon = Clone(original.SelectedWeapon, o => new Weapon(o));
            SelectedTargets = original.SelectedTargets;
            States = Clone(original.States, o => new State(o));
            MovingLocation = original.MovingLocation;
            ExecutedAction = original.ExecutedAction;
            CriticalHitRatio = original.CriticalHitRatio;
            TargetResults = Clone(original.TargetResults, o => new List<List<int>>(o));
            EffectResults = Clone(original.EffectResults, o => new List<int>(o));
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
        
        public void MoveToPosition(int z, int x)
        {
            int zNew = ZPosition + z;
            int xNew = XPosition + x;
            if (zNew < 0 || zNew > 2 || xNew < 0 || xNew > 2) return;
            ZPosition = zNew;
            XPosition = xNew;
        }

        public void ClearTurnChoices()
        {
            ComboPartners.Clear();
            SelectedSkill = null;
            SelectedItem = null;
            SelectedWeapon = null;
            SelectedTargets.Clear();
            MovingLocation = -1;
            CriticalHitRatio = 1;
            TargetResults.Clear();
            EffectResults.Clear();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Manage Types --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void SetAsPlayer() { Type = 0; }
        public void SetAsEnemy() { Type = 1; }
        public void SetAsAlly() { Type = 2; }
        public void SetAsPlayerSummon() { Type = 3; }

        public bool IsPlayer() { return Type == 0; }
        public bool IsEnemy() { return Type == 1; }
        public bool IsAlly() { return Type == 2; }
        public bool IsPlayerSummon() { return Type == 3; }


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
            State toAdd = new State(statesList[id]);
            foreach (State existingState in States)
            {
                if (toAdd.Id != existingState.Id) continue;
                if (existingState.Stack >= existingState.MaxStack) return -1;
                existingState.Stack++;
                existingState.TurnsLeft++;
                AddPassiveEffects(existingState);
                return existingState.Id;
            }
            toAdd.TurnsLeft = toAdd.TurnEnd2 > toAdd.TurnEnd1 ? RandInt(toAdd.TurnEnd1, toAdd.TurnEnd2) : -1;
            if (toAdd.KO || toAdd.Petrify) IsConscious = false;
            if (toAdd.Stun) CannotMove++;
            if (toAdd.ContactSpreadRate > 0) ContactSpread.AddRange(new int[] { toAdd.Id, toAdd.ContactSpreadRate });
            AddPassiveEffects(toAdd);
            States.Add(toAdd);
            return toAdd.Id;
        }
        public int RemoveState(int listIndex)
        {
            if (!ValidListInput(States, listIndex)) return -1;
            State toRemove = States[listIndex];
            if (toRemove.KO || toRemove.Petrify) IsConscious = true;
            if (toRemove.Stun) CannotMove--;
            for (int i = 0; i < ContactSpread.Count; i += 2)
                if (toRemove.Id == ContactSpread[i] && toRemove.ContactSpreadRate == ContactSpread[i + 1]) ContactSpread.RemoveRange(i, 2);
            for (int i = 0; i < States[listIndex].Stack; i++) RemovePassiveEffects(toRemove);
            States.RemoveAt(listIndex);
            return toRemove.Id;
        }

        public int AddPassiveEffects(PassiveEffect pe)
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
            return pe.Id;
        }
        public int RemovePassiveEffects(PassiveEffect pe)
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
            return pe.Id;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Tool Pre-Actions --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool RedirectUnconsciousTarget<T>(List<T> opponents) where T : Battler
        {
            int i;
            int removed = 0;
            bool removedMain = false;
            for (i = 0; i < SelectedTargets.Count; i++)
            {
                if (SelectedTargets[i].IsConscious) continue;
                SelectedTargets.RemoveAt(i);
                removed++;
                if (i == 0) removedMain = true;
            }
            if (SelectedTargets.Count > 0) return true;
            i = 0;
            int scope = 0;
            int randomActs = 0;
            if (SelectedSkill != null) scope = SelectedSkill.Scope;
            else if (SelectedItem != null) scope = SelectedItem.Scope;
            if (SelectedSkill != null) randomActs = SelectedSkill.RandomActs;
            else if (SelectedItem != null) randomActs = SelectedItem.RandomActs;
            switch (scope)
            {
                case 1:
                case 2:
                    while (i < opponents.Count && !opponents[i].IsConscious) i++;
                    if (i == opponents.Count) return false;
                    if (scope == 1 && randomActs > 0) RedirectRandomScope(opponents, removed);
                    else if (scope == 2 && removedMain) RedirectScopeSplash(opponents, i);
                    else SelectedTargets.Add(opponents[i]);
                    break;
                case 3:
                case 4:
                    int redirected = -1;
                    for (i = 0; i < opponents.Count; i++)
                    {
                        int pos = scope == 3 ? opponents[i].XPosition : opponents[i].ZPosition;
                        if (!opponents[i].IsConscious || redirected >= 0 && redirected != pos) continue;
                        SelectedTargets.Add(opponents[i]);
                        redirected = pos;
                    }
                    break;
            }
            return true;
        }

        // Helper function for RedirectUnconsciousTarget
        private void RedirectRandomScope<T>(List<T> opponents, int toReplace) where T : Battler
        {
            while (toReplace > 0)
            {
                T newTarget = RandList(opponents);
                if (!newTarget.IsConscious) continue;
                SelectedTargets.Add(newTarget);
                toReplace--;
            }
        }

        // Helper function for RedirectUnconsciousTarget
        private void RedirectScopeSplash<T>(List<T> opponents, int i) where T : Battler
        {
            T newTarget = opponents[i];
            SelectedTargets.Clear();
            SelectedTargets.Add(newTarget);
            foreach (T oneTarget in opponents)
            {
                int xDist = Math.Abs(oneTarget.XPosition - newTarget.XPosition);
                int zDist = Math.Abs(oneTarget.ZPosition - newTarget.ZPosition);
                if (xDist > 1 && zDist > 1) continue;
                if (xDist != 0 || zDist != 0) SelectedTargets.Add(newTarget);
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Tool Actions --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Tool ExecuteTool()
        {
            TargetResults.Clear();
            if (SelectedSkill != null) return ExecuteSkill();
            else if (SelectedItem != null) return ExecuteItem();
            return null;
        }

        private Skill ExecuteSkill()
        {
            Skill sk = SelectedSkill;
            sk.StartCharge();
            if (sk.ChargeCount > 0)
            {
                sk.Charge1Turn();
                return sk;
            }
            sk.DisableForCooldown();
            foreach (Battler p in ComboPartners)
            {
                p.ChangeSP(sk.SPConsume);
                if (sk.ShareTurns) p.ExecutedAction = true;
            }
            sk.SummonPlayers();
            sk.SummonEnemies();
            ApplyToolEffectsPerTarget(sk, ExecuteSteal);
            return sk;
        }
        private List<int> ExecuteSteal(List<int> oneActResult, Battler target, double effectMagnitude)
        {
            double willSteal = effectMagnitude * 100 * Luk() / target.Luk();
            oneActResult.Add(SelectedSkill.Steal && Chance((int)willSteal) ? 1 : 0);
            return oneActResult;
        }

        private Item ExecuteItem()
        {
            Item it = SelectedItem;
            ApplyToolEffectsPerTarget(it);
            Stats.Add(it.PermantentStatChanges);
            if (it.TurnsInto != null) Items[Items.FindIndex(x => x.Id == it.Id)] = new Item(it.TurnsInto);
            else if (it.Consumable) Items.Remove(it);
            return it;
        }


        private delegate List<int> ApplyExtra(List<int> extraResults, Battler b, double effectMagnitude);

        private void ApplyToolEffectsPerTarget(Tool selectedTool, ApplyExtra extraFunc = null)
        {
            int consecutiveActs = selectedTool.ConsecutiveActs;
            double decliningTimes = (selectedTool.Scope != 1 ? selectedTool.RandomActs : 0) + 1.0;
            if (SelectedWeapon != null)
            {
                consecutiveActs *= SelectedWeapon.ConsecutiveActs;
                decliningTimes *= (SelectedWeapon.Scope != 1 ? SelectedWeapon.RandomActs : 0) + 1.0;
            }
            for (int times = 1; times <= decliningTimes; times++)
            {
                if (!Chance(100 / times)) continue;
                double eMag = 1.0 / times;
                double eMag0 = eMag / (selectedTool.Scope != 2 ? 1.0 : 2.0);
                for (int i = 0; i < consecutiveActs; i++)
                {
                    List<List<int>> resultForOneAct = new List<List<int>>();
                    int j = 0;
                    resultForOneAct.Add(ApplyToolEffects(SelectedTargets[j++], selectedTool, eMag, extraFunc));
                    while (j < SelectedTargets.Count) resultForOneAct.Add(ApplyToolEffects(SelectedTargets[j++], selectedTool, eMag0, extraFunc));
                    TargetResults.Add(resultForOneAct);
                }
            }
        }
        
        private List<int> ApplyToolEffects(Battler b, Tool t, double effectMagnitude = 1.0, ApplyExtra extraFunc = null)
        {
            List<int> oneTarget = new List<int>();
            if (t.Hit(this, b, effectMagnitude))
            {
                CriticalHitRatio = t.CriticalHitRatio(this, b, effectMagnitude);
                oneTarget.Add(CriticalHitRatio);
                oneTarget.Add(t.ElementMagnitude(b));
                oneTarget.Add(t.GetToolFormula(this, b, effectMagnitude));
                List<int>[] states = t.TriggeredStates(this, b, effectMagnitude);
                oneTarget.Add(states[0].Count);
                foreach (int stateGiveId in states[0]) oneTarget.Add(stateGiveId);
                oneTarget.Add(states[1].Count);
                foreach (int stateReceiveId in states[1]) oneTarget.Add(stateReceiveId);
                if (extraFunc != null) oneTarget = extraFunc(oneTarget, b, effectMagnitude);
            }
            else oneTarget.Add(-t.Type);
            return oneTarget;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- General HP/SP Management --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MaxHPSP()
        {
            IsConscious = true;
            HP = Stats.MaxHP;
            SP = 100;
        }

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
            return CannotMove <= 0 && IsConscious;
        }

        public void ApplyStartActionEffects(Environment e)
        {
            //foreach (State s in States) if (s.) ;
            //foreach (PassiveSkill p in PassiveSkills) if () ;
        }
        public void ApplyEndActionEffects(Environment e)
        {

        }
        public void ApplyEndTurnEffects(Environment e)
        {

        }

        private void ApplyEffects(Environment e)
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

        public int Acc() { return (Stats.Acc + StatBoosts.Acc) * (StatModifiers.Acc + 100) / 100; }
        public int Eva() { return (Stats.Eva + StatBoosts.Eva) * (StatModifiers.Eva + 100) / 100; }
        public int Crt() { return (Stats.Crt + StatBoosts.Crt) * (StatModifiers.Crt + 100) / 100; }
        public int Cev() { return (Stats.Cev + StatBoosts.Cev) * (StatModifiers.Cev + 100) / 100; }
    }
}