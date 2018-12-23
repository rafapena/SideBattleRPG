namespace BattleSimulator.Templates
{
    partial class RPGBattler
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
            this.BattlerImage = new System.Windows.Forms.PictureBox();
            this.HPText = new System.Windows.Forms.Label();
            this.SPText = new System.Windows.Forms.Label();
            this.HP = new System.Windows.Forms.Label();
            this.MaxHP = new System.Windows.Forms.Label();
            this.SlashText = new System.Windows.Forms.Label();
            this.SP = new System.Windows.Forms.Label();
            this.BattlerName = new System.Windows.Forms.Label();
            this.State1 = new System.Windows.Forms.Label();
            this.State2 = new System.Windows.Forms.Label();
            this.State3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BattlerImage)).BeginInit();
            this.SuspendLayout();
            // 
            // BattlerImage
            // 
            this.BattlerImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BattlerImage.Location = new System.Drawing.Point(17, 11);
            this.BattlerImage.Name = "BattlerImage";
            this.BattlerImage.Size = new System.Drawing.Size(130, 118);
            this.BattlerImage.TabIndex = 0;
            this.BattlerImage.TabStop = false;
            // 
            // HPText
            // 
            this.HPText.AutoSize = true;
            this.HPText.Location = new System.Drawing.Point(13, 164);
            this.HPText.Name = "HPText";
            this.HPText.Size = new System.Drawing.Size(31, 20);
            this.HPText.TabIndex = 1;
            this.HPText.Text = "HP";
            // 
            // SPText
            // 
            this.SPText.AutoSize = true;
            this.SPText.Location = new System.Drawing.Point(14, 184);
            this.SPText.Name = "SPText";
            this.SPText.Size = new System.Drawing.Size(30, 20);
            this.SPText.TabIndex = 2;
            this.SPText.Text = "SP";
            // 
            // HP
            // 
            this.HP.AutoSize = true;
            this.HP.Location = new System.Drawing.Point(59, 164);
            this.HP.Name = "HP";
            this.HP.Size = new System.Drawing.Size(36, 20);
            this.HP.TabIndex = 3;
            this.HP.Text = "100";
            this.HP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MaxHP
            // 
            this.MaxHP.AutoSize = true;
            this.MaxHP.Location = new System.Drawing.Point(111, 164);
            this.MaxHP.Name = "MaxHP";
            this.MaxHP.Size = new System.Drawing.Size(36, 20);
            this.MaxHP.TabIndex = 4;
            this.MaxHP.Text = "100";
            this.MaxHP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SlashText
            // 
            this.SlashText.AutoSize = true;
            this.SlashText.Location = new System.Drawing.Point(98, 164);
            this.SlashText.Name = "SlashText";
            this.SlashText.Size = new System.Drawing.Size(13, 20);
            this.SlashText.TabIndex = 5;
            this.SlashText.Text = "/";
            this.SlashText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SP
            // 
            this.SP.AutoSize = true;
            this.SP.Location = new System.Drawing.Point(60, 184);
            this.SP.Name = "SP";
            this.SP.Size = new System.Drawing.Size(36, 20);
            this.SP.TabIndex = 6;
            this.SP.Text = "100";
            this.SP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BattlerName
            // 
            this.BattlerName.AutoSize = true;
            this.BattlerName.Location = new System.Drawing.Point(13, 144);
            this.BattlerName.Name = "BattlerName";
            this.BattlerName.Size = new System.Drawing.Size(98, 20);
            this.BattlerName.TabIndex = 7;
            this.BattlerName.Text = "BattlerName";
            // 
            // State1
            // 
            this.State1.AutoSize = true;
            this.State1.Location = new System.Drawing.Point(14, 204);
            this.State1.Name = "State1";
            this.State1.Size = new System.Drawing.Size(57, 20);
            this.State1.TabIndex = 8;
            this.State1.Text = "State1";
            // 
            // State2
            // 
            this.State2.AutoSize = true;
            this.State2.Location = new System.Drawing.Point(14, 224);
            this.State2.Name = "State2";
            this.State2.Size = new System.Drawing.Size(57, 20);
            this.State2.TabIndex = 9;
            this.State2.Text = "State2";
            // 
            // State3
            // 
            this.State3.AutoSize = true;
            this.State3.Location = new System.Drawing.Point(13, 244);
            this.State3.Name = "State3";
            this.State3.Size = new System.Drawing.Size(57, 20);
            this.State3.TabIndex = 10;
            this.State3.Text = "State3";
            // 
            // RPGBattler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.State3);
            this.Controls.Add(this.State2);
            this.Controls.Add(this.State1);
            this.Controls.Add(this.BattlerName);
            this.Controls.Add(this.SP);
            this.Controls.Add(this.SlashText);
            this.Controls.Add(this.MaxHP);
            this.Controls.Add(this.HP);
            this.Controls.Add(this.SPText);
            this.Controls.Add(this.HPText);
            this.Controls.Add(this.BattlerImage);
            this.Name = "RPGBattler";
            this.Size = new System.Drawing.Size(165, 274);
            ((System.ComponentModel.ISupportInitialize)(this.BattlerImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BattlerImage;
        private System.Windows.Forms.Label HPText;
        private System.Windows.Forms.Label SPText;
        private System.Windows.Forms.Label HP;
        private System.Windows.Forms.Label MaxHP;
        private System.Windows.Forms.Label SlashText;
        private System.Windows.Forms.Label SP;
        private System.Windows.Forms.Label BattlerName;
        private System.Windows.Forms.Label State1;
        private System.Windows.Forms.Label State2;
        private System.Windows.Forms.Label State3;
    }
}
