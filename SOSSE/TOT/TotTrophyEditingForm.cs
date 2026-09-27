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
    /// (trophy k = bits 3k to 3k+2). Earning a trophy also raises its counter to the trophy's
    /// target, where the counter is confirmed or highly likely, so that they match.
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
            TotGrid.AddTextColumn(trophyDataGridView, "Counter", 140, true);

            for (int i = 0; i < TotData.TrophyNameList.Length; i++)
            {
                int state = GetState(i);
                int row = trophyDataGridView.Rows.Add(TotData.TrophyNameList[i], state == earned, counterText(i));
                // Only "earned" and "not earned" are known; other states are kept as they are.
                if (state != earned && state != notEarned)
                    TotGrid.LockRow(trophyDataGridView.Rows[row]);
            }
        }

        private static string counterText(int trophy)
        {
            int offset = TotData.TrophyCounterOffset[trophy];
            int itemType = TotData.TrophyCounterItemType[trophy];
            if (itemType >= 0)
                return TotRecordEditingForm.HarvestedOfType(itemType) + " / " + TotData.TrophyCounterTarget[trophy];
            if (TotData.TrophyCounterIsLargeFish[trophy])
                return TotData.LargeFishCount() + " / " + TotData.TrophyCounterTarget[trophy] + " (likely)";
            if (TotData.TrophyCounterSlots[trophy] != null)
            {
                ulong sum = 0;
                foreach (int[] group in TotData.TrophyCounterSlots[trophy])
                {
                    uint[] values = group.Select(slotOffset => BitConverter.ToUInt32(TotSave.SaveData, slotOffset)).ToArray();
                    if (TotData.TrophyCounterCountsNonZero[trophy])
                        sum += values.Any(value => value > 0) ? 1u : 0u;
                    else
                        sum += (ulong)values.Sum(value => (long)value);
                }
                return sum + " / " + TotData.TrophyCounterTarget[trophy] + (TotData.TrophyCounterIsLikely[trophy] ? " (likely)" : "");
            }
            if (offset < 0) return "";
            return readCounter(trophy) + " / " + TotData.TrophyCounterTarget[trophy];
        }

        private static uint readCounter(int trophy)
        {
            int offset = TotData.TrophyCounterOffset[trophy];
            return TotData.TrophyCounterIsU16[trophy] ? BitConverter.ToUInt16(TotSave.SaveData, offset)
                : BitConverter.ToUInt32(TotSave.SaveData, offset);
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
                if (state == GetState(i)) continue;
                SetState(i, state);
                // Raise the counter to the target of a newly earned trophy; never lower it.
                int offset = TotData.TrophyCounterOffset[i];
                if (state == earned && offset >= 0 && readCounter(i) < TotData.TrophyCounterTarget[i])
                {
                    if (TotData.TrophyCounterIsU16[i])
                        Array.Copy(BitConverter.GetBytes((ushort)TotData.TrophyCounterTarget[i]), 0, TotSave.SaveData, offset, 2);
                    else
                        Array.Copy(BitConverter.GetBytes((uint)TotData.TrophyCounterTarget[i]), 0, TotSave.SaveData, offset, 4);
                }
            }
        }

        private void TotTrophyEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTrophies();
        }
    }
}
