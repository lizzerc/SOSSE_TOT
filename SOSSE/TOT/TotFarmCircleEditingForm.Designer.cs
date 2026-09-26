namespace SOSSE.TOT
{
    partial class TotFarmCircleEditingForm
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
            this.farmCircleDataGridView = new System.Windows.Forms.DataGridView();
            this.unlockAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.farmCircleDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // farmCircleDataGridView
            // 
            this.farmCircleDataGridView.Location = new System.Drawing.Point(12, 12);
            this.farmCircleDataGridView.Name = "farmCircleDataGridView";
            this.farmCircleDataGridView.Size = new System.Drawing.Size(380, 400);
            this.farmCircleDataGridView.TabIndex = 0;
            this.farmCircleDataGridView.AllowUserToAddRows = false;
            this.farmCircleDataGridView.AllowUserToDeleteRows = false;
            this.farmCircleDataGridView.AllowUserToResizeRows = false;
            this.farmCircleDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.farmCircleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.farmCircleDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.farmCircleDataGridView.RowHeadersVisible = false;
            // 
            // unlockAllButton
            // 
            this.unlockAllButton.Location = new System.Drawing.Point(302, 418);
            this.unlockAllButton.Name = "unlockAllButton";
            this.unlockAllButton.Size = new System.Drawing.Size(90, 23);
            this.unlockAllButton.TabIndex = 1;
            this.unlockAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.unlockAllButton.Text = "Unlock All";
            this.unlockAllButton.UseVisualStyleBackColor = true;
            this.unlockAllButton.Click += new System.EventHandler(this.unlockAllButton_Click);
            // 
            // TotFarmCircleEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(404, 453);
            this.Controls.Add(this.unlockAllButton);
            this.Controls.Add(this.farmCircleDataGridView);
            this.Name = "TotFarmCircleEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Farm Circles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotFarmCircleEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.farmCircleDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView farmCircleDataGridView;
        private System.Windows.Forms.Button unlockAllButton;
    }
}
