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
    /// Farm circles times crafted: u32 per circle at 0x2F28C, in the game's circle shop order.
    /// A circle crafted at least once has a count above 0. The circles in storage are in the Items window.
    /// </summary>
    public partial class TotFarmCircleEditingForm : Form
    {
        private const int craftedOffset = 0x2F28C;
        // Total circles crafted; goes up by one with each craft.
        private const int craftedTotalOffset = 0x2F0A0;

        private DataGridViewTextBoxColumn countColumn;

        public TotFarmCircleEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadFarmCircleData();

            TotGrid.AddTextColumn(farmCircleDataGridView, "Farm circle", 200, true);
            countColumn = TotGrid.AddTextColumn(farmCircleDataGridView, "Times crafted", 90, false);
            farmCircleDataGridView.CellValidating += farmCircleDataGridView_CellValidating;

            for (int i = 0; i < TotData.FarmCircleCraftedNameList.Length; i++)
                farmCircleDataGridView.Rows.Add(TotData.FarmCircleCraftedNameList[i], readCount(i));
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
