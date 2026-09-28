using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SOSSE.TOT;

namespace SOSSE
{
    /// <summary>
    /// Trophies: 247 x 1 bit from 0x4E614, in TrophyData order (1 = earned).
    /// </summary>
    public partial class TrophyEditingForm : Form
    {
        private const int trophyOffset = 0x4E614;

        // Each line: name, a tab, then the target from TrophyData
        public static string[] TrophyNameList;
        public static string[] TrophyTargetList;

        private DataGridViewCheckBoxColumn earnedColumn;

        public TrophyEditingForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            loadTrophyData();

            TotGrid.AddTextColumn(trophyDataGridView, "Trophy", 250, true);
            earnedColumn = TotGrid.AddCheckColumn(trophyDataGridView, "Earned", 60);
            TotGrid.AddTextColumn(trophyDataGridView, "Target", 110, true);

            for (int i = 0; i < TrophyNameList.Length; i++)
                trophyDataGridView.Rows.Add(TrophyNameList[i], IsEarned(i), TrophyTargetList[i]);
        }

        private static void loadTrophyData()
        {
            if (TrophyNameList != null) return;
            string[] lines;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SOSSE.Resources.TrophyName.txt"))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    lines = reader.ReadToEnd().Replace("\r", "").Split(new[] { '\n' });
                }
            }
            TrophyNameList = new string[lines.Length];
            TrophyTargetList = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('\t');
                TrophyNameList[i] = parts[0];
                TrophyTargetList[i] = parts.Length > 1 && parts[1] != "0" ? parts[1] : "";
            }
        }

        public static bool IsEarned(int trophy)
        {
            return (MainForm.SaveData[trophyOffset + trophy / 8] & (1 << (trophy % 8))) != 0;
        }

        public static void SetEarned(int trophy, bool earned)
        {
            byte mask = (byte)(1 << (trophy % 8));
            if (earned)
                MainForm.SaveData[trophyOffset + trophy / 8] |= mask;
            else
                MainForm.SaveData[trophyOffset + trophy / 8] &= (byte)~mask;
        }

        private void earnAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in trophyDataGridView.Rows)
                row.Cells[earnedColumn.Index].Value = true;
        }

        /// <summary>
        /// Save changed values to the main save file
        /// </summary>
        public void SaveTrophies()
        {
            trophyDataGridView.EndEdit();
            for (int i = 0; i < TrophyNameList.Length; i++)
                SetEarned(i, TotGrid.IsChecked(trophyDataGridView.Rows[i].Cells[earnedColumn.Index]));
        }

        private void TrophyEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTrophies();
        }
    }
}
