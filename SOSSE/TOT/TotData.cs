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

        // Wardrobe items in AvatarData order: clothes, then hats, then glasses.
        public static string[] ClothesNameList;
        public static string[] HatNameList;
        public static string[] GlassesNameList;
        public static string[] RecipeNameList;
        public static string[] TrophyNameList;
        // Save offset and target of the counter behind each trophy, where it is confirmed or likely; -1 otherwise.
        public static int[] TrophyCounterOffset;
        public static int[] TrophyCounterTarget;
        // Whether the counter of a trophy is a u16 instead of a u32.
        public static bool[] TrophyCounterIsU16;
        // Item type whose total harvest count is shown as the counter of a trophy, or -1. Display only.
        public static int[] TrophyCounterItemType;
        // Indexed by farm circle (PanelData) ID.
        public static string[] FarmCircleNameList;
        // Farm circles in the circle shop order, used by the times crafted list.
        public static string[] FarmCircleCraftedNameList;
        public static string[] WildAnimalNameList;

        public static readonly string[] RankList = { "None", "Bronze", "Silver", "Gold", "Rainbow" };
        public static readonly string[] SeasonList = { "Spring", "Summer", "Fall", "Winter" };
        public static readonly string[] PetAbilityList = { "Herding / Recovery", "Finding Materials",
            "Finding Fish", "Finding Ore", "Finding Plants", "Finding Misc." };

        // NPC friendship records: 17 marriage candidates (0x60 bytes each), then 32 others (0x58 bytes each).
        public static readonly string[] NPCLoverNameList = { "Wayne", "Ford", "Yuzuki", "Hinata", "Ludus",
            "Lisette", "Komari", "Kasumi", "Iluka", "Siluka", "Inari", "Woofio", "Stephanie",
            "(Unused 3)", "(Unused 4)", "(Unused 5)", "(Unused 6)" };
        public static readonly string[] NPCOtherNameList = { "Marco", "Brad", "Carrie", "Megan", "Hector",
            "Colin", "Frank", "Miranda", "Noel", "Umekichi", "Omiyo", "Moriya", "Sumomo", "Ginjiro", "Ittetsu",
            "Shizu", "Tatsumi", "Yaichi", "Tototara", "Zahau", "Caolila", "Schalk", "Alma", "Mithra", "Lotus",
            "Haulani", "Tigre", "Dessie", "Witchie", "Mother", "Sister", "Child" };

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

        public static void LoadWardrobeData()
        {
            if (ClothesNameList != null) return;
            ClothesNameList = loadLines("TotClothes.txt");
            HatNameList = loadLines("TotHats.txt");
            GlassesNameList = loadLines("TotGlasses.txt");
        }

        public static void LoadRecipeData()
        {
            if (RecipeNameList == null)
                RecipeNameList = loadLines("TotRecipes.txt");
        }

        public static void LoadTrophyData()
        {
            if (TrophyNameList != null) return;
            // Each line: name, then optionally a tab, the counter offset (hex) or "type" and an item type
            // (total harvested of that type), a tab and the target, then optionally a tab and "u16".
            string[] lines = loadLines("TotTrophies.txt");
            TrophyNameList = new string[lines.Length];
            TrophyCounterOffset = new int[lines.Length];
            TrophyCounterTarget = new int[lines.Length];
            TrophyCounterItemType = new int[lines.Length];
            TrophyCounterIsU16 = new bool[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split('\t');
                TrophyNameList[i] = fields[0];
                bool isItemType = fields.Length > 2 && fields[1].StartsWith("type");
                TrophyCounterOffset[i] = fields.Length > 2 && !isItemType ? Convert.ToInt32(fields[1], 16) : -1;
                TrophyCounterItemType[i] = isItemType ? Int32.Parse(fields[1].Substring(4)) : -1;
                TrophyCounterTarget[i] = fields.Length > 2 ? Int32.Parse(fields[2]) : -1;
                TrophyCounterIsU16[i] = fields.Length > 3 && fields[3] == "u16";
            }
        }

        public static void LoadFarmCircleData()
        {
            if (FarmCircleNameList == null)
                FarmCircleNameList = loadLines("TotFarmCircles.txt");
            if (FarmCircleCraftedNameList == null)
                FarmCircleCraftedNameList = loadLines("TotFarmCirclesCrafted.txt");
        }

        public static void LoadWildAnimalData()
        {
            if (WildAnimalNameList == null)
                WildAnimalNameList = loadLines("TotWildAnimals.txt");
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
