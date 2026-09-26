namespace SOSSE.TOT
{
    partial class TotAnimalPanel
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
            this.animalDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.animalDataGridView.Location = new System.Drawing.Point(12, 12);
            this.animalDataGridView.Name = "animalDataGridView";
            this.animalDataGridView.RowHeadersVisible = false;
            this.animalDataGridView.Size = new System.Drawing.Size(1010, 320);
            this.animalDataGridView.TabIndex = 0;
            this.animalDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.animalDataGridView_CellClick);
            this.animalDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.animalDataGridView_CellValidating);
            this.animalDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.animalDataGridView_CellValueChanged);
            this.animalDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.animalDataGridView_CurrentCellDirtyStateChanged);
            this.animalDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.animalDataGridView_DataError);
            // 
            // maxAffectionButton
            // 
            this.maxAffectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maxAffectionButton.Location = new System.Drawing.Point(922, 338);
            this.maxAffectionButton.Name = "maxAffectionButton";
            this.maxAffectionButton.Size = new System.Drawing.Size(100, 23);
            this.maxAffectionButton.TabIndex = 1;
            this.maxAffectionButton.Text = "Max Affection";
            this.maxAffectionButton.UseVisualStyleBackColor = true;
            this.maxAffectionButton.Click += new System.EventHandler(this.maxAffectionButton_Click);
            // 
            // TotAnimalPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1034, 373);
            this.Controls.Add(this.maxAffectionButton);
            this.Controls.Add(this.animalDataGridView);
            this.Name = "TotAnimalPanel";
            ((System.ComponentModel.ISupportInitialize)(this.animalDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView animalDataGridView;
        private System.Windows.Forms.Button maxAffectionButton;
    }
}
