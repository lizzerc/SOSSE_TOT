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
    /// Barn animals or pets.
    /// </summary>
    public partial class TotAnimalEditingForm : Form
    {
        /// <summary>
        /// One grid column. Editable number columns have Get/Set and a range;
        /// list columns have a List; read-only columns only Get or Derive.
        /// </summary>
        private class AnimalColumn
        {
            public string Header;
            public int Width;
            public Func<TotAnimal, object> Get;
            public Action<TotAnimal, int> Set;
            public int Min;
            public int Max;
            public string[] List;
            // Read-only column worked out from the value in the column before it
            public Func<int, object> Derive;
        }

        private List<TotAnimal> animals;
        private AnimalColumn[] columns;
        private int nameColumn;
        private int affectionColumn;

        public TotAnimalEditingForm(bool isPet)
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadAnimalData();
            this.Text = isPet ? "Pets" : "Animals";

            List<AnimalColumn> list = new List<AnimalColumn>();
            list.Add(new AnimalColumn { Header = "Slot", Width = 40, Get = a => a.Slot + 1 });
            list.Add(new AnimalColumn { Header = "Species", Width = 110, Get = a => TotData.GetAnimalName(a.Species) });
            nameColumn = list.Count;
            list.Add(new AnimalColumn { Header = "Name", Width = 70, Get = a => a.Name });
            affectionColumn = list.Count;
            list.Add(new AnimalColumn { Header = "Affection", Width = 65, Get = a => a.Affection,
                Set = (a, v) => a.Affection = (ushort)v, Min = 0, Max = TotAnimal.MaxAffection });
            list.Add(new AnimalColumn { Header = "Hearts", Width = 50, Derive = v => (v / 100.0).ToString("0.#") });
            list.Add(new AnimalColumn { Header = "Stress %", Width = 60, Get = a => a.Stress,
                Set = (a, v) => a.Stress = (ushort)v, Min = 0, Max = TotAnimal.MaxStress });
            list.Add(new AnimalColumn { Header = "Festival Wins", Width = 60, Get = a => a.FestivalWins,
                Set = (a, v) => a.FestivalWins = (byte)v, Min = 0, Max = Byte.MaxValue });
            if (isPet)
            {
                list.Add(new AnimalColumn { Header = "Ability", Width = 130, Get = a => (int)a.Ability,
                    Set = (a, v) => a.Ability = (byte)v, List = TotData.PetAbilityList });
                list.Add(new AnimalColumn { Header = "XP", Width = 60, Get = a => a.XP,
                    Set = (a, v) => a.XP = (ushort)v, Min = 0, Max = UInt16.MaxValue });
                list.Add(new AnimalColumn { Header = "Level", Width = 45, Derive = v => TotAnimal.GetPetLevel(v) });
            }
            else
            {
                list.Add(new AnimalColumn { Header = "Personality", Width = 80, Get = a => (int)a.Personality,
                    Set = (a, v) => a.Personality = (ushort)v, List = TotData.AnimalPersonalityList });
                list.Add(new AnimalColumn { Header = "Coat", Width = 50, Get = a => a.Coat,
                    Set = (a, v) => a.Coat = (ushort)v, Min = 0, Max = TotAnimal.MaxGrade });
                list.Add(new AnimalColumn { Header = "Grade", Width = 45, Derive = v => TotAnimal.GetCoatGrade(v) });
                list.Add(new AnimalColumn { Header = "Byprod. Amt Factor", Width = 70, Get = a => a.ByproductFactor,
                    Set = (a, v) => a.ByproductFactor = (ushort)v, Min = 0, Max = TotAnimal.MaxGrade });
                list.Add(new AnimalColumn { Header = "Byprod. Amt", Width = 55, Derive = v => TotAnimal.GetByproductAmount(v) });
                list.Add(new AnimalColumn { Header = "Byprod. Level", Width = 60, Get = a => a.ByproductLevel,
                    Set = (a, v) => a.ByproductLevel = (ushort)v, Min = 0, Max = TotAnimal.MaxGrade });
                list.Add(new AnimalColumn { Header = "Grade", Width = 45, Derive = v => TotAnimal.GetByproductGrade(v) });
            }
            list.Add(new AnimalColumn { Header = "Birthday", Width = 120, Get = a => a.Birthday });
            columns = list.ToArray();

            foreach (AnimalColumn column in columns)
                animalDataGridView.Columns.Add(createColumn(column));
            ((DataGridViewTextBoxColumn)animalDataGridView.Columns[nameColumn]).MaxInputLength = TotSave.MaxNameInput;

            animals = new List<TotAnimal>();
            int count = isPet ? TotAnimal.PetCount : TotAnimal.AnimalCount;
            for (int i = 0; i < count; i++)
            {
                TotAnimal animal = new TotAnimal(isPet, i);
                if (!animal.IsUsed) continue;
                animals.Add(animal);
                DataGridViewRow row = animalDataGridView.Rows[animalDataGridView.Rows.Add()];
                for (int c = 0; c < columns.Length; c++)
                {
                    if (columns[c].Get != null)
                        row.Cells[c].Value = displayValue(columns[c], columns[c].Get(animal));
                    else
                        updateDerived(row, c - 1);
                }
            }
        }

        private static DataGridViewColumn createColumn(AnimalColumn column)
        {
            DataGridViewColumn gridColumn;
            if (column.List != null)
            {
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                comboBoxColumn.Items.AddRange(column.List);
                gridColumn = comboBoxColumn;
            }
            else
                gridColumn = new DataGridViewTextBoxColumn();
            gridColumn.HeaderText = column.Header;
            gridColumn.Width = column.Width;
            gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridColumn.ReadOnly = column.Set == null && column.Header != "Name";
            if (gridColumn.ReadOnly)
                gridColumn.DefaultCellStyle.BackColor = Color.LightGray;
            return gridColumn;
        }

        // List values are shown by name. Values outside the list are left blank and not changed.
        private static object displayValue(AnimalColumn column, object value)
        {
            if (column.List == null) return value;
            int index = Convert.ToInt32(value);
            if (index >= 0 && index < column.List.Length) return column.List[index];
            return null;
        }

        private void updateDerived(DataGridViewRow row, int sourceColumn)
        {
            int value;
            if (Int32.TryParse(Convert.ToString(row.Cells[sourceColumn].Value), out value))
                row.Cells[sourceColumn + 1].Value = columns[sourceColumn + 1].Derive(value);
        }

        // Validate new values; they are written to the save when the form closes.
        private void animalDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewCell cell = animalDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            AnimalColumn column = columns[e.ColumnIndex];
            if (cell.ReadOnly || !animalDataGridView.IsCurrentCellInEditMode) return;
            if (column.Set == null || column.List != null) return;

            int value;
            bool isValid = Int32.TryParse(e.FormattedValue.ToString(), out value);
            if (!isValid || value < column.Min || value > column.Max)
            {
                cell.ErrorText = "Must be a valid number between " + column.Min + " and " + column.Max;
                animalDataGridView.CancelEdit();
            }
            else
                cell.ErrorText = null;
        }

        private void animalDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex + 1 >= columns.Length) return;
            if (columns[e.ColumnIndex + 1].Derive != null)
                updateDerived(animalDataGridView.Rows[e.RowIndex], e.ColumnIndex);
        }

        // Show dropdown list right after clicking into ComboBox cells.
        private void animalDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || columns[e.ColumnIndex].List == null) return;
            ComboBox cb = animalDataGridView.EditingControl as ComboBox;
            if (cb != null) cb.DroppedDown = true;
        }

        private void animalDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (animalDataGridView.IsCurrentCellDirty &&
                columns[animalDataGridView.CurrentCell.ColumnIndex].List != null)
            {
                animalDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void animalDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void maxAffectionButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in animalDataGridView.Rows)
                row.Cells[affectionColumn].Value = TotAnimal.MaxAffection;
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveAnimals()
        {
            animalDataGridView.EndEdit();
            for (int i = 0; i < animals.Count; i++)
            {
                TotAnimal animal = animals[i];
                DataGridViewRow row = animalDataGridView.Rows[i];

                string name = Convert.ToString(row.Cells[nameColumn].Value) ?? "";
                if (name != animal.Name)
                    animal.Name = name;

                for (int c = 0; c < columns.Length; c++)
                {
                    AnimalColumn column = columns[c];
                    if (column.Set == null) continue;
                    object cellValue = row.Cells[c].Value;
                    int value;
                    if (column.List != null)
                    {
                        value = Array.IndexOf(column.List, cellValue);
                        if (value < 0) continue;
                    }
                    else if (!Int32.TryParse(Convert.ToString(cellValue), out value) ||
                        value < column.Min || value > column.Max)
                        continue;
                    if (value != Convert.ToInt32(column.Get(animal)))
                        column.Set(animal, value);
                }
            }
        }

        private void TotAnimalEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAnimals();
        }
    }
}
