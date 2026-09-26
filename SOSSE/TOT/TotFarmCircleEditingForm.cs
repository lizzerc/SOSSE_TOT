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
    /// Farm circles crafted: 999 x (u16 farm circle ID, u16 times crafted) at 0x26550.
    /// Only the count of circles already in the list can be changed.
    /// </summary>
    public partial class TotFarmCircleEditingForm : Form
    {
        private const int circleOffset = 0x26550;
        private const int circleCount = 999;
        private const ushort emptySlot = 0xFFFF;
        private const int maxCount = UInt16.MaxValue;

        // Save offset of each row's count
        private List<int> rowOffsets = new List<int>();
        private DataGridViewTextBoxColumn countColumn;

        public TotFarmCircleEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadFarmCircleData();

            TotGrid.AddTextColumn(farmCircleDataGridView, "Farm circle", 200, true);
            countColumn = TotGrid.AddTextColumn(farmCircleDataGridView, "Times crafted", 90, false);
            farmCircleDataGridView.CellValidating += farmCircleDataGridView_CellValidating;

            for (int i = 0; i < circleCount; i++)
            {
                int offset = circleOffset + 4 * i;
                ushort id = BitConverter.ToUInt16(TotSave.SaveData, offset);
                if (id == emptySlot) continue;
                string name = id < TotData.FarmCircleNameList.Length ? TotData.FarmCircleNameList[id] : "#" + id;
                farmCircleDataGridView.Rows.Add(name, BitConverter.ToUInt16(TotSave.SaveData, offset + 2));
                rowOffsets.Add(offset + 2);
            }
        }

        private void farmCircleDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            TotGrid.ValidateRange(farmCircleDataGridView, e, countColumn.Index, 1, maxCount);
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveFarmCircles()
        {
            farmCircleDataGridView.EndEdit();
            for (int i = 0; i < rowOffsets.Count; i++)
            {
                int count;
                if (!TotGrid.TryGetInt(farmCircleDataGridView.Rows[i].Cells[countColumn.Index], out count)) continue;
                if (count != BitConverter.ToUInt16(TotSave.SaveData, rowOffsets[i]))
                    Array.Copy(BitConverter.GetBytes((ushort)count), 0, TotSave.SaveData, rowOffsets[i], 2);
            }
        }

        private void TotFarmCircleEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFarmCircles();
        }
    }
}
