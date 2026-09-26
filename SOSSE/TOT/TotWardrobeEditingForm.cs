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
    /// Worn outfit and wardrobe. The wardrobe is 293 x (u32 state, u32 value) at 0x4B7C:
    /// clothes, then hats, then glasses, in the same order as the worn outfit IDs.
    /// </summary>
    public partial class TotWardrobeEditingForm : Form
    {
        private const int clothesOffset = 0x76;
        private const int hatOffset = 0x7A;
        private const int glassesOffset = 0x7C;
        private const int wardrobeOffset = 0x4B7C;

        // Known states. Other states (tailor orders) are shown but can't be changed.
        private const uint notOwned = 0;
        private const uint owned = 1;
        private const uint ownedTailor = 7;
        // Second value of an item that is not made at the tailor
        private const uint noTailor = 255;

        private int wardrobeCount;
        private int initialClothes, initialHat, initialGlasses;
        private DataGridViewCheckBoxColumn ownedColumn;

        public TotWardrobeEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadWardrobeData();

            initialClothes = fillComboBox(clothesComboBox, TotData.ClothesNameList, BitConverter.ToUInt16(TotSave.SaveData, clothesOffset));
            initialHat = fillComboBox(hatComboBox, TotData.HatNameList, BitConverter.ToUInt16(TotSave.SaveData, hatOffset));
            initialGlasses = fillComboBox(glassesComboBox, TotData.GlassesNameList, TotSave.SaveData[glassesOffset]);

            TotGrid.AddTextColumn(wardrobeDataGridView, "Type", 60, true);
            TotGrid.AddTextColumn(wardrobeDataGridView, "Item", 190, true);
            ownedColumn = TotGrid.AddCheckColumn(wardrobeDataGridView, "Owned", 50);
            TotGrid.AddTextColumn(wardrobeDataGridView, "Status", 90, true);

            addItems("Clothes", TotData.ClothesNameList);
            addItems("Hat", TotData.HatNameList);
            addItems("Glasses", TotData.GlassesNameList);
        }

        /// <summary>
        /// Fill a worn outfit combo box. A value outside the list is added at the end
        /// and kept as it is unless another item is picked.
        /// </summary>
        private static int fillComboBox(ComboBox comboBox, string[] names, int value)
        {
            comboBox.Items.AddRange(names);
            if (value >= names.Length)
                comboBox.Items.Add("#" + value);
            comboBox.SelectedIndex = Math.Min(value, names.Length);
            return comboBox.SelectedIndex;
        }

        private void addItems(string type, string[] names)
        {
            foreach (string name in names)
            {
                uint state = getState(wardrobeCount);
                int row = wardrobeDataGridView.Rows.Add(type, name, state == owned || state == ownedTailor, status(state));
                if (state != notOwned && state != owned && state != ownedTailor)
                    TotGrid.LockRow(wardrobeDataGridView.Rows[row]);
                wardrobeCount++;
            }
        }

        private static string status(uint state)
        {
            switch (state)
            {
                case notOwned: return "";
                case owned: return "Owned";
                case ownedTailor: return "Made at tailor";
                default: return "Unknown (" + state + ")";
            }
        }

        private static uint getState(int item)
        {
            return BitConverter.ToUInt32(TotSave.SaveData, wardrobeOffset + 8 * item);
        }

        private void ownAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in wardrobeDataGridView.Rows)
            {
                if (!row.ReadOnly)
                    row.Cells[ownedColumn.Index].Value = true;
            }
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveWardrobe()
        {
            if (clothesComboBox.SelectedIndex != initialClothes)
                Array.Copy(BitConverter.GetBytes((ushort)clothesComboBox.SelectedIndex), 0, TotSave.SaveData, clothesOffset, 2);
            if (hatComboBox.SelectedIndex != initialHat)
                Array.Copy(BitConverter.GetBytes((ushort)hatComboBox.SelectedIndex), 0, TotSave.SaveData, hatOffset, 2);
            if (glassesComboBox.SelectedIndex != initialGlasses)
                TotSave.SaveData[glassesOffset] = (byte)glassesComboBox.SelectedIndex;

            wardrobeDataGridView.EndEdit();
            for (int i = 0; i < wardrobeCount; i++)
            {
                if (wardrobeDataGridView.Rows[i].ReadOnly) continue;
                uint state = getState(i);
                bool wasOwned = state == owned || state == ownedTailor;
                bool isOwned = TotGrid.IsChecked(wardrobeDataGridView.Rows[i].Cells[ownedColumn.Index]);
                if (isOwned == wasOwned) continue;
                // Items added here are owned like a gift; removed items go back to not owned.
                int offset = wardrobeOffset + 8 * i;
                Array.Copy(BitConverter.GetBytes(isOwned ? owned : notOwned), 0, TotSave.SaveData, offset, 4);
                Array.Copy(BitConverter.GetBytes(noTailor), 0, TotSave.SaveData, offset + 4, 4);
            }
        }

        private void TotWardrobeEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWardrobe();
        }
    }
}
