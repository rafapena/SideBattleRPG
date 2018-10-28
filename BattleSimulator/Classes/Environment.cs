using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

namespace BattleSimulator.Classes
{
    public class Environment : BaseObject
    {
        public PassiveEffect Effects { get; private set; }
        public Bitmap Foreground { get; private set; }
        public Bitmap Background { get; private set; }
        public int Acc { get; private set; }
        public int Eva { get; private set; }
        public int Crt { get; private set; }
        public int Cev { get; private set; }

        private Environment() { }
        public Environment(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public Environment(Environment original) : base(original)
        {
            Effects = new PassiveEffect(original.Effects);
            Foreground = original.Foreground;
            Background = original.Background;
            Acc = original.Acc;
            Eva = original.Eva;
            Crt = original.Crt;
            Cev = original.Cev;
        }

        public void SetEffects(PassiveEffect effects)
        {
            Effects = effects;
        }
        public void SetImages(Bitmap foreground, Bitmap background)
        {
            Foreground = foreground;
            Background = background;
        }
        public void SetModifiableStats(int acc, int eva, int crt, int cev)
        {
            Acc = acc;
            Eva = eva;
            Crt = crt;
            cev = Cev;
        }
    }
}
