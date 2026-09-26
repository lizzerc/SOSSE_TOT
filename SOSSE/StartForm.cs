using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOSSE
{
    /// <summary>
    /// Startup screen: choose which game's editor to open.
    /// </summary>
    public partial class StartForm : Form
    {
        public StartForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }

        private void openEditor(Form editor)
        {
            Hide();
            editor.ShowDialog();
            Show();
        }

        private void sosButton_Click(object sender, EventArgs e)
        {
            openEditor(new MainForm());
        }

        private void totButton_Click(object sender, EventArgs e)
        {
            openEditor(new TOT.TotMainForm());
        }
    }
}
