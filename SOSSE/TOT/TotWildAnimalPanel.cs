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
    /// Wild animals tab of the Animals window. Friendship: 22 x (u16 points, u16 flag) at 0xA328. The flag's meaning is unknown.
    /// </summary>
    public partial class TotWildAnimalPanel : UserControl
    {
        private const int wildAnimalOffset = 0xA328;
        private const int maxPoints = 1000;

        private DataGridViewTextBoxColumn pointsColumn;

        public TotWildAnimalPanel()
        {
            InitializeComponent();
            TotData.LoadWildAnimalData();

            TotGrid.AddTextColumn(wildAnimalDataGridView, "Animal", 150, true);
            pointsColumn = TotGrid.AddTextColumn(wildAnimalDataGridView, "Friendship", 80, false);
            TotGrid.AddTextColumn(wildAnimalDataGridView, "Flag", 50, true);
            wildAnimalDataGridView.CellValidating += wildAnimalDataGridView_CellValidating;

            for (int i = 0; i < TotData.WildAnimalNameList.Length; i++)
            {
                int offset = wildAnimalOffset + 4 * i;
                wildAnimalDataGridView.Rows.Add(TotData.WildAnimalNameList[i],
                    BitConverter.ToUInt16(TotSave.SaveData, offset), BitConverter.ToUInt16(TotSave.SaveData, offset + 2));
            }
        }

        private void wildAnimalDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            TotGrid.ValidateRange(wildAnimalDataGridView, e, pointsColumn.Index, 0, maxPoints);
        }

        private void maxAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in wildAnimalDataGridView.Rows)
                row.Cells[pointsColumn.Index].Value = maxPoints;
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveWildAnimals()
        {
            wildAnimalDataGridView.EndEdit();
            for (int i = 0; i < TotData.WildAnimalNameList.Length; i++)
            {
                int offset = wildAnimalOffset + 4 * i;
                int points;
                if (!TotGrid.TryGetInt(wildAnimalDataGridView.Rows[i].Cells[pointsColumn.Index], out points)) continue;
                if (points != BitConverter.ToUInt16(TotSave.SaveData, offset))
                    Array.Copy(BitConverter.GetBytes((ushort)points), 0, TotSave.SaveData, offset, 2);
            }
        }
    }
}
