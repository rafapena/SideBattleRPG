using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleSimulator.Templates;
using static BattleSimulator.Utilities.FileHelper;
using Database.Utilities;
using System.IO;

namespace BattleSimulator
{
    public partial class Main : Form
    {
        private const string PRTY_FILES_PATH = "../../PrtyFiles/";
        private const int NUMBER_OF_TEST_PARTIES = 50;
        private const int MAX_NUMBER_OF_PLAYERS = 5;
        private Color LIST_BUTTON_COLOR = Color.LightGray;
        private Color HIGHLIGHTED_LIST_BUTTON_COLOR = Color.Gray;

        private Button[] buttons;
        private int CurrentSelection;

        private BattlePlayer[] Players;
        private ComboBox[] Relations;
        private string[] RelationsOptions = new string[] { "Allies", "Supporters", "Battle Buddies", "Super Team", "Elite Duo", "Perfect Unison"};
        private int NumberOfPlayers;
        private ComboBoxInputData BattleData;

        public Main()
        {
            InitializeComponent();
            ListPanel.AutoScroll = true;
            PartyMembersPanel.AutoScroll = true;
            SetupData();
            SetupList();
        }


        /// <summary>
        /// Party-list-related operations
        /// </summary>

        public void SetupList()
        {
            buttons = new Button[NUMBER_OF_TEST_PARTIES];
            for (int i = 0; i < NUMBER_OF_TEST_PARTIES; i++)
            {
                Button btn = new Button();
                btn.Width = ListPanel.Width - 30;
                btn.Height = 30;
                btn.Location = new Point(0, i * btn.Height);
                btn.Text = RetrieveNameByPrtyFile(i + 1);
                btn.Tag = i;
                btn.BackColor = LIST_BUTTON_COLOR;
                btn.Click += new EventHandler(PartyButton_Clicked);
                buttons[i] = btn;
                ListPanel.Controls.Add(btn);
            }
            CurrentSelection = 0;
            buttons[0].BackColor = HIGHLIGHTED_LIST_BUTTON_COLOR;
            Read("Group" + (CurrentSelection + 1));
        }

        private string RetrieveNameByPrtyFile(int i)
        {
            string name = "";
            string filename = PRTY_FILES_PATH + "Group" + i + ".prty";
            using (var file = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))  name = file.Length > 0 ? ReadText(file) : "Group" + i;
            return name;
        }

        private void PartyButton_Clicked(object sender, EventArgs e)
        {
            buttons[CurrentSelection].BackColor = LIST_BUTTON_COLOR;
            Button btn = sender as Button;
            CurrentSelection = (int)btn.Tag;
            btn.BackColor = HIGHLIGHTED_LIST_BUTTON_COLOR;
            UpdatedText.Visible = false;
            Read("Group" + (CurrentSelection + 1));
        }


        /// <summary>
        /// I/ndividual party operations
        /// </summary>

        private void SetupData()
        {
            Players = new BattlePlayer[] { BattlePlayer1, BattlePlayer2, BattlePlayer3, BattlePlayer4, BattlePlayer5 };
            Relations = new ComboBox[] { Relation1, Relation2, Relation3, Relation4, Relation5, Relation6 };
            for (int i = 0; i < Players.Length; i++) Players[i].SetupData();
            for (int i = 0; i < Relations.Length; i++) Relations[i].Items.AddRange(RelationsOptions);
            BattleData = new ComboBoxInputData("Battle_ID", "Name", "BaseObject JOIN Battle", "BaseObject_ID = BaseObjectID", "Name");
            BattleInput.Items.AddRange(BattleData.OptionsListNames.ToArray());
        }

        private void Initialize()
        {
            NameInput.Text = "Group" + (CurrentSelection + 1);
            NumberOfPlayers = 1;
            NumberOfPlayersDisplay.Text = NumberOfPlayers.ToString();
            Players[0].Initialize();
            Players[0].Visible = true;
            for (int i = NumberOfPlayers; i < Players.Length; i++)
            {
                Players[i].Initialize();
                Players[i].Visible = false;
            }
            for (int i = 0; i < 6; i++) Relations[i].SelectedIndex = 0;
            BattleInput.SelectedIndex = 0;
        }

        private void Read(string name)
        {
            string filename = PRTY_FILES_PATH + name + ".prty";
            using (var file = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (file.Length == 0) Initialize();
                else
                {
                    NameInput.Text = ReadText(file);
                    NumberOfPlayers = ReadByte(file);
                    NumberOfPlayersDisplay.Text = NumberOfPlayers.ToString();
                    for (int i = 0; i < NumberOfPlayers; i++)
                    {
                        Players[i].Read(file);
                        Players[i].Visible = true;
                    }
                    for (int i = NumberOfPlayers; i < MAX_NUMBER_OF_PLAYERS; i++)
                    {
                        Players[i].Initialize();
                        Players[i].Visible = false;
                    }
                    for (int i = 0; i < 6; i++) Relations[i].SelectedIndex = ReadByte(file);
                    BattleInput.SelectedIndex = BattleData.FindIndex(ReadShort(file));
                }
            }
        }

        private string ValidateInputs()
        {
            string err = "";
            if (!Utils.InRequiredLength(Utils.CutSpaces(NameInput.Text), 30)) err += "Name must be between 1 to 30 characters in length\n";
            bool flag = false;
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                err += Players[i].ValidateInputs();
                if (flag) continue;
                for (int j = i + 1; j < NumberOfPlayers; j++)
                {
                    if (!Players[i].InSamePositionAs(Players[j])) continue;
                    err += "Players cannot share the same X and Z positions\n";
                    flag = true;
                    break;
                }
            }
            return err;
        }

        private bool UnsavedChanges()
        {
            List<bool> unsaved = new List<bool>();
            string filename = PRTY_FILES_PATH + "Group" + (CurrentSelection + 1) + ".prty";
            using (var file = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (file.Length > 0)
                {
                    unsaved.Add(NameInput.Text != ReadText(file));
                    unsaved.Add(NumberOfPlayers != ReadByte(file));
                    for (int i = 0; i < NumberOfPlayers; i++) unsaved.Add(Players[i].UnsavedChanges(file));
                    for (int i = 0; i < 6; i++) unsaved.Add(Relations[i].SelectedIndex != ReadByte(file));
                    unsaved.Add(BattleInput.SelectedIndex != BattleData.FindIndex(ReadShort(file)));
                }
            }
            if (unsaved.Count <= 0) return true;
            for (int i = 0; i < unsaved.Count; i++) if (unsaved[i]) return true;
            return false;
        }

        private void Write(string name)
        {
            string filename = PRTY_FILES_PATH + name + ".prty";
            File.WriteAllText(filename, string.Empty);
            using (var file = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                WriteText(file, NameInput.Text);
                WriteByte(file, NumberOfPlayers);
                for (int i = 0; i < NumberOfPlayers; i++) Players[i].Write(file);
                for (int i = 0; i < 6; i++) WriteByte(file, Relations[i].SelectedIndex);
                WriteShort(file, BattleData.OptionsListIds[BattleInput.SelectedIndex]);
            }
            buttons[CurrentSelection].Text = NameInput.Text;
            UpdatedText.Visible = true;
        }


        /// <summary>
        /// Button click outputs
        /// </summary>

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (NumberOfPlayers >= MAX_NUMBER_OF_PLAYERS) MessageBox.Show(MAX_NUMBER_OF_PLAYERS + " players per battle is the maximum limit.", "Could not add player");
            else Players[NumberOfPlayers++].Visible = true;
            NumberOfPlayersDisplay.Text = NumberOfPlayers.ToString();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (NumberOfPlayers <= 1) MessageBox.Show("1 players per battle is the minimum limit.", "Could not remove player");
            else Players[--NumberOfPlayers].Visible = false;
            NumberOfPlayersDisplay.Text = NumberOfPlayers.ToString();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show(err, "Could not update party");
            else Write("Group" + (CurrentSelection + 1));
        }

        private void BattleButton_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges() && Utils.Confirm("Do you want to save changes before the battle?", "Unsaved changes")) UpdateButton_Click(null, null);
            UpdatedText.Visible = false;
            RPGBattle battle = new RPGBattle();
            battle.Show();
        }
    }
}
