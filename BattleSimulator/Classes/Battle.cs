using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.ListManager;
using BattleSimulator.Utilities;
using System.Windows.Forms;

namespace BattleSimulator.Classes
{
    public class Battle : BaseObject
    {
        public List<Enemy> Enemies { get; private set; }


        public Battle() : base()
        {
            Enemies = new List<Enemy>();
        }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<Environment> environmentsData, List<Enemy> enemiesData, List<PassiveSkill> pSkillsData,
            List<Skill> skillsData, List<Item> itemsData, List<Weapon> weaponsData, List<State> statesData)
        {
            Initialize(data);
            int numberOfEnemies = Int(data["NumberOfEnemies"]);
            for (int i = 1; i <= numberOfEnemies; i++)
            {
                object beId = data["BattleEnemy" + i];
                BattleEnemy b = ReadBattleEnemy(beId, enemiesData, pSkillsData);
                Enemy enemyInBattle = b.Enemy;
                enemyInBattle.SetAllStats(b.Level, b.HPMultiplier);
                enemyInBattle.MoveToPosition(b.GridPositionZ, b.GridPositionX);
                if (b.PassiveSkill1 != null) enemyInBattle.AddPassiveSkill(pSkillsData, b.PassiveSkill1.Id);
                if (b.PassiveSkill2 != null) enemyInBattle.AddPassiveSkill(pSkillsData, b.PassiveSkill2.Id);
                List<EnemyTool<Skill>> enemySkills = ReadEnemyTools(beId, skillsData, statesData);
                List<EnemyTool<Item>> enemyItems = ReadEnemyTools(beId, itemsData, statesData);
                List<EnemyTool<Weapon>> enemyWeapons = ReadEnemyTools(beId, weaponsData, statesData);
                foreach (var et in enemySkills) enemyInBattle.AddSkill(skillsData, et.Tool.Id);
                foreach (var et in enemyItems) enemyInBattle.AddItem(itemsData, et.Tool.Id);
                foreach (var et in enemyWeapons) enemyInBattle.AddWeapon(weaponsData, et.Tool.Id);
                enemyInBattle.MaxHPSP();
                Enemies.Add(enemyInBattle);
            }
        }

        public Battle(Battle original) : base(original)
        {
            Enemies = Clone(original.Enemies, o => new Enemy(o));
        }
    }
}
