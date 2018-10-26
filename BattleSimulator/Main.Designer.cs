namespace BattleSimulator
{
    partial class Main
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

        private void InitializeComponent()
        {
            this.ListPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.BattleInput = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BattleButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.NameInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PartyMembersPanel = new System.Windows.Forms.Panel();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Relation1 = new System.Windows.Forms.ComboBox();
            this.Relation2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Relation4 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Relation3 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Relation6 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Relation5 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BattlePlayer5 = new BattleSimulator.Templates.BattlePlayer();
            this.BattlePlayer3 = new BattleSimulator.Templates.BattlePlayer();
            this.BattlePlayer4 = new BattleSimulator.Templates.BattlePlayer();
            this.BattlePlayer2 = new BattleSimulator.Templates.BattlePlayer();
            this.BattlePlayer1 = new BattleSimulator.Templates.BattlePlayer();
            this.PartyMembersPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListPanel
            // 
            this.ListPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListPanel.Location = new System.Drawing.Point(25, 55);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(249, 754);
            this.ListPanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Parties List";
            // 
            // BattleInput
            // 
            this.BattleInput.FormattingEnabled = true;
            this.BattleInput.Location = new System.Drawing.Point(785, 721);
            this.BattleInput.Name = "BattleInput";
            this.BattleInput.Size = new System.Drawing.Size(213, 28);
            this.BattleInput.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(781, 685);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 41;
            this.label2.Text = "This party versus:";
            // 
            // BattleButton
            // 
            this.BattleButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.BattleButton.Location = new System.Drawing.Point(1006, 716);
            this.BattleButton.Name = "BattleButton";
            this.BattleButton.Size = new System.Drawing.Size(147, 46);
            this.BattleButton.TabIndex = 40;
            this.BattleButton.Text = "Battle";
            this.BattleButton.UseVisualStyleBackColor = false;
            this.BattleButton.Click += new System.EventHandler(this.BattleButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.UpdateButton.Location = new System.Drawing.Point(1006, 664);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(147, 46);
            this.UpdateButton.TabIndex = 39;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // NameInput
            // 
            this.NameInput.Location = new System.Drawing.Point(361, 55);
            this.NameInput.Name = "NameInput";
            this.NameInput.Size = new System.Drawing.Size(174, 26);
            this.NameInput.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "Name";
            // 
            // PartyMembersPanel
            // 
            this.PartyMembersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PartyMembersPanel.Controls.Add(this.BattlePlayer5);
            this.PartyMembersPanel.Controls.Add(this.BattlePlayer3);
            this.PartyMembersPanel.Controls.Add(this.BattlePlayer4);
            this.PartyMembersPanel.Controls.Add(this.BattlePlayer2);
            this.PartyMembersPanel.Controls.Add(this.BattlePlayer1);
            this.PartyMembersPanel.Location = new System.Drawing.Point(305, 151);
            this.PartyMembersPanel.Name = "PartyMembersPanel";
            this.PartyMembersPanel.Size = new System.Drawing.Size(984, 440);
            this.PartyMembersPanel.TabIndex = 36;
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(459, 100);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(147, 46);
            this.RemoveButton.TabIndex = 35;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(306, 99);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(147, 46);
            this.AddButton.TabIndex = 34;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(304, 617);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Members 1 and 2 relation";
            // 
            // Relation1
            // 
            this.Relation1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Relation1.FormattingEnabled = true;
            this.Relation1.Location = new System.Drawing.Point(498, 614);
            this.Relation1.Name = "Relation1";
            this.Relation1.Size = new System.Drawing.Size(152, 28);
            this.Relation1.TabIndex = 44;
            // 
            // Relation2
            // 
            this.Relation2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Relation2.FormattingEnabled = true;
            this.Relation2.Location = new System.Drawing.Point(498, 648);
            this.Relation2.Name = "Relation2";
            this.Relation2.Size = new System.Drawing.Size(152, 28);
            this.Relation2.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(304, 651);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 20);
            this.label5.TabIndex = 45;
            this.label5.Text = "Members 1 and 3 relation";
            // 
            // Relation4
            // 
            this.Relation4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Relation4.FormattingEnabled = true;
            this.Relation4.Location = new System.Drawing.Point(498, 716);
            this.Relation4.Name = "Relation4";
            this.Relation4.Size = new System.Drawing.Size(152, 28);
            this.Relation4.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(304, 719);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "Members 2 and 3 relation";
            // 
            // Relation3
            // 
            this.Relation3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Relation3.FormattingEnabled = true;
            this.Relation3.Location = new System.Drawing.Point(498, 682);
            this.Relation3.Name = "Relation3";
            this.Relation3.Size = new System.Drawing.Size(152, 28);
            this.Relation3.TabIndex = 48;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(304, 685);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 20);
            this.label7.TabIndex = 47;
            this.label7.Text = "Members 1 and 4 relation";
            // 
            // Relation6
            // 
            this.Relation6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Relation6.FormattingEnabled = true;
            this.Relation6.Location = new System.Drawing.Point(498, 786);
            this.Relation6.Name = "Relation6";
            this.Relation6.Size = new System.Drawing.Size(152, 28);
            this.Relation6.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(304, 789);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(188, 20);
            this.label8.TabIndex = 53;
            this.label8.Text = "Members 3 and 4 relation";
            // 
            // Relation5
            // 
            this.Relation5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Relation5.FormattingEnabled = true;
            this.Relation5.Location = new System.Drawing.Point(498, 752);
            this.Relation5.Name = "Relation5";
            this.Relation5.Size = new System.Drawing.Size(152, 28);
            this.Relation5.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(304, 755);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(188, 20);
            this.label9.TabIndex = 51;
            this.label9.Text = "Members 2 and 4 relation";
            // 
            // BattlePlayer5
            // 
            this.BattlePlayer5.Location = new System.Drawing.Point(-1, 860);
            this.BattlePlayer5.Name = "BattlePlayer5";
            this.BattlePlayer5.Size = new System.Drawing.Size(938, 222);
            this.BattlePlayer5.TabIndex = 8;
            // 
            // BattlePlayer3
            // 
            this.BattlePlayer3.Location = new System.Drawing.Point(-1, 430);
            this.BattlePlayer3.Name = "BattlePlayer3";
            this.BattlePlayer3.Size = new System.Drawing.Size(938, 222);
            this.BattlePlayer3.TabIndex = 4;
            // 
            // BattlePlayer4
            // 
            this.BattlePlayer4.Location = new System.Drawing.Point(-1, 645);
            this.BattlePlayer4.Name = "BattlePlayer4";
            this.BattlePlayer4.Size = new System.Drawing.Size(938, 222);
            this.BattlePlayer4.TabIndex = 3;
            // 
            // BattlePlayer2
            // 
            this.BattlePlayer2.Location = new System.Drawing.Point(0, 215);
            this.BattlePlayer2.Name = "BattlePlayer2";
            this.BattlePlayer2.Size = new System.Drawing.Size(938, 222);
            this.BattlePlayer2.TabIndex = 2;
            // 
            // BattlePlayer1
            // 
            this.BattlePlayer1.Location = new System.Drawing.Point(-1, 0);
            this.BattlePlayer1.Name = "BattlePlayer1";
            this.BattlePlayer1.Size = new System.Drawing.Size(938, 222);
            this.BattlePlayer1.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 838);
            this.Controls.Add(this.Relation6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Relation5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Relation4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Relation3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Relation2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Relation1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BattleInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BattleButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.NameInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PartyMembersPanel);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Party Configuration";
            this.PartyMembersPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox BattleInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BattleButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.TextBox NameInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PartyMembersPanel;
        private Templates.BattlePlayer BattlePlayer5;
        private Templates.BattlePlayer BattlePlayer3;
        private Templates.BattlePlayer BattlePlayer4;
        private Templates.BattlePlayer BattlePlayer2;
        private Templates.BattlePlayer BattlePlayer1;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Relation1;
        private System.Windows.Forms.ComboBox Relation2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Relation4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Relation3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Relation6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Relation5;
        private System.Windows.Forms.Label label9;
    }
}

