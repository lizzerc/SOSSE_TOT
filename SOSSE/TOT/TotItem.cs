using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSSE.TOT
{
    /// <summary>
    /// A 16-byte Trio of Towns item slot:
    /// +0 u16 item ID, +2 u16 item ID (same), +4 4x u16 properties,
    /// +0xC u16 unknown (kept as is), +0xE u8 quantity, +0xF u8 "+" flag (dishes).
    /// </summary>
    public class TotItem
    {
        public const int Size = 0x10;
        public const ushort Empty = 0xFFFF;
        public const int MaxProperty = 300;
        public const ushort GoldenFlag = 0x8000;

        private byte[] itemBytes;

        public ushort Index
        {
            get
            {
                return BitConverter.ToUInt16(itemBytes, 0x0);
            }
            set
            {
                itemBytes[0x0] = (byte)(value & 0xFF);
                itemBytes[0x1] = (byte)((value >> 8) & 0xFF);
                itemBytes[0x2] = (byte)(value & 0xFF);
                itemBytes[0x3] = (byte)((value >> 8) & 0xFF);
            }
        }
        public byte Quantity
        {
            get
            {
                return itemBytes[0xE];
            }
            set
            {
                itemBytes[0xE] = value;
            }
        }
        public bool Plus
        {
            get
            {
                return itemBytes[0xF] != 0;
            }
            set
            {
                itemBytes[0xF] = (byte)(value ? 1 : 0);
            }
        }
        public bool IsEmpty
        {
            get
            {
                return Index == Empty;
            }
        }

        /// <summary>
        /// Raw property value (0-300, 300 = 100%), without the golden flag.
        /// </summary>
        public int GetProperty(int i)
        {
            return BitConverter.ToUInt16(itemBytes, 0x4 + 2 * i) & 0x7FFF;
        }
        /// <summary>
        /// Set a property value, keeping the golden flag.
        /// </summary>
        public void SetProperty(int i, int value)
        {
            ushort raw = BitConverter.ToUInt16(itemBytes, 0x4 + 2 * i);
            raw = (ushort)((raw & GoldenFlag) | (value & 0x7FFF));
            itemBytes[0x4 + 2 * i] = (byte)(raw & 0xFF);
            itemBytes[0x5 + 2 * i] = (byte)((raw >> 8) & 0xFF);
        }
        /// <summary>
        /// Set a single quality value, stored 4 times (items without property bars).
        /// </summary>
        public void SetQuality(int value)
        {
            for (int i = 0; i < 4; i++)
                SetProperty(i, value);
        }
        public bool IsGolden
        {
            get
            {
                for (int i = 0; i < 4; i++)
                    if ((BitConverter.ToUInt16(itemBytes, 0x4 + 2 * i) & GoldenFlag) != 0)
                        return true;
                return false;
            }
        }

        /// <summary>
        /// Displayed stars: average of the 4 properties, rounded up to the next half star (30).
        /// </summary>
        public double Stars
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < 4; i++)
                    sum += GetProperty(i);
                return Math.Ceiling(sum / 4.0 / 30) / 2;
            }
        }

        public TotItem(byte[] bytes)
        {
            itemBytes = bytes;
        }

        /// <summary>
        /// Get item name of this item
        /// </summary>
        public string GetItemName()
        {
            if (TotData.IsValidItem(Index))
                return TotData.ItemNameList[Index];
            else if (IsEmpty)
                return "None";
            else
                return "#" + Index;
        }

        /// <summary>
        /// Replace the item in this slot. Properties are reset, +0xC is kept.
        /// </summary>
        public void SetItem(ushort index)
        {
            Index = index;
            for (int i = 0; i < 4; i++)
            {
                itemBytes[0x4 + 2 * i] = 0;
                itemBytes[0x5 + 2 * i] = 0;
            }
            Plus = false;
            if (index == Empty)
                Quantity = 0;
            else
            {
                if (Quantity == 0) Quantity = 1;
                SetQuality(MaxProperty / 2);
            }
        }

        public byte[] ToArray()
        {
            return itemBytes;
        }
    }
}
