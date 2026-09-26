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
    /// Product crown ranks, one u8 per rank category (0 none, 1 bronze, 2 silver, 3 gold, 4 rainbow).
    /// </summary>
    public partial class TotCrownEditingForm : Form
    {
        private const int crownOffset = 0x2EEFC;

        public TotCrownEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadItemData();

            rankColumn.Items.AddRange(TotData.RankList);
            for (int i = 0; i < TotData.RankCategoryCount; i++)
            {
                byte rank = TotSave.SaveData[crownOffset + i];
                string rankName;
                if (rank < TotData.RankList.Length)
                    rankName = TotData.RankList[rank];
                else
                {
                    // Keep unexpected values as they are
                    rankName = "Unknown (" + rank + ")";
                    if (!rankColumn.Items.Contains(rankName))
                        rankColumn.Items.Add(rankName);
                }
                crownDataGridView.Rows.Add(i, TotData.RankCategoryName[i], rankName);
            }
        }

        // Show dropdown list right after clicking into ComboBox cells.
        private void crownDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != rankColumn.Index || e.RowIndex < 0) return;
            ComboBox cb = crownDataGridView.EditingControl as ComboBox;
            if (cb != null) cb.DroppedDown = true;
        }

        private void crownDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (crownDataGridView.IsCurrentCellDirty &&
                crownDataGridView.CurrentCell.ColumnIndex == rankColumn.Index)
            {
                crownDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void allRainbowButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in crownDataGridView.Rows)
                row.Cells[rankColumn.Index].Value = TotData.RankList[TotData.RankList.Length - 1];
        }

        /// <summary>
        /// Save all ranks to the main save file
        /// </summary>
        public void SaveCrowns()
        {
            crownDataGridView.EndEdit();
            for (int i = 0; i < TotData.RankCategoryCount; i++)
            {
                int rank = Array.IndexOf(TotData.RankList,
                    crownDataGridView.Rows[i].Cells[rankColumn.Index].Value);
                if (rank >= 0)
                    TotSave.SaveData[crownOffset + i] = (byte)rank;
            }
        }

        private void TotCrownEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCrowns();
        }
    }
}
