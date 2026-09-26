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
    /// Barn animals or pets. Editable: name, affection, and pet XP.
    /// </summary>
    public partial class TotAnimalEditingForm : Form
    {
        private readonly bool isPet;
        private List<TotAnimal> animals;

        public TotAnimalEditingForm(bool isPet)
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadAnimalData();
            this.isPet = isPet;
            this.Text = isPet ? "Pets" : "Animals";

            personalityColumn.Visible = !isPet;
            xpColumn.Visible = isPet;
            levelColumn.Visible = isPet;
            nameColumn.MaxInputLength = TotAnimal.MaxNameLength;

            animals = new List<TotAnimal>();
            int count = isPet ? TotAnimal.PetCount : TotAnimal.AnimalCount;
            for (int i = 0; i < count; i++)
            {
                TotAnimal animal = new TotAnimal(isPet, i);
                if (!animal.IsUsed) continue;
                animals.Add(animal);

                string personality = animal.Personality < TotData.AnimalPersonalityList.Length ?
                    TotData.AnimalPersonalityList[animal.Personality] : "#" + animal.Personality;
                animalDataGridView.Rows.Add(i + 1, TotData.GetAnimalName(animal.Species), animal.Name,
                    animal.Affection, hearts(animal.Affection), personality, animal.FestivalWins,
                    animal.Birthday, animal.XP, TotAnimal.GetPetLevel(animal.XP));
            }
        }

        private static string hearts(int affection)
        {
            return (affection / 100.0).ToString("0.#");
        }

        // Validate new values; they are written to the save when the form closes.
        private void animalDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewCell cell = animalDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.ReadOnly || !animalDataGridView.IsCurrentCellInEditMode) return;
            if (e.ColumnIndex == affectionColumn.Index)
            {
                int affection;
                bool isValid = Int32.TryParse(e.FormattedValue.ToString(), out affection);
                if (!isValid || affection < 0 || affection > TotAnimal.MaxAffection)
                {
                    cell.ErrorText = "Must be a valid number between 0 and " + TotAnimal.MaxAffection + " (100 per heart)";
                    animalDataGridView.CancelEdit();
                }
                else
                {
                    cell.ErrorText = null;
                    animalDataGridView.Rows[e.RowIndex].Cells[heartsColumn.Index].Value = hearts(affection);
                }
            }
            else if (e.ColumnIndex == xpColumn.Index)
            {
                ushort xp;
                bool isValid = UInt16.TryParse(e.FormattedValue.ToString(), out xp);
                if (!isValid)
                {
                    cell.ErrorText = "Must be a valid number between 0 and " + UInt16.MaxValue;
                    animalDataGridView.CancelEdit();
                }
                else
                {
                    cell.ErrorText = null;
                    animalDataGridView.Rows[e.RowIndex].Cells[levelColumn.Index].Value = TotAnimal.GetPetLevel(xp);
                }
            }
        }

        private void maxAffectionButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in animalDataGridView.Rows)
            {
                row.Cells[affectionColumn.Index].Value = TotAnimal.MaxAffection;
                row.Cells[heartsColumn.Index].Value = hearts(TotAnimal.MaxAffection);
            }
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

                string name = Convert.ToString(row.Cells[nameColumn.Index].Value) ?? "";
                if (name != animal.Name)
                    animal.Name = name;

                ushort affection;
                if (UInt16.TryParse(Convert.ToString(row.Cells[affectionColumn.Index].Value), out affection) &&
                    affection <= TotAnimal.MaxAffection && affection != animal.Affection)
                    animal.Affection = affection;

                ushort xp;
                if (isPet && UInt16.TryParse(Convert.ToString(row.Cells[xpColumn.Index].Value), out xp) &&
                    xp != animal.XP)
                    animal.XP = xp;
            }
        }

        private void TotAnimalEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAnimals();
        }
    }
}
