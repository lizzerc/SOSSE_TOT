namespace SOSSE.TOT
{
    partial class TotMainForm
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
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.playerNameTextBox = new System.Windows.Forms.TextBox();
            this.farmNameLabel = new System.Windows.Forms.Label();
            this.farmNameTextBox = new System.Windows.Forms.TextBox();
            this.petNameLabel = new System.Windows.Forms.Label();
            this.petNameTextBox = new System.Windows.Forms.TextBox();
            this.splitGroupBox = new System.Windows.Forms.GroupBox();
            this.itemButton = new System.Windows.Forms.Button();
            this.crownButton = new System.Windows.Forms.Button();
            this.animalButton = new System.Windows.Forms.Button();
            this.petButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Location = new System.Drawing.Point(12, 15);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(36, 13);
            this.playerNameLabel.TabIndex = 0;
            this.playerNameLabel.Text = "Player";
            // 
            // playerNameTextBox
            // 
            this.playerNameTextBox.Location = new System.Drawing.Point(93, 12);
            this.playerNameTextBox.Name = "playerNameTextBox";
            this.playerNameTextBox.Size = new System.Drawing.Size(156, 20);
            this.playerNameTextBox.TabIndex = 1;
            // 
            // farmNameLabel
            // 
            this.farmNameLabel.AutoSize = true;
            this.farmNameLabel.Location = new System.Drawing.Point(12, 41);
            this.farmNameLabel.Name = "farmNameLabel";
            this.farmNameLabel.Size = new System.Drawing.Size(36, 13);
            this.farmNameLabel.TabIndex = 2;
            this.farmNameLabel.Text = "Farm";
            // 
            // farmNameTextBox
            // 
            this.farmNameTextBox.Location = new System.Drawing.Point(93, 38);
            this.farmNameTextBox.Name = "farmNameTextBox";
            this.farmNameTextBox.Size = new System.Drawing.Size(156, 20);
            this.farmNameTextBox.TabIndex = 3;
            // 
            // petNameLabel
            // 
            this.petNameLabel.AutoSize = true;
            this.petNameLabel.Location = new System.Drawing.Point(12, 67);
            this.petNameLabel.Name = "petNameLabel";
            this.petNameLabel.Size = new System.Drawing.Size(36, 13);
            this.petNameLabel.TabIndex = 4;
            this.petNameLabel.Text = "Pet";
            // 
            // petNameTextBox
            // 
            this.petNameTextBox.Location = new System.Drawing.Point(93, 64);
            this.petNameTextBox.Name = "petNameTextBox";
            this.petNameTextBox.Size = new System.Drawing.Size(156, 20);
            this.petNameTextBox.TabIndex = 5;
            // 
            // splitGroupBox
            // 
            this.splitGroupBox.Location = new System.Drawing.Point(12, 92);
            this.splitGroupBox.Name = "splitGroupBox";
            this.splitGroupBox.Size = new System.Drawing.Size(238, 2);
            this.splitGroupBox.TabIndex = 6;
            this.splitGroupBox.TabStop = false;
            // 
            // itemButton
            // 
            this.itemButton.Location = new System.Drawing.Point(12, 100);
            this.itemButton.Name = "itemButton";
            this.itemButton.Size = new System.Drawing.Size(75, 23);
            this.itemButton.TabIndex = 7;
            this.itemButton.Text = "Items";
            this.itemButton.UseVisualStyleBackColor = true;
            this.itemButton.Click += new System.EventHandler(this.itemButton_Click);
            // 
            // crownButton
            // 
            this.crownButton.Location = new System.Drawing.Point(93, 100);
            this.crownButton.Name = "crownButton";
            this.crownButton.Size = new System.Drawing.Size(75, 23);
            this.crownButton.TabIndex = 8;
            this.crownButton.Text = "Crowns";
            this.crownButton.UseVisualStyleBackColor = true;
            this.crownButton.Click += new System.EventHandler(this.crownButton_Click);
            // 
            // animalButton
            // 
            this.animalButton.Location = new System.Drawing.Point(174, 100);
            this.animalButton.Name = "animalButton";
            this.animalButton.Size = new System.Drawing.Size(75, 23);
            this.animalButton.TabIndex = 9;
            this.animalButton.Text = "Animals";
            this.animalButton.UseVisualStyleBackColor = true;
            this.animalButton.Click += new System.EventHandler(this.animalButton_Click);
            // 
            // petButton
            // 
            this.petButton.Location = new System.Drawing.Point(12, 129);
            this.petButton.Name = "petButton";
            this.petButton.Size = new System.Drawing.Size(75, 23);
            this.petButton.TabIndex = 10;
            this.petButton.Text = "Pets";
            this.petButton.UseVisualStyleBackColor = true;
            this.petButton.Click += new System.EventHandler(this.petButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(174, 129);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(75, 23);
            this.saveAsButton.TabIndex = 11;
            this.saveAsButton.Text = "Save";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // TotMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(261, 164);
            this.Controls.Add(this.saveAsButton);
            this.Controls.Add(this.petButton);
            this.Controls.Add(this.animalButton);
            this.Controls.Add(this.crownButton);
            this.Controls.Add(this.itemButton);
            this.Controls.Add(this.splitGroupBox);
            this.Controls.Add(this.petNameTextBox);
            this.Controls.Add(this.petNameLabel);
            this.Controls.Add(this.farmNameTextBox);
            this.Controls.Add(this.farmNameLabel);
            this.Controls.Add(this.playerNameTextBox);
            this.Controls.Add(this.playerNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TotMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SOSSE - Trio of Towns";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotMainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.TextBox playerNameTextBox;
        private System.Windows.Forms.Label farmNameLabel;
        private System.Windows.Forms.TextBox farmNameTextBox;
        private System.Windows.Forms.Label petNameLabel;
        private System.Windows.Forms.TextBox petNameTextBox;
        private System.Windows.Forms.GroupBox splitGroupBox;
        private System.Windows.Forms.Button itemButton;
        private System.Windows.Forms.Button crownButton;
        private System.Windows.Forms.Button animalButton;
        private System.Windows.Forms.Button petButton;
        private System.Windows.Forms.Button saveAsButton;
    }
}
