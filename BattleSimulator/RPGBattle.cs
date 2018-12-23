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

namespace BattleSimulator
{
    public partial class RPGBattle : Form
    {
        private BattleSession BSess;
        private RPGBattler[][] Players, Enemies;

        public RPGBattle(string filenameWithPath)
        {
            AutoSize = true;
            InitializeComponent();
            KeyPress += new KeyPressEventHandler(KeyPressed);
            AllData.Setup();
            BSess = new BattleSession(filenameWithPath);
            InitializeBattlers();
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            char b = e.KeyChar;
            MessageBox.Show(b+"");
        }

        private void InitializeBattlers()
        {
            Players = new RPGBattler[][] { new RPGBattler[] { P00, P01, P02 }, new RPGBattler[] { P10, P11, P12 }, new RPGBattler[] { P20, P21, P22 } };
            Enemies = new RPGBattler[][] { new RPGBattler[] { E00, E01, E02 }, new RPGBattler[] { E10, E11, E12 }, new RPGBattler[] { E20, E21, E22 } };
            for (int i = 0; i < 3; i++)
            {
                Players[i][0].Visible = false;
                Players[i][1].Visible = false;
                Players[i][2].Visible = false;
                Enemies[i][0].Visible = false;
                Enemies[i][1].Visible = false;
                Enemies[i][2].Visible = false;
            }
            foreach (Player p in BSess.Players)
            {
                Players[p.XPosition][p.ZPosition].Visible = true;
            }
            foreach (Enemy e in BSess.Enemies)
            {
                Enemies[e.XPosition][e.ZPosition].Visible = true;
            }
        }
    }
}