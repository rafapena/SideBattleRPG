using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

namespace BattleSimulator.Classes
{
    public class Player : Battler
    {
        public int Companionship { get; private set; }
        public int SavePartnerRate { get; private set; }
        public int CounterattackRate { get; private set; }
        public int AssistDamageRate { get; private set; }

        public Player() { }
        public Player(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public Player(Player original) : base(original)
        {
            Companionship = original.Companionship;
            SavePartnerRate = original.SavePartnerRate;
            CounterattackRate = original.CounterattackRate;
            AssistDamageRate = original.AssistDamageRate;
        }
        public void SetTeamworkLevels(int companionShip, int savePartnerRate, int counterAttackRate, int assistDamageRate)
        {
            Companionship = companionShip;
            SavePartnerRate = savePartnerRate;
            CounterattackRate = counterAttackRate;
            AssistDamageRate = assistDamageRate;
        }
    }
}