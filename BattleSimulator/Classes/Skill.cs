using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

namespace BattleSimulator.Classes
{
    public class Skill : Tool
    {
        public int SPConsume { get; private set; }
        public int NumberOfUsers { get; private set; }
        public bool ShareTurns { get; private set; }
        public int Charge { get; private set; }
        public int Warmup { get; private set; }
        public int Cooldown { get; private set; }
        public bool Steal { get; private set; }

        public bool Disabled { get; private set; }


        public Skill() { }
        public Skill(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public Skill(Skill original) : base(original)
        {
            SPConsume = original.SPConsume;
            NumberOfUsers = original.NumberOfUsers;
            ShareTurns = original.ShareTurns;
            Charge = original.Charge;
            Warmup = original.Warmup;
            Cooldown = original.Cooldown;
            Steal = original.Steal;
            Disabled = original.Disabled;
        }
        
        public void SetSpecial(int spConsume, bool steal = false)
        {
            SPConsume = spConsume;
            Steal = steal;
        }
        public void SetTeamTraits(int numberOfUsers = 1, bool shareTurns = false)
        {
            NumberOfUsers = numberOfUsers;
            ShareTurns = shareTurns;
        }
        public void SetLimitations(int charge, int warmup, int cooldown)
        {
            Charge = charge;
            Warmup = warmup;
            Cooldown = cooldown;
        }
    }
}
