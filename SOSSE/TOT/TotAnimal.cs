using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSSE.TOT
{
    /// <summary>
    /// A Trio of Towns animal record. Barn animals are 0x94 bytes, pets are 0xC0 bytes;
    /// the fields used here are at the same place in both.
    /// </summary>
    public class TotAnimal
    {
        public const int AnimalOffset = 0x54A4;
        public const int AnimalSize = 0x94;
        public const int AnimalCount = 24;
        public const int PetOffset = 0x7068;
        public const int PetSize = 0xC0;
        public const int PetCount = 12;

        public const byte PassedAway = 0xFF;
        public const byte StateInUse = 4;
        public const int MaxAffection = 1000;
        public const int MaxNameLength = 13;

        // Pet level is derived from XP: level N starts at PetLevelXP[N - 1].
        public static readonly int[] PetLevelXP = { 0, 200, 500, 1000, 1500, 2000, 3000, 5000, 7000, 10000 };

        private readonly int offset;
        public bool IsPet { get; private set; }
        public int Slot { get; private set; }

        public TotAnimal(bool isPet, int slot)
        {
            IsPet = isPet;
            Slot = slot;
            offset = isPet ? PetOffset + slot * PetSize : AnimalOffset + slot * AnimalSize;
        }

        private static byte[] data
        {
            get
            {
                return TotSave.SaveData;
            }
        }

        private ushort readU16(int localOffset)
        {
            return BitConverter.ToUInt16(data, offset + localOffset);
        }

        private void writeU16(int localOffset, ushort value)
        {
            data[offset + localOffset] = (byte)(value & 0xFF);
            data[offset + localOffset + 1] = (byte)((value >> 8) & 0xFF);
        }

        public byte Species
        {
            get
            {
                return data[offset + 0x00];
            }
        }
        public byte State
        {
            get
            {
                return data[offset + 0x01];
            }
        }
        /// <summary>
        /// True if this slot holds an animal. Barn animals use the state byte;
        /// pets have no known state byte, so a pet is used when it has a name.
        /// </summary>
        public bool IsUsed
        {
            get
            {
                if (IsPet)
                    return Name.Length > 0;
                return State == StateInUse;
            }
        }
        public ushort Affection
        {
            get
            {
                return readU16(0x02);
            }
            set
            {
                writeU16(0x02, value);
            }
        }
        public string Name
        {
            get
            {
                return TotSave.ReadString(data, offset + 0x40, MaxNameLength);
            }
            set
            {
                TotSave.WriteString(data, offset + 0x40, MaxNameLength, value);
            }
        }
        public ushort BirthYear
        {
            get
            {
                return readU16(0x5A);
            }
        }
        public byte BirthSeason
        {
            get
            {
                return data[offset + 0x5C];
            }
        }
        public byte BirthDay
        {
            get
            {
                return data[offset + 0x5D];
            }
        }
        public byte FestivalWins
        {
            get
            {
                return data[offset + 0x6E];
            }
            set
            {
                data[offset + 0x6E] = value;
            }
        }
        /// <summary>
        /// Barn animals only.
        /// </summary>
        public ushort Personality
        {
            get
            {
                return readU16(0x70);
            }
            set
            {
                writeU16(0x70, value);
            }
        }
        /// <summary>
        /// Pets only.
        /// </summary>
        public ushort XP
        {
            get
            {
                return readU16(0x74);
            }
            set
            {
                writeU16(0x74, value);
            }
        }

        public string Birthday
        {
            get
            {
                string season = BirthSeason < TotData.SeasonList.Length ?
                    TotData.SeasonList[BirthSeason] : "?";
                return String.Format("{0} {1}, Year {2}", season, BirthDay, BirthYear);
            }
        }

        public static int GetPetLevel(int xp)
        {
            int level = 1;
            for (int i = 0; i < PetLevelXP.Length; i++)
                if (xp >= PetLevelXP[i]) level = i + 1;
            return level;
        }
    }
}
