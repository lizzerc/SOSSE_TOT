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
    /// Recipes learned: 1 bit per recipe at 0x27514, in the game's recipe order.
    /// </summary>
    public partial class TotRecipeEditingForm : Form
    {
        private const int recipeOffset = 0x27514;

        private class Category
        {
            public string Name;
            public int First;
        }
        private static readonly Category[] categories = {
            new Category { Name = "Salad", First = 0 },
            new Category { Name = "Soup", First = 41 },
            new Category { Name = "Grilled / Fried", First = 77 },
            new Category { Name = "Pot", First = 120 },
            new Category { Name = "Rice", First = 170 },
            new Category { Name = "Noodles / Bread", First = 228 },
            new Category { Name = "Dessert", First = 271 },
            new Category { Name = "Drink", First = 368 }
        };

        private DataGridViewCheckBoxColumn learnedColumn;

        public TotRecipeEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            TotData.LoadRecipeData();

            TotGrid.AddTextColumn(recipeDataGridView, "Category", 100, true);
            TotGrid.AddTextColumn(recipeDataGridView, "Recipe", 170, true);
            learnedColumn = TotGrid.AddCheckColumn(recipeDataGridView, "Learned", 60);

            for (int i = 0; i < TotData.RecipeNameList.Length; i++)
                recipeDataGridView.Rows.Add(categoryName(i), TotData.RecipeNameList[i], isLearned(i));
        }

        private static string categoryName(int recipe)
        {
            return categories.Last(c => c.First <= recipe).Name;
        }

        private static bool isLearned(int recipe)
        {
            return (TotSave.SaveData[recipeOffset + recipe / 8] & (1 << (recipe % 8))) != 0;
        }

        private void learnAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in recipeDataGridView.Rows)
                row.Cells[learnedColumn.Index].Value = true;
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveRecipes()
        {
            recipeDataGridView.EndEdit();
            for (int i = 0; i < TotData.RecipeNameList.Length; i++)
            {
                bool learned = TotGrid.IsChecked(recipeDataGridView.Rows[i].Cells[learnedColumn.Index]);
                if (learned == isLearned(i)) continue;
                TotSave.SaveData[recipeOffset + i / 8] ^= (byte)(1 << (i % 8));
            }
        }

        private void TotRecipeEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRecipes();
        }
    }
}
