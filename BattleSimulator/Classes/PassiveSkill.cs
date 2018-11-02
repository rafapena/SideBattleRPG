using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;


namespace BattleSimulator.Classes
{
    public class PassiveSkill : PassiveEffect
    {
        public int HPMin { get; private set; }
        public int HPMax { get; private set; }
        public int SPMin { get; private set; }
        public int SPMax { get; private set; }
        public bool AnyState { get; private set; }
        public bool NoState { get; private set; }
        public State StateActive1 { get; private set; }
        public State StateActive2 { get; private set; }
        public State StateInactive1 { get; private set; }
        public State StateInactive2 { get; private set; }
        public int ExpGainRate { get; private set; }
        public int GoldGainRate { get; private set; }
        public int AllyCondition { get; private set; }
        public int FoeCondition { get; private set; }
        public int UserCondition { get; private set; }

        
        public PassiveSkill() : base() { }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<string> elementsData, List<State> statesData)
        {
            Initialize(data);
            Id = Int(data["PassiveSkill_ID"]);
            ReadPassiveEffect(this, data["PassiveEffectID"], elementsData, statesData);
            StatModifiers = ReadStats(data["StatModifiers"]);
            HPMin = Int(data["HPMin"]);
            HPMax = Int(data["HPMax"]);
            SPMin = Int(data["SPMin"]);
            SPMax = Int(data["SPMax"]);
            AnyState = (bool)data["AnyState"];
            NoState = (bool)data["NoState"];
            StateActive1 = ReadObj(statesData, data["StateActive1"]);
            StateActive2 = ReadObj(statesData, data["StateActive2"]);
            StateInactive1 = ReadObj(statesData, data["StateInactive1"]);
            StateInactive2 = ReadObj(statesData, data["StateInactive2"]);
            ExpGainRate = Int(data["ExpGainRate"]);
            GoldGainRate = Int(data["GoldGainRate"]);
            AllyCondition = Int(data["AllyCondition"]);
            FoeCondition = Int(data["FoeCondition"]);
            UserCondition = Int(data["UserCondition"]);
        }

        public PassiveSkill(PassiveSkill original) : base(original)
        {
            HPMin = original.HPMin;
            HPMax = original.HPMax;
            SPMin = original.SPMin;
            SPMax = original.SPMax;
            AnyState = original.AnyState;
            NoState = original.NoState;
            StateActive1 = Clone(original.StateActive1, o => new State(o));
            StateActive2 = Clone(original.StateActive2, o => new State(o));
            StateInactive1 = Clone(original.StateInactive1, o => new State(o));
            StateInactive2 = Clone(original.StateInactive2, o => new State(o));
            ExpGainRate = original.ExpGainRate;
            GoldGainRate = original.GoldGainRate;
            AllyCondition = original.AllyCondition;
            FoeCondition = original.FoeCondition;
            UserCondition = original.UserCondition;
        }
    }
}
