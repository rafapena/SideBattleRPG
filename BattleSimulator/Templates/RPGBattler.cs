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
using System.Threading;

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
            BackColor = RPGBattlerHelper.DEFAULT_COLOR;
            Visible = true;
            LetterKey.Visible = false;
            RemovePopups();
            State1.Visible = false;
            State2.Visible = false;
            BattlerImage.Image = battler.Image;
            BattlerName.Text = battler.Name;
            HP.Text = battler.HP.ToString();
            MaxHP.Text = battler.Stats.MaxHP.ToString();
            SP.Text = battler.SP.ToString();
        }

        public void Transfer(RPGBattler original)
        {
            // IMPLEMENT LATER
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

        public void DisplayMissedOrFailed(string missedOrFailed)
        {
            Hit.Visible = true;
            Hit.Text = missedOrFailed;
            Hit.BackColor = RPGBattlerHelper.MISSED_COLOR;
        }

        public void DisplayActionDamageCritical()
        {
            Hit.Visible = true;
            Hit.Text = "Critical";
            Hit.BackColor = RPGBattlerHelper.CRITICAL_COLOR;
        }

        public void DisplayActionDamageElemental(int elementType, int mag)
        {
            if (elementType < 0 || elementType >= RPGBattlerHelper.ELEMENTS.Length || mag > 2 || mag < -2) return;
            ElementMag.Visible = true;
            ElementMag.Text = RPGBattlerHelper.ELEMENTS[elementType];
            ElementMag.BackColor = RPGBattlerHelper.E_MAG[mag + 2];
        }

        public void DisplaySPConsume(int val)
        {
            SPConsume.Visible = true;
            SPConsume.Text = "-" + val;
            SPMod.BackColor = RPGBattlerHelper.SP_CONSUME_COLOR;
        }
        public void DisplayHPDamage(int val)
        {
            HPMod.Visible = true;
            HPMod.Text = val.ToString();
            HPMod.BackColor = RPGBattlerHelper.HP_DAMAGE_COLOR;
        }
        public void DisplayHPRecover(int val)
        {
            HPMod.Visible = true;
            HPMod.Text = val.ToString();
            HPMod.BackColor = RPGBattlerHelper.HP_RECOVER_COLOR;
        }
        public void DisplaySPDamage(int val)
        {
            SPMod.Visible = true;
            SPMod.Text = val.ToString();
            SPMod.BackColor = RPGBattlerHelper.SP_DAMAGE_COLOR;
        }
        public void DisplaySPRecover(int val)
        {
            SPMod.Visible = true;
            SPMod.Text = val.ToString();
            SPMod.BackColor = RPGBattlerHelper.SP_RECOVER_COLOR;
        }

        public void DisplayActionSteal(int val)
        {
            // IMPLEMENT LATER
        }

        public void DisplayActionStates(string stateName)
        {
            if (State1.Text != "")
            {
                State2.Visible = true;
                State2.Text = State1.Text;
            }
            State1.Visible = true;
            State1.Text = stateName;
        }

        public void ClearStates()
        {
            State1.Text = "";
            State2.Text = "";
            State1.BackColor = RPGBattlerHelper.DEFAULT_COLOR;
            State2.BackColor = RPGBattlerHelper.DEFAULT_COLOR;
            State1.Visible = false;
            State2.Visible = false;
        }

        public void UpdateHP(int val)
        {
            HP.Text = val.ToString();
        }
        public void UpdateSP(int val)
        {
            SP.Text = val.ToString();
        }

        public void RemovePopups()
        {
            SPConsume.Visible = false;
            Hit.Visible = false;
            HPMod.Visible = false;
            SPMod.Visible = false;
            ElementMag.Visible = false;
            State1.BackColor = RPGBattlerHelper.DEFAULT_COLOR;
            State2.BackColor = RPGBattlerHelper.DEFAULT_COLOR;
        }

        public void KOBattler(Color koColor)
        {
            RemovePopups();
            ClearStates();
            BackColor = koColor;
        }


        public static class RPGBattlerHelper
        {
            public static Color DEFAULT_COLOR = Color.White;
            public static Color MISSED_COLOR = Color.Gray;
            public static Color CRITICAL_COLOR = Color.LightYellow;
            public static Color NEW_STATE_COLOR = Color.LightBlue;
            public static Color SP_CONSUME_COLOR = Color.Blue;
            public static Color HP_DAMAGE_COLOR = Color.LightGoldenrodYellow;
            public static Color SP_DAMAGE_COLOR = Color.BlueViolet;
            public static Color HP_RECOVER_COLOR = Color.LightGreen;
            public static Color SP_RECOVER_COLOR = Color.LightBlue;

            public static Color[] E_MAG = new Color[] { Color.DarkViolet, Color.Violet, DEFAULT_COLOR, Color.Orange, Color.OrangeRed };
            public static string[] ELEMENTS = new string[] { "N", "F", "I", "E", "W", "T", "L", "D" };
        }
    }
}
