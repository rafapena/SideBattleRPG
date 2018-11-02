using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;

namespace BattleSimulator.Classes.ClassTemplates
{
    public class BattleEnemy
    {
        public int Id { get; private set; }
        public Enemy Enemy { get; private set; }
        public int Level { get; private set; }
        public int GridPositionZ { get; private set; }
        public int GridPositionX { get; private set; }
        public double HPMultiplier { get; private set; }
        public PassiveSkill PassiveSkill1 { get; private set; }
        public PassiveSkill PassiveSkill2 { get; private set; }

        public BattleEnemy(System.Data.SQLite.SQLiteDataReader data, List<Enemy> enemiesData, List<PassiveSkill> pSkillsData)
        {
            Id = Int(data["BattleEnemy_ID"]);
            Enemy = ReadObj(enemiesData, data["EnemyID"]);
            Level = Int(data["Level"]);
            GridPositionZ = Int(data["GridPositionZ"]);
            GridPositionX = Int(data["GridPositionX"]);
            HPMultiplier = Int(data["HPMultiplier"]);
            PassiveSkill1 = ReadObj(pSkillsData, data["PassiveSkill1"]);
            PassiveSkill2 = ReadObj(pSkillsData, data["PassiveSkill2"]);
        }
    }
}