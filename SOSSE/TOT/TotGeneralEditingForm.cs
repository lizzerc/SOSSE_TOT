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
    /// Money, in-game date and stamina.
    /// </summary>
    public partial class TotGeneralEditingForm : Form
    {
        private const int moneyOffset = 0x88;
        private const int staminaOffset = 0x4E;
        private const int maxStaminaOffset = 0x50;
        // In-game date: u16 year, u8 season, u8 day (from 1), u8 hour, u8 minute
        private const int dateOffset = 0x4B68;
        private const int staminaPerHeart = 2000;
        private const int maxHearts = 10;

        public TotGeneralEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            byte[] data = TotSave.SaveData;

            moneyNumericUpDown.Maximum = UInt32.MaxValue;
            moneyNumericUpDown.Value = BitConverter.ToUInt32(data, moneyOffset);

            seasonComboBox.Items.AddRange(TotData.SeasonList);
            yearNumericUpDown.Maximum = UInt16.MaxValue;
            yearNumericUpDown.Value = Math.Max(BitConverter.ToUInt16(data, dateOffset), (ushort)1);
            if (data[dateOffset + 2] < TotData.SeasonList.Length)
                seasonComboBox.SelectedIndex = data[dateOffset + 2];
            dayNumericUpDown.Value = Math.Min(Math.Max(data[dateOffset + 3], (byte)1), (byte)31);
            hourNumericUpDown.Value = Math.Min(data[dateOffset + 4], (byte)23);
            minuteNumericUpDown.Value = Math.Min(data[dateOffset + 5], (byte)59);

            maxStaminaNumericUpDown.Maximum = staminaPerHeart * maxHearts;
            staminaNumericUpDown.Maximum = staminaPerHeart * maxHearts;
            maxStaminaNumericUpDown.Value = Math.Min(BitConverter.ToUInt16(data, maxStaminaOffset), (int)maxStaminaNumericUpDown.Maximum);
            staminaNumericUpDown.Value = Math.Min(BitConverter.ToUInt16(data, staminaOffset), (int)staminaNumericUpDown.Maximum);
            updateHearts();
        }

        private void updateHearts()
        {
            staminaHeartsLabel.Text = (staminaNumericUpDown.Value / staminaPerHeart).ToString("0.##") + " hearts";
            maxStaminaHeartsLabel.Text = (maxStaminaNumericUpDown.Value / staminaPerHeart).ToString("0.##") + " hearts";
        }

        private void stamina_ValueChanged(object sender, EventArgs e)
        {
            updateHearts();
        }

        private static void writeU16(int offset, int value)
        {
            if (BitConverter.ToUInt16(TotSave.SaveData, offset) == value) return;
            TotSave.SaveData[offset] = (byte)(value & 0xFF);
            TotSave.SaveData[offset + 1] = (byte)((value >> 8) & 0xFF);
        }

        private static void writeU8(int offset, int value)
        {
            TotSave.SaveData[offset] = (byte)value;
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveGeneral()
        {
            uint money = (uint)moneyNumericUpDown.Value;
            if (BitConverter.ToUInt32(TotSave.SaveData, moneyOffset) != money)
                Array.Copy(BitConverter.GetBytes(money), 0, TotSave.SaveData, moneyOffset, 4);

            // Only write date fields the user changed, so out-of-range values in the save are kept.
            if (yearNumericUpDown.Value != Math.Max(BitConverter.ToUInt16(TotSave.SaveData, dateOffset), (ushort)1))
                writeU16(dateOffset, (int)yearNumericUpDown.Value);
            if (seasonComboBox.SelectedIndex >= 0)
                writeU8(dateOffset + 2, seasonComboBox.SelectedIndex);
            if (dayNumericUpDown.Value != Math.Min(Math.Max(TotSave.SaveData[dateOffset + 3], (byte)1), (byte)31))
                writeU8(dateOffset + 3, (int)dayNumericUpDown.Value);
            if (hourNumericUpDown.Value != Math.Min(TotSave.SaveData[dateOffset + 4], (byte)23))
                writeU8(dateOffset + 4, (int)hourNumericUpDown.Value);
            if (minuteNumericUpDown.Value != Math.Min(TotSave.SaveData[dateOffset + 5], (byte)59))
                writeU8(dateOffset + 5, (int)minuteNumericUpDown.Value);

            if (maxStaminaNumericUpDown.Value != Math.Min(BitConverter.ToUInt16(TotSave.SaveData, maxStaminaOffset), (int)maxStaminaNumericUpDown.Maximum))
                writeU16(maxStaminaOffset, (int)maxStaminaNumericUpDown.Value);
            if (staminaNumericUpDown.Value != Math.Min(BitConverter.ToUInt16(TotSave.SaveData, staminaOffset), (int)staminaNumericUpDown.Maximum))
                writeU16(staminaOffset, (int)staminaNumericUpDown.Value);
        }

        private void TotGeneralEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveGeneral();
        }
    }
}
