using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SOSSE.TOT
{
    /// <summary>
    /// Name lists for Trio of Towns, loaded from resources embedded in the TOT folder.
    /// </summary>
    public static class TotData
    {
        public const int RankCategoryCount = 92;

        // Item data from TotItems.csv (id, hex, name, rank_category, item_type)
        public static string[] ItemNameList;
        public static int[] ItemRankCategory;
        public static int[] ItemType;
        public static string[] RankCategoryName;

        public static string[] AnimalNameList;
        public static string[] AnimalPersonalityList;

        public static readonly string[] RankList = { "None", "Bronze", "Silver", "Gold", "Rainbow" };
        public static readonly string[] SeasonList = { "Spring", "Summer", "Fall", "Winter" };

        // Item types with 4 property bars; other items store one quality value 4 times.
        private static readonly int[] propertyItemTypes = { 0, 1, 2, 3, 4, 5, 6, 7, 9 };

        private static string[] loadLines(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("SOSSE.TOT.Resources." + name))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd().Replace("\r", "").Split(new[] { '\n' });
                }
            }
        }

        public static void LoadItemData()
        {
            if (ItemNameList != null) return;

            string[] lines = loadLines("TotItems.csv");
            List<string> names = new List<string>();
            List<int> categories = new List<int>();
            List<int> types = new List<int>();
            HashSet<string> usedNames = new HashSet<string>();
            // Skip the header line; ids are consecutive from 0.
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Length == 0) continue;
                string[] fields = lines[i].Split(',');
                string name = fields[2];
                // Names are used to pick items in combo boxes, so make duplicates unique.
                if (!usedNames.Add(name))
                {
                    name = name + " (#" + fields[0] + ")";
                    usedNames.Add(name);
                }
                names.Add(name);
                categories.Add(Int32.Parse(fields[3]));
                types.Add(Int32.Parse(fields[4]));
            }
            ItemNameList = names.ToArray();
            ItemRankCategory = categories.ToArray();
            ItemType = types.ToArray();

            // Name each rank category after the first item in it.
            RankCategoryName = new string[RankCategoryCount];
            for (int i = 0; i < ItemNameList.Length; i++)
            {
                int category = ItemRankCategory[i];
                if (category >= 0 && category < RankCategoryCount && RankCategoryName[category] == null)
                    RankCategoryName[category] = ItemNameList[i];
            }
            for (int i = 0; i < RankCategoryCount; i++)
            {
                if (RankCategoryName[i] == null)
                    RankCategoryName[i] = "Category " + i;
            }
        }

        public static void LoadAnimalData()
        {
            if (AnimalNameList == null)
                AnimalNameList = loadLines("TotAnimalName.txt");
            if (AnimalPersonalityList == null)
                AnimalPersonalityList = loadLines("TotPersonality.txt");
        }

        public static bool IsValidItem(int index)
        {
            return index >= 0 && index < ItemNameList.Length;
        }

        /// <summary>
        /// True if the item has 4 separate property bars (crops, flowers, etc.).
        /// </summary>
        public static bool HasPropertyBars(int index)
        {
            return IsValidItem(index) && propertyItemTypes.Contains(ItemType[index]);
        }

        public static string GetAnimalName(byte species)
        {
            if (species == 0xFF) return "(Passed away)";
            if (species < AnimalNameList.Length && AnimalNameList[species].Length > 0)
                return AnimalNameList[species];
            return "#" + species;
        }
    }
}
