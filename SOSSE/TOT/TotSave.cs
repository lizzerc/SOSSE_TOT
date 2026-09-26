using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DSDecmp;
using DSDecmp.Formats.Nitro;

namespace SOSSE.TOT
{
    /// <summary>
    /// Load and save Story of Seasons: Trio of Towns save files (save0.bin).
    /// File layout: 0xF8-byte preview header + Nintendo LZ11 data. No checksum.
    /// </summary>
    public static class TotSave
    {
        public const int SaveLength = 0x38340;
        public const int HeaderLength = 0xF8;
        private const int headerSizeOffset = 0x84;

        // Save-select preview in the header, refreshed from the save data on writing.
        // 0x1A is the name the spouse calls the player; 0x34 is the farm name.
        private const int headerDateOffset = 0x88;
        private const int headerPlayerNameOffset = 0x94;
        private const int headerFarmNameOffset = 0xAE;
        private const int dateOffset = 0x4B68;
        private const int dateLength = 8;

        public const int PlayerNameOffset = 0x00;
        public const int NicknameOffset = 0x1A;
        public const int FarmNameOffset = 0x34;
        public const int MaxNameLength = 13;
        // Longest name the game lets the player enter
        public const int MaxNameInput = 6;

        // Save
        public static byte[] Header;
        public static byte[] SaveData;

        /// <summary>
        /// Check whether a file is a Trio of Towns save, either compressed
        /// (LZ11 at 0xF8, decompressed size 0x38340 in the header) or already decompressed.
        /// </summary>
        public static bool IsTotSave(string path)
        {
            long length = (new FileInfo(path)).Length;
            if (length == SaveLength) return true;
            if (length <= HeaderLength + 4) return false;

            byte[] header = new byte[HeaderLength + 1];
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                if (fs.Read(header, 0, header.Length) != header.Length) return false;
            }
            return header[HeaderLength] == 0x11 &&
                BitConverter.ToUInt32(header, headerSizeOffset) == SaveLength;
        }

        /// <summary>
        /// Load a save file. Returns false if the file is not a valid Trio of Towns save.
        /// </summary>
        public static bool Load(string path)
        {
            byte[] file = File.ReadAllBytes(path);

            // Open a decompressed save file, for testing purpose
            if (file.Length == SaveLength)
            {
                Header = null;
                SaveData = file;
                return true;
            }

            if (file.Length <= HeaderLength + 4 || file[HeaderLength] != 0x11)
                return false;

            byte[] header = new byte[HeaderLength];
            Array.Copy(file, header, HeaderLength);
            byte[] decompressed = decompress(file, HeaderLength);
            if (decompressed == null || decompressed.Length != SaveLength)
                return false;

            Header = header;
            SaveData = decompressed;
            return true;
        }

        /// <summary>
        /// Write the save to a file. If the file exists, it is copied to "path.bak" first,
        /// unless a backup already exists (the first backup is kept as the original).
        /// </summary>
        public static void Save(string path)
        {
            byte[] output;
            if (Header == null)
            {
                // For testing purpose
                output = SaveData;
            }
            else
            {
                byte[] compressed = compress(SaveData);

                // Verify by decompressing the output
                byte[] check = decompress(compressed, 0);
                if (check == null || !check.SequenceEqual(SaveData))
                    throw new InvalidDataException("Compressed data does not decompress back to the save data.");

                byte[] header = (byte[])Header.Clone();
                Array.Copy(SaveData, dateOffset, header, headerDateOffset, dateLength);
                Array.Copy(SaveData, PlayerNameOffset, header, headerPlayerNameOffset, MaxNameLength * 2);
                Array.Copy(SaveData, FarmNameOffset, header, headerFarmNameOffset, MaxNameLength * 2);

                output = new byte[header.Length + compressed.Length];
                Array.Copy(header, output, header.Length);
                Array.Copy(compressed, 0, output, header.Length, compressed.Length);
            }

            string backupPath = path + ".bak";
            if (File.Exists(path) && !File.Exists(backupPath))
                File.Copy(path, backupPath);
            File.WriteAllBytes(path, output);
        }

        private static byte[] decompress(byte[] data, int offset)
        {
            LZ11 lz11 = new LZ11();
            using (MemoryStream inStream = new MemoryStream(data, offset, data.Length - offset))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    try
                    {
                        lz11.Decompress(inStream, data.Length - offset, outStream);
                    }
                    catch (TooMuchInputException)
                    {
                        // Trailing padding after the compressed data; the output is complete.
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    return outStream.ToArray();
                }
            }
        }

        private static byte[] compress(byte[] data)
        {
            LZ11 lz11 = new LZ11();
            using (MemoryStream inStream = new MemoryStream(data))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    lz11.Compress(inStream, data.Length, outStream);
                    return outStream.ToArray();
                }
            }
        }

        #region Text
        /// <summary>
        /// Read a UTF-16LE string of up to maxLength characters, ending at the first 00 00.
        /// </summary>
        public static string ReadString(byte[] data, int offset, int maxLength)
        {
            int length = 0;
            while (length < maxLength * 2 && BitConverter.ToUInt16(data, offset + length) != 0)
                length += 2;
            return Encoding.Unicode.GetString(data, offset, length);
        }

        /// <summary>
        /// Write a UTF-16LE string into a field of maxLength characters, zero-filling the rest.
        /// </summary>
        public static void WriteString(byte[] data, int offset, int maxLength, string value)
        {
            if (value.Length > maxLength) value = value.Substring(0, maxLength);
            Array.Clear(data, offset, maxLength * 2);
            Encoding.Unicode.GetBytes(value, 0, value.Length, data, offset);
        }
        #endregion
    }
}
