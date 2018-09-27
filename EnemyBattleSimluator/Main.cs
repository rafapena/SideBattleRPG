using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database.BaseControls;
using Database.Utilities;

namespace BattleSimluator
{
    public partial class Main : Form
    {
        private int Mode;

        public Main()
        {
            InitializeComponent();
            Mode = 0;
        }

        private void ModeSwitcher_Click(object sender, EventArgs e)
        {
            if (Mode == 0)
            {
                ModeSwitcher.Text = "Switch to Battles";
                Mode = 1;
                return;
            }
            ModeSwitcher.Text = "Switch to Parties";
            Mode = 0;
        }
    }
}
