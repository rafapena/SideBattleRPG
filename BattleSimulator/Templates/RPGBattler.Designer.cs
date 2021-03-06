﻿namespace BattleSimulator.Templates
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
            this.HP = new System.Windows.Forms.Label();
            this.SP = new System.Windows.Forms.Label();
            this.BattlerName = new System.Windows.Forms.Label();
            this.State1 = new System.Windows.Forms.Label();
            this.State2 = new System.Windows.Forms.Label();
            this.HPMod = new System.Windows.Forms.Label();
            this.SPMod = new System.Windows.Forms.Label();
            this.MaxHP = new System.Windows.Forms.Label();
            this.SlashText = new System.Windows.Forms.Label();
            this.LetterKey = new System.Windows.Forms.Label();
            this.Hit = new System.Windows.Forms.Label();
            this.ElementMag = new System.Windows.Forms.Label();
            this.SPConsume = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BattlerImage)).BeginInit();
            this.SuspendLayout();
            // 
            // BattlerImage
            // 
            this.BattlerImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BattlerImage.Location = new System.Drawing.Point(17, 11);
            this.BattlerImage.Name = "BattlerImage";
            this.BattlerImage.Size = new System.Drawing.Size(160, 160);
            this.BattlerImage.TabIndex = 0;
            this.BattlerImage.TabStop = false;
            // 
            // HP
            // 
            this.HP.AutoSize = true;
            this.HP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HP.Location = new System.Drawing.Point(12, 216);
            this.HP.Name = "HP";
            this.HP.Size = new System.Drawing.Size(60, 22);
            this.HP.TabIndex = 3;
            this.HP.Text = "10000";
            this.HP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SP
            // 
            this.SP.AutoSize = true;
            this.SP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SP.Location = new System.Drawing.Point(12, 244);
            this.SP.Name = "SP";
            this.SP.Size = new System.Drawing.Size(40, 22);
            this.SP.TabIndex = 6;
            this.SP.Text = "100";
            this.SP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BattlerName
            // 
            this.BattlerName.AutoSize = true;
            this.BattlerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BattlerName.Location = new System.Drawing.Point(12, 185);
            this.BattlerName.Name = "BattlerName";
            this.BattlerName.Size = new System.Drawing.Size(109, 22);
            this.BattlerName.TabIndex = 7;
            this.BattlerName.Text = "BattlerName";
            // 
            // State1
            // 
            this.State1.AutoSize = true;
            this.State1.Location = new System.Drawing.Point(14, 279);
            this.State1.Name = "State1";
            this.State1.Size = new System.Drawing.Size(57, 20);
            this.State1.TabIndex = 8;
            this.State1.Text = "State1";
            // 
            // State2
            // 
            this.State2.AutoSize = true;
            this.State2.Location = new System.Drawing.Point(14, 301);
            this.State2.Name = "State2";
            this.State2.Size = new System.Drawing.Size(57, 20);
            this.State2.TabIndex = 9;
            this.State2.Text = "State2";
            // 
            // HPMod
            // 
            this.HPMod.AutoSize = true;
            this.HPMod.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.HPMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HPMod.Location = new System.Drawing.Point(25, 127);
            this.HPMod.Name = "HPMod";
            this.HPMod.Size = new System.Drawing.Size(60, 25);
            this.HPMod.TabIndex = 11;
            this.HPMod.Text = "1000";
            // 
            // SPMod
            // 
            this.SPMod.AutoSize = true;
            this.SPMod.BackColor = System.Drawing.SystemColors.Desktop;
            this.SPMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPMod.Location = new System.Drawing.Point(25, 93);
            this.SPMod.Name = "SPMod";
            this.SPMod.Size = new System.Drawing.Size(48, 25);
            this.SPMod.TabIndex = 12;
            this.SPMod.Text = "100";
            // 
            // MaxHP
            // 
            this.MaxHP.AutoSize = true;
            this.MaxHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxHP.Location = new System.Drawing.Point(117, 216);
            this.MaxHP.Name = "MaxHP";
            this.MaxHP.Size = new System.Drawing.Size(60, 22);
            this.MaxHP.TabIndex = 4;
            this.MaxHP.Text = "10000";
            this.MaxHP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SlashText
            // 
            this.SlashText.AutoSize = true;
            this.SlashText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SlashText.Location = new System.Drawing.Point(100, 216);
            this.SlashText.Name = "SlashText";
            this.SlashText.Size = new System.Drawing.Size(15, 22);
            this.SlashText.TabIndex = 5;
            this.SlashText.Text = "/";
            this.SlashText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LetterKey
            // 
            this.LetterKey.AutoSize = true;
            this.LetterKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LetterKey.Location = new System.Drawing.Point(115, 19);
            this.LetterKey.Name = "LetterKey";
            this.LetterKey.Size = new System.Drawing.Size(31, 32);
            this.LetterKey.TabIndex = 13;
            this.LetterKey.Text = "L";
            // 
            // Hit
            // 
            this.Hit.AutoSize = true;
            this.Hit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hit.Location = new System.Drawing.Point(25, 19);
            this.Hit.Name = "Hit";
            this.Hit.Size = new System.Drawing.Size(35, 25);
            this.Hit.TabIndex = 14;
            this.Hit.Text = "Hit";
            // 
            // ElementMag
            // 
            this.ElementMag.AutoSize = true;
            this.ElementMag.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElementMag.Location = new System.Drawing.Point(115, 120);
            this.ElementMag.Name = "ElementMag";
            this.ElementMag.Size = new System.Drawing.Size(34, 32);
            this.ElementMag.TabIndex = 15;
            this.ElementMag.Text = "E";
            // 
            // SPConsume
            // 
            this.SPConsume.AutoSize = true;
            this.SPConsume.BackColor = System.Drawing.SystemColors.Desktop;
            this.SPConsume.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPConsume.Location = new System.Drawing.Point(98, 242);
            this.SPConsume.Name = "SPConsume";
            this.SPConsume.Size = new System.Drawing.Size(56, 25);
            this.SPConsume.TabIndex = 16;
            this.SPConsume.Text = "-100";
            // 
            // RPGBattler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SPConsume);
            this.Controls.Add(this.ElementMag);
            this.Controls.Add(this.Hit);
            this.Controls.Add(this.LetterKey);
            this.Controls.Add(this.SPMod);
            this.Controls.Add(this.HPMod);
            this.Controls.Add(this.State2);
            this.Controls.Add(this.State1);
            this.Controls.Add(this.BattlerName);
            this.Controls.Add(this.SP);
            this.Controls.Add(this.SlashText);
            this.Controls.Add(this.MaxHP);
            this.Controls.Add(this.HP);
            this.Controls.Add(this.BattlerImage);
            this.Name = "RPGBattler";
            this.Size = new System.Drawing.Size(194, 324);
            ((System.ComponentModel.ISupportInitialize)(this.BattlerImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BattlerImage;
        private System.Windows.Forms.Label HP;
        private System.Windows.Forms.Label SP;
        private System.Windows.Forms.Label BattlerName;
        private System.Windows.Forms.Label State1;
        private System.Windows.Forms.Label State2;
        private System.Windows.Forms.Label HPMod;
        private System.Windows.Forms.Label SPMod;
        private System.Windows.Forms.Label MaxHP;
        private System.Windows.Forms.Label SlashText;
        private System.Windows.Forms.Label LetterKey;
        private System.Windows.Forms.Label Hit;
        private System.Windows.Forms.Label ElementMag;
        private System.Windows.Forms.Label SPConsume;
    }
}
