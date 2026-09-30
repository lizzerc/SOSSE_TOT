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
    /// NPC friendship points (u32 at the start of each NPC record). Unused slots and the player's mother
    /// and sister (not on the relationship screen) are left out.
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
        // Lover record byte that is 4 for the spouse
        private const int loverStatusOffset = 0x14;
        private const byte married = 4;
        // Marriage candidates (the first 10 lover records): gifts given (u16) and affection (u8, 0-200;
        // a reverse proposal needs 120).
        private const int candidateCount = 10;
        private const int giftsOffset = 0x06;
        private const int affectionOffset = 0x15;
        private const int maxAffection = 200;

        private class NPC
        {
            public string Name;
            public int Offset;
            public bool Spouse;
            public bool Candidate;

            public uint MaxPoints
            {
                get
                {
                    return Spouse ? maxPoints : fiveHearts;
                }
            }
        }
        private List<NPC> npcs;
        private DataGridViewTextBoxColumn giftsColumn;
        private DataGridViewTextBoxColumn affectionColumn;

        public TotNPCEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            giftsColumn = TotGrid.AddTextColumn(npcDataGridView, "Gifts", 70, false);
            affectionColumn = TotGrid.AddTextColumn(npcDataGridView, "Affection", 75, false);

            npcs = new List<NPC>();
            for (int i = 0; i < TotData.NPCLoverNameList.Length; i++)
            {
                if (TotData.NPCLoverNameList[i].StartsWith("(")) continue;
                int offset = loverOffset + i * loverSize;
                npcs.Add(new NPC { Name = TotData.NPCLoverNameList[i], Offset = offset,
                    Spouse = TotSave.SaveData[offset + loverStatusOffset] == married, Candidate = i < candidateCount });
            }
            for (int i = 0; i < TotData.NPCOtherNameList.Length; i++)
            {
                if (TotData.NPCOtherNameList[i] == "Mother" || TotData.NPCOtherNameList[i] == "Sister") continue;
                npcs.Add(new NPC { Name = TotData.NPCOtherNameList[i], Offset = otherOffset + i * otherSize });
            }

            foreach (NPC npc in npcs)
            {
                uint points = BitConverter.ToUInt32(TotSave.SaveData, npc.Offset);
                int row = npcDataGridView.Rows.Add(npc.Spouse ? npc.Name + " (spouse)" : npc.Name, points, hearts(points));
                if (npc.Candidate)
                {
                    npcDataGridView.Rows[row].Cells[giftsColumn.Index].Value = BitConverter.ToUInt16(TotSave.SaveData, npc.Offset + giftsOffset);
                    npcDataGridView.Rows[row].Cells[affectionColumn.Index].Value = TotSave.SaveData[npc.Offset + affectionOffset];
                }
                else
                {
                    // Only marriage candidates have gifts and affection.
                    foreach (DataGridViewColumn column in new[] { giftsColumn, affectionColumn })
                    {
                        npcDataGridView.Rows[row].Cells[column.Index].ReadOnly = true;
                        npcDataGridView.Rows[row].Cells[column.Index].Style.BackColor = Color.LightGray;
                    }
                }
            }
            npcDataGridView.CellValidating += (sender, e) =>
            {
                TotGrid.ValidateRange(npcDataGridView, e, giftsColumn.Index, 0, UInt16.MaxValue);
                TotGrid.ValidateRange(npcDataGridView, e, affectionColumn.Index, 0, maxAffection);
            };
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
            uint max = npcs[e.RowIndex].MaxPoints;
            uint points;
            bool isValid = UInt32.TryParse(e.FormattedValue.ToString(), out points);
            if (!isValid || points > max)
            {
                cell.ErrorText = "Must be a valid number between 0 and " + max;
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
                uint points;
                if (!UInt32.TryParse(Convert.ToString(npcDataGridView.Rows[i].Cells[pointsColumn.Index].Value), out points))
                    continue;
                if (points != BitConverter.ToUInt32(TotSave.SaveData, npcs[i].Offset))
                    Array.Copy(BitConverter.GetBytes(points), 0, TotSave.SaveData, npcs[i].Offset, 4);
                if (!npcs[i].Candidate) continue;
                int gifts, affection;
                if (TotGrid.TryGetInt(npcDataGridView.Rows[i].Cells[giftsColumn.Index], out gifts) && gifts >= 0 && gifts <= UInt16.MaxValue &&
                    gifts != BitConverter.ToUInt16(TotSave.SaveData, npcs[i].Offset + giftsOffset))
                    Array.Copy(BitConverter.GetBytes((ushort)gifts), 0, TotSave.SaveData, npcs[i].Offset + giftsOffset, 2);
                if (TotGrid.TryGetInt(npcDataGridView.Rows[i].Cells[affectionColumn.Index], out affection) && affection >= 0 && affection <= maxAffection)
                    TotSave.SaveData[npcs[i].Offset + affectionOffset] = (byte)affection;
            }
        }

        private void TotNPCEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveNPCs();
        }
    }
}
