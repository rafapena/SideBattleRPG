namespace EnemyBattleSimluator.Templates
{
    partial class TableList
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
            this.Grid = new System.Windows.Forms.Panel();
            this.AddNew = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Grid.Location = new System.Drawing.Point(19, 64);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(150, 550);
            this.Grid.TabIndex = 3;
            // 
            // AddNew
            // 
            this.AddNew.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AddNew.Location = new System.Drawing.Point(44, 16);
            this.AddNew.Name = "AddNew";
            this.AddNew.Size = new System.Drawing.Size(100, 30);
            this.AddNew.TabIndex = 2;
            this.AddNew.Text = "Add New";
            this.AddNew.UseVisualStyleBackColor = false;
            // 
            // TableList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.AddNew);
            this.Name = "TableList";
            this.Size = new System.Drawing.Size(186, 633);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Grid;
        private System.Windows.Forms.Button AddNew;
    }
}
