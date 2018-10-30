using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes;
using BattleSimulator.Classes.ClassTemplates;

namespace BattleSimulator.Simulator
{
    public class Battle : BaseObject
    {
        public int TurnNumber { get; private set; }
        public int NumberOfEnemies { get; private set; }
        public int Environment { get; private set; }

        public List<Player> Players { get; private set; }
        public List<Enemy> Enemies { get; private set; }

        public Battle(string partyFileWithPath)
        {

        }
    }
}
