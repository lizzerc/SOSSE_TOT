namespace SOSSE.TOT
{
    partial class TotCrownEditingForm
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
            this.crownDataGridView = new System.Windows.Forms.DataGridView();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.allRainbowButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.crownDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // crownDataGridView
            // 
            this.crownDataGridView.AllowUserToAddRows = false;
            this.crownDataGridView.AllowUserToDeleteRows = false;
            this.crownDataGridView.AllowUserToResizeRows = false;
            this.crownDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crownDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.crownDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.categoryColumn,
            this.nameColumn,
            this.rankColumn});
            this.crownDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.crownDataGridView.Location = new System.Drawing.Point(12, 12);
            this.crownDataGridView.Name = "crownDataGridView";
            this.crownDataGridView.RowHeadersVisible = false;
            this.crownDataGridView.Size = new System.Drawing.Size(360, 400);
            this.crownDataGridView.TabIndex = 0;
            this.crownDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.crownDataGridView_CellClick);
            this.crownDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.crownDataGridView_CurrentCellDirtyStateChanged);
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "#";
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            this.categoryColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.categoryColumn.Width = 40;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Category";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nameColumn.Width = 180;
            // 
            // rankColumn
            // 
            this.rankColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.rankColumn.HeaderText = "Crown";
            this.rankColumn.Name = "rankColumn";
            this.rankColumn.Width = 100;
            // 
            // allRainbowButton
            // 
            this.allRainbowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.allRainbowButton.Location = new System.Drawing.Point(282, 418);
            this.allRainbowButton.Name = "allRainbowButton";
            this.allRainbowButton.Size = new System.Drawing.Size(90, 23);
            this.allRainbowButton.TabIndex = 1;
            this.allRainbowButton.Text = "All Rainbow";
            this.allRainbowButton.UseVisualStyleBackColor = true;
            this.allRainbowButton.Click += new System.EventHandler(this.allRainbowButton_Click);
            // 
            // TotCrownEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 453);
            this.Controls.Add(this.allRainbowButton);
            this.Controls.Add(this.crownDataGridView);
            this.Name = "TotCrownEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crowns";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotCrownEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.crownDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView crownDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn rankColumn;
        private System.Windows.Forms.Button allRainbowButton;
    }
}
