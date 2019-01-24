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
    public class State : PassiveEffect
    {
        public int MaxStack { get; private set; }
        public int ContactSpreadRate { get; private set; }
        public bool Stun { get; private set; }
        public bool Petrify { get; private set; }
        public bool KO { get; private set; }

        public int Stack { get; set; }


        public State() : base() { }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<string> elementsData, List<State> statesData)
        {
            Initialize(data);
            Id = Int(data["State_ID"]);
            ReadPassiveEffect(this, data["PassiveEffectID"], elementsData, statesData);
            StatModifiers = ReadStats(data["StatModifiers"]);
            MaxStack = Int(data["MaxStack"]);
            ContactSpreadRate = Int(data["ContactSpreadRate"]);
            Stun = (bool)data["Stun"];
            Petrify = (bool)data["Petrify"];
            KO = (bool)data["KO"];
        }

        public State(State original) : base(original)
        {
            MaxStack = original.MaxStack;
            ContactSpreadRate = original.ContactSpreadRate;
            Stun = original.Stun;
            Petrify = original.Petrify;
            KO = original.KO;
        }
    }
}
