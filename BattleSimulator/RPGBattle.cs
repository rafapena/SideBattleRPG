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
        private RPGBattler[][] PlayersUI, EnemiesUI;

        public RPGBattle(string filenameWithPath)
        {
            AutoSize = true;
            InitializeComponent();
            KeyPress += new KeyPressEventHandler(KeyPressed);
            AllData.Setup();
            SetupBattleSession(filenameWithPath);
            InitializeBattlers();
        }

        public void SetupBattleSession(string prtyFilenameWithPath)
        {
            Players = new List<Player>();
            Enemies = new List<Enemy>();
            using (var file = File.Open(prtyFilenameWithPath, FileMode.Open, FileAccess.Read))
            {
                string partyName = ReadText(file);
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
                    Players.Add(battlePlayer);
                }
                for (int i = 0; i < 4; i++) for (int j = i + 1; j < 4; j++) PlayerSetRelationHelper(file, i, j);
                AllData.SetupBattle(ReadShort(file));
                Enemies = AllData.Battle.Enemies;
                Header.Text = partyName + " VS " + AllData.Battle.Name;
            }
        }
        private void PlayerSetRelationHelper(FileStream file, int i, int j)
        {
            int value = ReadByte(file);
            if (i >= Players.Count || j >= Players.Count) return;
            Players[i].SetRelation(Players[j].Id, value);
            Players[j].SetRelation(Players[i].Id, value);
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            char b = e.KeyChar;
            MessageBox.Show(b+"");
        }

        private void InitializeBattlers()
        {
            PlayersUI = new RPGBattler[][] { new RPGBattler[] { P00, P01, P02 }, new RPGBattler[] { P10, P11, P12 }, new RPGBattler[] { P20, P21, P22 } };
            EnemiesUI = new RPGBattler[][] { new RPGBattler[] { E00, E01, E02 }, new RPGBattler[] { E10, E11, E12 }, new RPGBattler[] { E20, E21, E22 } };
            for (int i = 0; i < 3; i++)
            {
                PlayersUI[i][0].Visible = false;
                PlayersUI[i][1].Visible = false;
                PlayersUI[i][2].Visible = false;
                EnemiesUI[i][0].Visible = false;
                EnemiesUI[i][1].Visible = false;
                EnemiesUI[i][2].Visible = false;
            }
            for (int i = 0; i < Players.Count; i++) PlayersUI[Players[i].XPosition][Players[i].ZPosition].Initialize(Players[i]);
            for (int i = 0; i < Enemies.Count; i++) EnemiesUI[Enemies[i].XPosition][Enemies[i].ZPosition].Initialize(Enemies[i]);
        }
    }
}