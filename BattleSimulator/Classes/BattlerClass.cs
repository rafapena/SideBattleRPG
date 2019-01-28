using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;

namespace BattleSimulator.Classes
{
    public class BattlerClass : BaseObject
    {
        public Stats BaseStats { get; private set; }
        public BattlerClass UpgradedClass1 { get; private set; }
        public BattlerClass UpgradedClass2 { get; private set; }
        public int UsableWeapon1Type { get; private set; }
        public int UsableWeapon2Type { get; private set; }
        public PassiveSkill PassiveSkill1 { get; private set; }
        public PassiveSkill PassiveSkill2 { get; private set; }
        public int PassiveSkillLvlRequired1 { get; private set; }
        public int PassiveSkillLvlrequired2 { get; private set; }
        private List<Skill> SkillSet;
        private List<int> SkillSetLevels;
        

        public BattlerClass()
        {
            SkillSet = new List<Skill>();
            SkillSetLevels = new List<int>();
        }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<BattlerClass> classesData, List<PassiveSkill> pSkillsData, List<Skill> skillsData)
        {
            Initialize(data);
            Id = Int(data["BattlerClass_ID"]);
            BaseStats = ReadStats(data["ScaledStats"], false);
            UpgradedClass1 = ReadObj(classesData, data["UpgradedClass1"]);
            UpgradedClass2 = ReadObj(classesData, data["UpgradedClass2"]);
            UsableWeapon1Type = Int(data["UsableWeaponType1"]);
            UsableWeapon2Type = Int(data["UsableWeaponType2"]);
            PassiveSkill1 = ReadObj(pSkillsData, data["PassiveSkill1"]);
            PassiveSkill2 = ReadObj(pSkillsData, data["PassiveSkill2"]);
            PassiveSkillLvlRequired1 = Int(data["PSkillLvlRequired1"]);
            PassiveSkillLvlrequired2 = Int(data["PSkillLvlrequired2"]);
            List<int> skillsList = ReadDBList(data, "BattlerClass", "Skill", "LevelRequired");
            for (int i = 0; i < skillsList.Count;)
            {
                SkillSet.Add(ReadObj(skillsData, skillsList[i++]));
                SkillSetLevels.Add(skillsList[i++]);
            }
        }

        public BattlerClass(BattlerClass original) : base(original)
        {
            BaseStats = Clone(original.BaseStats, o => new Stats(o));
            UpgradedClass1 = Clone(original.UpgradedClass1, o => new BattlerClass(o));
            UpgradedClass2 = Clone(original.UpgradedClass2, o => new BattlerClass(o));
            UsableWeapon1Type = original.UsableWeapon1Type;
            UsableWeapon2Type = original.UsableWeapon2Type;
            PassiveSkill1 = Clone(original.PassiveSkill1, o => new PassiveSkill(o));
            PassiveSkill2 = Clone(original.PassiveSkill2, o => new PassiveSkill(o));
            PassiveSkillLvlRequired1 = original.PassiveSkillLvlRequired1;
            PassiveSkillLvlrequired2 = original.PassiveSkillLvlrequired2;
            SkillSet = Clone(original.SkillSet, o => new Skill(o));
            SkillSetLevels = Clone(original.SkillSetLevels);
        }
    }
}
