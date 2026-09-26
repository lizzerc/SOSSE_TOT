namespace SOSSE
{
    partial class StartForm
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
            this.chooseLabel = new System.Windows.Forms.Label();
            this.sosButton = new System.Windows.Forms.Button();
            this.totButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chooseLabel
            // 
            this.chooseLabel.AutoSize = true;
            this.chooseLabel.Location = new System.Drawing.Point(12, 12);
            this.chooseLabel.Name = "chooseLabel";
            this.chooseLabel.Size = new System.Drawing.Size(120, 13);
            this.chooseLabel.TabIndex = 0;
            this.chooseLabel.Text = "Which game is the save from?";
            // 
            // sosButton
            // 
            this.sosButton.Location = new System.Drawing.Point(12, 36);
            this.sosButton.Name = "sosButton";
            this.sosButton.Size = new System.Drawing.Size(130, 40);
            this.sosButton.TabIndex = 1;
            this.sosButton.Text = "Story of Seasons";
            this.sosButton.UseVisualStyleBackColor = true;
            this.sosButton.Click += new System.EventHandler(this.sosButton_Click);
            // 
            // totButton
            // 
            this.totButton.Location = new System.Drawing.Point(148, 36);
            this.totButton.Name = "totButton";
            this.totButton.Size = new System.Drawing.Size(130, 40);
            this.totButton.TabIndex = 2;
            this.totButton.Text = "Trio of Towns";
            this.totButton.UseVisualStyleBackColor = true;
            this.totButton.Click += new System.EventHandler(this.totButton_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(290, 88);
            this.Controls.Add(this.totButton);
            this.Controls.Add(this.sosButton);
            this.Controls.Add(this.chooseLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SOSSE";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label chooseLabel;
        private System.Windows.Forms.Button sosButton;
        private System.Windows.Forms.Button totButton;
    }
}
