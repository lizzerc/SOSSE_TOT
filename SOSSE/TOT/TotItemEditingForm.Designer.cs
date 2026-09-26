namespace SOSSE.TOT
{
    partial class TotItemEditingForm
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
            this.itemTabControl = new System.Windows.Forms.TabControl();
            this.maxQualityButton = new System.Windows.Forms.Button();
            this.itemx99Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // itemTabControl
            // 
            this.itemTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemTabControl.Location = new System.Drawing.Point(13, 12);
            this.itemTabControl.Name = "itemTabControl";
            this.itemTabControl.SelectedIndex = 0;
            this.itemTabControl.Size = new System.Drawing.Size(685, 400);
            this.itemTabControl.TabIndex = 0;
            this.itemTabControl.SelectedIndexChanged += new System.EventHandler(this.itemTabControl_SelectedIndexChanged);
            // 
            // maxQualityButton
            // 
            this.maxQualityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maxQualityButton.Location = new System.Drawing.Point(542, 418);
            this.maxQualityButton.Name = "maxQualityButton";
            this.maxQualityButton.Size = new System.Drawing.Size(75, 23);
            this.maxQualityButton.TabIndex = 1;
            this.maxQualityButton.Text = "All 100%";
            this.maxQualityButton.UseVisualStyleBackColor = true;
            this.maxQualityButton.Click += new System.EventHandler(this.maxQualityButton_Click);
            // 
            // itemx99Button
            // 
            this.itemx99Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.itemx99Button.Location = new System.Drawing.Point(623, 418);
            this.itemx99Button.Name = "itemx99Button";
            this.itemx99Button.Size = new System.Drawing.Size(75, 23);
            this.itemx99Button.TabIndex = 2;
            this.itemx99Button.Text = "Item x99";
            this.itemx99Button.UseVisualStyleBackColor = true;
            this.itemx99Button.Click += new System.EventHandler(this.itemx99Button_Click);
            // 
            // TotItemEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(710, 453);
            this.Controls.Add(this.itemx99Button);
            this.Controls.Add(this.maxQualityButton);
            this.Controls.Add(this.itemTabControl);
            this.Name = "TotItemEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Items";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotItemEditingForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl itemTabControl;
        private System.Windows.Forms.Button maxQualityButton;
        private System.Windows.Forms.Button itemx99Button;
    }
}
