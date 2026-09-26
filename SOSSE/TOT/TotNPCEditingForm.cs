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
    /// NPC friendship points (u32 at the start of each NPC record).
    /// </summary>
    public partial class TotNPCEditingForm : Form
    {
        private const int loverOffset = 0x233C4;
        private const int loverSize = 0x60;
        private const int otherOffset = 0x23A24;
        private const int otherSize = 0x58;
        // Villagers show 5 hearts at 50000; only the spouse goes further (10 hearts at 100000).
        private const uint fiveHearts = 50000;
        private const uint maxPoints = 100000;

        private class NPC
        {
            public string Name;
            public int Offset;
            // Unused DLC slots and family members are not on the relationship screen.
            public bool Listed;
            public bool Editable;
        }
        private List<NPC> npcs;

        public TotNPCEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();

            npcs = new List<NPC>();
            for (int i = 0; i < TotData.NPCLoverNameList.Length; i++)
            {
                bool unused = TotData.NPCLoverNameList[i].StartsWith("(");
                npcs.Add(new NPC { Name = TotData.NPCLoverNameList[i], Offset = loverOffset + i * loverSize,
                    Listed = !unused, Editable = !unused });
            }
            for (int i = 0; i < TotData.NPCOtherNameList.Length; i++)
            {
                bool family = i >= TotData.NPCOtherNameList.Length - 3;
                npcs.Add(new NPC { Name = TotData.NPCOtherNameList[i], Offset = otherOffset + i * otherSize,
                    Listed = !family, Editable = true });
            }

            foreach (NPC npc in npcs)
            {
                uint points = BitConverter.ToUInt32(TotSave.SaveData, npc.Offset);
                int row = npcDataGridView.Rows.Add(npc.Name, points, hearts(points));
                if (!npc.Editable)
                {
                    npcDataGridView.Rows[row].ReadOnly = true;
                    npcDataGridView.Rows[row].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        /// <summary>
        /// Hearts on the relationship screen: a half-heart for each full 5000 points above a
        /// multiple of 5000 (5001-10000 = 1/2), 5 at 50000. The spouse goes up to 10 at 100000.
        /// </summary>
        private static string hearts(uint points)
        {
            if (points >= fiveHearts)
                return points > fiveHearts ? "5 (spouse: " + (points / 10000.0).ToString("0.#") + ")" : "5";
            return ((Math.Max(points, 1) - 1) / 5000 / 2.0).ToString("0.#");
        }

        private void npcDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != pointsColumn.Index || !npcDataGridView.IsCurrentCellInEditMode) return;
            DataGridViewCell cell = npcDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            uint points;
            bool isValid = UInt32.TryParse(e.FormattedValue.ToString(), out points);
            if (!isValid || points > maxPoints)
            {
                cell.ErrorText = "Must be a valid number between 0 and " + maxPoints;
                npcDataGridView.CancelEdit();
            }
            else
                cell.ErrorText = null;
        }

        private void npcDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != pointsColumn.Index) return;
            uint points;
            if (UInt32.TryParse(Convert.ToString(npcDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), out points))
                npcDataGridView.Rows[e.RowIndex].Cells[heartsColumn.Index].Value = hearts(points);
        }

        // Raise every NPC on the relationship screen to at least 5 hearts.
        private void allHeartsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                if (!npcs[i].Listed) continue;
                DataGridViewCell cell = npcDataGridView.Rows[i].Cells[pointsColumn.Index];
                uint points;
                if (UInt32.TryParse(Convert.ToString(cell.Value), out points) && points < fiveHearts)
                    cell.Value = fiveHearts;
            }
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveNPCs()
        {
            npcDataGridView.EndEdit();
            for (int i = 0; i < npcs.Count; i++)
            {
                if (!npcs[i].Editable) continue;
                uint points;
                if (!UInt32.TryParse(Convert.ToString(npcDataGridView.Rows[i].Cells[pointsColumn.Index].Value), out points))
                    continue;
                if (points != BitConverter.ToUInt32(TotSave.SaveData, npcs[i].Offset))
                    Array.Copy(BitConverter.GetBytes(points), 0, TotSave.SaveData, npcs[i].Offset, 4);
            }
        }

        private void TotNPCEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveNPCs();
        }
    }
}
