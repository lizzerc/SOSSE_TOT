namespace SOSSE.TOT
{
    partial class TotRecipeEditingForm
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
            this.recipeDataGridView = new System.Windows.Forms.DataGridView();
            this.learnAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.recipeDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // recipeDataGridView
            // 
            this.recipeDataGridView.Location = new System.Drawing.Point(12, 12);
            this.recipeDataGridView.Name = "recipeDataGridView";
            this.recipeDataGridView.Size = new System.Drawing.Size(360, 400);
            this.recipeDataGridView.TabIndex = 0;
            this.recipeDataGridView.AllowUserToAddRows = false;
            this.recipeDataGridView.AllowUserToDeleteRows = false;
            this.recipeDataGridView.AllowUserToResizeRows = false;
            this.recipeDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.recipeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recipeDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.recipeDataGridView.RowHeadersVisible = false;
            // 
            // learnAllButton
            // 
            this.learnAllButton.Location = new System.Drawing.Point(282, 418);
            this.learnAllButton.Name = "learnAllButton";
            this.learnAllButton.Size = new System.Drawing.Size(90, 23);
            this.learnAllButton.TabIndex = 1;
            this.learnAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.learnAllButton.Text = "Learn All";
            this.learnAllButton.UseVisualStyleBackColor = true;
            this.learnAllButton.Click += new System.EventHandler(this.learnAllButton_Click);
            // 
            // TotRecipeEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 453);
            this.Controls.Add(this.learnAllButton);
            this.Controls.Add(this.recipeDataGridView);
            this.Name = "TotRecipeEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recipes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotRecipeEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.recipeDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView recipeDataGridView;
        private System.Windows.Forms.Button learnAllButton;
    }
}
