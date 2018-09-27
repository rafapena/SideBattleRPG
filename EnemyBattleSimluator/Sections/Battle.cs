using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnemyBattleSimluator.Sections
{
    public partial class Battle : UserControl
    {
        public Battle()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Battle
            // 
            this.Name = "Battle";
            this.Size = new System.Drawing.Size(772, 650);
            this.ResumeLayout(false);

        }
    }
}
