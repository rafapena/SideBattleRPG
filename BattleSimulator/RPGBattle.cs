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
        private RPGBattler UserUI, TargetUI;
        private Label[] PosUIs;

        public int NumberOfPlayers { get; private set; }
        public string InfoText { get; private set; }
        public int Turns { get; private set; }
        public int CurrentPlayer { get; private set; }
        public int BattleState { get; private set; }
        public string CommandTrackerHelper { get; private set; }
        private List<int> TurnOrder;
        public int ActionWaitTime { get; private set; }
        public Battler CurrentBattler { get; private set; }

        private Color DEFAULT_BATTLER_COLOR = Color.White;
        private Color USER_INDICATION_COLOR = Color.LightGreen;
        private Color SELECTED_TARGET_COLOR = Color.LightSkyBlue;
        private Color LESS_SELECTED_TARGET_COLOR = Color.LightBlue;
        private Color RANDOM_TARGET_COLOR = Color.LightCyan;
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
                Environment = AllData.Environments[ReadShort(file)];
                int numberOfPlayers = ReadByte(file);
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    Player battlePlayer = new Player(AllData.Players[ReadByte(file)]);
                    battlePlayer.SetCurrentClass(AllData.Classes, ReadByte(file));
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
            PosUIs = new Label[] { Pos1, Pos2, Pos3, Pos4, Pos5, Pos6, Pos7, Pos8, Pos9 };
            HidePosUIs();
            for (int i = 0; i < 9; i++)
            {
                PlayersUI[i].Visible = false;
                EnemiesUI[i].Visible = false;
                PlayersUI[i].SetLetterKey(BattlerKeys[i].ToUpper());
                EnemiesUI[i].SetLetterKey(BattlerKeys[i].ToUpper());
            }
            SeparateEnemyDuplicates();
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].SetAsPlayer();
                Players[i].AddPassiveEffects(Environment);
                PlayersUI[GetPlayerLocation(Players[i])].Initialize(Players[i], i);
                foreach (Skill s in Players[i].Skills) s.DisableForWarmup();
            }
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].SetAsEnemy();
                Enemies[i].AddPassiveEffects(Environment);
                EnemiesUI[GetEnemyLocation(Enemies[i])].Initialize(Enemies[i], i);
                foreach (Skill s in Enemies[i].Skills) s.DisableForWarmup();
            }
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
                    else if (o == 'a')
                    {
                        p.SelectedSkill = AllData.Skills[1];
                        WeaponAndTargetSelection();
                    }
                    else if (o == 's') MoveSelection();
                    else if (o == 'd')
                    {
                        p.SelectedSkill = AllData.Skills[2];
                        p.SelectedTargets.Add(p);
                        FinishSelection();
                    }
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
                    if (o == 'p') { HidePosUIs(); ActionSelection(); }
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

        private void HidePosUIs()
        {
            foreach (Label pos in PosUIs) pos.Visible = false;
        }

        private int GetPlayerLocation(Battler p)
        {
            return p.XPosition * 3 + 2 - p.ZPosition;
        }
        private int GetEnemyLocation(Battler e)
        {
            return e.XPosition * 3 + e.ZPosition;
        }

        private int GetToolScope()
        {
            Player p = Players[CurrentPlayer];
            return p.SelectedSkill == null ? p.SelectedItem.Scope : p.SelectedSkill.Scope;
        }
        private int GetToolRandomActs()
        {
            Player p = Players[CurrentPlayer];
            return p.SelectedSkill == null ? p.SelectedItem.RandomActs : p.SelectedSkill.RandomActs;
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

        private void SelectPlayerTarget(int uiPos, int battlerIndex, Color c, bool koSelectable = false, bool addToList = true)
        {
            if (!BattlerSelectable(PlayersUI[uiPos]) || PlayersUI[uiPos].BackColor == PLAYER_KO_COLOR && !koSelectable) return;
            PlayersUI[uiPos].BackColor = c;
            if (addToList) Players[CurrentPlayer].SelectedTargets.Add(Players[battlerIndex]);
        }
        private void SelectEnemyTarget(int uiPos, int battlerIndex, Color c, bool koSelectable = false, bool addToList = true)
        {
            if (!BattlerSelectable(EnemiesUI[uiPos]) || PlayersUI[uiPos].BackColor == PLAYER_KO_COLOR && !koSelectable) return;
            EnemiesUI[uiPos].BackColor = c;
            if (addToList) Players[CurrentPlayer].SelectedTargets.Add(Enemies[battlerIndex]);
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
            Player p = Players[CurrentPlayer];
            PlayersUI[GetPlayerLocation(p)].BackColor = USER_INDICATION_COLOR;
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
            for (int i = 0; i < PlayersUI.Length; i++) if (!PlayersUI[i].Visible) PosUIs[i].Visible = true;
            Commands.Text = "Select New Location";
            FixedCommands.Text = "P: Back";
        }

        private void PartnerSelection()
        {
            BattleState = 6;
            ShowPlayerKeys();
            PlayersUI[GetPlayerLocation(Players[CurrentPlayer])].HideLetterKey();
            Commands.Text = "Select " + (Players[CurrentPlayer].SelectedSkill.NumberOfUsers == 2 ? "Ally" : "Allies");
            FixedCommands.Text = "P: Back";
        }

        private void TargetSelection()
        {
            BattleState = 7;
            Commands.Text = "";
            int randomActs = GetToolRandomActs();
            int scope = GetToolScope();
            ScopeCommand.Text = GetScopeCommandText(randomActs, scope);
            FixedCommands.Text = "P: Back\nL: Confirm";
            TargetSelectionScopes(randomActs, scope);
        }

        private void WeaponAndTargetSelection()
        {
            BattleState = 8;
            List<Weapon> wp = Players[CurrentPlayer].Weapons;
            Skill sk = Players[CurrentPlayer].SelectedSkill;
            CommandTrackerHelper = CommandTracker.Text;
            int selectedWpnId = -1;
            for (int i = 0; i < wp.Count; i++) if (!wp[i].Disabled) { selectedWpnId = i; break; }
            SelectWeapon(selectedWpnId);
            Commands.Text = "Select Weapon\nR: " + GetSelection(wp, 0) + "\nF: " + GetSelection(wp, 1) + "\nV: " + GetSelection(wp, 2);
            ScopeCommand.Text = GetScopeCommandText(sk.RandomActs, sk.Scope);
            FixedCommands.Text = "P: Back\nO: Info\nL: Confirm";
            InfoText = "R: " + GetInfo(wp, 0) + "\n\nF: " + GetInfo(wp, 1) + "\n\nV: " + GetInfo(wp, 2);
            TargetSelectionScopes(sk.RandomActs, sk.Scope);
        }

        private string GetScopeCommandText(int randomActs, int scope)
        {
            if (randomActs > 0)
            {
                if (scope == 1 || scope == 2) return "Selects " + randomActs + "\nEnemy/Enemies\nby Random";
                else if (scope == 3) return "Selects " + randomActs + " Row(s)\nof Enemies\nby Random";
                else if (scope == 4) return "Selects " + randomActs + " Column(s)\nof Enemies\nby Random";
                else if (scope == 7) return "Selects " + randomActs + "\nAlly/Allies\nby Random";
            }
            return ScopeText[GetToolScope()];
        }

        // Helper function for TargetSelection and WeaponAndTargetSelection
        private void TargetSelectionScopes(int randomActs, int scope)
        {
            Player p = Players[CurrentPlayer];
            int firstEnemyLocation = GetEnemyLocation(Enemies[0]);
            if (randomActs > 0)
            {
                if (scope == 7) RandomTargetSelectionScope(randomActs, PlayersUI, Players, SelectPlayerTarget, true);
                else RandomTargetSelectionScope(randomActs, EnemiesUI, Enemies, SelectEnemyTarget, true);
                return;
            }
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
                    SelectPlayerTarget(GetPlayerLocation(p), CurrentPlayer, SELECTED_TARGET_COLOR, true);
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

        // Helper delegate and function for TargetSelectionScopes
        private delegate void SelectTargetFunc(int uiPos, int battlerIndex, Color c, bool koSelectable, bool addToList);
        private void RandomTargetSelectionScope<T>(int randomActs, RPGBattler[] battlerUIList, List<T> battlerList,
            SelectTargetFunc selectFunc, bool koSelectable) where T : Battler
        {
            for (int i = 0; i < battlerUIList.Length; i++) selectFunc(i, battlerUIList[i].BattlerIndex, RANDOM_TARGET_COLOR, koSelectable, false);
            int listSize = battlerList.Count - 1;
            while (randomActs-- > 0) Players[CurrentPlayer].SelectedTargets.Add(RNG.RandList(battlerList));
        }

        private void GoToLastPlayer()
        {
            CurrentPlayer--;
            while (CurrentPlayer >= 0 && !Players[CurrentPlayer].CanMove()) CurrentPlayer--;
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
            else if (p.SelectedSkill.IsOffense()) WeaponAndTargetSelection();
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
            int prevPos = GetPlayerLocation(Players[CurrentPlayer]);
            Players[CurrentPlayer].MovingLocation = pos;
            HidePosUIs();
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
                    if (prevLocation == GetPlayerLocation(p)) PlayersUI[prevLocation].BackColor = USER_INDICATION_COLOR;
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
            while (CurrentPlayer < Players.Count && !Players[CurrentPlayer].CanMove()) CurrentPlayer++;
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
            foreach (Player p in Players) playerAv += p.Spd();
            playerAv /= Players.Count;
            double enemyAv = 0;
            foreach (Enemy e in Enemies) enemyAv += e.Spd();
            enemyAv /= Enemies.Count;
            MessageBox.Show("Ran away from battle: " + (50 + (playerAv - enemyAv) * 5) + "%");
            Close();
        }

        private void StartTurn()
        {
            Turns++;
            TurnNumber.Text = Turns.ToString();
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
                    else if (after == curr && RNG.RandInt(1, 2) == 1) j -= 2;
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
            if (b.SelectedSkill != null) return b.Spd() + b.SelectedSkill.Priority * 1000;
            else if (b.SelectedItem != null) return b.Spd() + b.SelectedItem.Priority * 1000;
            return b.Spd();
        }

        private void TurnActions()
        {
            ResetTurnUI();
            foreach (Enemy e in Enemies) e.DecideMove(Players, Enemies);
            SortTurnOrder();
            foreach (int id in TurnOrder)
            {
                if (id >= 16) CurrentBattler = Enemies[id - 16];
                else CurrentBattler = Players[id];
                UserUI = CurrentBattler.IsEnemy() ? EnemiesUI[GetEnemyLocation(CurrentBattler)] : PlayersUI[GetPlayerLocation(CurrentBattler)];
                if (!CurrentBattler.IsConscious || CurrentBattler.ExecutedAction) continue;
                StartAction();
            }
            EndTurn();
        }

        private void EndTurn()
        {
            foreach (Player p in Players)
            {
                p.ApplyEndTurnEffects(Environment);
                p.ClearTurnChoices();
                p.ExecutedAction = false;
            }
            foreach (Enemy e in Enemies)
            {
                e.ApplyEndTurnEffects(Environment);
                e.ClearTurnChoices();
                e.ExecutedAction = false;
            }
            StartTurn();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Battler Action Executions --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void StartAction()
        {
            int consecutiveActs = 1;
            if (CurrentBattler.SelectedWeapon != null)
            {
                CurrentBattler.StatBoosts.Add(CurrentBattler.SelectedWeapon.EquipBoosts);
                consecutiveActs = CurrentBattler.SelectedWeapon.ConsecutiveActs;
            }
            CurrentBattler.ApplyStartActionEffects(Environment);
            Commands.Text = "";
            DisplayMessage(CurrentBattler);
            if (CurrentBattler.SelectedSkill != null) consecutiveActs *= CurrentBattler.SelectedSkill.ConsecutiveActs;
            else if (CurrentBattler.SelectedItem != null) consecutiveActs *= CurrentBattler.SelectedItem.ConsecutiveActs;
            while (consecutiveActs-- > 0) ExecuteAction();
        }

        private void DisplayMessage(Battler user)
        {
            string selectedTool = "";
            int scope = 0;
            int randomActs = 0;
            if (user.SelectedSkill != null)
            {
                selectedTool = "Does " + user.SelectedSkill.Name;
                scope = user.SelectedSkill.Scope;
                randomActs = user.SelectedSkill.RandomActs;
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
                randomActs = user.SelectedItem.RandomActs;
            }
            else selectedTool = "Does Nothing...";
            string selectedWeapon = (user.SelectedWeapon != null) ? " wielding " + user.SelectedWeapon.Name : "";
            string targets = "";
            if (randomActs > 0 && (1 <= scope && scope <= 4 || scope == 7)) scope += 100;
            switch (scope)
            {
                case 1:
                case 2:
                    targets += "\nagainst " + user.SelectedTargets[0].Name; break;
                case 3: targets += "\nagainst the " + new string[] { "left", "center", "right" }[user.SelectedTargets[0].XPosition] + " row"; break;
                case 4: targets += "\nagainst the " + new string[] { "front", "center", "back" }[user.SelectedTargets[0].ZPosition] + " column"; break;
                case 5: targets += "\nagainst all of their enemies"; break;
                case 6: targets += "\non themself"; break;
                case 7: targets += "\non " + user.SelectedTargets[0].Name; break;
                case 8: targets += "\non themself and all of their allies"; break;
                case 9: targets += "\nagainst everyone but themself"; break;
                case 10: targets += "\naffecting everyone in battle"; break;
                case 101:
                case 102:
                    targets += "\nagainst " + randomActs + " enemy/enemies"; break;
                case 103: targets += "\nagainst " + randomActs + " row(s) of enemies by random"; break;
                case 104: targets += "\nagainst " + randomActs + " column(s) of enemies by random"; break;
                case 107: targets += "\non " + randomActs + " ally/allies by random"; break;
            }
            MessageBox.Show(selectedTool + selectedWeapon + targets, user.Name);
        }

        // TargetResults = { oneTarget, secondTarget, ... }
        // oneTarget = { oneAct, secondAct, ... }
        // oneAct = { missed/critical, elementMagnitude, formulaResult, giveStatesCount, {giveStates}, receiveStatesCount, {receiveStates}, steal }
        private void ExecuteAction()
        {
            Tool actionTool = CurrentBattler.ExecuteTool();
            for (int i = 0; i < CurrentBattler.TargetResults.Count; i++)
            {
                Battler t = CurrentBattler.SelectedTargets[i];
                TargetUI = t.IsEnemy() ? EnemiesUI[GetEnemyLocation(t)] : PlayersUI[GetPlayerLocation(t)];
                foreach (List<int> oneAct in CurrentBattler.TargetResults[i])
                {
                    if (MissedOrFailed(actionTool, i, oneAct[0])) continue;
                    ActionDamageIndicators(actionTool, i, oneAct[0], oneAct[1]);
                    ActionHPSPChange(actionTool, i, oneAct[2]);
                    int givenStates = oneAct[3];
                    int receivedStates = oneAct[3 + givenStates];
                    if (receivedStates + 1 <= oneAct.Count) ActionSteal(i);
                    ActionStates(actionTool, i, oneAct, givenStates, receivedStates);
                    TargetUI.RemovePopups();
                }
            }
            for (int i = 0; i < CurrentBattler.SelectedTargets.Count; i++) KOResult(i);
            EndAction();
        }

        // Helper method for ExecuteAction
        private bool MissedOrFailed(Tool actionTool, int currTarget, int noHitType)
        {
            if (noHitType > 0) return false;
            TargetUI.DisplayMissedOrFailed(actionTool.IsOffense() ? "Missed" : "Failed");
            return true;
        }

        // Helper method for ExecuteAction
        private void ActionDamageIndicators(Tool actionTool, int currTarget, int criticalHitRatio, int elementMagnitude)
        {
            Battler t = CurrentBattler.SelectedTargets[currTarget];
            if (criticalHitRatio > 1) TargetUI.DisplayActionDamageCritical();
            if (elementMagnitude != 0) TargetUI.DisplayActionDamageElemental(actionTool.Element, elementMagnitude);
            switch (elementMagnitude)
            {
                case 0: break;
                case -2: break;
                case -1: break;
                case 1: break;
                case 2: break;
            }
        }

        // Helper method for ExecuteAction
        private void ActionHPSPChange(Tool actionTool, int currTarget, int formulaResult)
        {
            Battler t = CurrentBattler.SelectedTargets[currTarget];
            switch (actionTool.HPSPModType)
            {
                case 0:
                    t.ChangeHP(-formulaResult);
                    TargetUI.DisplayHPDamage(formulaResult);
                    break;
                case 1:
                    t.ChangeSP(-formulaResult);
                    TargetUI.DisplaySPDamage(formulaResult);
                    break;
                case 2:
                    t.ChangeHP(formulaResult);
                    TargetUI.DisplayHPRecover(formulaResult);
                    break;
                case 3:
                    t.ChangeSP(formulaResult);
                    TargetUI.DisplaySPRecover(formulaResult);
                    break;
                case 4:
                    t.ChangeHP(-formulaResult);
                    CurrentBattler.ChangeHP(formulaResult);
                    TargetUI.DisplayHPDamage(formulaResult);
                    UserUI.DisplayHPRecover(formulaResult);
                    break;
                case 5:
                    t.ChangeSP(-formulaResult);
                    CurrentBattler.ChangeSP(formulaResult);
                    TargetUI.DisplaySPDamage(formulaResult);
                    UserUI.DisplaySPRecover(formulaResult);
                    break;
            }
            UserUI.UpdateHP(CurrentBattler.HP);
            UserUI.UpdateSP(CurrentBattler.SP);
            //if (CurrentBattler.IsPlayer()) foreach (Battler comboPartner in CurrentBattler.ComboPartners) PlayersUI[GetPlayerLocation(comboPartner)].UpdateSP(comboPartner.SP);
            //else foreach (Battler comboPartner in CurrentBattler.ComboPartners) EnemiesUI[GetEnemyLocation(comboPartner)].UpdateSP(comboPartner.SP);
            TargetUI.UpdateHP(t.HP);
            TargetUI.UpdateSP(t.SP);
        }

        // Helper method for ExecuteAction
        private void ActionStates(Tool actionTool, int currTarget, List<int> oneAct, int numberOfGivenStates, int numberOfReceivedStates)
        {
            Battler t = CurrentBattler.SelectedTargets[currTarget];
            for (int i = 0; i < numberOfGivenStates; i++)
            {
                int stateId = oneAct[4 + i];
                t.AddState(AllData.States, stateId);
                TargetUI.DisplayActionStates(AllData.States[stateId].Name);
            }
            for (int i = 0; i < numberOfReceivedStates; i++)
            {
                int stateId = oneAct[4 + numberOfGivenStates + i];
                CurrentBattler.AddState(AllData.States, stateId);
                UserUI.DisplayActionStates(AllData.States[stateId].Name);
            }
        }

        // Helper method for ExecuteAction
        private void ActionSteal(int currTarget)
        {
            Battler t = CurrentBattler.SelectedTargets[currTarget];
            // Inidicate stolen item/weapon onto GUI
        }

        private void EndAction()
        {
            CurrentBattler.ApplyEndActionEffects(Environment);
            if (CurrentBattler.SelectedWeapon != null) CurrentBattler.StatBoosts.Subtract(CurrentBattler.SelectedWeapon.EquipBoosts);
            CurrentBattler.ExecutedAction = true;
        }
        
        private void KOResult(int currTarget)
        {
            Battler t = CurrentBattler.SelectedTargets[currTarget];
            if (t.HP > 0) return;
            {
                while (t.States.Count > 0) t.RemoveState(0);
                TargetUI.ClearStates();
                t.AddState(AllData.States, 1);
            }
            if (t.IsConscious) return;
            if (t.IsPlayer() || t.IsAlly()) TargetUI.KOBattler(PLAYER_KO_COLOR);
            else TargetUI.Visible = false;
            int sleepTime = 500;
            Thread.Sleep(sleepTime);
            CheckWinOrLose();
        }

        private void CheckWinOrLose()
        {
            bool playersKOd = true;
            foreach (Player p in Players) if (p.IsConscious) { playersKOd = false; break; }
            if (playersKOd) LoseBattle();
            bool enemiesKOd = true;
            foreach (Enemy e in Enemies) if (e.IsConscious) { enemiesKOd = false; break; }
            if (enemiesKOd) WinBattle();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Battle End --
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void WinBattle()
        {
            MessageBox.Show("YOU WIN");
            // Talk about level-up statistics, etc.
            // Manage non-overworld passive effects: traverse through list
            // Get exp and gold rate
            Close();
        }

        private void LoseBattle()
        {
            MessageBox.Show("GAME OVER");
            Close();
        }
    }
}