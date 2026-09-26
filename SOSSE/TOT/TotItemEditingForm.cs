using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOSSE.TOT
{
    public partial class TotItemEditingForm : Form
    {
        private const int MaxQuantity = 99;

        private class ItemContainer
        {
            public string Name { get; set; }
            public int Offset { get; set; }
            public int Count { get; set; }
            public bool ReadOnly { get; set; }
            public TotItem[] Items { get; set; }
            public byte[] EmptySlot { get; set; }
            public DataGridView ContainerDataGridView { get; set; }
        }
        private ItemContainer[] containers;

        private enum Column { Slot, Item, Quantity, Property1, Property2, Property3, Property4, Stars, Golden, QuickList };

        // Item names shown in the item column, "None" first, then sorted by name.
        // Farm circle storage: 999 x (u16 farm circle ID, u16 count in storage), 0xFFFF = empty.
        // Only the count of circles already stored can be changed.
        private const int circleOffset = 0x26550;
        private const int circleCount = 999;
        private const ushort emptyCircle = 0xFFFF;
        private DataGridView circleDataGridView;
        private DataGridViewTextBoxColumn circleCountColumn;
        // Save offset of each circle row's count
        private List<int> circleRowOffsets = new List<int>();

        private string[] itemChoices;
        private Dictionary<string, ushort> itemIndexByName;

        public TotItemEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadItemData();

            // The unknown container is shown, but not editable.
            containers = new ItemContainer[] {
                new ItemContainer { Name = "Bag", Offset = 0x1E74C, Count = 100 },
                new ItemContainer { Name = "Storage Box", Offset = 0x275C4, Count = 900 },
                new ItemContainer { Name = "Feed Bin", Offset = 0x3351C, Count = 200 },
                new ItemContainer { Name = "Shipping Bin", Offset = 0x03898, Count = 300 },
                new ItemContainer { Name = "Unknown (100)", Offset = 0x36BC4, Count = 100, ReadOnly = true }
            };

            LoadItemData();
            loadFarmCircles();
        }

        private void loadFarmCircles()
        {
            TotData.LoadFarmCircleData();
            circleDataGridView = new DataGridView();
            circleDataGridView.Dock = DockStyle.Fill;
            circleDataGridView.AllowUserToAddRows = false;
            circleDataGridView.AllowUserToDeleteRows = false;
            circleDataGridView.AllowUserToResizeRows = false;
            circleDataGridView.RowHeadersVisible = false;
            circleDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            circleDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TotGrid.AddTextColumn(circleDataGridView, "Farm circle", 200, true);
            circleCountColumn = TotGrid.AddTextColumn(circleDataGridView, "In storage", 80, false);
            circleDataGridView.CellValidating += circleDataGridView_CellValidating;

            for (int i = 0; i < circleCount; i++)
            {
                int offset = circleOffset + 4 * i;
                ushort id = BitConverter.ToUInt16(TotSave.SaveData, offset);
                if (id == emptyCircle) continue;
                string name = id < TotData.FarmCircleNameList.Length ? TotData.FarmCircleNameList[id] : "#" + id;
                circleDataGridView.Rows.Add(name, BitConverter.ToUInt16(TotSave.SaveData, offset + 2));
                circleRowOffsets.Add(offset + 2);
            }

            TabPage tab = new TabPage("Farm Circles");
            tab.Controls.Add(circleDataGridView);
            itemTabControl.TabPages.Add(tab);
        }

        private void circleDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            TotGrid.ValidateRange(circleDataGridView, e, circleCountColumn.Index, 1, UInt16.MaxValue);
        }

        private void saveFarmCircles()
        {
            circleDataGridView.EndEdit();
            for (int i = 0; i < circleRowOffsets.Count; i++)
            {
                int count;
                if (!TotGrid.TryGetInt(circleDataGridView.Rows[i].Cells[circleCountColumn.Index], out count)) continue;
                if (count != BitConverter.ToUInt16(TotSave.SaveData, circleRowOffsets[i]))
                    Array.Copy(BitConverter.GetBytes((ushort)count), 0, TotSave.SaveData, circleRowOffsets[i], 2);
            }
        }

        private void LoadItemData()
        {
            itemIndexByName = new Dictionary<string, ushort>();
            itemIndexByName["None"] = TotItem.Empty;
            for (int i = 0; i < TotData.ItemNameList.Length; i++)
                itemIndexByName[TotData.ItemNameList[i]] = (ushort)i;

            foreach (ItemContainer container in containers)
                loadItem(container);

            // Items with an ID missing from the list keep their "#ID" name.
            List<string> names = TotData.ItemNameList.ToList();
            foreach (ItemContainer container in containers)
                foreach (TotItem item in container.Items)
                    if (!item.IsEmpty && !TotData.IsValidItem(item.Index) && !names.Contains(item.GetItemName()))
                    {
                        names.Add(item.GetItemName());
                        itemIndexByName[item.GetItemName()] = item.Index;
                    }
            names.Sort(StringComparer.OrdinalIgnoreCase);
            names.Insert(0, "None");
            itemChoices = names.ToArray();

            foreach (ItemContainer container in containers)
            {
                TabPage tab = new TabPage(container.Name);
                DataGridView dataGridView = createDataGridView(container);
                tab.Controls.Add(dataGridView);
                itemTabControl.TabPages.Add(tab);
                container.ContainerDataGridView = dataGridView;
                dataGridView.RowCount = container.Count;
            }
        }

        // Load items from save data. Slots keep their order.
        private void loadItem(ItemContainer container)
        {
            TotItem[] items = new TotItem[container.Count];
            int localOffset = container.Offset;
            for (int i = 0; i < container.Count; i++)
            {
                byte[] itemBytes = new byte[TotItem.Size];
                Array.Copy(TotSave.SaveData, localOffset, itemBytes, 0, TotItem.Size);
                items[i] = new TotItem(itemBytes);
                if (items[i].IsEmpty && container.EmptySlot == null)
                    container.EmptySlot = (byte[])itemBytes.Clone();
                localOffset += TotItem.Size;
            }
            container.Items = items;
        }

        private DataGridView createDataGridView(ItemContainer container)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.VirtualMode = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.ReadOnly = container.ReadOnly;
            dataGridView.Tag = container;

            dataGridView.Columns.Add(textColumn("Slot", 40, true));
            DataGridViewComboBoxColumn itemColumn = new DataGridViewComboBoxColumn();
            itemColumn.HeaderText = "Item";
            itemColumn.Width = 180;
            itemColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            itemColumn.Items.AddRange(itemChoices);
            dataGridView.Columns.Add(itemColumn);
            dataGridView.Columns.Add(textColumn("Qty", 45, false));
            for (int i = 1; i <= 4; i++)
                dataGridView.Columns.Add(textColumn("Property " + i, 65, false));
            dataGridView.Columns.Add(textColumn("Stars", 45, true));
            dataGridView.Columns.Add(textColumn("Golden", 50, true));
            dataGridView.Columns.Add(textColumn("Quick list", 60, true));

            dataGridView.CellValueNeeded += dataGridView_CellValueNeeded;
            dataGridView.CellValuePushed += dataGridView_CellValuePushed;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.CellValidating += dataGridView_CellValidating;
            dataGridView.CellClick += dataGridView_CellClick;
            dataGridView.CurrentCellDirtyStateChanged += dataGridView_CurrentCellDirtyStateChanged;
            dataGridView.DataError += dataGridView_DataError;
            return dataGridView;
        }

        private static DataGridViewTextBoxColumn textColumn(string header, int width, bool readOnly)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = header;
            column.Width = width;
            column.ReadOnly = readOnly;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            return column;
        }

        /// <summary>
        /// Crops and similar items have 4 property bars. Other items store one quality value 4 times,
        /// except seeds, which carry the values of their crop.
        /// </summary>
        private static bool hasPropertyBars(TotItem item)
        {
            if (TotData.HasPropertyBars(item.Index)) return true;
            for (int i = 1; i < 4; i++)
                if (item.GetProperty(i) != item.GetProperty(0)) return true;
            return false;
        }

        private static ItemContainer getContainer(object sender)
        {
            return (ItemContainer)((DataGridView)sender).Tag;
        }

        // Display item data when needed
        private void dataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            TotItem item = getContainer(sender).Items[e.RowIndex];
            switch ((Column)e.ColumnIndex)
            {
                case Column.Slot:
                    e.Value = e.RowIndex + 1;
                    break;
                case Column.Item:
                    e.Value = item.GetItemName();
                    break;
                case Column.Quantity:
                    e.Value = item.IsEmpty ? "" : item.Quantity.ToString();
                    break;
                case Column.Property1:
                case Column.Property2:
                case Column.Property3:
                case Column.Property4:
                    int property = e.ColumnIndex - (int)Column.Property1;
                    if (item.IsEmpty || (property > 0 && !hasPropertyBars(item)))
                        e.Value = "";
                    else
                        e.Value = item.GetProperty(property);
                    break;
                case Column.Stars:
                    e.Value = item.IsEmpty ? "" : item.Stars.ToString("0.#");
                    break;
                case Column.Golden:
                    e.Value = item.IsGolden ? "Yes" : "";
                    break;
                case Column.QuickList:
                    if (item.QuickList == 0)
                        e.Value = "";
                    else
                        e.Value = item.QuickList == 1 ? "Yes" : item.QuickList.ToString();
                    break;
            }
        }

        // Push item changes back to the TotItem array.
        // Quantity and properties were handled in CellValidating event.
        private void dataGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if ((Column)e.ColumnIndex != Column.Item || e.Value == null) return;
            ItemContainer container = getContainer(sender);
            ushort itemIndex;
            if (!itemIndexByName.TryGetValue(e.Value.ToString(), out itemIndex)) return;

            TotItem item = container.Items[e.RowIndex];
            if (itemIndex == item.Index) return;
            if (itemIndex == TotItem.Empty && container.EmptySlot != null)
                container.Items[e.RowIndex] = new TotItem((byte[])container.EmptySlot.Clone());
            else
                item.SetItem(itemIndex);
            ((DataGridView)sender).InvalidateRow(e.RowIndex); // Repaint
        }

        // Grayed out cells that can't be edited.
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            ItemContainer container = getContainer(sender);
            TotItem item = container.Items[e.RowIndex];
            bool readOnly;
            switch ((Column)e.ColumnIndex)
            {
                case Column.Item:
                    readOnly = container.ReadOnly;
                    break;
                case Column.Quantity:
                case Column.Property1:
                    readOnly = container.ReadOnly || item.IsEmpty;
                    break;
                case Column.Property2:
                case Column.Property3:
                case Column.Property4:
                    readOnly = container.ReadOnly || item.IsEmpty || !hasPropertyBars(item);
                    break;
                default:
                    return;
            }
            e.CellStyle.BackColor = readOnly ? Color.LightGray : dataGridView.DefaultCellStyle.BackColor;
            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = readOnly;
        }

        // Validate and push new values into the TotItem array.
        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            ItemContainer container = getContainer(sender);
            DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (container.ReadOnly || cell.ReadOnly || !dataGridView.IsCurrentCellInEditMode)
                return;
            TotItem item = container.Items[e.RowIndex];
            switch ((Column)e.ColumnIndex)
            {
                case Column.Quantity:
                    byte quantity;
                    bool isValidQuantity = Byte.TryParse(e.FormattedValue.ToString(), out quantity);
                    if (!isValidQuantity || quantity < 1 || quantity > MaxQuantity)
                    {
                        cell.ErrorText = "Must be a valid number between 1 and " + MaxQuantity;
                        dataGridView.CancelEdit();
                    }
                    else
                    {
                        cell.ErrorText = null;
                        item.Quantity = quantity;
                    }
                    break;
                case Column.Property1:
                case Column.Property2:
                case Column.Property3:
                case Column.Property4:
                    int value;
                    bool isValidValue = Int32.TryParse(e.FormattedValue.ToString(), out value);
                    if (!isValidValue || value < 0 || value > TotItem.MaxProperty)
                    {
                        cell.ErrorText = "Must be a valid number between 0 and " + TotItem.MaxProperty + " (300 = 100%)";
                        dataGridView.CancelEdit();
                    }
                    else
                    {
                        cell.ErrorText = null;
                        if (hasPropertyBars(item))
                            item.SetProperty(e.ColumnIndex - (int)Column.Property1, value);
                        else
                            item.SetQuality(value);
                        dataGridView.InvalidateRow(e.RowIndex);
                    }
                    break;
            }
        }

        // Show dropdown list right after clicking into ComboBox cells.
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != (int)Column.Item || e.RowIndex < 0) return;
            ComboBox cb = ((DataGridView)sender).EditingControl as ComboBox;
            if (cb != null) cb.DroppedDown = true;
        }

        // Commit cell value change for the item ComboBox column.
        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.IsCurrentCellDirty &&
                dataGridView.CurrentCell.ColumnIndex == (int)Column.Item)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // Save item data for specified container
        private void saveItems(ItemContainer container)
        {
            if (container.ReadOnly) return;
            for (int i = 0; i < container.Count; i++)
            {
                Array.Copy(container.Items[i].ToArray(), 0, TotSave.SaveData,
                    container.Offset + TotItem.Size * i, TotItem.Size);
            }
        }

        private ItemContainer selectedContainer()
        {
            // The farm circle tab comes after the item containers.
            int index = itemTabControl.SelectedIndex;
            return index < containers.Length ? containers[index] : null;
        }

        // Increase quantity of all items to 99.
        private void itemx99Button_Click(object sender, EventArgs e)
        {
            ItemContainer container = selectedContainer();
            if (container == null || container.ReadOnly) return;
            for (int i = 0; i < container.Count; i++)
            {
                if (container.Items[i].IsEmpty) continue;
                container.Items[i].Quantity = MaxQuantity;
            }
            container.ContainerDataGridView.Invalidate();
        }

        // Change all properties of all items to maximum value.
        private void maxQualityButton_Click(object sender, EventArgs e)
        {
            ItemContainer container = selectedContainer();
            if (container == null || container.ReadOnly) return;
            for (int i = 0; i < container.Count; i++)
            {
                if (container.Items[i].IsEmpty) continue;
                container.Items[i].SetQuality(TotItem.MaxProperty);
            }
            container.ContainerDataGridView.Invalidate();
        }

        private void itemTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemContainer container = selectedContainer();
            bool editable = container != null && !container.ReadOnly;
            itemx99Button.Enabled = editable;
            maxQualityButton.Enabled = editable;
        }

        private void TotItemEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ItemContainer container in containers)
            {
                if (container.ContainerDataGridView != null)
                    container.ContainerDataGridView.EndEdit();
                saveItems(container);
            }
            saveFarmCircles();
        }
    }
}
