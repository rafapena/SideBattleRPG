using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.ListManager;

namespace BattleSimulator.Classes
{
    public class Item : Tool
    {
        public Stats PermantentStatChanges { get; private set; }
        public int DefaultPrice { get; private set; }
        public bool Consumable { get; private set; }
        public Item TurnsInto { get; private set; }


        public Item() : base() { }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<BattlerClass> classesData, List<State> statesData, List<Item> itemsData)
        {
            Initialize(data);
            Id = Int(data["Item_ID"]);
            ReadTool(this, data["ToolID"], classesData, statesData);
            PermantentStatChanges = ReadStats(data["PermStatMods"], false);
            DefaultPrice = Int(data["DefaultPrice"]);
            Consumable = (bool)data["Consumable"];
            TurnsInto = ReadObj(itemsData, data["TurnsInto"]);
        }

        public Item(Item original) : base(original)
        {
            PermantentStatChanges = Clone(original.PermantentStatChanges, o => new Stats(o));
            DefaultPrice = original.DefaultPrice;
            Consumable = original.Consumable;
            TurnsInto = Clone(original.TurnsInto, o => new Item(o));
        }
    }
}
