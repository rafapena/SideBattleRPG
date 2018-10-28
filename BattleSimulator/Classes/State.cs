using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

namespace BattleSimulator.Classes
{
    public class State : PassiveEffect
    {
        public int MaxStack { get; private set; }
        public int ContactSpreadRate { get; private set; }
        public bool Stun { get; private set; }
        public bool Petrify { get; private set; }
        public bool KO { get; private set; }

        public State() { }
        public State(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public State(State original) : base(original)
        {
            MaxStack = original.MaxStack;
            ContactSpreadRate = original.ContactSpreadRate;
            Stun = original.Stun;
            Petrify = original.Petrify;
            KO = original.KO;
        }
        public void SetMiscAttributes(int maxstack, int contactSpreadRate, bool stun, bool petrify, bool ko)
        {
            MaxStack = maxstack;
            ContactSpreadRate = contactSpreadRate;
            Stun = stun;
            Petrify = petrify;
            KO = ko;
        }
    }
}
