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
    /// Main window for Story of Seasons: Trio of Towns saves.
    /// </summary>
    public partial class TotMainForm : Form
    {
        private string fileName;

        public TotMainForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            playerNameTextBox.MaxLength = TotSave.MaxNameInput;
            nicknameTextBox.MaxLength = TotSave.MaxNameInput;
            farmNameTextBox.MaxLength = TotSave.MaxNameInput;
        }

        private void enableButtons()
        {
            saveAsButton.Enabled = true;
            playerNameTextBox.Enabled = true;
            nicknameTextBox.Enabled = true;
            farmNameTextBox.Enabled = true;
            itemButton.Enabled = true;
            crownButton.Enabled = true;
            generalButton.Enabled = true;
            animalButton.Enabled = true;
            petButton.Enabled = true;
            npcButton.Enabled = true;
            recordButton.Enabled = true;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            OpenSave(openFileDialog.FileName);
        }

        /// <summary>
        /// Load a save file and show its names
        /// </summary>
        public bool OpenSave(string path)
        {
            if (!TotSave.IsTotSave(path) || !TotSave.Load(path))
            {
                MessageBox.Show("Not a valid Trio of Towns save file", "Error");
                return false;
            }
            fileName = path;
            this.Text = "SOSSE - Trio of Towns - " + Path.GetFileName(path);
            playerNameTextBox.Text = TotSave.ReadString(TotSave.SaveData, TotSave.PlayerNameOffset, TotSave.MaxNameLength);
            nicknameTextBox.Text = TotSave.ReadString(TotSave.SaveData, TotSave.NicknameOffset, TotSave.MaxNameLength);
            farmNameTextBox.Text = TotSave.ReadString(TotSave.SaveData, TotSave.FarmNameOffset, TotSave.MaxNameLength);
            enableButtons();
            return true;
        }

        /// <summary>
        /// Write names back to the save data, only if they were changed
        /// </summary>
        private void saveNames()
        {
            if (TotSave.SaveData == null || fileName == null) return;
            saveName(TotSave.PlayerNameOffset, playerNameTextBox.Text);
            saveName(TotSave.NicknameOffset, nicknameTextBox.Text);
            saveName(TotSave.FarmNameOffset, farmNameTextBox.Text);
        }

        private static void saveName(int offset, string name)
        {
            if (name != TotSave.ReadString(TotSave.SaveData, offset, TotSave.MaxNameLength))
                TotSave.WriteString(TotSave.SaveData, offset, TotSave.MaxNameLength, name);
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

        private void generalButton_Click(object sender, EventArgs e)
        {
            new TotGeneralEditingForm().ShowDialog();
        }

        private void animalButton_Click(object sender, EventArgs e)
        {
            new TotAnimalEditingForm(false).ShowDialog();
        }

        private void petButton_Click(object sender, EventArgs e)
        {
            new TotAnimalEditingForm(true).ShowDialog();
        }

        private void npcButton_Click(object sender, EventArgs e)
        {
            new TotNPCEditingForm().ShowDialog();
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            new TotRecordEditingForm().ShowDialog();
        }

        private void TotMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveNames();
        }
    }
}
