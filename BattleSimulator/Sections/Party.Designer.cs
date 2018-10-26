namespace BattleSimulator.Sections
{
    partial class Party
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
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.PartyMembersPanel = new System.Windows.Forms.Panel();
            this.battlePlayer5 = new BattleSimulator.Templates.BattlePlayer();
            this.battlePlayer3 = new BattleSimulator.Templates.BattlePlayer();
            this.battlePlayer4 = new BattleSimulator.Templates.BattlePlayer();
            this.battlePlayer2 = new BattleSimulator.Templates.BattlePlayer();
            this.battlePlayer1 = new BattleSimulator.Templates.BattlePlayer();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.BattleButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.oneRelation4 = new BattleSimulator.Templates.OneRelation();
            this.oneRelation5 = new BattleSimulator.Templates.OneRelation();
            this.oneRelation6 = new BattleSimulator.Templates.OneRelation();
            this.oneRelation3 = new BattleSimulator.Templates.OneRelation();
            this.oneRelation2 = new BattleSimulator.Templates.OneRelation();
            this.oneRelation1 = new BattleSimulator.Templates.OneRelation();
            this.tableList1 = new BattleSimulator.Templates.TableList();
            this.PartyMembersPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(1003, 46);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(147, 46);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(1156, 46);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(147, 46);
            this.RemoveButton.TabIndex = 4;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            // 
            // PartyMembersPanel
            // 
            this.PartyMembersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PartyMembersPanel.Controls.Add(this.battlePlayer5);
            this.PartyMembersPanel.Controls.Add(this.battlePlayer3);
            this.PartyMembersPanel.Controls.Add(this.battlePlayer4);
            this.PartyMembersPanel.Controls.Add(this.battlePlayer2);
            this.PartyMembersPanel.Controls.Add(this.battlePlayer1);
            this.PartyMembersPanel.Location = new System.Drawing.Point(321, 108);
            this.PartyMembersPanel.Name = "PartyMembersPanel";
            this.PartyMembersPanel.Size = new System.Drawing.Size(984, 635);
            this.PartyMembersPanel.TabIndex = 5;
            // 
            // battlePlayer5
            // 
            this.battlePlayer5.Location = new System.Drawing.Point(-1, 860);
            this.battlePlayer5.Name = "battlePlayer5";
            this.battlePlayer5.Size = new System.Drawing.Size(938, 222);
            this.battlePlayer5.TabIndex = 8;
            // 
            // battlePlayer3
            // 
            this.battlePlayer3.Location = new System.Drawing.Point(-1, 430);
            this.battlePlayer3.Name = "battlePlayer3";
            this.battlePlayer3.Size = new System.Drawing.Size(938, 222);
            this.battlePlayer3.TabIndex = 4;
            // 
            // battlePlayer4
            // 
            this.battlePlayer4.Location = new System.Drawing.Point(-1, 645);
            this.battlePlayer4.Name = "battlePlayer4";
            this.battlePlayer4.Size = new System.Drawing.Size(938, 222);
            this.battlePlayer4.TabIndex = 3;
            // 
            // battlePlayer2
            // 
            this.battlePlayer2.Location = new System.Drawing.Point(0, 215);
            this.battlePlayer2.Name = "battlePlayer2";
            this.battlePlayer2.Size = new System.Drawing.Size(938, 222);
            this.battlePlayer2.TabIndex = 2;
            // 
            // battlePlayer1
            // 
            this.battlePlayer1.Location = new System.Drawing.Point(-1, 0);
            this.battlePlayer1.Name = "battlePlayer1";
            this.battlePlayer1.Size = new System.Drawing.Size(938, 222);
            this.battlePlayer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(372, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 26);
            this.textBox1.TabIndex = 7;
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.UpdateButton.Location = new System.Drawing.Point(1079, 845);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(147, 46);
            this.UpdateButton.TabIndex = 8;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = false;
            // 
            // BattleButton
            // 
            this.BattleButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.BattleButton.Location = new System.Drawing.Point(1079, 897);
            this.BattleButton.Name = "BattleButton";
            this.BattleButton.Size = new System.Drawing.Size(147, 46);
            this.BattleButton.TabIndex = 9;
            this.BattleButton.Text = "Battle";
            this.BattleButton.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(854, 866);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "This party versus:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(858, 902);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(213, 28);
            this.comboBox1.TabIndex = 11;
            // 
            // oneRelation4
            // 
            this.oneRelation4.Location = new System.Drawing.Point(322, 973);
            this.oneRelation4.Name = "oneRelation4";
            this.oneRelation4.Size = new System.Drawing.Size(291, 37);
            this.oneRelation4.TabIndex = 17;
            // 
            // oneRelation5
            // 
            this.oneRelation5.Location = new System.Drawing.Point(322, 930);
            this.oneRelation5.Name = "oneRelation5";
            this.oneRelation5.Size = new System.Drawing.Size(291, 37);
            this.oneRelation5.TabIndex = 16;
            // 
            // oneRelation6
            // 
            this.oneRelation6.Location = new System.Drawing.Point(322, 890);
            this.oneRelation6.Name = "oneRelation6";
            this.oneRelation6.Size = new System.Drawing.Size(291, 37);
            this.oneRelation6.TabIndex = 15;
            // 
            // oneRelation3
            // 
            this.oneRelation3.Location = new System.Drawing.Point(322, 845);
            this.oneRelation3.Name = "oneRelation3";
            this.oneRelation3.Size = new System.Drawing.Size(291, 37);
            this.oneRelation3.TabIndex = 14;
            // 
            // oneRelation2
            // 
            this.oneRelation2.Location = new System.Drawing.Point(322, 802);
            this.oneRelation2.Name = "oneRelation2";
            this.oneRelation2.Size = new System.Drawing.Size(291, 37);
            this.oneRelation2.TabIndex = 13;
            // 
            // oneRelation1
            // 
            this.oneRelation1.Location = new System.Drawing.Point(322, 762);
            this.oneRelation1.Name = "oneRelation1";
            this.oneRelation1.Size = new System.Drawing.Size(291, 37);
            this.oneRelation1.TabIndex = 12;
            // 
            // tableList1
            // 
            this.tableList1.Location = new System.Drawing.Point(34, -6);
            this.tableList1.Name = "tableList1";
            this.tableList1.Size = new System.Drawing.Size(264, 998);
            this.tableList1.TabIndex = 2;
            // 
            // Party
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 1063);
            this.Controls.Add(this.oneRelation4);
            this.Controls.Add(this.oneRelation5);
            this.Controls.Add(this.oneRelation6);
            this.Controls.Add(this.oneRelation3);
            this.Controls.Add(this.oneRelation2);
            this.Controls.Add(this.oneRelation1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BattleButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PartyMembersPanel);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.tableList1);
            this.Name = "Party";
            this.Text = "Party";
            this.PartyMembersPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Templates.BattlePlayer battlePlayer1;
        private Templates.TableList tableList1;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Panel PartyMembersPanel;
        private Templates.BattlePlayer battlePlayer3;
        private Templates.BattlePlayer battlePlayer2;
        private Templates.BattlePlayer battlePlayer5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button BattleButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private Templates.BattlePlayer battlePlayer4;
        private Templates.OneRelation oneRelation1;
        private Templates.OneRelation oneRelation2;
        private Templates.OneRelation oneRelation3;
        private Templates.OneRelation oneRelation4;
        private Templates.OneRelation oneRelation5;
        private Templates.OneRelation oneRelation6;
    }
}