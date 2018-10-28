using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes.ClassTemplates;
using System.Drawing;

namespace BattleSimulator.Classes
{
    public class Enemy : Battler
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BossType { get; private set; }
        public int Flying { get; private set; }
        public int Exp { get; private set; }
        public int Gold { get; private set; }

        public List<EnemyToolAI> SkillAI { get; private set; }
        public List<EnemyToolAI> ItemAI { get; private set; }
        public List<EnemyToolAI> WeaponAI { get; private set; }


        private Enemy() { }
        public Enemy(int id, string name, string description, Bitmap image = null) : base(id, name, description, image) { }
        public Enemy(Enemy original) : base(original)
        {
            Width = original.Width;
            Height = original.Height;
            BossType = original.BossType;
            Flying = original.Flying;
            Exp = original.Exp;
            Gold = original.Gold;
            SkillAI = CloneObjectList(original.SkillAI, o => new EnemyToolAI(o));
            ItemAI = CloneObjectList(original.SkillAI, o => new EnemyToolAI(o));
            WeaponAI = CloneObjectList(original.SkillAI, o => new EnemyToolAI(o));
        }

        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public void SetAttributes(int bossType, int flying, int exp, int gold)
        {
            BossType = bossType;
            Flying = flying;
            Exp = exp;
            Gold = gold;
        }

        public void AddSkillAI(EnemyToolAI ai)
        {

        }
        public void AddItemAI(EnemyToolAI ai)
        {

        }
        public void AddWeaponAI(EnemyToolAI ai)
        {

        }
    }
}
