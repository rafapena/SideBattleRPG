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
            Hit.Visible = false;
            LetterKey.Visible = false;
            Damage.Visible = false;
            Restore.Visible = false;
            ElementMag.Visible = false;
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
            Hit.Text = missedOrFailed;
            Hit.BackColor = RPGBattlerHelper.MISSED_COLOR;
        }

        public void DisplayActionDamageCritical()
        {
            Hit.Text = "Critical";
            Hit.BackColor = RPGBattlerHelper.CRITICAL_COLOR;
        }

        public void DisplayActionDamageElemental(int elementType, int mag)
        {
            if (elementType < 0 || elementType >= RPGBattlerHelper.ELEMENTS.Length || mag > 2 || mag < -2) return;
            ElementMag.Text = RPGBattlerHelper.ELEMENTS[elementType];
            ElementMag.BackColor = RPGBattlerHelper.E_MAG[mag + 2];
        }

        public void DisplayHPDamage(int val)
        {
            Damage.Text = val.ToString();
            Damage.BackColor = RPGBattlerHelper.HP_DAMAGE_COLOR;
        }
        public void DisplayHPRecover(int val)
        {
            Restore.Text = val.ToString();
            Restore.BackColor = RPGBattlerHelper.HP_RECOVER_COLOR;
        }
        public void DisplaySPDamage(int val)
        {
            Damage.Text = val.ToString();
            Damage.BackColor = RPGBattlerHelper.SP_DAMAGE_COLOR;
        }
        public void DisplaySPRecover(int val)
        {
            Restore.Text = val.ToString();
            Restore.BackColor = RPGBattlerHelper.SP_RECOVER_COLOR;
        }

        public void DisplayActionSteal(int val)
        {
            // IMPLEMENT LATER
        }

        public void DisplayActionStates(string stateName)
        {
            if (State1.Text != "") State2.Text = State1.Text;
            State1.Text = stateName;
        }

        public void ClearStates()
        {
            State1.Text = "";
            State2.Text = "";
            State1.BackColor = RPGBattlerHelper.DEFAULT_COLOR;
            State2.BackColor = RPGBattlerHelper.DEFAULT_COLOR;
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
            Hit.Visible = false;
            Damage.Visible = false;
            Restore.Visible = false;
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
            public static Color HP_DAMAGE_COLOR = Color.LightGoldenrodYellow;
            public static Color SP_DAMAGE_COLOR = Color.BlueViolet;
            public static Color HP_RECOVER_COLOR = Color.LightGreen;
            public static Color SP_RECOVER_COLOR = Color.LightBlue;

            public static Color[] E_MAG = new Color[] { Color.DarkViolet, Color.Violet, DEFAULT_COLOR, Color.Orange, Color.OrangeRed };
            public static string[] ELEMENTS = new string[] { "N", "F", "I", "E", "W", "T", "L", "D" };
        }
    }
}
