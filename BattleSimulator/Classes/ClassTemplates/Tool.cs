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
        private List<int> StateGiveRate;
        private List<int> StateReceiveRate;

        public bool Disabled { get; protected set; }


        public Tool() : base()
        {
            StateGiveRate = new List<int>();
            StateReceiveRate = new List<int>();
        }
        public void Setup(System.Data.SQLite.SQLiteDataReader data, List<BattlerClass> classesData, List<State> statesData)
        {
            Type = Int(data["Type"]);
            Formula = Int(data["Formula"]);
            HPSPModType = Int(data["HPSPModType"]);
            HPAmount = Int(data["HPAmount"]);
            SPAmount = Int(data["SPAmount"]);
            HPPercent = Int(data["HPPercent"]);
            SPPecent = Int(data["SPPercent"]);
            HPRecoil = Int(data["HPRecoil"]);
            Scope = Int(data["Scope"]);
            ConsecutiveActs = Int(data["ConsecutiveActs"]);
            RandomActs = Int(data["RandomActs"]);
            Element = Int(data["Element"]);
            Power = Int(data["Power"]);
            Accuracy = Int(data["Accuracy"]);
            CritcalRate = Int(data["CriticalRate"]);
            Priority = Int(data["Priority"]);
            ClassExclusive1 = ReadObj(classesData, data["ClassExclusive1"]);
            ClassExclusive2 = ReadObj(classesData, data["ClassExclusive2"]);
            StateGiveRate = ReadRatesList(data, "Tool", statesData, "Chance", "Give");
            StateReceiveRate = ReadRatesList(data, "Tool", statesData, "Chance", "Receive");
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
            ClassExclusive1 = Clone(original.ClassExclusive1, o => new BattlerClass(o));
            ClassExclusive2 = Clone(original.ClassExclusive2, o => new BattlerClass(o));
            StateGiveRate = Clone(original.StateGiveRate);
            StateReceiveRate = Clone(original.StateReceiveRate);
            Disabled = original.Disabled;
        }
    }
}
