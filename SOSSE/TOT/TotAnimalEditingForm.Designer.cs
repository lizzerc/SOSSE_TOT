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
            this.animalDataGridView = new System.Windows.Forms.DataGridView();
            this.slotColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speciesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.affectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heartsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personalityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.winsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthdayColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xpColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxAffectionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.animalDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // animalDataGridView
            // 
            this.animalDataGridView.AllowUserToAddRows = false;
            this.animalDataGridView.AllowUserToDeleteRows = false;
            this.animalDataGridView.AllowUserToResizeRows = false;
            this.animalDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animalDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.animalDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slotColumn,
            this.speciesColumn,
            this.nameColumn,
            this.affectionColumn,
            this.heartsColumn,
            this.personalityColumn,
            this.winsColumn,
            this.birthdayColumn,
            this.xpColumn,
            this.levelColumn});
            this.animalDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.animalDataGridView.Location = new System.Drawing.Point(12, 12);
            this.animalDataGridView.Name = "animalDataGridView";
            this.animalDataGridView.RowHeadersVisible = false;
            this.animalDataGridView.Size = new System.Drawing.Size(700, 320);
            this.animalDataGridView.TabIndex = 0;
            this.animalDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.animalDataGridView_CellValidating);
            // 
            // slotColumn
            // 
            this.slotColumn.HeaderText = "Slot";
            this.slotColumn.Name = "slotColumn";
            this.slotColumn.ReadOnly = true;
            this.slotColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.slotColumn.Width = 40;
            // 
            // speciesColumn
            // 
            this.speciesColumn.HeaderText = "Species";
            this.speciesColumn.Name = "speciesColumn";
            this.speciesColumn.ReadOnly = true;
            this.speciesColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.speciesColumn.Width = 110;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nameColumn.Width = 110;
            // 
            // affectionColumn
            // 
            this.affectionColumn.HeaderText = "Affection";
            this.affectionColumn.Name = "affectionColumn";
            this.affectionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.affectionColumn.Width = 65;
            // 
            // heartsColumn
            // 
            this.heartsColumn.HeaderText = "Hearts";
            this.heartsColumn.Name = "heartsColumn";
            this.heartsColumn.ReadOnly = true;
            this.heartsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.heartsColumn.Width = 50;
            // 
            // personalityColumn
            // 
            this.personalityColumn.HeaderText = "Personality";
            this.personalityColumn.Name = "personalityColumn";
            this.personalityColumn.ReadOnly = true;
            this.personalityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.personalityColumn.Width = 75;
            // 
            // winsColumn
            // 
            this.winsColumn.HeaderText = "Festival Wins";
            this.winsColumn.Name = "winsColumn";
            this.winsColumn.ReadOnly = true;
            this.winsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.winsColumn.Width = 80;
            // 
            // birthdayColumn
            // 
            this.birthdayColumn.HeaderText = "Birthday";
            this.birthdayColumn.Name = "birthdayColumn";
            this.birthdayColumn.ReadOnly = true;
            this.birthdayColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.birthdayColumn.Width = 100;
            // 
            // xpColumn
            // 
            this.xpColumn.HeaderText = "XP";
            this.xpColumn.Name = "xpColumn";
            this.xpColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.xpColumn.Width = 60;
            // 
            // levelColumn
            // 
            this.levelColumn.HeaderText = "Level";
            this.levelColumn.Name = "levelColumn";
            this.levelColumn.ReadOnly = true;
            this.levelColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.levelColumn.Width = 45;
            // 
            // maxAffectionButton
            // 
            this.maxAffectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maxAffectionButton.Location = new System.Drawing.Point(612, 338);
            this.maxAffectionButton.Name = "maxAffectionButton";
            this.maxAffectionButton.Size = new System.Drawing.Size(100, 23);
            this.maxAffectionButton.TabIndex = 1;
            this.maxAffectionButton.Text = "Max Affection";
            this.maxAffectionButton.UseVisualStyleBackColor = true;
            this.maxAffectionButton.Click += new System.EventHandler(this.maxAffectionButton_Click);
            // 
            // TotAnimalEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(724, 373);
            this.Controls.Add(this.maxAffectionButton);
            this.Controls.Add(this.animalDataGridView);
            this.Name = "TotAnimalEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Animals";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotAnimalEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.animalDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView animalDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn slotColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn speciesColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn affectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heartsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn personalityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn winsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthdayColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xpColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelColumn;
        private System.Windows.Forms.Button maxAffectionButton;
    }
}
