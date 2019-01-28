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
    public class Weapon : Tool
    {
        public Stats EquipBoosts { get; private set; }
        public int WeaponType { get; private set; }
        public int Range { get; private set; }
        public bool CollideRange { get; private set; }
        public int DefaultPrice { get; private set; }
        public int DefaultQuantity { get; private set; }

        public int Quantity { get; set; }


        public Weapon() : base() { }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<BattlerClass> classesData, List<State> statesData)
        {
            Initialize(data);
            Id = Int(data["Weapon_ID"]);
            ReadTool(this, data["ToolID"], classesData, statesData);
            EquipBoosts = ReadStats(data["EquipBoosts"], false);
            WeaponType = Int(data["WeaponType"]);
            Range = Int(data["Range"]);
            CollideRange = (bool)data["CollideRange"];
            DefaultPrice = Int(data["DefaultPrice"]);
            DefaultQuantity = Int(data["DefaultQuantity"]);
            Quantity = DefaultQuantity;
        }

        public Weapon(Weapon original) : base(original)
        {
            EquipBoosts = Clone(original.EquipBoosts, o => new Stats(o));
            WeaponType = original.WeaponType;
            Range = original.Range;
            CollideRange = original.CollideRange;
            DefaultPrice = original.DefaultPrice;
            DefaultQuantity = original.DefaultQuantity;
            Quantity = original.Quantity;
        }
    }
}
