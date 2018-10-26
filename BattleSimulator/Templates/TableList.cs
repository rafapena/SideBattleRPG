using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSimulator.Templates
{
    public partial class TableList : UserControl
    {
        private const int NUMBER_OF_TEST_PARTIES = 50;
        private int LastButton;

        public TableList()
        {
            InitializeComponent();
            TablePanel.AutoScroll = true;
        }

        public void Setup()
        {
            LastButton = 0;
            for (int i = 0; i < NUMBER_OF_TEST_PARTIES;)
            {
                Button btn = new Button();
                btn.Width = TablePanel.Width - 20;
                btn.Height = 30;
                btn.Location = new Point(0, i * btn.Height);
                btn.Text = "Party " + ++i;
                btn.Tag = i;
                btn.BackColor = Color.LightGray;
                btn.Click += new EventHandler(PartyButton_Clicked);
                TablePanel.Controls.Add(btn);
            }
        }

        private void PartyButton_Clicked(object sender, EventArgs e)
        {
            if (LastButton > 0)
            {
                //Button lastButton = Find;
            }
            Button btn = sender as Button;
            LastButton = (int)btn.Tag;
            btn.BackColor = Color.Gray;
        }
    }
}
