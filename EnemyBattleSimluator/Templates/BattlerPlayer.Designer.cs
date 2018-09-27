namespace EnemyBattleSimluator.Templates
{
    partial class Battlers
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
            this.BattlerLevel = new System.Windows.Forms.Label();
            this.BattlerClass = new System.Windows.Forms.Label();
            this.BattlerName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.LevelInput = new System.Windows.Forms.TextBox();
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
            // BattlerLevel
            // 
            this.BattlerLevel.AutoSize = true;
            this.BattlerLevel.Location = new System.Drawing.Point(14, 183);
            this.BattlerLevel.Name = "BattlerLevel";
            this.BattlerLevel.Size = new System.Drawing.Size(50, 20);
            this.BattlerLevel.TabIndex = 14;
            this.BattlerLevel.Text = "Level:";
            this.BattlerLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BattlerClass
            // 
            this.BattlerClass.AutoSize = true;
            this.BattlerClass.Location = new System.Drawing.Point(14, 154);
            this.BattlerClass.Name = "BattlerClass";
            this.BattlerClass.Size = new System.Drawing.Size(82, 20);
            this.BattlerClass.TabIndex = 13;
            this.BattlerClass.Text = "Class: N/A";
            this.BattlerClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BattlerName
            // 
            this.BattlerName.AutoSize = true;
            this.BattlerName.Location = new System.Drawing.Point(14, 126);
            this.BattlerName.Name = "BattlerName";
            this.BattlerName.Size = new System.Drawing.Size(51, 20);
            this.BattlerName.TabIndex = 12;
            this.BattlerName.Text = "Name";
            this.BattlerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 27);
            this.button1.TabIndex = 11;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(14, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(110, 110);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // LevelInput
            // 
            this.LevelInput.Location = new System.Drawing.Point(65, 181);
            this.LevelInput.Name = "LevelInput";
            this.LevelInput.Size = new System.Drawing.Size(52, 26);
            this.LevelInput.TabIndex = 15;
            this.LevelInput.Text = "1";
            // 
            // Battlers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LevelInput);
            this.Controls.Add(this.BattlerLevel);
            this.Controls.Add(this.BattlerClass);
            this.Controls.Add(this.BattlerName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Name = "Battlers";
            this.Size = new System.Drawing.Size(391, 254);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label BattlerLevel;
        private System.Windows.Forms.Label BattlerClass;
        private System.Windows.Forms.Label BattlerName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox LevelInput;
    }
}
