using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

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

        
        public PassiveSkill() { }
        public PassiveSkill(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public PassiveSkill(PassiveSkill original) : base(original)
        {
            HPMin = original.HPMin;
            HPMax = original.HPMax;
            SPMin = original.SPMin;
            SPMax = original.SPMax;
            AnyState = original.AnyState;
            NoState = original.NoState;
            StateActive1 = new State(StateActive1);
            StateActive2 = new State(StateActive2);
            StateInactive1 = new State(StateInactive1);
            StateInactive2 = new State(StateInactive2);
            ExpGainRate = original.ExpGainRate;
            GoldGainRate = original.GoldGainRate;
            AllyCondition = original.AllyCondition;
            FoeCondition = original.FoeCondition;
            UserCondition = original.UserCondition;
        }

        public void SetHPSP(int hpMin, int hpMax, int spMin, int spMax)
        {
            HPMin = hpMin;
            HPMax = hpMax;
            SPMin = spMin;
            SPMax = spMax;
        }
        public void SetStateSettings(bool anyState, bool noState, State stateActive1, State stateActive2, State stateInactive1, State stateInactive2)
        {
            AnyState = anyState;
            NoState = noState;
            StateActive1 = stateActive1;
            StateActive2 = stateActive2;
            StateInactive1 = stateInactive1;
            StateInactive2 = stateInactive2;
        }
        public void SetCustomGainRates(int expGainRate, int goldGainRate)
        {
            ExpGainRate = expGainRate;
            GoldGainRate = goldGainRate;
        }
        public void SetConditions(int allyCondition, int foeCondition, int userCondition)
        {
            AllyCondition = allyCondition;
            FoeCondition = foeCondition;
            UserCondition = userCondition;
        }
    }
}
