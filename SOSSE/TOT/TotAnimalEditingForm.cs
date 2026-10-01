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
        // Horse record after the pets: species byte (67-74 = the 8 horse types) and name at +0x40.
        private const int horseOffset = 0x7978;
        private const int horseNameOffset = horseOffset + 0x40;
        private const int firstHorseSpecies = 67;
        private const int horseTypeCount = 8;
        private ComboBox horseTypeComboBox;
        private TextBox horseNameTextBox;

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

            addTab("Horse", createHorsePanel());
        }

        private Panel createHorsePanel()
        {
            TotData.LoadAnimalData();
            Panel panel = new Panel();
            Label typeLabel = new Label { AutoSize = true, Location = new Point(12, 15), Text = "Type" };
            horseTypeComboBox = new ComboBox { Location = new Point(70, 12), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 0; i < horseTypeCount; i++)
                horseTypeComboBox.Items.Add(TotData.GetAnimalName((byte)(firstHorseSpecies + i)));
            // A value outside the horse types is shown blank and kept unless changed.
            int type = TotSave.SaveData[horseOffset] - firstHorseSpecies;
            if (type >= 0 && type < horseTypeCount)
                horseTypeComboBox.SelectedIndex = type;
            Label nameLabel = new Label { AutoSize = true, Location = new Point(12, 44), Text = "Name" };
            horseNameTextBox = new TextBox { Location = new Point(70, 41), Width = 150, MaxLength = TotSave.MaxNameInput };
            horseNameTextBox.Text = TotSave.ReadString(TotSave.SaveData, horseNameOffset, TotAnimal.MaxNameLength);
            panel.Controls.AddRange(new Control[] { typeLabel, horseTypeComboBox, nameLabel, horseNameTextBox });
            return panel;
        }

        private void saveHorse()
        {
            if (horseTypeComboBox.SelectedIndex >= 0)
                TotSave.SaveData[horseOffset] = (byte)(firstHorseSpecies + horseTypeComboBox.SelectedIndex);
            if (horseNameTextBox.Text != TotSave.ReadString(TotSave.SaveData, horseNameOffset, TotAnimal.MaxNameLength))
                TotSave.WriteString(TotSave.SaveData, horseNameOffset, TotAnimal.MaxNameLength, horseNameTextBox.Text);
        }

        private void addTab(string title, Control control)
        {
            TabPage tab = new TabPage(title);
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
            saveHorse();
        }

        private void TotAnimalEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAnimals();
        }
    }
}
