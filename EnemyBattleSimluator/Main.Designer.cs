namespace BattleSimluator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ModeSwitcher = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ModeSwitcher
            // 
            this.ModeSwitcher.Location = new System.Drawing.Point(514, 12);
            this.ModeSwitcher.Name = "ModeSwitcher";
            this.ModeSwitcher.Size = new System.Drawing.Size(160, 40);
            this.ModeSwitcher.TabIndex = 0;
            this.ModeSwitcher.Text = "Switch to Parties";
            this.ModeSwitcher.UseVisualStyleBackColor = true;
            this.ModeSwitcher.Click += new System.EventHandler(this.ModeSwitcher_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 725);
            this.Controls.Add(this.ModeSwitcher);
            this.Name = "Main";
            this.Text = "Battle Simulator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ModeSwitcher;
    }
}

