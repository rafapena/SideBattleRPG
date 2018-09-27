namespace EnemyBattleSimluator.Templates
{
    partial class BattlerEnemy
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.BattlerName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.LevelInput = new System.Windows.Forms.TextBox();
            this.BattlerLevel = new System.Windows.Forms.Label();
            this.HPxInput = new System.Windows.Forms.TextBox();
            this.HPx = new System.Windows.Forms.Label();
            this.PassiveSkillPanel = new System.Windows.Forms.Panel();
            this.SkillPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ItemPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            // 
            // BattlerName
            // 
            this.BattlerName.AutoSize = true;
            this.BattlerName.Location = new System.Drawing.Point(24, 130);
            this.BattlerName.Name = "BattlerName";
            this.BattlerName.Size = new System.Drawing.Size(51, 20);
            this.BattlerName.TabIndex = 17;
            this.BattlerName.Text = "Name";
            this.BattlerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(18, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(110, 110);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(134, 16);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(103, 20);
            this.label.TabIndex = 20;
            this.label.Text = "Passive Skills";
            // 
            // LevelInput
            // 
            this.LevelInput.Location = new System.Drawing.Point(79, 158);
            this.LevelInput.Name = "LevelInput";
            this.LevelInput.Size = new System.Drawing.Size(40, 26);
            this.LevelInput.TabIndex = 23;
            this.LevelInput.Text = "1";
            // 
            // BattlerLevel
            // 
            this.BattlerLevel.AutoSize = true;
            this.BattlerLevel.Location = new System.Drawing.Point(25, 160);
            this.BattlerLevel.Name = "BattlerLevel";
            this.BattlerLevel.Size = new System.Drawing.Size(50, 20);
            this.BattlerLevel.TabIndex = 22;
            this.BattlerLevel.Text = "Level:";
            this.BattlerLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HPxInput
            // 
            this.HPxInput.Location = new System.Drawing.Point(79, 190);
            this.HPxInput.Name = "HPxInput";
            this.HPxInput.Size = new System.Drawing.Size(40, 26);
            this.HPxInput.TabIndex = 25;
            this.HPxInput.Text = "1";
            // 
            // HPx
            // 
            this.HPx.AutoSize = true;
            this.HPx.Location = new System.Drawing.Point(25, 193);
            this.HPx.Name = "HPx";
            this.HPx.Size = new System.Drawing.Size(42, 20);
            this.HPx.TabIndex = 24;
            this.HPx.Text = "HP x";
            this.HPx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PassiveSkillPanel
            // 
            this.PassiveSkillPanel.Location = new System.Drawing.Point(138, 39);
            this.PassiveSkillPanel.Name = "PassiveSkillPanel";
            this.PassiveSkillPanel.Size = new System.Drawing.Size(124, 177);
            this.PassiveSkillPanel.TabIndex = 26;
            // 
            // SkillPanel
            // 
            this.SkillPanel.Location = new System.Drawing.Point(272, 39);
            this.SkillPanel.Name = "SkillPanel";
            this.SkillPanel.Size = new System.Drawing.Size(124, 177);
            this.SkillPanel.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Skills";
            // 
            // ItemPanel
            // 
            this.ItemPanel.Location = new System.Drawing.Point(405, 39);
            this.ItemPanel.Name = "ItemPanel";
            this.ItemPanel.Size = new System.Drawing.Size(124, 177);
            this.ItemPanel.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(401, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "Items";
            // 
            // BattlerEnemy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ItemPanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SkillPanel);
            this.Controls.Add(this.PassiveSkillPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HPxInput);
            this.Controls.Add(this.HPx);
            this.Controls.Add(this.LevelInput);
            this.Controls.Add(this.BattlerLevel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.BattlerName);
            this.Controls.Add(this.pictureBox2);
            this.Name = "BattlerEnemy";
            this.Size = new System.Drawing.Size(547, 236);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label BattlerName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox LevelInput;
        private System.Windows.Forms.Label BattlerLevel;
        private System.Windows.Forms.TextBox HPxInput;
        private System.Windows.Forms.Label HPx;
        private System.Windows.Forms.Panel PassiveSkillPanel;
        private System.Windows.Forms.Panel SkillPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ItemPanel;
        private System.Windows.Forms.Label label3;
    }
}
