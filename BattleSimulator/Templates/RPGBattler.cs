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
        public int BattlerIndex { get; private set; }
        public bool OnlyShowingLetterKey { get; private set; }

        public RPGBattler()
        {
            InitializeComponent();
        }

        public void Initialize(Battler battler, int battlerIndex)
        {
            BattlerIndex = battlerIndex;
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

        public void Transfer(RPGBattler original)
        {

        }

        public void SetLetterKey(string c)
        {
            LetterKey.Text = c;
        }

        public void HideLetterKey()
        {
            LetterKey.Visible = false;
            OnlyShowingLetterKey = false;
        }

        public void ShowLetterKey()
        {
            LetterKey.Visible = true;
        }

        public void OnlyDisplayLetterKey()
        {
            if (OnlyShowingLetterKey) return;
            Visible = true;
            LetterKey.Visible = true;
            OnlyShowingLetterKey = true;
            Damage.Visible = false;
            Restore.Visible = false;
            State1.Visible = false;
            State2.Visible = false;
            State3.Visible = false;
            BattlerImage.Visible = false;
            BattlerName.Visible = false;
            HP.Visible = false;
            SlashText.Visible = false;
            MaxHP.Visible = false;
            SP.Visible = false;
        }

        public void ResetFromOnlyShowingLetterKey()
        {
            if (!OnlyShowingLetterKey) return;
            Visible = false;
            LetterKey.Visible = false;
            OnlyShowingLetterKey = false;
            Damage.Visible = true;
            Restore.Visible = true;
            State1.Visible = true;
            State2.Visible = true;
            State3.Visible = true;
            BattlerImage.Visible = true;
            BattlerName.Visible = true;
            HP.Visible = true;
            SlashText.Visible = true;
            MaxHP.Visible = true;
            SP.Visible = true;
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
