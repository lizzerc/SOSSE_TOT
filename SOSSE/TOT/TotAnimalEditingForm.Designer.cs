namespace SOSSE.TOT
{
    partial class TotAnimalEditingForm
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
            this.animalTabControl = new System.Windows.Forms.TabControl();
            this.animalTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // animalTabControl
            // 
            this.animalTabControl.Location = new System.Drawing.Point(12, 12);
            this.animalTabControl.Name = "animalTabControl";
            this.animalTabControl.Size = new System.Drawing.Size(1010, 429);
            this.animalTabControl.TabIndex = 0;
            this.animalTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.animalTabControl.SelectedIndex = 0;
            // 
            // TotAnimalEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1034, 453);
            this.Controls.Add(this.animalTabControl);
            this.Name = "TotAnimalEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Animals";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotAnimalEditingForm_FormClosing);
            this.animalTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl animalTabControl;
    }
}
