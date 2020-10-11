using BoardGame.Domain_Model;
using System;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class PropertyCard : Form
    {
        public string FieldName { get; set; }
        public int FieldValue { get; set; }
        public int FieldFinishedValue { get; set; }
        public int FieldBuildingTurns { get; set; }


        private Cards m_Cards;
        private Player m_Player;


        public PropertyCard(Player player, Cards card)
        {
            InitializeComponent();

            ButtonTip.SetToolTip(Cancel, "Nem veszem meg");
            ButtonTip.SetToolTip(Ok, "Lelet megvásárlása");

            this.m_Player = player;
            this.m_Cards = card;

            Random randomCard = new Random();
            int randomCardNumber = randomCard.Next(0, m_Cards.PropertyCards.Count);

            PlayerOrRobotName.Text = player.Name;

            for (int i = 0; i < m_Cards.PropertyCards.Count; i++)
            {
                if (i == randomCardNumber)
                {
                    CardText.Text = "Gratulálok! Felfedeztél egy " + m_Cards.PropertyCards[i].PropertyName + "-t.";
                    TurnText.Text = "A feltárásának idelye: " + m_Cards.PropertyCards[i].BuildingTurns + " kör.";
                    FinishedValue.Text = "Ammennyiben elvégezted " + m_Cards.PropertyCards[i].PropertyFinishedValue + "$-t  kapsz.";
                    Price.Text = m_Cards.PropertyCards[i].PropertyValue + "$";

                    if (player.Balance >= m_Cards.PropertyCards[i].PropertyValue)
                    {
                        FieldFinishedValue = m_Cards.PropertyCards[i].PropertyFinishedValue;
                        FieldValue = m_Cards.PropertyCards[i].PropertyValue;
                        FieldName = m_Cards.PropertyCards[i].PropertyName;
                        FieldBuildingTurns = m_Cards.PropertyCards[i].BuildingTurns;

                        m_Player.MyPropertyCards.Add(m_Cards.PropertyCards[i]);
                        m_Cards.PropertyCards.RemoveAt(i);
                    }
                    else
                    {
                        MessageBox.Show("Sajnáljuk nincs elég Pénezd!", "Bank", MessageBoxButtons.OK);
                        Ok.Enabled = false;
                    }
                }
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
