namespace SOSSE.TOT
{
    partial class TotRecordEditingForm
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
            this.recordTabControl = new System.Windows.Forms.TabControl();
            this.recordTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // recordTabControl
            // 
            this.recordTabControl.Location = new System.Drawing.Point(12, 12);
            this.recordTabControl.Name = "recordTabControl";
            this.recordTabControl.Size = new System.Drawing.Size(310, 429);
            this.recordTabControl.TabIndex = 0;
            this.recordTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.recordTabControl.SelectedIndex = 0;
            // 
            // TotRecordEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(334, 453);
            this.Controls.Add(this.recordTabControl);
            this.Name = "TotRecordEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Records";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotRecordEditingForm_FormClosing);
            this.recordTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl recordTabControl;
    }
}
