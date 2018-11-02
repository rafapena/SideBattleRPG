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
    public class Environment : BaseObject
    {
        public PassiveEffect Effects { get; private set; }
        public Bitmap MapForeground { get; private set; }
        public Bitmap MapBackground { get; private set; }
        public Bitmap BattleForeground { get; private set; }
        public Bitmap BattleBackground { get; private set; }
        public int Acc { get; private set; }
        public int Eva { get; private set; }
        public int Crt { get; private set; }
        public int Cev { get; private set; }


        public Environment() : base()
        {
            Effects = new PassiveEffect();
        }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<string> elementsData, List<State> statesData)
        {
            Initialize(data);
            Id = Int(data["Environment_ID"]);
            Effects = ReadPassiveEffect(Effects, data["PassiveEffectID"], elementsData, statesData);
            MapForeground = BytesToImage(data, 8);
            MapBackground = BytesToImage(data, 9);
            BattleForeground = BytesToImage(data, 10);
            BattleBackground = BytesToImage(data, 11);
            Acc = Int(data["Accuracy"]);
            Eva = Int(data["Evasion"]);
            Crt = Int(data["CriticalRate"]);
            Cev = Int(data["CritEvadeRate"]);
        }

        public Environment(Environment original) : base(original)
        {
            Effects = Clone(original.Effects, o => new PassiveEffect(o));
            MapForeground = original.MapForeground;
            MapBackground = original.MapBackground;
            BattleForeground = original.BattleForeground;
            BattleBackground = original.BattleBackground;
            Acc = original.Acc;
            Eva = original.Eva;
            Crt = original.Crt;
            Cev = original.Cev;
        }
    }
}
