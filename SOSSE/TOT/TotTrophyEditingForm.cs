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
    /// Trophies: 320 x 3 bits packed from 0x2EF88, read as one little-endian number
    /// (trophy k = bits 3k to 3k+2).
    /// </summary>
    public partial class TotTrophyEditingForm : Form
    {
        private const int trophyOffset = 0x2EF88;
        private const int earned = 5;
        private const int notEarned = 1;

        private DataGridViewCheckBoxColumn earnedColumn;

        public TotTrophyEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadTrophyData();

            TotGrid.AddTextColumn(trophyDataGridView, "Trophy", 250, true);
            earnedColumn = TotGrid.AddCheckColumn(trophyDataGridView, "Earned", 60);

            for (int i = 0; i < TotData.TrophyNameList.Length; i++)
            {
                int state = GetState(i);
                int row = trophyDataGridView.Rows.Add(TotData.TrophyNameList[i], state == earned);
                // Only "earned" and "not earned" are known; other states are kept as they are.
                if (state != earned && state != notEarned)
                    TotGrid.LockRow(trophyDataGridView.Rows[row]);
            }
        }

        public static int GetState(int trophy)
        {
            int state = 0;
            for (int bit = 0; bit < 3; bit++)
            {
                int position = 3 * trophy + bit;
                if ((TotSave.SaveData[trophyOffset + position / 8] & (1 << (position % 8))) != 0)
                    state |= 1 << bit;
            }
            return state;
        }

        public static void SetState(int trophy, int state)
        {
            for (int bit = 0; bit < 3; bit++)
            {
                int position = 3 * trophy + bit;
                byte mask = (byte)(1 << (position % 8));
                if ((state & (1 << bit)) != 0)
                    TotSave.SaveData[trophyOffset + position / 8] |= mask;
                else
                    TotSave.SaveData[trophyOffset + position / 8] &= (byte)~mask;
            }
        }

        private void earnAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in trophyDataGridView.Rows)
            {
                if (!row.ReadOnly)
                    row.Cells[earnedColumn.Index].Value = true;
            }
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveTrophies()
        {
            trophyDataGridView.EndEdit();
            for (int i = 0; i < TotData.TrophyNameList.Length; i++)
            {
                if (trophyDataGridView.Rows[i].ReadOnly) continue;
                int state = TotGrid.IsChecked(trophyDataGridView.Rows[i].Cells[earnedColumn.Index]) ? earned : notEarned;
                if (state != GetState(i))
                    SetState(i, state);
            }
        }

        private void TotTrophyEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTrophies();
        }
    }
}
