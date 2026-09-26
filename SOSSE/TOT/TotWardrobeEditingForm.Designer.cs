namespace SOSSE.TOT
{
    partial class TotWardrobeEditingForm
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
            this.clothesLabel = new System.Windows.Forms.Label();
            this.clothesComboBox = new System.Windows.Forms.ComboBox();
            this.hatLabel = new System.Windows.Forms.Label();
            this.hatComboBox = new System.Windows.Forms.ComboBox();
            this.glassesLabel = new System.Windows.Forms.Label();
            this.glassesComboBox = new System.Windows.Forms.ComboBox();
            this.wardrobeDataGridView = new System.Windows.Forms.DataGridView();
            this.ownAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wardrobeDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // clothesLabel
            // 
            this.clothesLabel.Location = new System.Drawing.Point(12, 15);
            this.clothesLabel.Name = "clothesLabel";
            this.clothesLabel.Size = new System.Drawing.Size(60, 13);
            this.clothesLabel.TabIndex = 0;
            this.clothesLabel.AutoSize = true;
            this.clothesLabel.Text = "Clothes";
            // 
            // clothesComboBox
            // 
            this.clothesComboBox.Location = new System.Drawing.Point(93, 12);
            this.clothesComboBox.Name = "clothesComboBox";
            this.clothesComboBox.Size = new System.Drawing.Size(220, 21);
            this.clothesComboBox.TabIndex = 1;
            this.clothesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clothesComboBox.FormattingEnabled = true;
            // 
            // hatLabel
            // 
            this.hatLabel.Location = new System.Drawing.Point(12, 42);
            this.hatLabel.Name = "hatLabel";
            this.hatLabel.Size = new System.Drawing.Size(60, 13);
            this.hatLabel.TabIndex = 2;
            this.hatLabel.AutoSize = true;
            this.hatLabel.Text = "Hat";
            // 
            // hatComboBox
            // 
            this.hatComboBox.Location = new System.Drawing.Point(93, 39);
            this.hatComboBox.Name = "hatComboBox";
            this.hatComboBox.Size = new System.Drawing.Size(220, 21);
            this.hatComboBox.TabIndex = 3;
            this.hatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hatComboBox.FormattingEnabled = true;
            // 
            // glassesLabel
            // 
            this.glassesLabel.Location = new System.Drawing.Point(12, 69);
            this.glassesLabel.Name = "glassesLabel";
            this.glassesLabel.Size = new System.Drawing.Size(60, 13);
            this.glassesLabel.TabIndex = 4;
            this.glassesLabel.AutoSize = true;
            this.glassesLabel.Text = "Glasses";
            // 
            // glassesComboBox
            // 
            this.glassesComboBox.Location = new System.Drawing.Point(93, 66);
            this.glassesComboBox.Name = "glassesComboBox";
            this.glassesComboBox.Size = new System.Drawing.Size(220, 21);
            this.glassesComboBox.TabIndex = 5;
            this.glassesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.glassesComboBox.FormattingEnabled = true;
            // 
            // wardrobeDataGridView
            // 
            this.wardrobeDataGridView.Location = new System.Drawing.Point(12, 93);
            this.wardrobeDataGridView.Name = "wardrobeDataGridView";
            this.wardrobeDataGridView.Size = new System.Drawing.Size(410, 379);
            this.wardrobeDataGridView.TabIndex = 6;
            this.wardrobeDataGridView.AllowUserToAddRows = false;
            this.wardrobeDataGridView.AllowUserToDeleteRows = false;
            this.wardrobeDataGridView.AllowUserToResizeRows = false;
            this.wardrobeDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.wardrobeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wardrobeDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.wardrobeDataGridView.RowHeadersVisible = false;
            // 
            // ownAllButton
            // 
            this.ownAllButton.Location = new System.Drawing.Point(332, 478);
            this.ownAllButton.Name = "ownAllButton";
            this.ownAllButton.Size = new System.Drawing.Size(90, 23);
            this.ownAllButton.TabIndex = 7;
            this.ownAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ownAllButton.Text = "Own All";
            this.ownAllButton.UseVisualStyleBackColor = true;
            this.ownAllButton.Click += new System.EventHandler(this.ownAllButton_Click);
            // 
            // TotWardrobeEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(434, 513);
            this.Controls.Add(this.ownAllButton);
            this.Controls.Add(this.wardrobeDataGridView);
            this.Controls.Add(this.glassesComboBox);
            this.Controls.Add(this.glassesLabel);
            this.Controls.Add(this.hatComboBox);
            this.Controls.Add(this.hatLabel);
            this.Controls.Add(this.clothesComboBox);
            this.Controls.Add(this.clothesLabel);
            this.Name = "TotWardrobeEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wardrobe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotWardrobeEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.wardrobeDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clothesLabel;
        private System.Windows.Forms.ComboBox clothesComboBox;
        private System.Windows.Forms.Label hatLabel;
        private System.Windows.Forms.ComboBox hatComboBox;
        private System.Windows.Forms.Label glassesLabel;
        private System.Windows.Forms.ComboBox glassesComboBox;
        private System.Windows.Forms.DataGridView wardrobeDataGridView;
        private System.Windows.Forms.Button ownAllButton;
    }
}
