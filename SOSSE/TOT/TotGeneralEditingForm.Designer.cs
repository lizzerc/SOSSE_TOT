namespace SOSSE.TOT
{
    partial class TotGeneralEditingForm
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
            this.moneyLabel = new System.Windows.Forms.Label();
            this.moneyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.yearLabel = new System.Windows.Forms.Label();
            this.yearNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.seasonLabel = new System.Windows.Forms.Label();
            this.seasonComboBox = new System.Windows.Forms.ComboBox();
            this.dayLabel = new System.Windows.Forms.Label();
            this.dayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.hourLabel = new System.Windows.Forms.Label();
            this.hourNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.minuteLabel = new System.Windows.Forms.Label();
            this.minuteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.staminaLabel = new System.Windows.Forms.Label();
            this.staminaNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.staminaHeartsLabel = new System.Windows.Forms.Label();
            this.maxStaminaLabel = new System.Windows.Forms.Label();
            this.maxStaminaNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.maxStaminaHeartsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.moneyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staminaNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxStaminaNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // moneyLabel
            // 
            this.moneyLabel.Location = new System.Drawing.Point(12, 14);
            this.moneyLabel.Name = "moneyLabel";
            this.moneyLabel.Size = new System.Drawing.Size(80, 13);
            this.moneyLabel.TabIndex = 0;
            this.moneyLabel.AutoSize = true;
            this.moneyLabel.Text = "Money (G)";
            // 
            // moneyNumericUpDown
            // 
            this.moneyNumericUpDown.Location = new System.Drawing.Point(115, 12);
            this.moneyNumericUpDown.Name = "moneyNumericUpDown";
            this.moneyNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.moneyNumericUpDown.TabIndex = 1;
            this.moneyNumericUpDown.ThousandsSeparator = true;
            // 
            // yearLabel
            // 
            this.yearLabel.Location = new System.Drawing.Point(12, 46);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(80, 13);
            this.yearLabel.TabIndex = 2;
            this.yearLabel.AutoSize = true;
            this.yearLabel.Text = "Year";
            // 
            // yearNumericUpDown
            // 
            this.yearNumericUpDown.Location = new System.Drawing.Point(115, 44);
            this.yearNumericUpDown.Name = "yearNumericUpDown";
            this.yearNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.yearNumericUpDown.TabIndex = 3;
            this.yearNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // seasonLabel
            // 
            this.seasonLabel.Location = new System.Drawing.Point(12, 72);
            this.seasonLabel.Name = "seasonLabel";
            this.seasonLabel.Size = new System.Drawing.Size(80, 13);
            this.seasonLabel.TabIndex = 4;
            this.seasonLabel.AutoSize = true;
            this.seasonLabel.Text = "Season";
            // 
            // seasonComboBox
            // 
            this.seasonComboBox.Location = new System.Drawing.Point(115, 70);
            this.seasonComboBox.Name = "seasonComboBox";
            this.seasonComboBox.Size = new System.Drawing.Size(90, 20);
            this.seasonComboBox.TabIndex = 5;
            this.seasonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // dayLabel
            // 
            this.dayLabel.Location = new System.Drawing.Point(12, 98);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(80, 13);
            this.dayLabel.TabIndex = 6;
            this.dayLabel.AutoSize = true;
            this.dayLabel.Text = "Day";
            // 
            // dayNumericUpDown
            // 
            this.dayNumericUpDown.Location = new System.Drawing.Point(115, 96);
            this.dayNumericUpDown.Name = "dayNumericUpDown";
            this.dayNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.dayNumericUpDown.TabIndex = 7;
            this.dayNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.dayNumericUpDown.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // hourLabel
            // 
            this.hourLabel.Location = new System.Drawing.Point(12, 124);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(80, 13);
            this.hourLabel.TabIndex = 8;
            this.hourLabel.AutoSize = true;
            this.hourLabel.Text = "Hour";
            // 
            // hourNumericUpDown
            // 
            this.hourNumericUpDown.Location = new System.Drawing.Point(115, 122);
            this.hourNumericUpDown.Name = "hourNumericUpDown";
            this.hourNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.hourNumericUpDown.TabIndex = 9;
            this.hourNumericUpDown.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // minuteLabel
            // 
            this.minuteLabel.Location = new System.Drawing.Point(12, 150);
            this.minuteLabel.Name = "minuteLabel";
            this.minuteLabel.Size = new System.Drawing.Size(80, 13);
            this.minuteLabel.TabIndex = 10;
            this.minuteLabel.AutoSize = true;
            this.minuteLabel.Text = "Minute";
            // 
            // minuteNumericUpDown
            // 
            this.minuteNumericUpDown.Location = new System.Drawing.Point(115, 148);
            this.minuteNumericUpDown.Name = "minuteNumericUpDown";
            this.minuteNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.minuteNumericUpDown.TabIndex = 11;
            this.minuteNumericUpDown.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            // 
            // staminaLabel
            // 
            this.staminaLabel.Location = new System.Drawing.Point(12, 182);
            this.staminaLabel.Name = "staminaLabel";
            this.staminaLabel.Size = new System.Drawing.Size(80, 13);
            this.staminaLabel.TabIndex = 12;
            this.staminaLabel.AutoSize = true;
            this.staminaLabel.Text = "Stamina";
            // 
            // staminaNumericUpDown
            // 
            this.staminaNumericUpDown.Location = new System.Drawing.Point(115, 180);
            this.staminaNumericUpDown.Name = "staminaNumericUpDown";
            this.staminaNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.staminaNumericUpDown.TabIndex = 13;
            this.staminaNumericUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            this.staminaNumericUpDown.ValueChanged += new System.EventHandler(this.stamina_ValueChanged);
            // 
            // staminaHeartsLabel
            // 
            this.staminaHeartsLabel.Location = new System.Drawing.Point(201, 182);
            this.staminaHeartsLabel.Name = "staminaHeartsLabel";
            this.staminaHeartsLabel.Size = new System.Drawing.Size(60, 13);
            this.staminaHeartsLabel.TabIndex = 14;
            this.staminaHeartsLabel.AutoSize = true;
            this.staminaHeartsLabel.Text = "0 hearts";
            // 
            // maxStaminaLabel
            // 
            this.maxStaminaLabel.Location = new System.Drawing.Point(12, 208);
            this.maxStaminaLabel.Name = "maxStaminaLabel";
            this.maxStaminaLabel.Size = new System.Drawing.Size(80, 13);
            this.maxStaminaLabel.TabIndex = 15;
            this.maxStaminaLabel.AutoSize = true;
            this.maxStaminaLabel.Text = "Max stamina";
            // 
            // maxStaminaNumericUpDown
            // 
            this.maxStaminaNumericUpDown.Location = new System.Drawing.Point(115, 206);
            this.maxStaminaNumericUpDown.Name = "maxStaminaNumericUpDown";
            this.maxStaminaNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.maxStaminaNumericUpDown.TabIndex = 16;
            this.maxStaminaNumericUpDown.Increment = new decimal(new int[] { 2000, 0, 0, 0 });
            this.maxStaminaNumericUpDown.ValueChanged += new System.EventHandler(this.stamina_ValueChanged);
            // 
            // maxStaminaHeartsLabel
            // 
            this.maxStaminaHeartsLabel.Location = new System.Drawing.Point(201, 208);
            this.maxStaminaHeartsLabel.Name = "maxStaminaHeartsLabel";
            this.maxStaminaHeartsLabel.Size = new System.Drawing.Size(60, 13);
            this.maxStaminaHeartsLabel.TabIndex = 17;
            this.maxStaminaHeartsLabel.AutoSize = true;
            this.maxStaminaHeartsLabel.Text = "0 hearts";
            // 
            // TotGeneralEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(290, 240);
            this.Controls.Add(this.maxStaminaHeartsLabel);
            this.Controls.Add(this.maxStaminaNumericUpDown);
            this.Controls.Add(this.maxStaminaLabel);
            this.Controls.Add(this.staminaHeartsLabel);
            this.Controls.Add(this.staminaNumericUpDown);
            this.Controls.Add(this.staminaLabel);
            this.Controls.Add(this.minuteNumericUpDown);
            this.Controls.Add(this.minuteLabel);
            this.Controls.Add(this.hourNumericUpDown);
            this.Controls.Add(this.hourLabel);
            this.Controls.Add(this.dayNumericUpDown);
            this.Controls.Add(this.dayLabel);
            this.Controls.Add(this.seasonComboBox);
            this.Controls.Add(this.seasonLabel);
            this.Controls.Add(this.yearNumericUpDown);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.moneyNumericUpDown);
            this.Controls.Add(this.moneyLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TotGeneralEditingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "General";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TotGeneralEditingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.moneyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dayNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staminaNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxStaminaNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label moneyLabel;
        private System.Windows.Forms.NumericUpDown moneyNumericUpDown;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.NumericUpDown yearNumericUpDown;
        private System.Windows.Forms.Label seasonLabel;
        private System.Windows.Forms.ComboBox seasonComboBox;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.NumericUpDown dayNumericUpDown;
        private System.Windows.Forms.Label hourLabel;
        private System.Windows.Forms.NumericUpDown hourNumericUpDown;
        private System.Windows.Forms.Label minuteLabel;
        private System.Windows.Forms.NumericUpDown minuteNumericUpDown;
        private System.Windows.Forms.Label staminaLabel;
        private System.Windows.Forms.NumericUpDown staminaNumericUpDown;
        private System.Windows.Forms.Label staminaHeartsLabel;
        private System.Windows.Forms.Label maxStaminaLabel;
        private System.Windows.Forms.NumericUpDown maxStaminaNumericUpDown;
        private System.Windows.Forms.Label maxStaminaHeartsLabel;
    }
}
