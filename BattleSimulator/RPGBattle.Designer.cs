namespace BattleSimulator
{
    partial class RPGBattle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.TurnNumber = new System.Windows.Forms.Label();
            this.PlayersHeader = new System.Windows.Forms.Label();
            this.EnemiesHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Commands = new System.Windows.Forms.Label();
            this.FixedCommands = new System.Windows.Forms.Label();
            this.CommandTracker = new System.Windows.Forms.Label();
            this.ScopeCommand = new System.Windows.Forms.Label();
            this.PLF = new BattleSimulator.Templates.RPGBattler();
            this.PCF = new BattleSimulator.Templates.RPGBattler();
            this.PRF = new BattleSimulator.Templates.RPGBattler();
            this.PRC = new BattleSimulator.Templates.RPGBattler();
            this.PCC = new BattleSimulator.Templates.RPGBattler();
            this.PLC = new BattleSimulator.Templates.RPGBattler();
            this.PLB = new BattleSimulator.Templates.RPGBattler();
            this.PCB = new BattleSimulator.Templates.RPGBattler();
            this.PRB = new BattleSimulator.Templates.RPGBattler();
            this.ELF = new BattleSimulator.Templates.RPGBattler();
            this.ELC = new BattleSimulator.Templates.RPGBattler();
            this.ELB = new BattleSimulator.Templates.RPGBattler();
            this.ECF = new BattleSimulator.Templates.RPGBattler();
            this.ECC = new BattleSimulator.Templates.RPGBattler();
            this.ECB = new BattleSimulator.Templates.RPGBattler();
            this.ERF = new BattleSimulator.Templates.RPGBattler();
            this.ERC = new BattleSimulator.Templates.RPGBattler();
            this.ERB = new BattleSimulator.Templates.RPGBattler();
            this.Pos1 = new System.Windows.Forms.Label();
            this.Pos2 = new System.Windows.Forms.Label();
            this.Pos3 = new System.Windows.Forms.Label();
            this.Pos6 = new System.Windows.Forms.Label();
            this.Pos5 = new System.Windows.Forms.Label();
            this.Pos4 = new System.Windows.Forms.Label();
            this.Pos9 = new System.Windows.Forms.Label();
            this.Pos8 = new System.Windows.Forms.Label();
            this.Pos7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(708, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 29);
            this.label2.TabIndex = 19;
            this.label2.Text = "Turn:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TurnNumber
            // 
            this.TurnNumber.AutoSize = true;
            this.TurnNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TurnNumber.Location = new System.Drawing.Point(805, 10);
            this.TurnNumber.Name = "TurnNumber";
            this.TurnNumber.Size = new System.Drawing.Size(26, 29);
            this.TurnNumber.TabIndex = 20;
            this.TurnNumber.Text = "0";
            this.TurnNumber.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PlayersHeader
            // 
            this.PlayersHeader.AutoSize = true;
            this.PlayersHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayersHeader.Location = new System.Drawing.Point(283, 9);
            this.PlayersHeader.Name = "PlayersHeader";
            this.PlayersHeader.Size = new System.Drawing.Size(93, 29);
            this.PlayersHeader.TabIndex = 21;
            this.PlayersHeader.Text = "Players";
            this.PlayersHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EnemiesHeader
            // 
            this.EnemiesHeader.AutoSize = true;
            this.EnemiesHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnemiesHeader.Location = new System.Drawing.Point(1142, 9);
            this.EnemiesHeader.Name = "EnemiesHeader";
            this.EnemiesHeader.Size = new System.Drawing.Size(108, 29);
            this.EnemiesHeader.TabIndex = 22;
            this.EnemiesHeader.Text = "Enemies";
            this.EnemiesHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Pos9);
            this.panel1.Controls.Add(this.Pos8);
            this.panel1.Controls.Add(this.Pos7);
            this.panel1.Controls.Add(this.Pos6);
            this.panel1.Controls.Add(this.Pos5);
            this.panel1.Controls.Add(this.Pos4);
            this.panel1.Controls.Add(this.Pos3);
            this.panel1.Controls.Add(this.Pos2);
            this.panel1.Controls.Add(this.Pos1);
            this.panel1.Controls.Add(this.PLF);
            this.panel1.Controls.Add(this.PCF);
            this.panel1.Controls.Add(this.PRF);
            this.panel1.Controls.Add(this.PRC);
            this.panel1.Controls.Add(this.PCC);
            this.panel1.Controls.Add(this.PLC);
            this.panel1.Controls.Add(this.PLB);
            this.panel1.Controls.Add(this.PCB);
            this.panel1.Controls.Add(this.PRB);
            this.panel1.Location = new System.Drawing.Point(26, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 1006);
            this.panel1.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ELF);
            this.panel2.Controls.Add(this.ELC);
            this.panel2.Controls.Add(this.ELB);
            this.panel2.Controls.Add(this.ECF);
            this.panel2.Controls.Add(this.ECC);
            this.panel2.Controls.Add(this.ECB);
            this.panel2.Controls.Add(this.ERF);
            this.panel2.Controls.Add(this.ERC);
            this.panel2.Controls.Add(this.ERB);
            this.panel2.Location = new System.Drawing.Point(906, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(588, 1002);
            this.panel2.TabIndex = 24;
            // 
            // Commands
            // 
            this.Commands.AutoSize = true;
            this.Commands.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Commands.Location = new System.Drawing.Point(21, 1117);
            this.Commands.Name = "Commands";
            this.Commands.Size = new System.Drawing.Size(124, 29);
            this.Commands.TabIndex = 26;
            this.Commands.Text = "Command";
            // 
            // FixedCommands
            // 
            this.FixedCommands.AutoSize = true;
            this.FixedCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixedCommands.Location = new System.Drawing.Point(1370, 1117);
            this.FixedCommands.Name = "FixedCommands";
            this.FixedCommands.Size = new System.Drawing.Size(124, 29);
            this.FixedCommands.TabIndex = 27;
            this.FixedCommands.Text = "Command";
            this.FixedCommands.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CommandTracker
            // 
            this.CommandTracker.AutoSize = true;
            this.CommandTracker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommandTracker.Location = new System.Drawing.Point(620, 66);
            this.CommandTracker.Name = "CommandTracker";
            this.CommandTracker.Size = new System.Drawing.Size(103, 25);
            this.CommandTracker.TabIndex = 28;
            this.CommandTracker.Text = "Command";
            // 
            // ScopeCommand
            // 
            this.ScopeCommand.AutoSize = true;
            this.ScopeCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScopeCommand.Location = new System.Drawing.Point(707, 1117);
            this.ScopeCommand.Name = "ScopeCommand";
            this.ScopeCommand.Size = new System.Drawing.Size(124, 29);
            this.ScopeCommand.TabIndex = 29;
            this.ScopeCommand.Text = "Command";
            this.ScopeCommand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PLF
            // 
            this.PLF.Location = new System.Drawing.Point(392, 3);
            this.PLF.Name = "PLF";
            this.PLF.Size = new System.Drawing.Size(190, 330);
            this.PLF.TabIndex = 1;
            // 
            // PCF
            // 
            this.PCF.Location = new System.Drawing.Point(393, 339);
            this.PCF.Name = "PCF";
            this.PCF.Size = new System.Drawing.Size(190, 330);
            this.PCF.TabIndex = 2;
            // 
            // PRF
            // 
            this.PRF.Location = new System.Drawing.Point(393, 675);
            this.PRF.Name = "PRF";
            this.PRF.Size = new System.Drawing.Size(190, 330);
            this.PRF.TabIndex = 3;
            // 
            // PRC
            // 
            this.PRC.Location = new System.Drawing.Point(200, 675);
            this.PRC.Name = "PRC";
            this.PRC.Size = new System.Drawing.Size(190, 330);
            this.PRC.TabIndex = 6;
            // 
            // PCC
            // 
            this.PCC.Location = new System.Drawing.Point(200, 339);
            this.PCC.Name = "PCC";
            this.PCC.Size = new System.Drawing.Size(190, 330);
            this.PCC.TabIndex = 5;
            // 
            // PLC
            // 
            this.PLC.Location = new System.Drawing.Point(200, 3);
            this.PLC.Name = "PLC";
            this.PLC.Size = new System.Drawing.Size(190, 330);
            this.PLC.TabIndex = 4;
            // 
            // PLB
            // 
            this.PLB.Location = new System.Drawing.Point(5, 3);
            this.PLB.Name = "PLB";
            this.PLB.Size = new System.Drawing.Size(190, 330);
            this.PLB.TabIndex = 7;
            // 
            // PCB
            // 
            this.PCB.Location = new System.Drawing.Point(5, 339);
            this.PCB.Name = "PCB";
            this.PCB.Size = new System.Drawing.Size(190, 330);
            this.PCB.TabIndex = 8;
            // 
            // PRB
            // 
            this.PRB.Location = new System.Drawing.Point(5, 675);
            this.PRB.Name = "PRB";
            this.PRB.Size = new System.Drawing.Size(190, 330);
            this.PRB.TabIndex = 9;
            // 
            // ELF
            // 
            this.ELF.Location = new System.Drawing.Point(3, 0);
            this.ELF.Name = "ELF";
            this.ELF.Size = new System.Drawing.Size(190, 330);
            this.ELF.TabIndex = 16;
            // 
            // ELC
            // 
            this.ELC.Location = new System.Drawing.Point(197, 0);
            this.ELC.Name = "ELC";
            this.ELC.Size = new System.Drawing.Size(190, 330);
            this.ELC.TabIndex = 13;
            // 
            // ELB
            // 
            this.ELB.Location = new System.Drawing.Point(393, 0);
            this.ELB.Name = "ELB";
            this.ELB.Size = new System.Drawing.Size(190, 330);
            this.ELB.TabIndex = 10;
            // 
            // ECF
            // 
            this.ECF.Location = new System.Drawing.Point(3, 335);
            this.ECF.Name = "ECF";
            this.ECF.Size = new System.Drawing.Size(190, 330);
            this.ECF.TabIndex = 17;
            // 
            // ECC
            // 
            this.ECC.Location = new System.Drawing.Point(197, 335);
            this.ECC.Name = "ECC";
            this.ECC.Size = new System.Drawing.Size(190, 330);
            this.ECC.TabIndex = 14;
            // 
            // ECB
            // 
            this.ECB.Location = new System.Drawing.Point(393, 335);
            this.ECB.Name = "ECB";
            this.ECB.Size = new System.Drawing.Size(190, 330);
            this.ECB.TabIndex = 11;
            // 
            // ERF
            // 
            this.ERF.Location = new System.Drawing.Point(3, 671);
            this.ERF.Name = "ERF";
            this.ERF.Size = new System.Drawing.Size(190, 330);
            this.ERF.TabIndex = 18;
            // 
            // ERC
            // 
            this.ERC.Location = new System.Drawing.Point(197, 671);
            this.ERC.Name = "ERC";
            this.ERC.Size = new System.Drawing.Size(190, 330);
            this.ERC.TabIndex = 15;
            // 
            // ERB
            // 
            this.ERB.Location = new System.Drawing.Point(393, 671);
            this.ERB.Name = "ERB";
            this.ERB.Size = new System.Drawing.Size(190, 330);
            this.ERB.TabIndex = 12;
            // 
            // Pos1
            // 
            this.Pos1.AutoSize = true;
            this.Pos1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos1.Location = new System.Drawing.Point(87, 74);
            this.Pos1.Name = "Pos1";
            this.Pos1.Size = new System.Drawing.Size(37, 32);
            this.Pos1.TabIndex = 30;
            this.Pos1.Text = "Q";
            // 
            // Pos2
            // 
            this.Pos2.AutoSize = true;
            this.Pos2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos2.Location = new System.Drawing.Point(277, 74);
            this.Pos2.Name = "Pos2";
            this.Pos2.Size = new System.Drawing.Size(41, 32);
            this.Pos2.TabIndex = 31;
            this.Pos2.Text = "W";
            // 
            // Pos3
            // 
            this.Pos3.AutoSize = true;
            this.Pos3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos3.Location = new System.Drawing.Point(476, 74);
            this.Pos3.Name = "Pos3";
            this.Pos3.Size = new System.Drawing.Size(34, 32);
            this.Pos3.TabIndex = 32;
            this.Pos3.Text = "E";
            // 
            // Pos6
            // 
            this.Pos6.AutoSize = true;
            this.Pos6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos6.Location = new System.Drawing.Point(476, 409);
            this.Pos6.Name = "Pos6";
            this.Pos6.Size = new System.Drawing.Size(35, 32);
            this.Pos6.TabIndex = 35;
            this.Pos6.Text = "D";
            // 
            // Pos5
            // 
            this.Pos5.AutoSize = true;
            this.Pos5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos5.Location = new System.Drawing.Point(284, 409);
            this.Pos5.Name = "Pos5";
            this.Pos5.Size = new System.Drawing.Size(34, 32);
            this.Pos5.TabIndex = 34;
            this.Pos5.Text = "S";
            // 
            // Pos4
            // 
            this.Pos4.AutoSize = true;
            this.Pos4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos4.Location = new System.Drawing.Point(87, 410);
            this.Pos4.Name = "Pos4";
            this.Pos4.Size = new System.Drawing.Size(34, 32);
            this.Pos4.TabIndex = 33;
            this.Pos4.Text = "A";
            // 
            // Pos9
            // 
            this.Pos9.AutoSize = true;
            this.Pos9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos9.Location = new System.Drawing.Point(476, 746);
            this.Pos9.Name = "Pos9";
            this.Pos9.Size = new System.Drawing.Size(35, 32);
            this.Pos9.TabIndex = 38;
            this.Pos9.Text = "C";
            // 
            // Pos8
            // 
            this.Pos8.AutoSize = true;
            this.Pos8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos8.Location = new System.Drawing.Point(284, 746);
            this.Pos8.Name = "Pos8";
            this.Pos8.Size = new System.Drawing.Size(34, 32);
            this.Pos8.TabIndex = 37;
            this.Pos8.Text = "X";
            // 
            // Pos7
            // 
            this.Pos7.AutoSize = true;
            this.Pos7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pos7.Location = new System.Drawing.Point(87, 746);
            this.Pos7.Name = "Pos7";
            this.Pos7.Size = new System.Drawing.Size(32, 32);
            this.Pos7.TabIndex = 36;
            this.Pos7.Text = "Z";
            // 
            // RPGBattle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1522, 1326);
            this.Controls.Add(this.ScopeCommand);
            this.Controls.Add(this.CommandTracker);
            this.Controls.Add(this.FixedCommands);
            this.Controls.Add(this.Commands);
            this.Controls.Add(this.EnemiesHeader);
            this.Controls.Add(this.PlayersHeader);
            this.Controls.Add(this.TurnNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "RPGBattle";
            this.Text = "Battle";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Templates.RPGBattler PCF;
        private Templates.RPGBattler PCC;
        private Templates.RPGBattler PCB;
        private Templates.RPGBattler PRF;
        private Templates.RPGBattler PRC;
        private Templates.RPGBattler PRB;
        private Templates.RPGBattler PLF;
        private Templates.RPGBattler PLC;
        private Templates.RPGBattler PLB;
        private Templates.RPGBattler ERF;
        private Templates.RPGBattler ECF;
        private Templates.RPGBattler ELF;
        private Templates.RPGBattler ERC;
        private Templates.RPGBattler ECC;
        private Templates.RPGBattler ELC;
        private Templates.RPGBattler ERB;
        private Templates.RPGBattler ECB;
        private Templates.RPGBattler ELB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TurnNumber;
        private System.Windows.Forms.Label PlayersHeader;
        private System.Windows.Forms.Label EnemiesHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Commands;
        private System.Windows.Forms.Label FixedCommands;
        private System.Windows.Forms.Label CommandTracker;
        private System.Windows.Forms.Label ScopeCommand;
        private System.Windows.Forms.Label Pos9;
        private System.Windows.Forms.Label Pos8;
        private System.Windows.Forms.Label Pos7;
        private System.Windows.Forms.Label Pos6;
        private System.Windows.Forms.Label Pos5;
        private System.Windows.Forms.Label Pos4;
        private System.Windows.Forms.Label Pos3;
        private System.Windows.Forms.Label Pos2;
        private System.Windows.Forms.Label Pos1;
    }
}