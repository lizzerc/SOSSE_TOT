using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOSSE.TOT
{
    /// <summary>
    /// Animals window: barn animals, pets, wild animals and horse tabs.
    /// </summary>
    public partial class TotAnimalEditingForm : Form
    {
        private TotAnimalPanel barnPanel;
        private TotAnimalPanel petPanel;
        private TotWildAnimalPanel wildPanel;

        public TotAnimalEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();

            barnPanel = new TotAnimalPanel(false);
            petPanel = new TotAnimalPanel(true);
            wildPanel = new TotWildAnimalPanel();
            addTab("Barn Animals", barnPanel);
            addTab("Pets", petPanel);
            addTab("Wild Animals", wildPanel);

            // Horse data isn't mapped yet.
            Label horseLabel = new Label();
            horseLabel.AutoSize = true;
            horseLabel.Location = new Point(12, 12);
            horseLabel.Text = "Horses can't be edited yet.";
            addTab("Horse", horseLabel);
        }

        private void addTab(string title, Control control)
        {
            TabPage tab = new TabPage(title);
            if (!(control is Label))
                control.Dock = DockStyle.Fill;
            tab.Controls.Add(control);
            animalTabControl.TabPages.Add(tab);
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveAnimals()
        {
            barnPanel.SaveAnimals();
            petPanel.SaveAnimals();
            wildPanel.SaveWildAnimals();
        }

        private void TotAnimalEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAnimals();
        }
    }
}
