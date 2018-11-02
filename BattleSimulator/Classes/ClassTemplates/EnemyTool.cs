using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;

namespace BattleSimulator.Classes.ClassTemplates
{
    public class EnemyTool<T> where T : Tool
    {
        public T Tool { get; private set; }
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
        private string TargetElementRate;
        private string TargetStateRate1;
        private string TargetStateRate2;
        private string TargetStatCondition1;
        private string TargetStatCondition2;
        public int TargetToolElement { get; private set; }


        public EnemyTool(System.Data.SQLite.SQLiteDataReader data, List<T> toolsData, List<State> statesData)
        {
            Tool = ReadToolObj(toolsData, data["Tool_ID"]);
            Priority = Int(data["Priority"]);
            Quantity = Int(data["Quantity"]);
            HPLow = Int(data["HPLow"]);
            HPHigh = Int(data["HPHigh"]);
            SPLow = Int(data["SPLow"]);
            SPHigh = Int(data["SPHigh"]);
            ActiveState1 = ReadObj(statesData, data["ActiveState1"]);
            ActiveState2 = ReadObj(statesData, data["ActiveState2"]);
            InactiveState1 = ReadObj(statesData, data["InactiveState1"]);
            InactiveState2 = ReadObj(statesData, data["InactiveState2"]);
            AllyCondition = Int(data["AllyCondition"]);
            FoeCondition = Int(data["FoeCondition"]);
            UserCondition = Int(data["UserCondition"]);
            TargetElementRate = data["TargetElementRate"].ToString();
            TargetStateRate1 = data["TargetStateRate1"].ToString();
            TargetStateRate2 = data["TargetStateRate2"].ToString();
            TargetStatCondition1 = data["TargetStatCondition1"].ToString();
            TargetStatCondition2 = data["TargetStatCondition2"].ToString();
            TargetToolElement = Int(data["TargetToolElement"]);
        }

        public EnemyTool(EnemyTool<T> original)
        {
            Priority = original.Priority;
            Quantity = original.Quantity;
            HPLow = original.HPLow;
            HPHigh = original.HPHigh;
            SPLow = original.SPLow;
            SPHigh = original.SPHigh;
            ActiveState1 = Clone(original.ActiveState1, o => new State(o));
            ActiveState2 = Clone(original.ActiveState2, o => new State(o));
            InactiveState1 = Clone(original.InactiveState1, o => new State(o));
            InactiveState2 = Clone(original.InactiveState2, o => new State(o));
            AllyCondition = original.AllyCondition;
            FoeCondition = original.FoeCondition;
            UserCondition = original.UserCondition;
            TargetElementRate = original.TargetElementRate;
            TargetStateRate1 = original.TargetStateRate1;
            TargetStateRate2 = original.TargetStateRate2;
            TargetStatCondition1 = original.TargetStatCondition1;
            TargetStatCondition2 = original.TargetStatCondition2;
            TargetToolElement = original.TargetToolElement;
        }
    }
}
