using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class TradeWithRobotsView : Form
    {
        private Player m_Player;

        private List<Field> m_OwnedProperties;

        public Field PlayersPropertyForTrade { get; set; }
        public Field RobotsPropertyForTrade { get; set; }

        public TradeWithRobotsView(Player player, List<Field> ownedProperties)
        {
            InitializeComponent();

            ButtonTip.SetToolTip(Cancel, "Mégse cserélek");
            ButtonTip.SetToolTip(Trade, "Lelet csere");

            this.m_Player = player;

            this.m_OwnedProperties = ownedProperties;

            PlayerName.Text = m_Player.Name;

            foreach (Field field in m_OwnedProperties)
            {
                if (field.StartedField)
                {
                    if (field.FieldOwner == player)
                    {

                        PlayerStartedProperties.Items.Add(field);

                    }
                    else 
                    {
                        RobotsStartedProperties.Items.Add(field);
                    }
                }
            }
        }

        private void Trade_Click(object sender, EventArgs e)
        {            
            if (PlayerStartedProperties.SelectedItem != null)
            {
                PlayersPropertyForTrade = (Field)PlayerStartedProperties.SelectedItem;
            }
            else
            {
                MessageBox.Show("Válassz ki egyet a leleteid közül!");
            }

            if (RobotsStartedProperties.SelectedItem != null)
            {
                RobotsPropertyForTrade = (Field)RobotsStartedProperties.SelectedItem;
            }
            else
            {
                MessageBox.Show("Válassz ki egyet a robotok leletei közül!");
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
