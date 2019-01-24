using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;

namespace BattleSimulator.Classes.ClassTemplates
{
    public class PassiveEffect : BaseObject
    {
        public Stats StatModifiers { get; protected set; }
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
        public List<int> StateRates { get; private set; }

        public int TurnsLeft { get; set; }

        
        public PassiveEffect() : base()
        {
            ElementRates = new List<int>();
            StateRates = new List<int>();
        }
        public void Setup(System.Data.SQLite.SQLiteDataReader data, List<string> elementsData, List<State> statesData)
        {
            ElementRates = ReadRatesList(data, "PassiveEffect", elementsData, "ElementRates");
            HPRegen = Int(data["HPRegen"]);
            SPRegen = Int(data["SPRegen"]);
            SPConsumeRate = Int(data["SPConsumeRate"]);
            ComboDifficulty = Int(data["ComboDifficulty"]);
            TurnEnd1 = Int(data["TurnEnd1"]);
            TurnEnd2 = Int(data["TurnEnd2"]);
            TurnSequence = Int(data["TurnSequence"]);
            RemoveByHit = Int(data["RemoveByHit"]);
            Counter = Int(data["Counter"]);
            Reflect = Int(data["Reflect"]);
            PhysicalDamageRate = Int(data["PhysicalDamageRate"]);
            MagicalDamageRate = Int(data["MagicalDamageRate"]);
            DisabledToolType1 = Int(data["DisabledToolType1"]);
            DisabledToolType2 = Int(data["DisabledToolType2"]);
            ExtraTurns = Int(data["ExtraTurns"]);
            StateRates = ReadRatesList(data, "PassiveEffect", statesData, "Vulnerability");
        }

        public PassiveEffect(PassiveEffect original) : base(original)
        {
            StatModifiers = new Stats(original.StatModifiers);
            ElementRates = Clone(original.ElementRates);
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
            StateRates = Clone(original.StateRates);
            TurnsLeft = original.TurnsLeft;
        }
    }
}