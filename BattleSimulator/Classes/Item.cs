using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

namespace BattleSimulator.Classes
{
    public class Item : Tool
    {
        public Stats PermantentStatChanges { get; private set; }
        public bool Consumable { get; private set; }
        public Item TurnsInto { get; private set; }

        public Item() { }
        public Item(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public Item(Item original) : base(original)
        {
            PermantentStatChanges = new Stats(PermantentStatChanges);
            Consumable = original.Consumable;
            TurnsInto = new Item(TurnsInto);
        }
        public void SetOtherAttributes(Stats permanentStatChanges, bool consumable, Item turnsInto)
        {
            PermantentStatChanges = permanentStatChanges;
            Consumable = consumable;
            TurnsInto = turnsInto;
        }
    }
}
