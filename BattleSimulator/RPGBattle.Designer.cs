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
            this.ERF = new BattleSimulator.Templates.RPGBattler();
            this.ECF = new BattleSimulator.Templates.RPGBattler();
            this.ELF = new BattleSimulator.Templates.RPGBattler();
            this.ERC = new BattleSimulator.Templates.RPGBattler();
            this.ECC = new BattleSimulator.Templates.RPGBattler();
            this.ELC = new BattleSimulator.Templates.RPGBattler();
            this.ERB = new BattleSimulator.Templates.RPGBattler();
            this.ECB = new BattleSimulator.Templates.RPGBattler();
            this.ELB = new BattleSimulator.Templates.RPGBattler();
            this.PRB = new BattleSimulator.Templates.RPGBattler();
            this.PCB = new BattleSimulator.Templates.RPGBattler();
            this.PLB = new BattleSimulator.Templates.RPGBattler();
            this.PRC = new BattleSimulator.Templates.RPGBattler();
            this.PCC = new BattleSimulator.Templates.RPGBattler();
            this.PLC = new BattleSimulator.Templates.RPGBattler();
            this.PRF = new BattleSimulator.Templates.RPGBattler();
            this.PCF = new BattleSimulator.Templates.RPGBattler();
            this.PLF = new BattleSimulator.Templates.RPGBattler();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(599, 9);
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
            this.TurnNumber.Location = new System.Drawing.Point(696, 9);
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
            this.PlayersHeader.Location = new System.Drawing.Point(246, 9);
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
            this.EnemiesHeader.Location = new System.Drawing.Point(969, 9);
            this.EnemiesHeader.Name = "EnemiesHeader";
            this.EnemiesHeader.Size = new System.Drawing.Size(108, 29);
            this.EnemiesHeader.TabIndex = 22;
            this.EnemiesHeader.Text = "Enemies";
            this.EnemiesHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(26, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 826);
            this.panel1.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(765, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(527, 826);
            this.panel2.TabIndex = 24;
            // 
            // Commands
            // 
            this.Commands.AutoSize = true;
            this.Commands.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Commands.Location = new System.Drawing.Point(34, 975);
            this.Commands.Name = "Commands";
            this.Commands.Size = new System.Drawing.Size(124, 29);
            this.Commands.TabIndex = 26;
            this.Commands.Text = "Command";
            // 
            // FixedCommands
            // 
            this.FixedCommands.AutoSize = true;
            this.FixedCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixedCommands.Location = new System.Drawing.Point(1159, 975);
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
            this.CommandTracker.Location = new System.Drawing.Point(559, 95);
            this.CommandTracker.Name = "CommandTracker";
            this.CommandTracker.Size = new System.Drawing.Size(103, 25);
            this.CommandTracker.TabIndex = 28;
            this.CommandTracker.Text = "Command";
            // 
            // ScopeCommand
            // 
            this.ScopeCommand.AutoSize = true;
            this.ScopeCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScopeCommand.Location = new System.Drawing.Point(598, 975);
            this.ScopeCommand.Name = "ScopeCommand";
            this.ScopeCommand.Size = new System.Drawing.Size(124, 29);
            this.ScopeCommand.TabIndex = 29;
            this.ScopeCommand.Text = "Command";
            this.ScopeCommand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ERF
            // 
            this.ERF.Location = new System.Drawing.Point(776, 640);
            this.ERF.Name = "ERF";
            this.ERF.Size = new System.Drawing.Size(165, 266);
            this.ERF.TabIndex = 18;
            // 
            // ECF
            // 
            this.ECF.Location = new System.Drawing.Point(776, 372);
            this.ECF.Name = "ECF";
            this.ECF.Size = new System.Drawing.Size(165, 266);
            this.ECF.TabIndex = 17;
            // 
            // ELF
            // 
            this.ELF.Location = new System.Drawing.Point(776, 104);
            this.ELF.Name = "ELF";
            this.ELF.Size = new System.Drawing.Size(165, 266);
            this.ELF.TabIndex = 16;
            // 
            // ERC
            // 
            this.ERC.Location = new System.Drawing.Point(947, 640);
            this.ERC.Name = "ERC";
            this.ERC.Size = new System.Drawing.Size(165, 266);
            this.ERC.TabIndex = 15;
            // 
            // ECC
            // 
            this.ECC.Location = new System.Drawing.Point(947, 372);
            this.ECC.Name = "ECC";
            this.ECC.Size = new System.Drawing.Size(165, 266);
            this.ECC.TabIndex = 14;
            // 
            // ELC
            // 
            this.ELC.Location = new System.Drawing.Point(947, 104);
            this.ELC.Name = "ELC";
            this.ELC.Size = new System.Drawing.Size(165, 266);
            this.ELC.TabIndex = 13;
            // 
            // ERB
            // 
            this.ERB.Location = new System.Drawing.Point(1118, 640);
            this.ERB.Name = "ERB";
            this.ERB.Size = new System.Drawing.Size(165, 266);
            this.ERB.TabIndex = 12;
            // 
            // ECB
            // 
            this.ECB.Location = new System.Drawing.Point(1118, 372);
            this.ECB.Name = "ECB";
            this.ECB.Size = new System.Drawing.Size(165, 266);
            this.ECB.TabIndex = 11;
            // 
            // ELB
            // 
            this.ELB.Location = new System.Drawing.Point(1118, 104);
            this.ELB.Name = "ELB";
            this.ELB.Size = new System.Drawing.Size(165, 266);
            this.ELB.TabIndex = 10;
            // 
            // PRB
            // 
            this.PRB.Location = new System.Drawing.Point(39, 640);
            this.PRB.Name = "PRB";
            this.PRB.Size = new System.Drawing.Size(165, 273);
            this.PRB.TabIndex = 9;
            // 
            // PCB
            // 
            this.PCB.Location = new System.Drawing.Point(39, 367);
            this.PCB.Name = "PCB";
            this.PCB.Size = new System.Drawing.Size(165, 277);
            this.PCB.TabIndex = 8;
            // 
            // PLB
            // 
            this.PLB.Location = new System.Drawing.Point(38, 104);
            this.PLB.Name = "PLB";
            this.PLB.Size = new System.Drawing.Size(165, 268);
            this.PLB.TabIndex = 7;
            // 
            // PRC
            // 
            this.PRC.Location = new System.Drawing.Point(210, 640);
            this.PRC.Name = "PRC";
            this.PRC.Size = new System.Drawing.Size(165, 273);
            this.PRC.TabIndex = 6;
            // 
            // PCC
            // 
            this.PCC.Location = new System.Drawing.Point(210, 367);
            this.PCC.Name = "PCC";
            this.PCC.Size = new System.Drawing.Size(165, 277);
            this.PCC.TabIndex = 5;
            // 
            // PLC
            // 
            this.PLC.Location = new System.Drawing.Point(210, 104);
            this.PLC.Name = "PLC";
            this.PLC.Size = new System.Drawing.Size(165, 268);
            this.PLC.TabIndex = 4;
            // 
            // PRF
            // 
            this.PRF.Location = new System.Drawing.Point(381, 640);
            this.PRF.Name = "PRF";
            this.PRF.Size = new System.Drawing.Size(165, 273);
            this.PRF.TabIndex = 3;
            // 
            // PCF
            // 
            this.PCF.Location = new System.Drawing.Point(381, 367);
            this.PCF.Name = "PCF";
            this.PCF.Size = new System.Drawing.Size(165, 277);
            this.PCF.TabIndex = 2;
            // 
            // PLF
            // 
            this.PLF.Location = new System.Drawing.Point(381, 104);
            this.PLF.Name = "PLF";
            this.PLF.Size = new System.Drawing.Size(165, 268);
            this.PLF.TabIndex = 1;
            // 
            // RPGBattle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 1205);
            this.Controls.Add(this.ScopeCommand);
            this.Controls.Add(this.CommandTracker);
            this.Controls.Add(this.FixedCommands);
            this.Controls.Add(this.Commands);
            this.Controls.Add(this.EnemiesHeader);
            this.Controls.Add(this.PlayersHeader);
            this.Controls.Add(this.TurnNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ERF);
            this.Controls.Add(this.ECF);
            this.Controls.Add(this.ELF);
            this.Controls.Add(this.ERC);
            this.Controls.Add(this.ECC);
            this.Controls.Add(this.ELC);
            this.Controls.Add(this.ERB);
            this.Controls.Add(this.ECB);
            this.Controls.Add(this.ELB);
            this.Controls.Add(this.PRB);
            this.Controls.Add(this.PCB);
            this.Controls.Add(this.PLB);
            this.Controls.Add(this.PRC);
            this.Controls.Add(this.PCC);
            this.Controls.Add(this.PLC);
            this.Controls.Add(this.PRF);
            this.Controls.Add(this.PCF);
            this.Controls.Add(this.PLF);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "RPGBattle";
            this.Text = "Battle";
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
    }
}