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

namespace BattleSimulator
{
    public partial class RPGBattle : Form
    {
        private Battle Battle;

        public RPGBattle(string filenameWithPath)
        {
            InitializeComponent();
            DataManager.Setup();
            Battle = new Battle(filenameWithPath);
        }
    }
}
