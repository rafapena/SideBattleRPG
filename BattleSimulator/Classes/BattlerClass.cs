using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

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
        public List<Skill> SkillSet { get; private set; }
        public List<int> SkillSetLevels { get; private set; }

        private BattlerClass() { }
        public BattlerClass(int id, string name, string description, Bitmap image = null) : base(id, name, description, image)
        {
            SkillSet = new List<Skill>();
            SkillSetLevels = new List<int>();
        }
        public BattlerClass(BattlerClass original) : base(original)
        {
            BaseStats = new Stats(original.BaseStats);
            UpgradedClass1 = new BattlerClass(original.UpgradedClass1);
            UpgradedClass2 = new BattlerClass(original.UpgradedClass2);
            UsableWeapon1Type = original.UsableWeapon1Type;
            UsableWeapon2Type = original.UsableWeapon2Type;
            PassiveSkill1 = new PassiveSkill(original.PassiveSkill1);
            PassiveSkill2 = new PassiveSkill(original.PassiveSkill2);
            PassiveSkillLvlRequired1 = original.PassiveSkillLvlRequired1;
            PassiveSkillLvlrequired2 = original.PassiveSkillLvlrequired2;
            SkillSet = CloneObjectList(original.SkillSet, o => new Skill(o));
            SkillSetLevels = ClonePrimitiveList(original.SkillSetLevels);
        }

        public void SetBaseStats(Stats stats)
        {
            BaseStats = stats;
        }
        public void SetUpgradedClasses(BattlerClass class1, BattlerClass class2)
        {
            UpgradedClass1 = class1;
            UpgradedClass2 = class2;
        }
        public void SetWeaponTypes(int weaponType1, int weaponType2)
        {
            UsableWeapon1Type = weaponType1;
            UsableWeapon2Type = weaponType2;
        }
        public void SetPassiveSkills(PassiveSkill pSkill1, PassiveSkill pSkill2, int lvlReq1, int lvlReq2)
        {
            PassiveSkill1 = pSkill1;
            PassiveSkill2 = pSkill2;
            PassiveSkillLvlRequired1 = lvlReq1;
            PassiveSkillLvlrequired2 = lvlReq2;
        }
    }
}
