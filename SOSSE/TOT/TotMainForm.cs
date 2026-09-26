using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SOSSE.TOT
{
    /// <summary>
    /// Main window for a Story of Seasons: Trio of Towns save.
    /// </summary>
    public partial class TotMainForm : Form
    {
        private readonly string fileName;

        public TotMainForm(string fileName)
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            this.fileName = fileName;
            this.Text = "SOSSE - Trio of Towns - " + Path.GetFileName(fileName);

            playerNameTextBox.MaxLength = TotSave.MaxNameLength;
            nicknameTextBox.MaxLength = TotSave.MaxNameLength;
            farmNameTextBox.MaxLength = TotSave.MaxNameLength;
            playerNameTextBox.Text = TotSave.ReadString(TotSave.SaveData, TotSave.PlayerNameOffset, TotSave.MaxNameLength);
            nicknameTextBox.Text = TotSave.ReadString(TotSave.SaveData, TotSave.NicknameOffset, TotSave.MaxNameLength);
            farmNameTextBox.Text = TotSave.ReadString(TotSave.SaveData, TotSave.FarmNameOffset, TotSave.MaxNameLength);
        }

        /// <summary>
        /// Write names back to the save data
        /// </summary>
        private void saveNames()
        {
            TotSave.WriteString(TotSave.SaveData, TotSave.PlayerNameOffset, TotSave.MaxNameLength, playerNameTextBox.Text);
            TotSave.WriteString(TotSave.SaveData, TotSave.NicknameOffset, TotSave.MaxNameLength, nicknameTextBox.Text);
            TotSave.WriteString(TotSave.SaveData, TotSave.FarmNameOffset, TotSave.MaxNameLength, farmNameTextBox.Text);
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            saveNames();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Binary files (*.bin)|*.bin";
            saveDialog.InitialDirectory = Path.GetDirectoryName(fileName);
            saveDialog.FileName = Path.GetFileName(fileName);
            saveDialog.OverwritePrompt = true;
            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                TotSave.Save(saveDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save: " + ex.Message, "Error");
                return;
            }
            MessageBox.Show("Saved. A backup of the original file is kept as " +
                Path.GetFileName(saveDialog.FileName) + ".bak.", "Saved");
        }

        private void itemButton_Click(object sender, EventArgs e)
        {
            new TotItemEditingForm().ShowDialog();
        }

        private void crownButton_Click(object sender, EventArgs e)
        {
            new TotCrownEditingForm().ShowDialog();
        }

        private void animalButton_Click(object sender, EventArgs e)
        {
            new TotAnimalEditingForm(false).ShowDialog();
        }

        private void petButton_Click(object sender, EventArgs e)
        {
            new TotAnimalEditingForm(true).ShowDialog();
        }

        private void TotMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveNames();
        }
    }
}
