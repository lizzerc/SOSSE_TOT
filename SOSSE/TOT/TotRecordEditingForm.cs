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
    /// Records screens: festival and activity counters, count shipped per town, times fished, harvest details
    /// and per-item harvest/produce counts (all u32).
    /// </summary>
    public partial class TotRecordEditingForm : Form
    {
        private const int produceCountOffset = 0x2F6B4;

        private class Record
        {
            public string Name;
            public int Offset;
            // Records made of several u32 parts show their sum and can't be edited.
            public int Parts = 1;
            // Records that are the sum of other records on the same tab; can't be edited.
            public int[] SumOf;
            // Records whose meaning isn't confirmed in game; shown but can't be edited.
            public bool ReadOnly;
            // Stored as a u16 instead of a u32.
            public bool U16;

            public bool IsSum
            {
                get
                {
                    return Parts > 1 || SumOf != null;
                }
            }

            public bool Locked
            {
                get
                {
                    return IsSum || ReadOnly;
                }
            }
        }
        private static readonly Record[] festivalRecords = {
            new Record { Name = "Festivals entered", Offset = 0x2F048 },
            new Record { Name = "Festival victories", Offset = 0x2F04C },
            new Record { Name = "Harvest Festival victories", Offset = 0x2F054 },
            new Record { Name = "Animal Festival victories", Offset = 0x2F05C },
            new Record { Name = "Pet Promenade victories", Offset = 0x2F064 },
            new Record { Name = "Cooking Festival victories", Offset = 0x2F068 },
            new Record { Name = "Fashion Festival victories", Offset = 0x2F070 }
        };
        // Each town's count shipped is the sum of 6 parts; what each part counts is unknown.
        private static readonly Record[] shippingRecords = {
            new Record { Name = "Westown", Offset = 0x2F100, Parts = 6 },
            new Record { Name = "Tsuyukusa", Offset = 0x2F118, Parts = 6 },
            new Record { Name = "Lulukoko", Offset = 0x2F130, Parts = 6 }
        };
        // Times Fished on the Records screen is the sum of these two counters. Which one is the
        // fish trap is inferred from trophy counts.
        private const int rodCatchOffset = 0x2D9C8;
        private const int fishTrapOffset = 0x2DB64;
        private static readonly Record[] fishingRecords = {
            new Record { Name = "Times Fished", SumOf = new[] { rodCatchOffset, fishTrapOffset } },
            new Record { Name = "Rod catches", Offset = rodCatchOffset },
            new Record { Name = "Fish trap uses", Offset = fishTrapOffset }
        };
        // Activity counters used by trophies. "(likely)" ones match the trophy thresholds on several saves
        // but aren't tested in game.
        private static readonly Record[] counterRecords = {
            new Record { Name = "Times slept at the inn", Offset = 0x2F08C },
            new Record { Name = "Times cooked", Offset = 0x275BC },
            new Record { Name = "Loom items made", Offset = 0x2F01C },
            new Record { Name = "Seed maker items made", Offset = 0x2F020 },
            new Record { Name = "Dairy maker items made", Offset = 0x2F030 },
            new Record { Name = "Cows ever owned", Offset = 0x2F074 },
            new Record { Name = "Sheep ever owned", Offset = 0x2F078 },
            new Record { Name = "Birds ever owned", Offset = 0x2F07C },
            new Record { Name = "Rabbits ever owned", Offset = 0x2F080 },
            new Record { Name = "Alpacas and llamas ever owned", Offset = 0x2F084 },
            new Record { Name = "Cow babies born", Offset = 0x2F204 },
            new Record { Name = "Sheep babies born", Offset = 0x2F208 },
            new Record { Name = "Llama babies born", Offset = 0x2F210 },
            new Record { Name = "Rabbit babies born", Offset = 0x2F214 },
            new Record { Name = "Bird babies born", Offset = 0x2F218 },
            new Record { Name = "Mill items made", Offset = 0x2F018 },
            new Record { Name = "Jam pot items made", Offset = 0x2F024 },
            new Record { Name = "Pickle jar items made", Offset = 0x2F028 },
            new Record { Name = "Wine maker items made", Offset = 0x2F02C },
            new Record { Name = "Fertilizer maker items made", Offset = 0x2F034 },
            new Record { Name = "Feed maker items made", Offset = 0x2F038 },
            new Record { Name = "Spa baths", Offset = 0x2F088 },
            new Record { Name = "Westown restaurant meals", Offset = 0x2F090 },
            new Record { Name = "Teahouse meals", Offset = 0x2F094 },
            new Record { Name = "Seaside cafe meals", Offset = 0x2F098 },
            new Record { Name = "Part-time jobs", Offset = 0x2F044 },
            new Record { Name = "Fish species caught (likely)", Offset = 0x2F0AC },
            new Record { Name = "Value shipped to Westown (likely)", Offset = 0x2F008 },
            new Record { Name = "Value shipped to Tsuyukusa (likely)", Offset = 0x2F00C },
            new Record { Name = "Value shipped to Lulukoko (likely)", Offset = 0x2F010 },
            new Record { Name = "Times mined", Offset = 0x2F040 },
            new Record { Name = "Offerings to Dessie", Offset = 0x24372, U16 = true },
            new Record { Name = "Offerings to Witchie", Offset = 0x243CA, U16 = true },
            new Record { Name = "Offerings to Inari (likely)", Offset = 0x2378A, U16 = true },
            new Record { Name = "Unknown", Offset = 0x2F014, ReadOnly = true },
            // 0 on every save seen so far: alpaca babies born or an unused slot.
            new Record { Name = "Babies born, unknown (alpacas?)", Offset = 0x2F20C, ReadOnly = true }
        };

        // Harvest Details screen: each box is the sum of the per-item counts of some item types.
        private class HarvestCategory
        {
            public string Name;
            public int[] Types;
            public string[] Items = new string[0];
        }
        private static readonly HarvestCategory[] harvestCategories = {
            new HarvestCategory { Name = "Field Crop", Types = new[] { 0 }, Items = new[] { "Bamboo Shoot" } },
            new HarvestCategory { Name = "Tree Crop", Types = new[] { 1 } },
            new HarvestCategory { Name = "Flower", Types = new[] { 2 } },
            new HarvestCategory { Name = "Textile", Types = new[] { 3 } },
            new HarvestCategory { Name = "Grains", Types = new[] { 4 } },
            new HarvestCategory { Name = "Paddy", Types = new[] { 5 } },
            new HarvestCategory { Name = "Spices", Types = new[] { 6 } },
            new HarvestCategory { Name = "Tea", Types = new[] { 7 } },
            new HarvestCategory { Name = "Honey", Types = new[] { 8 } },
            new HarvestCategory { Name = "Mushroom", Types = new[] { 9 } },
            new HarvestCategory { Name = "Lumber", Types = new[] { 10 }, Items = new[] { "Twig", "Branch", "Black Branch" } },
            new HarvestCategory { Name = "Cultured", Types = new[] { 12, 15 } },
            new HarvestCategory { Name = "Milk", Types = new[] { 13 } },
            new HarvestCategory { Name = "Eggs", Types = new[] { 14 } },
            new HarvestCategory { Name = "Wool", Types = new[] { 17 } },
            new HarvestCategory { Name = "Alpaca Wool", Types = new[] { 18 } },
            new HarvestCategory { Name = "Rabbit Fur", Types = new[] { 19 } },
            new HarvestCategory { Name = "Llama Wool", Types = new[] { 20 } },
            new HarvestCategory { Name = "Mined", Types = new[] { 21, 11 } },
            new HarvestCategory { Name = "Other", Types = new int[0] }
        };
        private Record[] harvestDetailRecords;
        private const int wildPlantType = 16;

        private static int[] itemOffsetsOfType(int itemType)
        {
            return Enumerable.Range(0, TotData.ItemNameList.Length).Where(i => TotData.ItemType[i] == itemType)
                .Select(i => produceCountOffset + 4 * i).ToArray();
        }

        /// <summary>
        /// Total harvest count of all items of a type
        /// </summary>
        public static ulong HarvestedOfType(int itemType)
        {
            TotData.LoadItemData();
            ulong sum = 0;
            foreach (int offset in itemOffsetsOfType(itemType))
                sum += BitConverter.ToUInt32(TotSave.SaveData, offset);
            return sum;
        }
        private Record[] produceRecords;

        public TotRecordEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadItemData();

            produceRecords = new Record[TotData.ItemNameList.Length];
            for (int i = 0; i < produceRecords.Length; i++)
                produceRecords[i] = new Record { Name = TotData.ItemNameList[i], Offset = produceCountOffset + 4 * i };

            // Every item goes in the first category that lists it or its type, else in "Other".
            List<int>[] categoryOffsets = harvestCategories.Select(c => new List<int>()).ToArray();
            for (int i = 0; i < TotData.ItemNameList.Length; i++)
            {
                int category = Array.FindIndex(harvestCategories, c =>
                    c.Items.Contains(TotData.ItemNameList[i]) || c.Types.Contains(TotData.ItemType[i]));
                if (category < 0)
                    category = harvestCategories.Length - 1;
                categoryOffsets[category].Add(produceCountOffset + 4 * i);
            }
            harvestDetailRecords = harvestCategories.Select((c, i) =>
                new Record { Name = c.Name, SumOf = categoryOffsets[i].ToArray() }).ToArray();
            // What the Forager trophies count: every wild plant, including those in other boxes above.
            harvestDetailRecords = harvestDetailRecords.Concat(new[] {
                new Record { Name = "Wild plants", SumOf = itemOffsetsOfType(wildPlantType) } }).ToArray();

            addTab("Festivals", festivalRecords, 180);
            addTab("Count Shipped", shippingRecords, 180);
            addTab("Fishing", fishingRecords, 180);
            addTab("Counters", counterRecords, 260);
            addTab("Harvest Details", harvestDetailRecords, 180);
            addTab("Harvest / Produce", produceRecords, 180);
        }

        private void addTab(string title, Record[] records, int nameWidth)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Tag = records;

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.HeaderText = "Record";
            nameColumn.Width = nameWidth;
            nameColumn.ReadOnly = true;
            nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewTextBoxColumn valueColumn = new DataGridViewTextBoxColumn();
            valueColumn.HeaderText = "Count";
            valueColumn.Width = 90;
            valueColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView.Columns.Add(nameColumn);
            dataGridView.Columns.Add(valueColumn);

            foreach (Record record in records)
            {
                int row = dataGridView.Rows.Add(record.Name, readRecord(record));
                if (record.Locked)
                    TotGrid.LockRow(dataGridView.Rows[row]);
            }
            dataGridView.CellValidating += dataGridView_CellValidating;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;

            TabPage tab = new TabPage(title);
            tab.Controls.Add(dataGridView);
            recordTabControl.TabPages.Add(tab);
        }

        private static ulong readRecord(Record record)
        {
            if (record.SumOf != null)
                return (ulong)record.SumOf.Sum(offset => (long)BitConverter.ToUInt32(TotSave.SaveData, offset));
            if (record.U16)
                return BitConverter.ToUInt16(TotSave.SaveData, record.Offset);
            ulong sum = 0;
            for (int i = 0; i < record.Parts; i++)
                sum += BitConverter.ToUInt32(TotSave.SaveData, record.Offset + 4 * i);
            return sum;
        }

        // Update sums when one of their records changes
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex != 1) return;
            Record[] records = (Record[])dataGridView.Tag;
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].SumOf == null) continue;
                ulong sum = 0;
                for (int j = 0; j < records.Length; j++)
                {
                    uint value;
                    if (records[i].SumOf.Contains(records[j].Offset) && !records[j].IsSum &&
                        UInt32.TryParse(Convert.ToString(dataGridView.Rows[j].Cells[1].Value), out value))
                        sum += value;
                }
                if (Convert.ToString(dataGridView.Rows[i].Cells[1].Value) != sum.ToString())
                    dataGridView.Rows[i].Cells[1].Value = sum;
            }
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.ColumnIndex != 1 || !dataGridView.IsCurrentCellInEditMode) return;
            DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            uint max = ((Record[])dataGridView.Tag)[e.RowIndex].U16 ? UInt16.MaxValue : UInt32.MaxValue;
            uint value;
            if (!UInt32.TryParse(e.FormattedValue.ToString(), out value) || value > max)
            {
                cell.ErrorText = "Must be a valid number between 0 and " + max;
                dataGridView.CancelEdit();
            }
            else
                cell.ErrorText = null;
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveRecords()
        {
            foreach (TabPage tab in recordTabControl.TabPages)
            {
                DataGridView dataGridView = (DataGridView)tab.Controls[0];
                dataGridView.EndEdit();
                Record[] records = (Record[])dataGridView.Tag;
                for (int i = 0; i < records.Length; i++)
                {
                    if (records[i].Locked) continue;
                    uint value;
                    if (!UInt32.TryParse(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), out value))
                        continue;
                    if (records[i].U16)
                    {
                        if (value <= UInt16.MaxValue && value != BitConverter.ToUInt16(TotSave.SaveData, records[i].Offset))
                            Array.Copy(BitConverter.GetBytes((ushort)value), 0, TotSave.SaveData, records[i].Offset, 2);
                    }
                    else if (value != BitConverter.ToUInt32(TotSave.SaveData, records[i].Offset))
                        Array.Copy(BitConverter.GetBytes(value), 0, TotSave.SaveData, records[i].Offset, 4);
                }
            }
        }

        private void TotRecordEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRecords();
        }
    }
}
