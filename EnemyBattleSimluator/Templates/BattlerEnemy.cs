using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database.TableTemplates;

namespace EnemyBattleSimluator.Templates
{
    public partial class BattlerEnemy : UserControl
    {
        public BattlerEnemy()
        {
            InitializeComponent();
            DualInputDBTable skillPanel = new DualInputDBTable();
            skillPanel.Setup();
            SkillPanel.Controls.Add(skillPanel);
        }
    }
}
