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
    public class Environment : PassiveEffect
    {
        public Bitmap MapForeground { get; private set; }
        public Bitmap MapBackground { get; private set; }
        public Bitmap BattleForeground { get; private set; }
        public Bitmap BattleBackground { get; private set; }


        public Environment() : base() { }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<string> elementsData, List<State> statesData)
        {
            Initialize(data);
            Id = Int(data["Environment_ID"]);
            ReadPassiveEffect(this, data["PassiveEffectID"], elementsData, statesData);
            MapForeground = BytesToImage(data, 8);
            MapBackground = BytesToImage(data, 9);
            BattleForeground = BytesToImage(data, 10);
            BattleBackground = BytesToImage(data, 11);
            StatModifiers = new Stats();
            StatModifiers.Add(8, Int(data["Accuracy"]) - 100);
            StatModifiers.Add(9, Int(data["Evasion"]) - 100);
            StatModifiers.Add(10, Int(data["CriticalRate"]) - 100);
            StatModifiers.Add(11, Int(data["CritEvadeRate"]) - 100);
        }

        public Environment(Environment original) : base(original)
        {
            MapForeground = original.MapForeground;
            MapBackground = original.MapBackground;
            BattleForeground = original.BattleForeground;
            BattleBackground = original.BattleBackground;
        }
    }
}
