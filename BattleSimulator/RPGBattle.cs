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
using BattleSimulator.Classes.ClassTemplates;
using BattleSimulator.Simulator;
using BattleSimulator.Utilities;
using BattleSimulator.Templates;
using System.IO;
using static BattleSimulator.Utilities.FileHelper;
using System.Threading;

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
        public int CurrentPlayer { get; private set; }
        public int BattleState { get; private set; }
        public string CommandTrackerHelper { get; private set; }
        private List<int> TurnOrder;
        public int actionWaitTime { get; private set; }
        public Battler CurrentBattler { get; private set; }

        private Color DEFAULT_BATTLER_COLOR = Color.White;
        private Color USER_INDICATION_COLOR = Color.LightGreen;
        private Color SELECTED_TARGET_COLOR = Color.LightSkyBlue;
        private Color LESS_SELECTED_TARGET_COLOR = Color.LightBlue;
        private Color PLAYER_KO_COLOR = Color.Red;
        
        private string[] BattlerKeys = new string[] { "q", "w", "e", "a", "s", "d", "z", "x", "c" };
        private string[] ScopeText = new string[] { "", "Select Enemy", "Select Enemies", "Select Enemies", "Select Enemies", "Affects All Enemies",
                "Only Affects User", "Select Ally", "Affects All Allies", "Affects Everyone Except User", "Affects Everyone" };


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Initialization --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public RPGBattle(string filenameWithPath)
        {
            AutoSize = true;
            InitializeComponent();
            KeyPreview = true;
            KeyPress += new KeyPressEventHandler(KeyPressed);
            AllData.Setup();
            SetupBattleSession(filenameWithPath);
            InitializeBattlers();
            StartTurn();
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
            TurnOrder = new List<int>();
            for (int i = 0; i < Players.Count; i++) TurnOrder.Add(i);
            for (int i = 0; i < Enemies.Count; i++) TurnOrder.Add(i + 16);
            ScopeCommand.Text = "";
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
            PlayersUI = new RPGBattler[] { PLB, PLC, PLF, PCB, PCC, PCF, PRB, PRC, PRF };
            EnemiesUI = new RPGBattler[] { ELF, ELC, ELB, ECF, ECC, ECB, ERF, ERC, ERB };
            for (int i = 0; i < 9; i++)
            {
                PlayersUI[i].Visible = false;
                EnemiesUI[i].Visible = false;
                PlayersUI[i].SetLetterKey(BattlerKeys[i].ToUpper());
                EnemiesUI[i].SetLetterKey(BattlerKeys[i].ToUpper());
            }
            SeparateEnemyDuplicates();
            for (int i = 0; i < Players.Count; i++) PlayersUI[GetPlayerLocation(i)].Initialize(Players[i], i);
            for (int i = 0; i < Enemies.Count; i++) EnemiesUI[GetEnemyLocation(i)].Initialize(Enemies[i], i);
        }

        private void SeparateEnemyDuplicates()
        {
            string[] originalNames = new string[Enemies.Count];
            bool[] dupList = new bool[Enemies.Count];
            for (int i = 0; i < Enemies.Count; i++) originalNames[i] = Enemies[i].Name;
            for (int i = 0; i < Enemies.Count - 1; i++)
            {
                int numberOfDups = 0;
                if (dupList[i]) continue;
                for (int j = i + 1; j < Enemies.Count; j++)
                {
                    if (originalNames[i] != originalNames[j]) continue;
                    if (!dupList[i])
                    {
                        Enemies[i].ClarifyName(65);
                        dupList[i] = true;
                    }
                    numberOfDups++;
                    Enemies[j].ClarifyName(65 + numberOfDups);
                    dupList[j] = true;
                }
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Keyboard Inputs --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            Player p = Players[CurrentPlayer];
            char o = char.ToLower(e.KeyChar);
            if (o == 'o' && (BattleState < 5 || BattleState > 7)) MessageBox.Show(InfoText);
            switch (BattleState)
            {
                case 1: // Action
                    if (o == 'q') SoloSkillSelection();
                    else if (o == 'w') ItemSelection();
                    else if (o == 'e') ComboSkillSelection();
                    else if (o == 'a') { p.SelectedSkill = AllData.Skills[1]; WeaponAndTargetSelection(); }
                    else if (o == 's') MoveSelection();
                    else if (o == 'd') { p.SelectedSkill = AllData.Skills[2]; FinishSelection(); }
                    else if (o == 'z' && CurrentPlayer == 0) AutoBattle();
                    else if (o == 'x' && CurrentPlayer == 0) RunAway();
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 2: // Solo Skill
                    if (o == 'q') ActionSelection();
                    else if (o == 'w') ItemSelection();
                    else if (o == 'e') ComboSkillSelection();
                    else if (o == 'a' && p.Skills.Count > 0) SelectSkill(0);
                    else if (o == 's' && p.Skills.Count > 1) SelectSkill(1);
                    else if (o == 'z' && p.Skills.Count > 2) SelectSkill(2);
                    else if (o == 'x' && p.Skills.Count > 3) SelectSkill(3);
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 3: // Item
                    if (o == 'q') SoloSkillSelection();
                    else if (o == 'w') ActionSelection();
                    else if (o == 'e') ComboSkillSelection();
                    else if (o == 'a' && p.Items.Count > 0) SelectItem(0);
                    else if (o == 's' && p.Items.Count > 1) SelectItem(1);
                    else if (o == 'z' && p.Items.Count > 2) SelectItem(2);
                    else if (o == 'x' && p.Items.Count > 3) SelectItem(3);
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 4: // Combo Skill
                    if (o == 'q') SoloSkillSelection();
                    else if (o == 'w') ItemSelection();
                    else if (o == 'e') ActionSelection();
                    else if (o == 'a' && p.ComboSkills.Count > 0) SelectSkill(0);
                    else if (o == 's' && p.ComboSkills.Count > 1) SelectSkill(1);
                    else if (o == 'd' && p.ComboSkills.Count > 2) SelectSkill(2);
                    else if (o == 'p' && CurrentPlayer != 0) GoToLastPlayer();
                    break;
                case 5: // Move
                    for (int i = 0; i < BattlerKeys.Length; i++) if (o.ToString() == BattlerKeys[i]) { SelectLocation(i); break; }
                    if (o == 'p') { HideAllInactiveRPGBattlers(); ActionSelection(); }
                    break;
                case 6: // Partner
                    for (int i = 0; i < BattlerKeys.Length; i++) if (o.ToString() == BattlerKeys[i]) { SelectPartner(i); break; }
                    if (o == 'p') ActionSelection();
                    break;
                case 7: // Item Target
                    for (int i = 0; i < BattlerKeys.Length; i++) if (o.ToString() == BattlerKeys[i]) { SelectTarget(i); break; }
                    if (o == 'p') ActionSelection();
                    else if (o == 'l') FinishSelection();
                    break;
                case 8: // Weapon and Target
                    for (int i = 0; i < BattlerKeys.Length; i++) if (o.ToString() == BattlerKeys[i]) { SelectTarget(i); break; }
                    if (o == 'r' && p.Weapons.Count > 0) SelectWeapon(0);
                    else if (o == 'f' && p.Weapons.Count > 1) SelectWeapon(1);
                    else if (o == 'v' && p.Weapons.Count > 2) SelectWeapon(2);
                    else if (o == 'p') ActionSelection();
                    else if (o == 'l') FinishSelection();
                    break;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Scope Functionalities --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void ResetTurnUI()
        {
            CommandTracker.Text = "";
            for (int i = 0; i < PlayersUI.Length; i++)
            {
                PlayersUI[i].HideLetterKey();
                EnemiesUI[i].HideLetterKey();
                if (PlayersUI[i].BackColor == PLAYER_KO_COLOR) continue;
                PlayersUI[i].BackColor = DEFAULT_BATTLER_COLOR;
                EnemiesUI[i].BackColor = DEFAULT_BATTLER_COLOR;
            }
        }

        private void ShowPlayerKeys()
        {
            foreach (RPGBattler p in PlayersUI) if (p.Visible) p.ShowLetterKey();
        }
        private void ShowEnemyKeys()
        {
            foreach (RPGBattler e in EnemiesUI) if (e.Visible) e.ShowLetterKey();
        }

        private void HideAllInactiveRPGBattlers()
        {
            for (int i = 0; i < PlayersUI.Length; i++)
            {
                if (PlayersUI[i].OnlyShowingLetterKey) PlayersUI[i].ResetFromOnlyShowingLetterKey();
                if (EnemiesUI[i].OnlyShowingLetterKey) EnemiesUI[i].ResetFromOnlyShowingLetterKey();
            }
        }

        private int GetPlayerLocation(int i)
        {
            return Players[i].XPosition * 3 + 2 - Players[i].ZPosition;
        }
        private int GetEnemyLocation(int i)
        {
            return Enemies[i].XPosition * 3 + Enemies[i].ZPosition;
        }

        private int GetToolScope()
        {
            Player p = Players[CurrentPlayer];
            return p.SelectedSkill == null ? p.SelectedItem.Scope : p.SelectedSkill.Scope;
        }

        private bool BattlerSelectable(RPGBattler battlerUi)
        {
            return battlerUi.Visible;
        }

        private int[] ScopeSplash(int pos)
        {
            int[][] positions = new int[][] {
                new int[] { 1, 3, 4 }, new int[] { 0, 2, 3, 4, 5 }, new int[] { 1, 4, 5 },
                new int[] { 0, 1, 4, 6, 7 }, new int[] { 0, 1, 2, 3, 5, 6, 7, 8 }, new int[] { 1, 2, 4, 7, 8 },
                new int[] { 3, 4, 7 }, new int[] { 3, 4, 5, 6, 8 }, new int[] { 4, 5, 7 }
            };
            return positions[pos];
        }
        private int[] ScopeRow(int pos)
        {
            if (pos <= 2) return new int[] { 0, 1, 2 };
            else if (pos <= 5) return new int[] { 3, 4, 5 };
            return new int[] { 6, 7, 8 };
        }
        private int[] ScopeColumn(int pos)
        {
            if (pos % 3 == 0) return new int[] { 0, 3, 6 };
            else if (pos % 3 == 1) return new int[] { 1, 4, 7 };
            return new int[] { 2, 5, 8 };
        }

        private void SelectPlayerTarget(int uiPos, int battlerIndex, Color c, bool koSelectable=false)
        {
            if (!BattlerSelectable(PlayersUI[uiPos]) || PlayersUI[uiPos].BackColor == PLAYER_KO_COLOR && !koSelectable) return;
            PlayersUI[uiPos].BackColor = c;
            Players[CurrentPlayer].SelectedTargets.Add(Players[battlerIndex]);
        }
        private void SelectEnemyTarget(int uiPos, int battlerIndex, Color c, bool koSelectable = false)
        {
            if (!BattlerSelectable(EnemiesUI[uiPos]) || PlayersUI[uiPos].BackColor == PLAYER_KO_COLOR && !koSelectable) return;
            EnemiesUI[uiPos].BackColor = c;
            Players[CurrentPlayer].SelectedTargets.Add(Enemies[battlerIndex]);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Tool Selection and Info --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Selections --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void ActionSelection()
        {
            BattleState = 1;
            ResetTurnUI();
            PlayersUI[GetPlayerLocation(CurrentPlayer)].BackColor = USER_INDICATION_COLOR;
            Player p = Players[CurrentPlayer];
            p.ComboPartners.Clear();
            p.SelectedTargets.Clear();
            p.MovingLocation = -1;
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
            Commands.Text = "Q: Back   W: Items   E: Combo Skills\n\nSelect Solo Skill:\nA: " + GetSelection(ss,0) + "   S: " + GetSelection(ss,1) + "\nZ: " + GetSelection(ss,2) + "   X: " + GetSelection(ss,3);
            FixedCommands.Text = (CurrentPlayer != 0 ? "P: Back\n" : "") + "O: Info";
            InfoText = "A: " + GetInfo(ss, 0) + "\n\nS: " + GetInfo(ss, 1) + "\n\nZ: " + GetInfo(ss, 2) + "\n\nX: " + GetInfo(ss, 3);
        }

        private void ItemSelection()
        {
            BattleState = 3;
            List<Item> it = Players[CurrentPlayer].Items;
            Commands.Text = "Q: Skills   W: Back   E: Combo Skills\n\nSelect Item:\nA: " + GetSelection(it, 0) + "   S: " + GetSelection(it, 1) + "\nZ: " + GetSelection(it, 2) + "   X: " + GetSelection(it, 3);
            FixedCommands.Text = (CurrentPlayer != 0 ? "P: Back\n" : "") + "O: Info";
            InfoText = "A: " + GetInfo(it, 0) + "\n\nS: " + GetInfo(it, 1) + "\n\nZ: " + GetInfo(it, 2) + "\n\nX: " + GetInfo(it, 3);
        }

        private void ComboSkillSelection()
        {
            BattleState = 4;
            List<Skill> cs = Players[CurrentPlayer].ComboSkills;
            Commands.Text = "Q: Skills   W: Items   E: Back\n\nSelect Combo Skill:\nA: " + GetSelection(cs, 0) + "   S: " + GetSelection(cs, 1) + "   D: " + GetSelection(cs, 2);
            FixedCommands.Text = (CurrentPlayer != 0 ? "P: Back\n" : "") + "O: Info";
            InfoText = "A: " + GetInfo(cs, 0) + "\n\nS: " + GetInfo(cs, 1) + "\n\nD: " + GetInfo(cs, 2);
        }

        private void MoveSelection()
        {
            BattleState = 5;
            foreach (RPGBattler p in PlayersUI) if (!p.Visible) p.OnlyDisplayLetterKey();
            Commands.Text = "Select New Location";
            FixedCommands.Text = "P: Back";
        }

        private void PartnerSelection()
        {
            BattleState = 6;
            ShowPlayerKeys();
            PlayersUI[GetPlayerLocation(CurrentPlayer)].HideLetterKey();
            Commands.Text = "Select " + (Players[CurrentPlayer].SelectedSkill.NumberOfUsers == 2 ? "Ally" : "Allies");
            FixedCommands.Text = "P: Back";
        }

        private void TargetSelection()
        {
            BattleState = 7;
            Commands.Text = "";
            ScopeCommand.Text = ScopeText[GetToolScope()];
            FixedCommands.Text = "P: Back\nL: Confirm";
            TargetSelectionScopes();
        }

        private void WeaponAndTargetSelection()
        {
            BattleState = 8;
            List<Weapon> wp = Players[CurrentPlayer].Weapons;
            CommandTrackerHelper = CommandTracker.Text;
            int selectedWpnId = -1;
            for (int i = 0; i < wp.Count; i++) if (!wp[i].Disabled) { selectedWpnId = i; break; }
            SelectWeapon(selectedWpnId);
            Commands.Text = "Select Weapon\nR: " + GetSelection(wp, 0) + "\nF: " + GetSelection(wp, 1) + "\nV: " + GetSelection(wp, 2);
            ScopeCommand.Text = ScopeText[Players[CurrentPlayer].SelectedSkill.Scope];
            FixedCommands.Text = "P: Back\nO: Info\nL: Confirm";
            InfoText = "R: " + GetInfo(wp, 0) + "\n\nF: " + GetInfo(wp, 1) + "\n\nV: " + GetInfo(wp, 2);
            TargetSelectionScopes();
        }

        private void TargetSelectionScopes()
        {
            Player p = Players[CurrentPlayer];
            int scope = GetToolScope();
            int firstEnemyLocation = GetEnemyLocation(0);
            switch (scope)
            {
                case 1:
                    ShowEnemyKeys();
                    SelectEnemyTarget(firstEnemyLocation, 0, SELECTED_TARGET_COLOR);
                    break;
                case 2:
                    ShowEnemyKeys();
                    SelectEnemyTarget(firstEnemyLocation, 0, SELECTED_TARGET_COLOR);
                    int[] initialTarget = ScopeSplash(firstEnemyLocation);
                    for (int i = 0; i < initialTarget.Length; i++)
                        SelectEnemyTarget(initialTarget[i], EnemiesUI[initialTarget[i]].BattlerIndex, LESS_SELECTED_TARGET_COLOR);
                    break;
                case 3:
                case 4:
                    ShowEnemyKeys();
                    int[] initialTargets = scope == 3 ? ScopeRow(firstEnemyLocation) : ScopeColumn(firstEnemyLocation);
                    for (int i = 0; i < initialTargets.Length; i++)
                        SelectEnemyTarget(initialTargets[i], EnemiesUI[initialTargets[i]].BattlerIndex, SELECTED_TARGET_COLOR);
                    break;
                case 5:
                    for (int i = 0; i < EnemiesUI.Length; i++) SelectEnemyTarget(i, EnemiesUI[i].BattlerIndex, SELECTED_TARGET_COLOR);
                    break;
                case 6:
                case 7:
                    if (scope == 7) ShowPlayerKeys();
                    SelectPlayerTarget(GetPlayerLocation(CurrentPlayer), CurrentPlayer, SELECTED_TARGET_COLOR, true);
                    break;
                case 8:
                    for (int i = 0; i < PlayersUI.Length; i++) SelectPlayerTarget(i, PlayersUI[i].BattlerIndex, SELECTED_TARGET_COLOR, true);
                    break;
                case 9:
                case 10:
                    for (int i = 0; i < PlayersUI.Length; i++)
                        if (PlayersUI[i].BattlerIndex != CurrentPlayer || scope != 9) SelectPlayerTarget(i, PlayersUI[i].BattlerIndex, SELECTED_TARGET_COLOR);
                    for (int i = 0; i < EnemiesUI.Length; i++) SelectEnemyTarget(i, EnemiesUI[i].BattlerIndex, SELECTED_TARGET_COLOR);
                    break;
            }
        }

        private void GoToLastPlayer()
        {
            CurrentPlayer--;
            ActionSelection();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Select Confirmations --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void SelectSkill(int i)
        {
            Player p = Players[CurrentPlayer];
            Skill s = BattleState == 4 ? p.ComboSkills[i] : p.Skills[i];
            if (s.DisabledCount > 0 || p.SP < s.SPConsume || s.Disabled || s.NumberOfUsers > Players.Count) return;
            CommandTracker.Text += "\ndoes...\n" + GetSelection(BattleState == 4 ? p.ComboSkills : p.Skills, i);
            p.SelectedSkill = s;
            p.SelectedItem = null;
            if (BattleState == 4)
            {
                CommandTracker.Text += "\nwith...";
                PartnerSelection();
            }
            else if (p.SelectedSkill.Type % 2 == 1) WeaponAndTargetSelection();
            else TargetSelection();
        }

        private void SelectItem(int i)
        {
            Player p = Players[CurrentPlayer];
            Item it = p.Items[i];
            if (it.Disabled) return;
            CommandTracker.Text += "\nuses...\n" + GetSelection(p.Items, i);
            p.SelectedSkill = null;
            p.SelectedItem = it;
            TargetSelection();
        }

        private void SelectLocation(int pos)
        {
            if (!PlayersUI[pos].OnlyShowingLetterKey) return;
            int prevPos = GetPlayerLocation(CurrentPlayer);
            Players[CurrentPlayer].MovingLocation = pos;
            HideAllInactiveRPGBattlers();
            FinishSelection();
        }

        private void SelectPartner(int pos)
        {
            RPGBattler p = PlayersUI[pos];
            if (!BattlerSelectable(p) || p.BackColor == USER_INDICATION_COLOR) return;
            p.BackColor = USER_INDICATION_COLOR;
            Players[CurrentPlayer].ComboPartners.Add(Players[p.BattlerIndex]);
            CommandTracker.Text += "\n" + Players[p.BattlerIndex].Name;
            Player cp = Players[CurrentPlayer];
            if (cp.ComboPartners == null || cp.ComboPartners.Count + 1 != cp.SelectedSkill.NumberOfUsers) return;
            foreach (RPGBattler pui in PlayersUI) pui.HideLetterKey();
            WeaponAndTargetSelection();
        }

        private void SelectWeapon(int i)
        {
            CommandTracker.Text = CommandTrackerHelper + "\nwielding...\n";
            if (i >= 0)
            {
                Weapon wp = Players[CurrentPlayer].Weapons[i];
                if (wp.Disabled) return;
                CommandTracker.Text += GetSelection(Players[CurrentPlayer].Weapons, i);
                return;
            }
            CommandTracker.Text += "Nothing";
            Players[CurrentPlayer].SelectedWeapon = null;
        }

        private void SelectTarget(int pos)
        {
            Player p = Players[CurrentPlayer];
            int scope = GetToolScope();
            int prevLocation;
            switch (scope)
            {
                case 1:
                    if (!BattlerSelectable(EnemiesUI[pos])) break;
                    prevLocation = p.SelectedTargets[0].XPosition * 3 + p.SelectedTargets[0].ZPosition;
                    EnemiesUI[prevLocation].BackColor = DEFAULT_BATTLER_COLOR;
                    p.SelectedTargets.RemoveRange(0, 1);
                    SelectEnemyTarget(pos, EnemiesUI[pos].BattlerIndex, SELECTED_TARGET_COLOR);
                    break;
                case 2:
                    if (!BattlerSelectable(EnemiesUI[pos])) break;
                    foreach (RPGBattler e in EnemiesUI) e.BackColor = DEFAULT_BATTLER_COLOR;
                    p.SelectedTargets.Clear();
                    SelectEnemyTarget(pos, EnemiesUI[pos].BattlerIndex, SELECTED_TARGET_COLOR);
                    int[] target = ScopeSplash(pos);
                    for (int i = 0; i < target.Length; i++) SelectEnemyTarget(target[i], EnemiesUI[target[i]].BattlerIndex, LESS_SELECTED_TARGET_COLOR);
                    break;
                case 3:
                case 4:
                    if (!BattlerSelectable(EnemiesUI[pos])) break;
                    foreach (RPGBattler e in EnemiesUI) e.BackColor = DEFAULT_BATTLER_COLOR;
                    p.SelectedTargets.Clear();
                    int[] targets = scope == 3 ? ScopeRow(pos) : ScopeColumn(pos);
                    for (int i = 0; i < targets.Length; i++) SelectEnemyTarget(targets[i], EnemiesUI[targets[i]].BattlerIndex, SELECTED_TARGET_COLOR);
                    break;
                case 7:
                    if (!BattlerSelectable(PlayersUI[pos])) break;
                    prevLocation = p.SelectedTargets[0].XPosition * 3 + 2 - p.SelectedTargets[0].ZPosition;
                    if (prevLocation == GetPlayerLocation(CurrentPlayer)) PlayersUI[prevLocation].BackColor = USER_INDICATION_COLOR;
                    else if (Players[PlayersUI[prevLocation].BattlerIndex].HP <= 0) PlayersUI[prevLocation].BackColor = PLAYER_KO_COLOR;
                    else PlayersUI[prevLocation].BackColor = DEFAULT_BATTLER_COLOR;
                    p.SelectedTargets.RemoveRange(0, 1);
                    SelectPlayerTarget(pos, PlayersUI[pos].BattlerIndex, SELECTED_TARGET_COLOR, true);
                    break;
            }
        }

        private void FinishSelection()
        {
            CurrentPlayer++;
            if (CurrentPlayer >= Players.Count) TurnActions();
            else ActionSelection();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Overall Action Executions --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void AutoBattle()
        {
            MessageBox.Show("Autobattle feature not implemented");
        }

        private void RunAway()
        {
            double playerAv = 0;
            foreach (Player p in Players) playerAv += p.Stats.Spd;
            playerAv /= Players.Count;
            double enemyAv = 0;
            foreach (Enemy e in Enemies) enemyAv += e.Stats.Spd;
            enemyAv /= Enemies.Count;
            MessageBox.Show("Ran away from battle: " + (50 + (playerAv - enemyAv) * 5) + "%");
            Close();
        }

        private void StartTurn()
        {
            Turns++;
            TurnNumber.Text = Turns.ToString();
            foreach (Player p in Players) p.ExecutedAction = false;
            foreach (Enemy e in Enemies) e.ExecutedAction = false;
            SortTurnOrder();
            CurrentPlayer = 0;
            ActionSelection();
        }

        private void SortTurnOrder()
        {
            for (int i = 1; i < TurnOrder.Count; i++)
            {
                int j = i;
                bool flag = true;
                while (j > 0 && flag)
                {
                    int curr = GetOrder(TurnOrder[j]);
                    int after = GetOrder(TurnOrder[j - 1]);
                    if (after > curr) flag = false;
                    else if (after == curr && Utils.RandInt(1, 2) == 1) j -= 2;
                    else
                    {
                        int tmp = TurnOrder[j];
                        TurnOrder[j] = TurnOrder[j - 1];
                        TurnOrder[j - 1] = tmp;
                        j--;
                    }
                }
            }
        }
        private int GetOrder(int id)
        {
            Battler b;
            if (id >= 16) b = Enemies[id - 16];
            else b = Players[id];
            int priority = 0;
            if (b.SelectedSkill != null) priority = b.SelectedSkill.Priority;
            else if (b.SelectedItem != null) priority = b.SelectedItem.Priority;
            return b.Stats.Spd + priority;
        }

        private void TurnActions()
        {
            ResetTurnUI();
            foreach (Enemy e in Enemies) e.DecideMove(Players, Enemies);
            foreach (int id in TurnOrder)
            {
                if (id >= 16) CurrentBattler = Enemies[id - 16];
                else CurrentBattler = Players[id];
                if (!CurrentBattler.IsConscious() || CurrentBattler.ExecutedAction) continue;
                StartAction();
            }
            EndTurn();
        }

        private void EndTurn()
        {
            foreach (Player p in Players) p.ApplyEndTurnEffects();
            foreach (Enemy e in Enemies) e.ApplyEndTurnEffects();
            StartTurn();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Battler Action Executions --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void StartAction()
        {
            CurrentBattler.ApplyStartActionEffects();
            Commands.Text = "";
            DisplayMessage(CurrentBattler);
            CurrentBattler.ExecuteTool(Environment);
            EndAction();
        }

        private void DisplayMessage(Battler user)
        {
            string selectedTool = "";
            int scope = 0;
            if (user.SelectedSkill != null)
            {
                selectedTool = "Does " + user.SelectedSkill.Name;
                scope = user.SelectedSkill.Scope;
                if (user.SelectedSkill.NumberOfUsers > 1)
                {
                    selectedTool += " with";
                    foreach (Battler bt in user.ComboPartners) selectedTool += " " + bt.Name;
                }
            }
            else if (user.SelectedItem != null)
            {
                selectedTool = "Uses " + user.SelectedItem.Name;
                scope = user.SelectedItem.Scope;
            }
            else selectedTool = "Does Nothing...";
            string selectedWeapon = (user.SelectedWeapon != null) ? " wielding " + user.SelectedWeapon.Name : "";
            string targets = "";
            switch (scope)
            {
                case 1:
                case 2:
                    targets += "\nagainst " + user.SelectedTargets[0].Name; break;
                case 3:
                    targets += "\nagainst the ";
                    string[] rows = new string[] { "left", "center", "right" };
                    targets += rows[user.SelectedTargets[0].XPosition] + " row";
                    break;
                case 4:
                    targets += "\nagainst the ";
                    string[] columns = new string[] { "front", "center", "back" };
                    targets += columns[user.SelectedTargets[0].ZPosition] + " column";
                    break;
                case 5: targets += "\nagainst all of their enemies"; break;
                case 6: targets += "\non themself"; break;
                case 7: targets += "\non " + user.SelectedTargets[0].Name; break;
                case 8: targets += "\non all of their allies"; break;
                case 9: targets += "\nagainst everyone but themself"; break;
                case 10: targets += "\naffecting everyone in battle"; break;
            }
            MessageBox.Show(selectedTool + selectedWeapon + targets, user.Name);
        }

        private void EndAction()
        {
            CurrentBattler.ApplyEndActionEffects();
            CurrentBattler.ExecutedAction = true;
        }

        private void CheckWinOrLose()
        {
            bool playersKOd = true;
            foreach (Player p in Players) if (p.IsConscious()) { playersKOd = false; break; }
            if (playersKOd) LoseBattle();
            bool enemiesKOd = true;
            foreach (Enemy e in Enemies) if (e.IsConscious()) { enemiesKOd = false; break; }
            if (enemiesKOd) WinBattle();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Battle End --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void WinBattle()
        {
            MessageBox.Show("YOU WIN");
            // TALK ABOUT LEVEL UP STATISTICS
            Close();
        }

        private void LoseBattle()
        {
            MessageBox.Show("GAME OVER");
            Close();
        }
    }
}