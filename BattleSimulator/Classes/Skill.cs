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
        private List<Player> SummonedPlayers;
        private List<int> SummonPlayerChances;
        private List<Enemy> SummonedEnemies;
        private List<int> SummonEnemyChances;


        public int DisabledCount { get; private set; }
        public int ChargeCount { get; private set; }
        

        public Skill() : base()
        {
            SummonedPlayers = new List<Player>();
            SummonPlayerChances = new List<int>();
            SummonedEnemies = new List<Enemy>();
            SummonEnemyChances = new List<int>();
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
            for (int i = 0; i < playersList.Count;)
            {
                SummonedPlayers.Add(ReadObj(playersData, playersList[i++]));
                SummonPlayerChances.Add(playersList[i++]);
            }
            for (int i = 0; i < enemiesList.Count;)
            {
                SummonedEnemies.Add(ReadObj(enemiesData, enemiesList[i++]));
                SummonEnemyChances.Add(enemiesList[i++]);
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
            SummonedPlayers = Clone(original.SummonedPlayers, o => new Player(o));
            SummonPlayerChances = Clone(original.SummonPlayerChances);
            SummonedEnemies = Clone(original.SummonedEnemies, o => new Enemy(o)); 
            SummonEnemyChances = Clone(original.SummonEnemyChances);
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
        public void StartCharge()
        {
            ChargeCount = Charge;
        }
        public void Charge1Turn()
        {
            ChargeCount--;
        }


        public List<int> SummonPlayers()
        {
            List<int> summonedIds = new List<int>();
            for (int i = 0; i < SummonedPlayers.Count; i++) if (Chance(SummonPlayerChances[i])) summonedIds.Add(SummonedPlayers[i].Id);
            return summonedIds;
        }
        public List<int> SummonEnemies()
        {
            List<int> summonedIds = new List<int>();
            for (int i = 0; i < SummonedEnemies.Count; i++) if (Chance(SummonEnemyChances[i])) summonedIds.Add(SummonedEnemies[i].Id);
            return summonedIds;
        }
    }
}
