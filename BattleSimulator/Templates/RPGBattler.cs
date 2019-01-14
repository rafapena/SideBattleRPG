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
            BackColor = Color.White;
            Visible = true;
            LetterKey.Visible = false;
            Damage.Visible = false;
            Restore.Visible = false;
            State1.Visible = false;
            State2.Visible = false;
            State3.Visible = false;
            BattlerImage.Image = battler.Image;
            BattlerName.Text = battler.Name;
            HP.Text = battler.HP.ToString();
            MaxHP.Text = battler.Stats.MaxHP.ToString();
            SP.Text = battler.SP.ToString();
        }

        public void SetLetterKey(char c)
        {
            LetterKey.Text = c.ToString();
        }

        public void HideLetterKey()
        {
            LetterKey.Visible = false;
        }

        public void ShowLetterKey()
        {
            LetterKey.Visible = true;
        }

        public void SelectBattler()
        {
            if (Visible == false) return;
            BackColor = Color.LightGreen;
        }

        public void DeselectBattler()
        {
            BackColor = Color.White;
        }
    }
}
