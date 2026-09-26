namespace SOSSE.TOT
{
    partial class TotTrophyEditingForm
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
            this.trophyDataGridView = new System.Windows.Forms.DataGridView();
            this.earnAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trophyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // trophyDataGridView
            // 
            this.trophyDataGridView.Location = new System.Drawing.Point(12, 12);
            this.trophyDataGridView.Name = "trophyDataGridView";
            this.trophyDataGridView.Size = new System.Drawing.Size(360, 400);
            this.trophyDataGridView.TabIndex = 0;
            this.trophyDataGridView.AllowUserToAddRows = false;
            this.trophyDataGridView.AllowUserToDeleteRows = false;
            this.trophyDataGridView.AllowUserToResizeRows = false;
            this.trophyDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.trophyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trophyDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.trophyDataGridView.RowHeadersVisible = false;
            // 
            // earnAllButton
            // 
            this.earnAllButton.Location = new System.Drawing.Point(282, 418);
            this.earnAllButton.Name = "earnAllButton";
            this.earnAllButton.Size = new System.Drawing.Size(90, 23);
            this.earnAllButton.TabIndex = 1;
            this.earnAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.earnAllButton.Text = "Earn All";
            this.earnAllButton.UseVisualStyleBackColor = true;
            this.earnAllButton.Click += new System.EventHandler(this.earnAllButton_Click);
            // 
            // TotTrophyEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 453);
            this.Controls.Add(this.earnAllButton);
            this.Controls.Add(this.trophyDataGridView);
            this.Name = "TotTrophyEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trophies";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotTrophyEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trophyDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView trophyDataGridView;
        private System.Windows.Forms.Button earnAllButton;
    }
}
