using System;
using System.Windows.Forms;

namespace BoardGame.InGameInformationForms
{
    public partial class EndResult : Form
    {
        public EndResult(string playersSortedByTheirPlaces)
        {
            InitializeComponent();

            string[] places = playersSortedByTheirPlaces.Split(',');
            FirstName.Text = places[0];
            SecondName.Text = places[1];
            ThirdName.Text = places[2];
            FourthName.Text = places[3];
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
