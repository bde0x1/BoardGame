using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BoardGame.Domain_Model;

namespace BoardGame
{
    public partial class StealPassiveAbilityFromRobotsView : Form
    {
        private Player m_Player;
        private Robot m_Robot1;
        private Robot m_Robot2;
        private Robot m_Robot3;

        private List<CardModelFromDatabase.PassiveCard> RobotsPassiveCards = new List<CardModelFromDatabase.PassiveCard>();

        public CardModelFromDatabase.PassiveCard RobotSelectedPassiveAbility { get; set; }

        public StealPassiveAbilityFromRobotsView(Player player,Robot robot1, Robot robot2, Robot robot3)
        {
            InitializeComponent();

            ButtonTip.SetToolTip(Cancel, "Mégse lopok");
            ButtonTip.SetToolTip(Steal, "Lelet lopás");

            this.m_Player = player;
            this.m_Robot1 = robot1;
            this.m_Robot2 = robot2;
            this.m_Robot3 = robot3;

            PlayerName.Text = m_Player.Name;

            foreach (CardModelFromDatabase.PassiveCard Card in m_Robot1.MyPassiveCards)
            {
                RobotsPassiveCards.Add(Card);
            }
            foreach (CardModelFromDatabase.PassiveCard Card in m_Robot2.MyPassiveCards)
            {
                RobotsPassiveCards.Add(Card);
            }
            foreach (CardModelFromDatabase.PassiveCard Card in m_Robot3.MyPassiveCards)
            {
                RobotsPassiveCards.Add(Card);
            }

            foreach (CardModelFromDatabase.PassiveCard card in RobotsPassiveCards)
            {
                RobotsPassiveAbilities.Items.Add(card);
            }
        }

        private void Steal_Click(object sender, EventArgs e)
        {
            if (RobotsPassiveAbilities.SelectedItem != null)
            {
                RobotSelectedPassiveAbility = (CardModelFromDatabase.PassiveCard)RobotsPassiveAbilities.SelectedItem;
            }
            else
            {
                MessageBox.Show("Válassz ki egyet a robotok passziv képességei közül!");
            }

            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }
    }
}
