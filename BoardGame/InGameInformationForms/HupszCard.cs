using BoardGame.Domain_Model;
using System;
using System.Windows.Forms;

namespace BoardGame
{

    public enum HupszAction { Jump, Accident, Balance }

    public partial class HupszCard : Form
    {

        public HupszAction ActionType { get; set; }
        public int Amount { get; set; }

        private Cards m_Cards;
        private Player m_Player;
        private Robot m_Robot1;
        private Robot m_Robot2;
        private Robot m_Robot3;

        private int m_Turn;

        public HupszCard(int turn, Player player, Robot robot1, Robot robot2, Robot robot3, Cards card)
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

                    PlayerOrRobotName.Text = m_Player.Name;

                    break;
                case 1:

                    PlayerOrRobotName.Text = m_Robot1.Name;

                    break;
                case 2:

                    PlayerOrRobotName.Text = m_Robot2.Name;

                    break;
                case 3:

                    PlayerOrRobotName.Text = m_Robot3.Name;

                    break;
                default:
                    throw new Exception("Wrong turn");
            }

            Random randomCard = new Random();
            int randomCardNumber = randomCard.Next(0, m_Cards.HupszCard.Count);

            if (m_Cards.HupszCard.Count == 0)
            {
                //there is 0 card left so we need to use the cards again

                foreach (CardModelFromDatabase.SadCard cards in m_Cards.UsedHupszCard)
                {
                    m_Cards.HupszCard.Add(cards);
                }

                m_Cards.UsedHupszCard.Clear();

                DrawingACardAndPutItDownAndChangeTheTextBasedOnTheActionType(randomCardNumber);
            }
            else
            {
                DrawingACardAndPutItDownAndChangeTheTextBasedOnTheActionType(randomCardNumber);
            }
        }

        private void OK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DrawingACardAndPutItDownAndChangeTheTextBasedOnTheActionType(int randomCardNumber)
        {
            for (int i = 0; i < m_Cards.HupszCard.Count; i++)
            {
                if (i == randomCardNumber)
                {
                    this.ActionType = (HupszAction)Enum.Parse(typeof(HupszAction), m_Cards.HupszCard[i].Situation);

                    this.Amount = m_Cards.HupszCard[i].Ammount;

                    switch (ActionType)
                    {
                        case global::BoardGame.HupszAction.Jump:

                            HupszAction.Text = "Valamit elhagytál? Vissza kell lépned " + m_Cards.HupszCard[i].Ammount + " mezőt.";

                            break;
                        case global::BoardGame.HupszAction.Accident:

                            HupszAction.Text = "Balesetet szenvedtél kimaradsz " + m_Cards.HupszCard[i].Ammount + " körből, amíg rendbejössz.";

                            break;
                        case global::BoardGame.HupszAction.Balance:

                            HupszAction.Text ="Nem fizettél adót! " + m_Cards.HupszCard[i].Ammount + " $ -t vontunk le az egyenlegedről.";

                            break;
                        default:
                            throw new Exception("There is not an action like this: "+ m_Cards.HupszCard[i].Situation);
                    }                    

                    m_Cards.UsedHupszCard.Add(m_Cards.HupszCard[i]);

                    m_Cards.HupszCard.RemoveAt(i);
                }
            }
        }
    }
}
