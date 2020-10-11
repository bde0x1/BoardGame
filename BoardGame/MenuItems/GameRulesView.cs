using System;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class GameRulesView : Form
    {
        public GameRulesView()
        {
            InitializeComponent();

            richTextBox1.SelectAll();
            richTextBox1.SelectionProtected = true;
            ButtonTip.SetToolTip(OK, "Kilépés a Menübe");
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
