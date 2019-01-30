using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.ListManager;
using static BattleSimulator.Utilities.RNG;
using BattleSimulator.Utilities;
using System.Windows.Forms;

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
        private int[] StatesGiveRate;
        private int[] StatesReceiveRate;

        public bool Disabled { get; protected set; }


        public Tool() : base() { }

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
            StatesGiveRate = ReadRatesList(data, "Tool", statesData, "Chance", 0, "Give");
            StatesReceiveRate = ReadRatesList(data, "Tool", statesData, "Chance", 0, "Receive");
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
            StatesGiveRate = Clone(original.StatesGiveRate);
            StatesReceiveRate = Clone(original.StatesReceiveRate);
            Disabled = original.Disabled;
        }


        public bool Hit(Battler u, Battler t, double effectMagnitude = 1.0)
        {
            double toolAcc = Accuracy * (u.SelectedWeapon != null ? u.SelectedWeapon.Accuracy : 100) / 10000.0;
            double def = t.Spd() * t.Eva();
            double result = 95 * toolAcc * u.Tec() * u.Acc() / (def != 0 ? def : 0.01) * effectMagnitude;
            return Chance((int)result);
        }

        public int CriticalHitRatio(Battler u, Battler t, double effectMagnitude = 1.0)
        {
            double toolCrt = CritcalRate * (u.SelectedWeapon != null ? u.SelectedWeapon.CritcalRate : 100) / 10000.0;
            double def = t.Tec() * t.Cev();
            double result = 2 * Math.Pow(u.Tec() * toolCrt, 1.1) * u.Crt() / (def != 0 ? def : 0.01) * effectMagnitude;
            return Chance((int)result) ? 3 : 1;
        }

        public int GetToolFormula(Battler u, Battler t, double effectMagnitude = 1.0)
        {
            double total = 0;
            double power = Power * (u.SelectedWeapon != null ? u.SelectedWeapon.Power : 10) / 100.0;
            double rates = power * u.CriticalHitRatio * t.ElementRates[Element] / 1000.0;
            switch (Formula)
            {
                case 1: total = (1.5 * u.Atk() - 1.25 * t.Def()) * rates; break;    // Physical standard
                case 2: total = (1.5 * u.Map() - 1.25 * t.Mar()) * rates; break;    // Magical standard
                case 3: break;  // Mixed standard
                case 4: break;  // Physical gun
                case 5: break;  // Magical gun
            }
            int intTotal = (int)(total * effectMagnitude);
            int variance = intTotal / 10;
            return intTotal + RandInt(-variance, variance);
        }

        public List<int>[] TriggeredStates(Battler u, Battler t, double effectMagnitude = 1.0)
        {
            List<int>[] stateIds = new List<int>[] { new List<int>(), new List<int>() };
            for (int i = 0; i < StatesGiveRate.Length; i++)
            {
                if (StatesGiveRate[i] <= 0) continue;
                double tAttr = t.StateRates[i] * u.Luk() / (100 * t.Luk());
                double result = StatesGiveRate[i] * tAttr / 100.0 * effectMagnitude;
                if (Chance((int)result)) stateIds[0].Add(i);
            }
            for (int i = 0; i < StatesReceiveRate.Length; i++)
            {
                if (StatesReceiveRate[i] <= 0) continue;
                double result = StatesReceiveRate[i] * u.StateRates[i] / 10000.0;
                if (Chance((int)result)) stateIds[1].Add(i);
            }
            return stateIds;
        }
    }
}
