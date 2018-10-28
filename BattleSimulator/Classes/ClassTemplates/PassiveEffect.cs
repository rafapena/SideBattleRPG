using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleSimulator.Classes.ClassTemplates
{
    public class PassiveEffect : BaseObject
    {
        public Stats StatModifiers { get; private set; }
        public List<int> ElementRates { get; private set; }
        public int HPRegen { get; private set; }
        public int SPRegen { get; private set; }
        public int SPConsumeRate { get; private set; }
        public int ComboDifficulty { get; private set; }
        public int TurnEnd1 { get; private set; }
        public int TurnEnd2 { get; private set; }
        public int TurnSequence { get; private set; }
        public int RemoveByHit { get; private set; }
        public int Counter { get; private set; }
        public int Reflect { get; private set; }
        public int PhysicalDamageRate { get; private set; }
        public int MagicalDamageRate { get; private set; }
        public int DisabledToolType1 { get; private set; }
        public int DisabledToolType2 { get; private set; }
        public int ExtraTurns { get; private set; }
        private List<State> SRStates;
        private List<int> SRRates;


        protected PassiveEffect() { }
        public PassiveEffect(int id, string name, string description, Bitmap image = null) : base(id, name, description, image)
        {
            ElementRates = new List<int>();
            SRStates = new List<State>();
            SRRates = new List<int>();
        }
        public PassiveEffect(PassiveEffect original) : base(original)
        {
            StatModifiers = new Stats(original.StatModifiers);
            ElementRates = ClonePrimitiveList(ElementRates);
            HPRegen = original.HPRegen;
            SPRegen = original.SPRegen;
            SPConsumeRate = original.SPConsumeRate;
            ComboDifficulty = original.ComboDifficulty;
            TurnEnd1 = original.TurnEnd1;
            TurnEnd2 = original.TurnEnd2;
            TurnSequence = original.TurnSequence;
            RemoveByHit = original.RemoveByHit;
            Counter = original.Counter;
            Reflect = original.Reflect;
            PhysicalDamageRate = original.PhysicalDamageRate;
            MagicalDamageRate = original.MagicalDamageRate;
            DisabledToolType1 = original.DisabledToolType1;
            DisabledToolType2 = original.DisabledToolType2;
            ExtraTurns = original.ExtraTurns;
            SRStates = CloneObjectList(original.SRStates, o => new State(o));
            SRRates = ClonePrimitiveList(original.SRRates);
        }

        public void StatMods(Stats statMods)
        {
            StatModifiers = statMods;
        }
        public void SetHPSPRates(int hpRegen, int spRegen, int spConsumeRate = 100)
        {
            HPRegen = hpRegen;
            SPRegen = spRegen;
            SPConsumeRate = spConsumeRate;
        }
        public void SetDefRates(int removeByHit, int counterRate, int reflectRate, int physDmgRate = 100, int magDmgRate = 100)
        {
            RemoveByHit = removeByHit;
            Counter = counterRate;
            Reflect = reflectRate;
            PhysicalDamageRate = physDmgRate;
            MagicalDamageRate = magDmgRate;
        }
        public void SetTurnValues(int turnEnd1, int turnEnd2, int turnSequence)
        {
            TurnEnd1 = turnEnd1;
            TurnEnd2 = turnEnd2;
            TurnSequence = turnSequence;
        }
        public void SetMiscValues(int disabledToolType1, int disabledToolType2, int extraTurns, int comboDiff = 100)
        {
            DisabledToolType1 = disabledToolType1;
            DisabledToolType2 = disabledToolType2;
            ExtraTurns = extraTurns;
            ComboDifficulty = comboDiff;
        }
        public void AddStateRate(State state, int rate)
        {
            SRStates.Add(state);
            SRRates.Add(rate);
        }
    }
}