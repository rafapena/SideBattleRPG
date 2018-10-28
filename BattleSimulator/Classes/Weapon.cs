using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

namespace BattleSimulator.Classes
{
    public class Weapon : Tool
    {
        public Stats EquipBoosts { get; private set; }
        public int WeaponType { get; private set; }
        public int Range { get; private set; }
        public bool CollideRange { get; private set; }
        public int Quantity { get; private set; }

        public Weapon() { }
        public Weapon(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public Weapon(Weapon original) : base(original)
        {
            EquipBoosts = new Stats(EquipBoosts);
            WeaponType = original.WeaponType;
            Range = original.Range;
            CollideRange = original.CollideRange;
            Quantity = original.Quantity;
        }

        public void SetStatBoosts(Stats equipBoosts)
        {
            EquipBoosts = equipBoosts;
        }
        public void SetMiscAttributes(int weaponType, int range, bool collideRange, int quantity)
        {
            WeaponType = weaponType;
            Range = range;
            CollideRange = collideRange;
            Quantity = quantity;
        }
    }
}
