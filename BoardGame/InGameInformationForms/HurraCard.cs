using BoardGame.Domain_Model;
using System;
using System.Windows.Forms;

namespace BoardGame
{
    public enum HurraAction { Jump, Roll, Balance }

    public partial class HurraCard : Form
    {
        public HurraAction ActionType { get; set; }
        public int Amount { get; set; }

        private Cards m_Cards;
        private Player m_Player;
        private Robot m_Robot1;
        private Robot m_Robot2;
        private Robot m_Robot3;

        private int m_Turn;

        public HurraCard(int turn, Player player, Robot robot1, Robot robot2, Robot robot3, Cards card)
        {
            InitializeComponent();

            this.m_Player = player;
            this.m_Robot1 = robot1;
            this.m_Robot2 = robot2;
            this.m_Robot3 = robot3;
            this.m_Turn = turn;
            this.m_Cards = card;

            switch (m_Turn)
            {
                case 0:

                    PlayerOrRobotName.Text = player.Name;

                    break;
                case 1:

                    PlayerOrRobotName.Text = robot1.Name;

                    break;
                case 2:

                    PlayerOrRobotName.Text = robot2.Name;

                    break;
                case 3:

                    PlayerOrRobotName.Text = robot3.Name;

                    break;
                default:
                    throw new Exception("Wrong turn");
            }

            Random randomCard = new Random();
            int randomCardNumber = randomCard.Next(0, m_Cards.HurraCard.Count);

            if (m_Cards.HurraCard.Count == 0)
            {
                //there is 0 card left so we need to use the cards again

                foreach (CardModelFromDatabase.HappyCard cards in m_Cards.UsedHurraCard)
                {
                    m_Cards.HurraCard.Add(cards);
                }

                m_Cards.UsedHupszCard.Clear();

                DrawingACardAndPutItDownAndChangeTheTextBasedOnTheActionType(randomCardNumber);
            }
            else
            {
                DrawingACardAndPutItDownAndChangeTheTextBasedOnTheActionType(randomCardNumber);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DrawingACardAndPutItDownAndChangeTheTextBasedOnTheActionType(int randomCardNumber)
        {
            for (int i = 0; i < m_Cards.HurraCard.Count; i++)
            {
                if (i == randomCardNumber)
                {
                    this.ActionType = (HurraAction)Enum.Parse(typeof(HurraAction), m_Cards.HurraCard[i].Situation);

                    this.Amount = m_Cards.HurraCard[i].Ammount;

                    switch (ActionType)
                    {
                        case global::BoardGame.HurraAction.Jump:

                            HurraAction.Text = "Sietsz valahova? Lépj előre " + m_Cards.HurraCard[i].Ammount + " mezőt.";

                            break;
                        case global::BoardGame.HurraAction.Roll:

                            HurraAction.Text = "Úgy néz ki a többiek elfoglaltak. Megint te jössz!";

                            break;
                        case global::BoardGame.HurraAction.Balance:

                            HurraAction.Text = "Szerencsés vagy, találtál " + m_Cards.HurraCard[i].Ammount + " $-t.";

                            break;
                        default:
                            throw new Exception("There is not an action like this: " + m_Cards.HurraCard[i].Situation);
                    }                    

                    m_Cards.UsedHurraCard.Add(m_Cards.HurraCard[i]);

                    m_Cards.HurraCard.RemoveAt(i);
                }
            }
        }
    }
}
