using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;

namespace BattleSimulator.Classes
{
    public class EnemyToolAI
    {
        public int Priority { get; private set; }
        public int Quantity { get; private set; }
        public int HPLow { get; private set; }
        public int HPHigh { get; private set; }
        public int SPLow { get; private set; }
        public int SPHigh { get; private set; }
        public State ActiveState1 { get; private set; }
        public State ActiveState2 { get; private set; }
        public State InactiveState1 { get; private set; }
        public State InactiveState2 { get; private set; }
        public int AllyCondition { get; private set; }
        public int FoeCondition { get; private set; }
        public int UserCondition { get; private set; }
        private int TargetERElement;
        private int TargetERGate;
        private int TargetERRate;
        private State TargetSR1State;
        private int TargetSR1Gate;
        private int TargetSR1Rate;
        private State TargetSR2State;
        private int TargetSR2Gate;
        private int TargetSR2Rate;
        private int TargetSC1Stat;
        private int TargetSC1Gate;
        private int TargetSC1Rate;
        private int TargetSC2Stat;
        private int TargetSC2Gate;
        private int TargetSC2Rate;
        public int TargetToolElement { get; private set; }

        public EnemyToolAI() { }
        public EnemyToolAI(EnemyToolAI original)
        {
            Priority = original.Priority;
            Quantity = original.Quantity;
            HPLow = original.HPLow;
            HPHigh = original.HPHigh;
            SPLow = original.SPLow;
            SPHigh = original.SPHigh;
            ActiveState1 = new State(original.ActiveState1);
            ActiveState2 = new State(original.ActiveState2);
            InactiveState1 = new State(original.InactiveState1);
            InactiveState2 = new State(original.InactiveState2);
            AllyCondition = original.AllyCondition;
            FoeCondition = original.FoeCondition;
            UserCondition = original.UserCondition;
            TargetERElement = original.TargetERElement;
            TargetERGate = original.TargetERGate;
            TargetERRate = original.TargetERRate;
            TargetSR1State = new State(original.TargetSR1State);
            TargetSR1Gate = original.TargetSR1Gate;
            TargetSR1Rate = original.TargetSR1Rate;
            TargetSR2State = new State(original.TargetSR2State);
            TargetSR2Gate = original.TargetSR2Gate;
            TargetSR2Rate = original.TargetSR2Rate;
            TargetSC1Stat = original.TargetSC1Stat;
            TargetSC1Gate = original.TargetSC1Gate;
            TargetSC1Rate = original.TargetSC1Rate;
            TargetSC2Stat = original.TargetSC2Stat;
            TargetSC2Gate = original.TargetSC2Gate;
            TargetSC2Rate = original.TargetSC2Rate;
            TargetToolElement = original.TargetToolElement;
        }

        public void SetUseLimitations(int priority, int quantity)
        {
            Priority = priority;
            Quantity = quantity;
        }
        public void SetHPSP(int hpLow, int hpHigh, int spLow, int spHigh)
        {
            HPLow = hpLow;
            HPHigh = hpHigh;
            SPLow = spLow;
            SPHigh = spHigh;
        }
        public void SetStates(State active1, State active2, State inactive1, State inactive2)
        {
            ActiveState1 = active1;
            ActiveState2 = active2;
            InactiveState1 = inactive1;
            InactiveState2 = inactive2;
        }
        public void SetConditions(int ally, int foe, int user)
        {
            AllyCondition = ally;
            FoeCondition = foe;
            UserCondition = user;
        }
        public void SetElementRates(string targetElementRate)
        {
            string[] ter = targetElementRate.Split('_');
            TargetERElement = int.Parse(ter[0]);
            TargetERGate = int.Parse(ter[1]);
            TargetERRate = int.Parse(ter[2]);
        }
        public void SetStateRates(string targetStateRates)
        {
            string[] tsr = targetStateRates.Split('_');
            //TargetSR1State = int.Parse(tsr[0]);
            TargetSR1Gate = int.Parse(tsr[1]);
            TargetSR1Rate = int.Parse(tsr[2]);
            //TargetSR2State = int.Parse(tsr[3]);
            TargetSR2Gate = int.Parse(tsr[4]);
            TargetSR2Rate = int.Parse(tsr[5]);
        }
        public void SetStatConditions(string targetStatConditions)
        {
            string[] tsc = targetStatConditions.Split('_');
            TargetSC1Stat = int.Parse(tsc[0]);
            TargetSC1Gate = int.Parse(tsc[1]);
            TargetSC1Rate = int.Parse(tsc[2]);
            TargetSC2Stat = int.Parse(tsc[3]);
            TargetSC2Gate = int.Parse(tsc[4]);
            TargetSC2Rate = int.Parse(tsc[5]);
        }
        public void SetTargetToolElement(int targetToolElement)
        {
            TargetToolElement = TargetToolElement;
        }
    }
}
