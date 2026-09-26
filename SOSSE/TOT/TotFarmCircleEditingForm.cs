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
    /// Farm circles unlocked (4 bits per circle at 0x34E44) and times crafted (u32 per circle at 0x2F28C),
    /// both in the game's circle shop order. The circles in storage are in the Items window.
    /// </summary>
    public partial class TotFarmCircleEditingForm : Form
    {
        private const int craftedOffset = 0x2F28C;
        // Total circles crafted; goes up by one with each craft.
        private const int craftedTotalOffset = 0x2F0A0;
        // 4 bits per circle, low nibble first
        private const int unlockedOffset = 0x34E44;
        private const int unlocked = 5;
        private const int locked = 0;

        private DataGridViewCheckBoxColumn unlockedColumn;
        private DataGridViewTextBoxColumn countColumn;

        public TotFarmCircleEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadFarmCircleData();

            TotGrid.AddTextColumn(farmCircleDataGridView, "Farm circle", 200, true);
            unlockedColumn = TotGrid.AddCheckColumn(farmCircleDataGridView, "Unlocked", 60);
            countColumn = TotGrid.AddTextColumn(farmCircleDataGridView, "Times crafted", 90, false);
            farmCircleDataGridView.CellValidating += farmCircleDataGridView_CellValidating;

            for (int i = 0; i < TotData.FarmCircleCraftedNameList.Length; i++)
            {
                int state = GetUnlockState(i);
                int row = farmCircleDataGridView.Rows.Add(TotData.FarmCircleCraftedNameList[i], state == unlocked, readCount(i));
                // Only "unlocked" and "locked" are known; other values are kept as they are.
                if (state != unlocked && state != locked)
                {
                    DataGridViewCell cell = farmCircleDataGridView.Rows[row].Cells[unlockedColumn.Index];
                    cell.ReadOnly = true;
                    cell.Style.BackColor = Color.LightGray;
                }
            }
        }

        public static int GetUnlockState(int circle)
        {
            return (TotSave.SaveData[unlockedOffset + circle / 2] >> (4 * (circle % 2))) & 0xF;
        }

        private static void setUnlockState(int circle, int state)
        {
            int shift = 4 * (circle % 2);
            byte value = TotSave.SaveData[unlockedOffset + circle / 2];
            TotSave.SaveData[unlockedOffset + circle / 2] = (byte)((value & ~(0xF << shift)) | (state << shift));
        }

        private void unlockAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in farmCircleDataGridView.Rows)
            {
                if (!row.Cells[unlockedColumn.Index].ReadOnly)
                    row.Cells[unlockedColumn.Index].Value = true;
            }
        }

        private static uint readCount(int circle)
        {
            return BitConverter.ToUInt32(TotSave.SaveData, craftedOffset + 4 * circle);
        }

        private void farmCircleDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            TotGrid.ValidateRange(farmCircleDataGridView, e, countColumn.Index, 0, 9999);
        }

        /// <summary>
        /// Save changed values to the main save file, and change the total by the same amount
        /// </summary>
        public void SaveFarmCircles()
        {
            farmCircleDataGridView.EndEdit();
            long difference = 0;
            for (int i = 0; i < TotData.FarmCircleCraftedNameList.Length; i++)
            {
                DataGridViewCell unlockedCell = farmCircleDataGridView.Rows[i].Cells[unlockedColumn.Index];
                if (!unlockedCell.ReadOnly)
                {
                    int state = TotGrid.IsChecked(unlockedCell) ? unlocked : locked;
                    if (state != GetUnlockState(i))
                        setUnlockState(i, state);
                }

                int count;
                if (!TotGrid.TryGetInt(farmCircleDataGridView.Rows[i].Cells[countColumn.Index], out count)) continue;
                uint oldCount = readCount(i);
                if (count == oldCount) continue;
                difference += count - (long)oldCount;
                Array.Copy(BitConverter.GetBytes((uint)count), 0, TotSave.SaveData, craftedOffset + 4 * i, 4);
            }
            if (difference == 0) return;
            long total = Math.Max(0, BitConverter.ToUInt32(TotSave.SaveData, craftedTotalOffset) + difference);
            Array.Copy(BitConverter.GetBytes((uint)Math.Min(total, UInt32.MaxValue)), 0, TotSave.SaveData, craftedTotalOffset, 4);
        }

        private void TotFarmCircleEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFarmCircles();
        }
    }
}
