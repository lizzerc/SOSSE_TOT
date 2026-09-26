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
    /// Shared helpers for the grids built in code.
    /// </summary>
    public static class TotGrid
    {
        public static DataGridViewTextBoxColumn AddTextColumn(DataGridView grid, string header, int width, bool readOnly)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = header;
            column.Width = width;
            column.ReadOnly = readOnly;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            grid.Columns.Add(column);
            return column;
        }

        public static DataGridViewCheckBoxColumn AddCheckColumn(DataGridView grid, string header, int width)
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.HeaderText = header;
            column.Width = width;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            grid.Columns.Add(column);
            return column;
        }

        /// <summary>
        /// Show a row that can't be edited (unknown values)
        /// </summary>
        public static void LockRow(DataGridViewRow row)
        {
            row.ReadOnly = true;
            row.DefaultCellStyle.BackColor = Color.LightGray;
        }

        public static bool IsChecked(DataGridViewCell cell)
        {
            return cell.Value is bool && (bool)cell.Value;
        }

        /// <summary>
        /// Cancel the edit if the typed value is not a number between min and max
        /// </summary>
        public static void ValidateRange(DataGridView grid, DataGridViewCellValidatingEventArgs e, int columnIndex, int min, int max)
        {
            if (e.ColumnIndex != columnIndex || !grid.IsCurrentCellInEditMode) return;
            DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            int value;
            if (!Int32.TryParse(e.FormattedValue.ToString(), out value) || value < min || value > max)
            {
                cell.ErrorText = "Must be a valid number between " + min + " and " + max;
                grid.CancelEdit();
            }
            else
                cell.ErrorText = null;
        }

        public static bool TryGetInt(DataGridViewCell cell, out int value)
        {
            return Int32.TryParse(Convert.ToString(cell.Value), out value);
        }
    }
}
