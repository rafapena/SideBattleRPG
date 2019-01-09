using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleSimulator.Classes.ClassTemplates;

namespace BattleSimulator.Templates
{
    public partial class RPGBattler : UserControl
    {
        public RPGBattler()
        {
            InitializeComponent();
        }

        public void Initialize(Battler battler)
        {
            Visible = true;
            Damage.Visible = false;
            Restore.Visible = false;
            State1.Visible = false;
            State2.Visible = false;
            State3.Visible = false;
            BattlerImage.Image = battler.Image;
            BattlerName.Text = battler.Name;
            HP.Text = battler.HP.ToString();
            MaxHP.Text = battler.Stats.MaxHP.ToString();
            SP.Text = "100";
        }
    }
}
