using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleSimulator.Classes.ClassTemplates
{
    public abstract class Tool : BaseObject
    {
        public int Type { get; private set; }
        public int Formula { get; private set; }
        public int HPSPModType { get; private set; }
        public int HPAmount { get; private set; }
        public int SPAmount { get; private set; }
        public int HPPercent { get; private set; }
        public int SPPecent { get; private set; }
        public int HPRecoil { get; private set; }
        public int Scope { get; private set; }
        public int ConsecutiveActs { get; private set; }
        public int RandomActs { get; private set; }
        public int Element { get; private set; }
        public int Power { get; private set; }
        public int Accuracy { get; private set; }
        public int CritcalRate { get; private set; }
        public int Priority { get; private set; }
        public BattlerClass ClassExclusive1 { get; private set; }
        public BattlerClass ClassExclusive2 { get; private set; }
        private List<State> SRGiveState;
        private List<State> SRReceiveState;
        private List<int> SRGiveRate;
        private List<int> SRReceiveRate;


        protected Tool() { }
        public Tool(int id, string name, string description, Bitmap image = null) : base(id, name, description, image)
        {
            SRGiveState = new List<State>();
            SRReceiveState = new List<State>();
            SRGiveRate = new List<int>();
            SRReceiveRate = new List<int>();
        }
        public Tool(Tool original) : base(original)
        {
            Type = original.Type;
            Formula = original.Formula;
            HPSPModType = original.HPSPModType;
            HPAmount = original.HPAmount;
            SPAmount = original.SPAmount;
            HPPercent = original.HPPercent;
            SPPecent = original.SPPecent;
            HPRecoil = original.HPRecoil;
            Scope = original.Scope;
            ConsecutiveActs = original.ConsecutiveActs;
            RandomActs = original.RandomActs;
            Element = original.Element;
            Power = original.Power;
            Accuracy = original.Accuracy;
            CritcalRate = original.CritcalRate;
            Priority = original.Priority;
            ClassExclusive1 = new BattlerClass(original.ClassExclusive1);
            ClassExclusive2 = new BattlerClass(original.ClassExclusive2);
            SRGiveState = CloneObjectList(original.SRGiveState, o => new State(o));
            SRReceiveState = CloneObjectList(original.SRReceiveState, o => new State(o));
            SRGiveRate = ClonePrimitiveList(original.SRGiveRate);
            SRReceiveRate = ClonePrimitiveList(original.SRReceiveRate);
        }

        public void SetTypes(int type, int formula)
        {
            Type = type;
            Formula = formula;
        }
        public void SetHPSPValues(int hpspModType, int hpAmt, int spAmt, int hpPercent, int spPercent, int hpRec)
        {
            HPSPModType = hpspModType;
            HPAmount = hpAmt;
            SPAmount = spAmt;
            HPPercent = hpPercent;
            SPPecent = spPercent;
            HPRecoil = HPRecoil;
        }
        public void SetTargets(int scope, int consecutiveActs, int randomActs)
        {
            Scope = scope;
            ConsecutiveActs = consecutiveActs;
            RandomActs = randomActs;
        }
        public void SetAmplifiers(int element, int power, int accuracy, int criticalRate, int priority)
        {
            Element = element;
            Power = power;
            Accuracy = accuracy;
            CritcalRate = criticalRate;
            Priority = priority;
        }
        public void SetExclusiveClasses(BattlerClass class1, BattlerClass class2)
        {
            ClassExclusive1 = class1;
            ClassExclusive2 = class2;
        }
        public void AddStateRateGive(State state, int rate)
        {
            SRGiveState.Add(state);
            SRGiveRate.Add(rate);
        }
        public void AddStateRateReceive(State state, int rate)
        {
            SRReceiveState.Add(state);
            SRReceiveRate.Add(rate);
        }
    }
}
