using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;
using BattleSimulator.Utilities;

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
        private List<int> StatesGiveRate;
        private List<int> StatesReceiveRate;

        public bool Disabled { get; protected set; }


        public Tool() : base()
        {
            StatesGiveRate = new List<int>();
            StatesReceiveRate = new List<int>();
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


        public bool Hit(Battler u, Battler t, Environment e)
        {
            double toolAcc = Accuracy * (u.SelectedWeapon != null ? u.SelectedWeapon.Accuracy : 100) / 10000;
            double result = 95 * u.Tec() * u.Acc() * e.Acc * toolAcc / (t.Spd() * t.Eva() * e.Eva);
            return Chance((int)result);
        }

        public int CriticalHitRatio(Battler u, Battler t, Environment e)
        {
            double toolCrt = CritcalRate * (u.SelectedWeapon != null ? u.SelectedWeapon.CritcalRate : 100) / 10000;
            double result = 2 * Math.Pow(u.Tec() * toolCrt, 1.1) * u.Crt() / (t.Tec() * t.Cev());
            return Chance((int)result) ? 3 : 1;
        }

        public int GetToolFormula(Battler u, Battler t, Environment e)
        {
            double total = 0;
            double power = Power * (u.SelectedWeapon != null ? u.SelectedWeapon.Power : 10) / 100.0;
            double rates = power * e.ElementRates[Element] * u.CriticalHitRatio * t.ElementRates[Element] / 10000.0;
            switch (Formula)
            {
                case 1: // Physical standard
                    total = (1.5 * u.Atk() - 1.25 * t.Def()) * rates;
                    break;
                case 2: // Magical standard
                    total = (1.5 * u.Map() - 1.25 * t.Mar()) * rates;
                    break;
                case 3: // Mixed standard
                    break;
                case 4: // Physical gun
                    break;
                case 5: // Magical gun
                    break;
            }
            int intTotal = (int)total;
            int variance = intTotal / 10;
            return intTotal + RandInt(-variance, variance);
        }

        public List<int>[] TriggeredStates(Battler u, Battler t, Environment e)
        {
            List<int>[] stateIds = new List<int>[] { new List<int>(), new List<int>() };
            for (int i = 0; i < StatesGiveRate.Count; i++)
            {
                if (StatesGiveRate[i] <= 0) continue;
                double result = StatesGiveRate[i] * t.StateRates[i] * e.StateRates[i] / 1000000;
                if (Chance((int)result)) stateIds[0].Add(i);
            }
            for (int i = 0; i < StatesReceiveRate.Count; i++)
            {
                if (StatesReceiveRate[i] <= 0) continue;
                double result = StatesReceiveRate[i] * u.StateRates[i] * e.StateRates[i] / 1000000;
                if (Chance((int)result)) stateIds[1].Add(i);
            }
            return stateIds;
        }
    }
}
