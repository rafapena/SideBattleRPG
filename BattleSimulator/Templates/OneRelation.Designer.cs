namespace BattleSimulator.Templates
{
    partial class OneRelation
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
            this.RelationText = new System.Windows.Forms.Label();
            this.RelationType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // RelationText
            // 
            this.RelationText.AutoSize = true;
            this.RelationText.Location = new System.Drawing.Point(3, 6);
            this.RelationText.Name = "RelationText";
            this.RelationText.Size = new System.Drawing.Size(147, 20);
            this.RelationText.TabIndex = 0;
            this.RelationText.Text = "None and None are";
            this.RelationText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // RelationType
            // 
            this.RelationType.FormattingEnabled = true;
            this.RelationType.Location = new System.Drawing.Point(156, 3);
            this.RelationType.Name = "RelationType";
            this.RelationType.Size = new System.Drawing.Size(121, 28);
            this.RelationType.TabIndex = 1;
            // 
            // OneRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RelationType);
            this.Controls.Add(this.RelationText);
            this.Name = "OneRelation";
            this.Size = new System.Drawing.Size(291, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RelationText;
        private System.Windows.Forms.ComboBox RelationType;
    }
}
