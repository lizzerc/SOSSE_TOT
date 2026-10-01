namespace SOSSE.TOT
{
    partial class TotNPCEditingForm
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
            this.npcDataGridView = new System.Windows.Forms.DataGridView();
            this.allHeartsButton = new System.Windows.Forms.Button();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heartsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.npcDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // npcDataGridView
            // 
            this.npcDataGridView.Location = new System.Drawing.Point(12, 12);
            this.npcDataGridView.Name = "npcDataGridView";
            this.npcDataGridView.Size = new System.Drawing.Size(505, 400);
            this.npcDataGridView.TabIndex = 0;
            this.npcDataGridView.AllowUserToAddRows = false;
            this.npcDataGridView.AllowUserToDeleteRows = false;
            this.npcDataGridView.AllowUserToResizeRows = false;
            this.npcDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.npcDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.npcDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.nameColumn, this.pointsColumn, this.heartsColumn });
            this.npcDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.npcDataGridView.RowHeadersVisible = false;
            this.npcDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.npcDataGridView_CellValidating);
            this.npcDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.npcDataGridView_CellValueChanged);
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "NPC";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nameColumn.Width = 130;
            // 
            // pointsColumn
            // 
            this.pointsColumn.HeaderText = "Friendship";
            this.pointsColumn.Name = "pointsColumn";
            this.pointsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pointsColumn.Width = 80;
            // 
            // heartsColumn
            // 
            this.heartsColumn.HeaderText = "Hearts";
            this.heartsColumn.Name = "heartsColumn";
            this.heartsColumn.ReadOnly = true;
            this.heartsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.heartsColumn.Width = 125;
            // 
            // allHeartsButton
            // 
            this.allHeartsButton.Location = new System.Drawing.Point(427, 418);
            this.allHeartsButton.Name = "allHeartsButton";
            this.allHeartsButton.Size = new System.Drawing.Size(90, 23);
            this.allHeartsButton.TabIndex = 1;
            this.allHeartsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.allHeartsButton.Text = "All 5 Hearts";
            this.allHeartsButton.UseVisualStyleBackColor = true;
            this.allHeartsButton.Click += new System.EventHandler(this.allHeartsButton_Click);
            // 
            // TotNPCEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(529, 453);
            this.Controls.Add(this.allHeartsButton);
            this.Controls.Add(this.npcDataGridView);
            this.Name = "TotNPCEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NPCs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotNPCEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.npcDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView npcDataGridView;
        private System.Windows.Forms.Button allHeartsButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heartsColumn;
    }
}
