using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;
using System.Windows.Forms;

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
        private bool SummonEnemies;
        private List<Battler> SummonedBattlers;
        private List<int> SummonChances;

        public int DisabledCount { get; private set; }
        public int ChargeCount { get; private set; }
        

        public Skill() : base()
        {
            SummonEnemies = true;
            SummonedBattlers = new List<Battler>();
            SummonChances = new List<int>();
        }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<BattlerClass> classesData, List<State> statesData, List<Player> playersData, List<Enemy> enemiesData)
        {
            Initialize(data);
            Id = Int(data["Skill_ID"]);
            ReadTool(this, data["ToolID"], classesData, statesData);
            SPConsume = Int(data["SPConsume"]);
            NumberOfUsers = Int(data["NumberOfUsers"]) + 1;
            ShareTurns = (bool)data["ShareTurns"];
            Charge = Int(data["Charge"]);
            Warmup = Int(data["Warmup"]);
            Cooldown = Int(data["CoolDown"]);
            Steal = (bool)data["Steal"];
            List<int> enemiesList = ReadDBList(data, "Skill", "Enemy", "Response");
            List<int> playersList = ReadDBList(data, "Skill", "Player", "Response");
            if (enemiesList.Count <= 0 && playersList.Count <= 0) return;
            SummonEnemies = playersList.Count <= 0;
            if (SummonEnemies)
            {
                for (int i = 0; i < enemiesList.Count;)
                {
                    SummonedBattlers.Add(ReadObj(enemiesData, enemiesList[i++]));
                    SummonChances.Add(enemiesList[i++]);
                }
                return;
            }
            for (int i = 0; i < playersList.Count;)
            {
                SummonedBattlers.Add(ReadObj(playersData, playersList[i++]));
                SummonChances.Add(playersList[i++]);
            }
        }

        public Skill(Skill original) : base(original)
        {
            SPConsume = original.SPConsume;
            NumberOfUsers = original.NumberOfUsers;
            ShareTurns = original.ShareTurns;
            Charge = original.Charge;
            Warmup = original.Warmup;
            Cooldown = original.Cooldown;
            Steal = original.Steal;
            SummonEnemies = original.SummonEnemies;
            //SummonedBattlers = CloneObjectList(original.SummonEnemies, o => new Enemy(o));
            SummonChances = Clone(original.SummonChances);
            DisabledCount = original.DisabledCount;
            ChargeCount = original.ChargeCount;
        }

        public void DisableForWarmup()
        {
            DisabledCount = Warmup;
        }

        public void DisableForCooldown()
        {
            DisabledCount = Cooldown;
        }
    }
}
