namespace SOSSE.TOT
{
    partial class TotWildAnimalPanel
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
            this.wildAnimalDataGridView = new System.Windows.Forms.DataGridView();
            this.maxAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wildAnimalDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // wildAnimalDataGridView
            // 
            this.wildAnimalDataGridView.Location = new System.Drawing.Point(12, 12);
            this.wildAnimalDataGridView.Name = "wildAnimalDataGridView";
            this.wildAnimalDataGridView.Size = new System.Drawing.Size(320, 400);
            this.wildAnimalDataGridView.TabIndex = 0;
            this.wildAnimalDataGridView.AllowUserToAddRows = false;
            this.wildAnimalDataGridView.AllowUserToDeleteRows = false;
            this.wildAnimalDataGridView.AllowUserToResizeRows = false;
            this.wildAnimalDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.wildAnimalDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wildAnimalDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.wildAnimalDataGridView.RowHeadersVisible = false;
            // 
            // maxAllButton
            // 
            this.maxAllButton.Location = new System.Drawing.Point(242, 418);
            this.maxAllButton.Name = "maxAllButton";
            this.maxAllButton.Size = new System.Drawing.Size(90, 23);
            this.maxAllButton.TabIndex = 1;
            this.maxAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxAllButton.Text = "Max All";
            this.maxAllButton.UseVisualStyleBackColor = true;
            this.maxAllButton.Click += new System.EventHandler(this.maxAllButton_Click);
            // 
            // TotWildAnimalPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(344, 453);
            this.Controls.Add(this.maxAllButton);
            this.Controls.Add(this.wildAnimalDataGridView);
            this.Name = "TotWildAnimalPanel";
            ((System.ComponentModel.ISupportInitialize)(this.wildAnimalDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView wildAnimalDataGridView;
        private System.Windows.Forms.Button maxAllButton;
    }
}
