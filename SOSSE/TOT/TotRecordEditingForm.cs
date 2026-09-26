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
    /// Records screens: festival counters, count shipped per town and per-item harvest/produce counts (all u32).
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
        private Record[] produceRecords;

        public TotRecordEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadItemData();

            produceRecords = new Record[TotData.ItemNameList.Length];
            for (int i = 0; i < produceRecords.Length; i++)
                produceRecords[i] = new Record { Name = TotData.ItemNameList[i], Offset = produceCountOffset + 4 * i };

            addTab("Festivals", festivalRecords, 180);
            addTab("Count Shipped", shippingRecords, 180);
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
                if (record.Parts > 1)
                    TotGrid.LockRow(dataGridView.Rows[row]);
            }
            dataGridView.CellValidating += dataGridView_CellValidating;

            TabPage tab = new TabPage(title);
            tab.Controls.Add(dataGridView);
            recordTabControl.TabPages.Add(tab);
        }

        private static ulong readRecord(Record record)
        {
            ulong sum = 0;
            for (int i = 0; i < record.Parts; i++)
                sum += BitConverter.ToUInt32(TotSave.SaveData, record.Offset + 4 * i);
            return sum;
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.ColumnIndex != 1 || !dataGridView.IsCurrentCellInEditMode) return;
            DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            uint value;
            if (!UInt32.TryParse(e.FormattedValue.ToString(), out value))
            {
                cell.ErrorText = "Must be a valid number between 0 and " + UInt32.MaxValue;
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
                    if (records[i].Parts > 1) continue;
                    uint value;
                    if (!UInt32.TryParse(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), out value))
                        continue;
                    if (value != BitConverter.ToUInt32(TotSave.SaveData, records[i].Offset))
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
