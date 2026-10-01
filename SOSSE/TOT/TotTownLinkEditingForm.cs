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
    /// Town Link points and rank per town. Points are kept within the range of the chosen rank.
    /// </summary>
    public partial class TotTownLinkEditingForm : Form
    {
        // 12-byte records (u32 points, u32 0, u32 rank) for Tsuyukusa, Westown, Lulukoko.
        private const int westownLinkOffset = 0x365F4;
        private const int tsuyukusaLinkOffset = 0x365E8;
        private const int lulukokoLinkOffset = 0x36600;
        private const int maxLinkPoints = 10000;
        // Rank 0 = E ... 5 = S, with the points each rank starts at (TownCommunicationData RankData).
        private static readonly string[] linkRankList = { "E", "D", "C", "B", "A", "S" };
        private static readonly int[] linkRankPoints = { 0, 500, 1500, 3500, 6500, 10000 };
        private NumericUpDown[] linkNumericUpDowns;
        private ComboBox[] linkRankComboBoxes;
        private int[] linkOffsets;
        private decimal[] initialPoints;

        public TotTownLinkEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            byte[] data = TotSave.SaveData;

            linkNumericUpDowns = new[] { westownNumericUpDown, tsuyukusaNumericUpDown, lulukokoNumericUpDown };
            linkRankComboBoxes = new[] { westownRankComboBox, tsuyukusaRankComboBox, lulukokoRankComboBox };
            linkOffsets = new[] { westownLinkOffset, tsuyukusaLinkOffset, lulukokoLinkOffset };
            initialPoints = new decimal[linkOffsets.Length];
            for (int i = 0; i < linkOffsets.Length; i++)
            {
                linkNumericUpDowns[i].Maximum = maxLinkPoints;
                linkNumericUpDowns[i].Value = Math.Min(BitConverter.ToUInt32(data, linkOffsets[i]), (uint)maxLinkPoints);
                linkRankComboBoxes[i].Items.AddRange(linkRankList);
                // A rank outside E-S is shown blank and kept unless changed.
                uint rank = BitConverter.ToUInt32(data, linkOffsets[i] + 8);
                if (rank < linkRankList.Length)
                    linkRankComboBoxes[i].SelectedIndex = (int)rank;
                linkRankComboBoxes[i].SelectedIndexChanged += linkRankComboBox_SelectedIndexChanged;
                setPointRange(i);
                initialPoints[i] = linkNumericUpDowns[i].Value;
            }
        }

        // Limit the points to the chosen rank: from where it starts to just below the next rank.
        private void setPointRange(int town)
        {
            int rank = linkRankComboBoxes[town].SelectedIndex;
            if (rank < 0) return;
            int min = linkRankPoints[rank];
            int max = rank + 1 < linkRankPoints.Length ? linkRankPoints[rank + 1] - 1 : maxLinkPoints;
            // Changing the limits moves the value inside them.
            linkNumericUpDowns[town].Maximum = maxLinkPoints;
            linkNumericUpDowns[town].Minimum = min;
            linkNumericUpDowns[town].Maximum = max;
        }

        private void linkRankComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPointRange(Array.IndexOf(linkRankComboBoxes, sender));
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveTownLink()
        {
            for (int i = 0; i < linkOffsets.Length; i++)
            {
                uint points = (uint)linkNumericUpDowns[i].Value;
                if (linkNumericUpDowns[i].Value != initialPoints[i] && points != BitConverter.ToUInt32(TotSave.SaveData, linkOffsets[i]))
                    Array.Copy(BitConverter.GetBytes(points), 0, TotSave.SaveData, linkOffsets[i], 4);
                if (linkRankComboBoxes[i].SelectedIndex < 0) continue;
                uint rank = (uint)linkRankComboBoxes[i].SelectedIndex;
                if (rank != BitConverter.ToUInt32(TotSave.SaveData, linkOffsets[i] + 8))
                    Array.Copy(BitConverter.GetBytes(rank), 0, TotSave.SaveData, linkOffsets[i] + 8, 4);
            }
        }

        private void TotTownLinkEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTownLink();
        }
    }
}
