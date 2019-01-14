using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleSimulator.Classes;
using BattleSimulator.Simulator;
using BattleSimulator.Utilities;
using BattleSimulator.Templates;
using System.IO;
using static BattleSimulator.Utilities.FileHelper;

namespace BattleSimulator
{
    public partial class RPGBattle : Form
    {
        public Classes.Environment Environment { get; private set; }
        public List<Player> Players { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        private RPGBattler[] PlayersUI, EnemiesUI;

        public int NumberOfPlayers { get; private set; }
        public string InfoText { get; private set; }
        public int Turns { get; private set; }
        public int CurrentPlayer { get; set; }
        public int BattleState { get; private set; }

        private string[] ScopeText = new string[] { "", "Select Enemy", "Select Enemy (Splash)", "Select Row of Enemies", "Select Column of Enemies",
                "Affects All Enemies", "Only Affects User", "Select Ally", "Affects All Allies", "Affects Everyone Except User", "Affects Everyone", "Select Ally/Allies" };


        public RPGBattle(string filenameWithPath)
        {
            AutoSize = true;
            InitializeComponent();
            KeyPreview = true;
            KeyPress += new KeyPressEventHandler(KeyPressed);
            AllData.Setup();
            SetupBattleSession(filenameWithPath);
            InitializeBattlers();
        }

        private void SetupBattleSession(string prtyFilenameWithPath)
        {
            Players = new List<Player>();
            Enemies = new List<Enemy>();
            string partyName;
            using (var file = File.Open(prtyFilenameWithPath, FileMode.Open, FileAccess.Read))
            {
                partyName = ReadText(file);
                int numberOfPlayers = ReadByte(file);
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    Player battlePlayer = new Player(AllData.Players[ReadByte(file)]);
                    battlePlayer.SetCurrentClass(ReadByte(file));
                    battlePlayer.SetAllStats(ReadByte(file));
                    battlePlayer.MoveToPosition(ReadByte(file), ReadByte(file));
                    int[] Ids = new int[9];
                    for (int j = 0; j < 9; j++) Ids[j] = ReadShort(file);
                    for (int j = 0; j < 4; j++) if (Ids[j] > 0) battlePlayer.AddItem(AllData.Items, Ids[j]);
                    for (int j = 4; j < 7; j++) if (Ids[j] > 0) battlePlayer.AddWeapon(AllData.Weapons, Ids[j]);
                    for (int j = 7; j < 9; j++) if (Ids[j] > 0) battlePlayer.AddPassiveSkill(AllData.PassiveSkills, Ids[j]);
                    battlePlayer.MaxHPSP();
                    battlePlayer.AddSkillsFromLevel();
                    Players.Add(battlePlayer);
                }
                for (int i = 0; i < 4; i++) for (int j = i + 1; j < 4; j++) PlayerSetRelationHelper(file, i, j);
                AllData.SetupBattle(ReadShort(file));
            }
            Enemies = AllData.Battle.Enemies;
            PlayersHeader.Text = partyName;
            EnemiesHeader.Text = AllData.Battle.Name;
            Turns = 0;
            NumberOfPlayers = Players.Count;
            ScopeCommand.Text = "";
            StartTurn();
        }
        private void PlayerSetRelationHelper(FileStream file, int i, int j)
        {
            int value = ReadByte(file);
            if (i >= Players.Count || j >= Players.Count) return;
            Players[i].SetRelation(Players[j].Id, value);
            Players[j].SetRelation(Players[i].Id, value);
        }

        private void InitializeBattlers()
        {
            PlayersUI = new RPGBattler[] { PLF, PLC, PLB, PCF, PCC, PCB, PRF, PRC, PRB };
            EnemiesUI = new RPGBattler[] { ELF, ELC, ELB, ECF, ECC, ECB, ERF, ERC, ERB };
            string letterKeys = "QWEASDZXC";
            for (int i = 0; i < 9; i++)
            {
                PlayersUI[i].Visible = false;
                EnemiesUI[i].Visible = false;
                PlayersUI[i].SetLetterKey(letterKeys[i]);
                EnemiesUI[i].SetLetterKey(letterKeys[i]);
            }
            for (int i = 0; i < Players.Count; i++) PlayersUI[Players[i].XPosition * 3 + Players[i].ZPosition].Initialize(Players[i]);
            for (int i = 0; i < Enemies.Count; i++) EnemiesUI[Enemies[i].XPosition * 3 + Enemies[i].ZPosition].Initialize(Enemies[i]);
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            Player p = Players[CurrentPlayer];
            char o = e.KeyChar;
            if (o == 'o') MessageBox.Show(InfoText);
            switch (BattleState)
            {
                case 1: // Action
                    if (o == 'q' && p.Skills.Count > 0) SoloSkillSelection();
                    else if (o == 'w' && p.Items.Count > 0) ItemSelection();
                    else if (o == 'e' && p.ComboSkills.Count > 0) ComboSkillSelection();
                    else if (o == 'a') { p.SelectedSkill = AllData.Skills[1]; WeaponAndTargetSelection(); }
                    else if (o == 's') MoveSelection();
                    else if (o == 'd') { p.SelectedSkill = AllData.Skills[2]; FinishSelection(); }
                    else if (o == 'z' && CurrentPlayer == 0) AutoBattle();
                    else if (o == 'x' && CurrentPlayer == 0) RunAway();
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 2: // Solo Skill
                    if (o == 'q') ActionSelection();
                    else if (o == 'w' && p.Items.Count > 0) ItemSelection();
                    else if (o == 'e' && p.ComboSkills.Count > 0) ComboSkillSelection();
                    else if (o == 'a') SelectSkill(0);
                    else if (o == 's' && p.Skills.Count > 1) SelectSkill(1);
                    else if (o == 'z' && p.Skills.Count > 2) SelectSkill(2);
                    else if (o == 'x' && p.Skills.Count > 3) SelectSkill(3);
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 3: // Item
                    if (o == 'q' && p.Skills.Count > 0) SoloSkillSelection();
                    else if (o == 'w') ActionSelection();
                    else if (o == 'e' && p.ComboSkills.Count > 0) ComboSkillSelection();
                    else if (o == 'a') SelectItem(0);
                    else if (o == 's' && p.Items.Count > 1) SelectItem(1);
                    else if (o == 'z' && p.Items.Count > 2) SelectItem(2);
                    else if (o == 'x' && p.Items.Count > 0) SelectItem(3);
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 4: // Combo Skill
                    if (o == 'q' && p.Skills.Count > 0) SoloSkillSelection();
                    else if (o == 'w' && p.Items.Count > 0) ItemSelection();
                    else if (o == 'e') ActionSelection();
                    else if (o == 'a') SelectSkill(0);
                    else if (o == 's' && p.ComboSkills.Count > 1) SelectSkill(1);
                    else if (o == 'd' && p.ComboSkills.Count > 2) SelectSkill(2);
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 5: // Move
                    if (o == 'q') SelectPartner(0, 2);
                    else if (o == 'w') SelectPartner(0, 1);
                    else if (o == 'e') SelectPartner(0, 0);
                    else if (o == 'a') SelectPartner(1, 2);
                    else if (o == 's') SelectPartner(1, 1);
                    else if (o == 'd') SelectPartner(1, 0);
                    else if (o == 'z') SelectPartner(2, 2);
                    else if (o == 'x') SelectPartner(2, 1);
                    else if (o == 'c') SelectPartner(2, 0);
                    else if (o == 'p') ActionSelection();
                    break;
                case 6: // Partner
                    if (o == 'q') SelectPartner(0, 2);
                    else if (o == 'w') SelectPartner(0, 1);
                    else if (o == 'e') SelectPartner(0, 0);
                    else if (o == 'a') SelectPartner(1, 2);
                    else if (o == 's') SelectPartner(1, 1);
                    else if (o == 'd') SelectPartner(1, 0);
                    else if (o == 'z') SelectPartner(2, 2);
                    else if (o == 'x') SelectPartner(2, 1);
                    else if (o == 'c') SelectPartner(2, 0);
                    else if (o == 'p') ActionSelection();
                    break;
                case 7: // Item Target
                    if (o == 'q') SelectPartner(0, 0);
                    else if (o == 'w') SelectPartner(1, 0);
                    else if (o == 'e') SelectPartner(2, 0);
                    else if (o == 'a') SelectPartner(0, 1);
                    else if (o == 's') SelectPartner(1, 1);
                    else if (o == 'd') SelectPartner(2, 1);
                    else if (o == 'z') SelectPartner(0, 2);
                    else if (o == 'x') SelectPartner(1, 2);
                    else if (o == 'c') SelectPartner(0, 2);
                    else if (o == 'p') ActionSelection();
                    else if (o == 'l') FinishSelection();
                    break;
                case 8: // Weapon and Target
                    if (o == 'q') { }
                    else if (o == 'w') { }
                    else if (o == 'e') { }
                    else if (o == 'a') { }
                    else if (o == 's') { }
                    else if (o == 'd') { }
                    else if (o == 'z') { }
                    else if (o == 'x') { }
                    else if (o == 'c') { }
                    else if (o == 'r') { }
                    else if (o == 'f') { }
                    else if (o == 'v') { }
                    else if (o == 'p') ActionSelection();
                    else if (o == 'l') FinishSelection();
                    break;
            }
        }


        private string GetSelection(List<Skill> skills, int i)
        {
            if (skills == null || i >= skills.Count) return "N/A";
            string spCost = skills[i].SPConsume > 0 ? " (" + skills[i].SPConsume + ")" : "";
            string disabledCount = skills[i].DisabledCount > 0 ? " [" + skills[i].DisabledCount + "]" : "";
            return skills[i].Name + spCost + disabledCount;
        }
        private string GetSelection(List<Item> items, int i)
        {
            if (items == null || i >= items.Count) return "N/A";
            return items[i].Name;
        }
        private string GetSelection(List<Weapon> weapons, int i)
        {
            if (weapons == null || i >= weapons.Count) return "N/A";
            string remaining = weapons[i].DefaultQuantity != 0 ? " <" + weapons[i].Quantity + ">" : "";
            return weapons[i].Name + remaining;
        }

        private string GetInfo(List<Skill> skills, int i)
        {
            if (skills == null || i >= skills.Count) return "N/A";
            string element = "Element: " + AllData.Elements[skills[i].Element];
            string power = "Power: " + skills[i].Power;
            string users = "No. of Users: " + skills[i].NumberOfUsers;
            return skills[i].Name + "\n" + element + "\n" + power + "\n" + users + "\n" + skills[i].Description;
        }
        private string GetInfo(List<Item> items, int i)
        {
            if (items == null || i >= items.Count) return "N/A";
            return items[i].Name + "\n" + items[i].Description;
        }
        private string GetInfo(List<Weapon> weapons, int i)
        {
            if (weapons == null || i >= weapons.Count) return "N/A";
            string element = "Element: " + AllData.Elements[weapons[i].Element];
            string power = "Power: " + weapons[i].Power;
            string acc = "Accuracy: " + weapons[i].Accuracy;
            return weapons[i].Name + "\n" + element + "\n" + power + "\n" + acc + "\n" + weapons[i].Description;
        }


        private void StartTurn()
        {
            Turns++;
            TurnNumber.Text = Turns.ToString();
            ActionSelection();
            CurrentPlayer = 0;
        }

        private void ActionSelection()
        {
            BattleState = 1;
            Players[CurrentPlayer].ComboPartners = new List<Player>();
            CommandTracker.Text = Players[CurrentPlayer].Name;
            Commands.Text = "Q: Skills   W: Items   E: Combo Skills\n\nSelect Action:\nA: Attack   S: Move   D: Defend" + (CurrentPlayer == 0 ? "\nZ: Auto     X: Run" : "");
            ScopeCommand.Text = "";
            FixedCommands.Text = (CurrentPlayer != 0 ? "P: Back\n" : "") + "O: Info";
            InfoText = "A: Standard attack\nS: Move to adjacent spot\nD: Halves damage\nZ: Auto battle\nX: Leave the battle";
        }

        private void SoloSkillSelection()
        {
            BattleState = 2;
            List<Skill> ss = Players[CurrentPlayer].Skills;
            CommandTracker.Text = Players[CurrentPlayer].Name;
            Commands.Text = "Q: Back   W: Items   E: Combo Skills\n\nSelect Solo Skill:\nA: " + GetSelection(ss,0) + "   S: " + GetSelection(ss,1) + "\nZ: " + GetSelection(ss,2) + "   X: " + GetSelection(ss,3);
            ScopeCommand.Text = "";
            FixedCommands.Text = (CurrentPlayer != 0 ? "P: Back\n" : "") + "O: Info";
            InfoText = "A: " + GetInfo(ss, 0) + "\n\nS: " + GetInfo(ss, 1) + "\n\nZ: " + GetInfo(ss, 2) + "\n\nX: " + GetInfo(ss, 3);
        }

        private void ItemSelection()
        {
            BattleState = 3;
            List<Item> it = Players[CurrentPlayer].Items;
            Commands.Text = "Q: Skills   W: Back   E: Combo Skills\n\nSelect Item:\nA: " + GetSelection(it, 0) + "   S: " + GetSelection(it, 1) + "\nZ: " + GetSelection(it, 2) + "   X: " + GetSelection(it, 3);
            ScopeCommand.Text = "";
            FixedCommands.Text = (CurrentPlayer != 0 ? "P: Back\n" : "") + "O: Info";
            InfoText = "A: " + GetInfo(it, 0) + "\n\nS: " + GetInfo(it, 1) + "\n\nZ: " + GetInfo(it, 2) + "\n\nX: " + GetInfo(it, 3);
        }

        private void ComboSkillSelection()
        {
            BattleState = 4;
            List<Skill> cs = Players[CurrentPlayer].ComboSkills;
            CommandTracker.Text = Players[CurrentPlayer].Name;
            Commands.Text = "Q: Skills   W: Items   E: Back\n\nSelect Combo Skill:\nA: " + GetSelection(cs, 0) + "   S: " + GetSelection(cs, 1) + "   D: " + GetSelection(cs, 2);
            ScopeCommand.Text = "";
            FixedCommands.Text = (CurrentPlayer != 0 ? "P: Back\n" : "") + "O: Info";
            InfoText = "A: " + GetInfo(cs, 0) + "\n\nS: " + GetInfo(cs, 1) + "\n\nD: " + GetInfo(cs, 2);
        }

        private void MoveSelection()
        {
            BattleState = 5;
            Commands.Text = "Select New Location";
            ScopeCommand.Text = "";
            FixedCommands.Text = "P: Back";
        }

        private void PartnerSelection()
        {
            BattleState = 6;
            Commands.Text = "Select Partner";
            ScopeCommand.Text = "";
            FixedCommands.Text = "P: Back";
        }

        private void TargetSelection()
        {
            BattleState = 7;
            List<Item> it = Players[CurrentPlayer].Items;
            Commands.Text = "Select Item:\nR: " + GetSelection(it, 0) + "   T: " + GetSelection(it, 1) + "\nF: " + GetSelection(it, 2) + "   G: " + GetSelection(it, 3);
            ScopeCommand.Text = ScopeText[Players[CurrentPlayer].SelectedItem.Scope];
            FixedCommands.Text = "P: Back\nO: Info\nL: Confirm";
            InfoText = "R: " + GetInfo(it, 0) + "\n\nT: " + GetInfo(it, 1) + "\n\nF: " + GetInfo(it, 2) + "\n\nG: " + GetInfo(it, 3);
        }

        private void WeaponAndTargetSelection()
        {
            BattleState = 8;
            List<Weapon> wp = Players[CurrentPlayer].Weapons;
            Commands.Text = "Select Weapon\nR: " + GetSelection(wp, 0) + "\nF: " + GetSelection(wp, 1) + "\nV: " + GetSelection(wp, 2);
            ScopeCommand.Text = ScopeText[Players[CurrentPlayer].SelectedSkill.Scope];
            FixedCommands.Text = "P: Back\nO: Info\nL: Confirm";
            InfoText = "R: " + GetInfo(wp, 0) + "\n\nF: " + GetInfo(wp, 1) + "\n\nV: " + GetInfo(wp, 2);
        }

        private void GoToLastPlayer()
        {
            CurrentPlayer--;
            ActionSelection();
        }


        private void SelectSkill(int i)
        {
            Player p = Players[CurrentPlayer];
            Skill s = BattleState == 3 ? p.ComboSkills[i] : p.Skills[i];
            if (s.DisabledCount > 0 || p.SP < s.SPConsume || s.Disabled) return;
            CommandTracker.Text += "\n" + GetSelection(p.Skills, i);
            Players[CurrentPlayer].SelectedSkill = s;
            if (BattleState == 3)
            {
                CommandTracker.Text += "\nwith...";
                ScopeCommand.Text = ScopeText[11];
                PartnerSelection();
                return;
            }
            WeaponAndTargetSelection();
        }

        private void SelectItem(int i)
        {
            Item it = Players[CurrentPlayer].Items[i];
            if (it.Disabled) return;
            CommandTracker.Text += "\n" + GetSelection(Players[CurrentPlayer].Items, i);
            Players[CurrentPlayer].SelectedItem = it;
            ItemSelection();
        }

        private void SelectLocation(int x, int z)
        {

        }

        private void SelectPartner(int x, int z)
        {
            Player cp = Players[CurrentPlayer];
            if (cp.ZPosition == z && cp.XPosition == x) return;
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Player p = Players[i];
                if (p.ZPosition != z || p.XPosition != x) continue;
                Players[CurrentPlayer].ComboPartners.Add(p);
                CommandTracker.Text += "\n" + p.Name;
                break;
            }
            if (cp.ComboPartners == null || cp.ComboPartners.Count + 1 != cp.SelectedSkill.NumberOfUsers) return;
            WeaponAndTargetSelection();
        }

        private void FinishSelection()
        {
            CurrentPlayer++;
            if (CurrentPlayer >= Players.Count) StartAction();
            else ActionSelection();
        }


        private void AutoBattle()
        {

        }

        private void RunAway()
        {

        }


        private void StartAction()
        {
            EndAction();
        }

        private void EndAction()
        {
            EndTurn();
        }

        private void EndTurn()
        {
            StartTurn();
        }
    }
}