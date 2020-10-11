using BoardGame.Domain_Model;
using System;
using System.Windows.Forms;

namespace BoardGame
{
    public enum Abilities { Kereskedelem, Régész, Vállalkozó, Óvatos, Őrangyal, Szárnysegéd, Talpraesett, Tolvaj}

    public partial class PassiveCard : Form
    {
        private Cards m_Cards;
        private Player m_Player;
        private Robot m_Robot1;
        private Robot m_Robot2;
        private Robot m_Robot3;

        private int m_Turn;

        public Abilities PassiveAbility { get; set; }

        public int AbilityValue {get; set;}
        public bool NoPassiveCard { get; set; }

        public PassiveCard(int turn, Player player, Robot robot1, Robot robot2, Robot robot3, Cards card)
        {
            InitializeComponent();

            this.m_Player = player;
            this.m_Robot1 = robot1;
            this.m_Robot2 = robot2;
            this.m_Robot3 = robot3;
            this.m_Turn = turn;
            this.m_Cards = card;

            Random randomCard = new Random();
            int randomCardNumber = randomCard.Next(0, m_Cards.PassiveCards.Count);

            switch (m_Turn)
            {
                case 0: //player turn

                    PlayerOrRobotName.Text = m_Player.Name;
                    DrawingAPassiveCard(randomCardNumber, m_Player, null);


                    break;
                case 1: //robot1 turn

                    PlayerOrRobotName.Text = m_Robot1.Name;
                    DrawingAPassiveCard(randomCardNumber, null, m_Robot1);


                    break;
                case 2: //robot2 turn

                    PlayerOrRobotName.Text = m_Robot2.Name;
                    DrawingAPassiveCard(randomCardNumber, null, m_Robot2);

                    break;
                case 3: //robot3 turn

                    PlayerOrRobotName.Text = m_Robot3.Name;
                    DrawingAPassiveCard(randomCardNumber, null, m_Robot3);

                    break;

                default:
                    throw new Exception("Turn is not exist");
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DrawingAPassiveCard(int randomCardNumber, Player m_Player, Robot m_Robot)
        {
            if (m_Cards.PassiveCards.Count == 0)
            {
                Message.Text = "A passzív kártyák elfogytak";
                NoPassiveCard = true;
            }
            else
            {
                for (int i = 0; i < m_Cards.PassiveCards.Count; i++)
                {
                    if (i == randomCardNumber)
                    {
                        if (m_Cards.PassiveCards[i].AbilityValue > 0)
                        {
                            AbilityText.Text = m_Cards.PassiveCards[i].SpecialPassiveAbility + " (" + m_Cards.PassiveCards[i].AbilityValue + " db)";
                        }
                        else
                        {
                            AbilityText.Text = m_Cards.PassiveCards[i].SpecialPassiveAbility + " (végtelen)";
                        }


                        if (m_Player != null)
                        {
                            PassiveAbility = (Abilities)Enum.Parse(typeof(Abilities), m_Cards.PassiveCards[i].SpecialPassiveAbility);
                            AbilityValue = m_Cards.PassiveCards[i].AbilityValue;
                            m_Player.MyPassiveCards.Add(m_Cards.PassiveCards[i]);
                        }
                        else if (m_Robot != null)
                        {
                            PassiveAbility = (Abilities)Enum.Parse(typeof(Abilities), m_Cards.PassiveCards[i].SpecialPassiveAbility);
                            AbilityValue = m_Cards.PassiveCards[i].AbilityValue;
                            m_Robot.MyPassiveCards.Add(m_Cards.PassiveCards[i]);                            
                        }

                        m_Cards.PassiveCards.Remove(m_Cards.PassiveCards[i]);
                    }
                }
            }
        }
    }
}
