using BoardGame.Domain_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.BusinessLogics
{
    public class UpdateListBoxes
    {
        private GameHandler m_GameHandler;
        private BoardGame m_BoardGameForm;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;

        public UpdateListBoxes(GameHandler gameHandler, BoardGame boardGame)
        {
            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;
        }

        public void UpdatePlayerPassiveCardListBox()
        {
            m_BoardGameForm.PlayerPassiveCardList.Items.Clear();
            foreach (CardModelFromDatabase.PassiveCard cards in player.MyPassiveCards)
            {
                m_BoardGameForm.PlayerPassiveCardList.Items.Add(cards);
            }
        }

        public void UpdateRobot1PassiveCardListBox()
        {
            m_BoardGameForm.Robot1PassiveCardList.Items.Clear();
            foreach (CardModelFromDatabase.PassiveCard cards in robot1.MyPassiveCards)
            {
                m_BoardGameForm.Robot1PassiveCardList.Items.Add(cards);
            }
        }
        public void UpdateRobot2PassiveCardListBox()
        {
            m_BoardGameForm.Robot2PassiveCardList.Items.Clear();
            foreach (CardModelFromDatabase.PassiveCard cards in robot2.MyPassiveCards)
            {
                m_BoardGameForm.Robot2PassiveCardList.Items.Add(cards);
            }
        }
        public void UpdateRobot3PassiveCardListBox()
        {
            m_BoardGameForm.Robot3PassiveCardList.Items.Clear();
            foreach (CardModelFromDatabase.PassiveCard cards in robot3.MyPassiveCards)
            {
                m_BoardGameForm.Robot3PassiveCardList.Items.Add(cards);
            }
        }

        public void UpdatePlayerPropertyCardListBox()
        {
            m_BoardGameForm.PlayerPropertyCardList.Items.Clear();
            foreach (CardModelFromDatabase.PropertyCard cards in player.MyPropertyCards)
            {
                m_BoardGameForm.PlayerPropertyCardList.Items.Add(cards);
            }
        }

        public void UpdateRobot1PropertyCardListBox()
        {
            m_BoardGameForm.Robot1PropertyCardList.Items.Clear();
            foreach (CardModelFromDatabase.PropertyCard cards in robot1.MyPropertyCards)
            {
                m_BoardGameForm.Robot1PropertyCardList.Items.Add(cards);
            }
        }
        public void UpdateRobot2PropertyCardListBox()
        {
            m_BoardGameForm.Robot2PropertyCardList.Items.Clear();
            foreach (CardModelFromDatabase.PropertyCard cards in robot2.MyPropertyCards)
            {
                m_BoardGameForm.Robot2PropertyCardList.Items.Add(cards);
            }
        }
        public void UpdateRobot3PropertyCardListBox()
        {
            m_BoardGameForm.Robot3PropertyCardList.Items.Clear();
            foreach (CardModelFromDatabase.PropertyCard cards in robot3.MyPropertyCards)
            {
                m_BoardGameForm.Robot3PropertyCardList.Items.Add(cards);
            }
        }

        public void AddTheSelectedPropertytoRobotsPropertyListBox(CardModelFromDatabase.PropertyCard card)
        {
            switch (m_BoardGameForm.CurrentTurn)
            {
                case 1:
                    m_BoardGameForm.Robot1PropertyCardList.Items.Add(card);
                    break;
                case 2:
                    m_BoardGameForm.Robot1PropertyCardList.Items.Add(card);
                    break;
                case 3:
                    m_BoardGameForm.Robot1PropertyCardList.Items.Add(card);
                    break;
                default:
                    throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
            }
        }

        public void RemoveTheSelectedPropertytoRobotsPropertyListBox(CardModelFromDatabase.PropertyCard card)
        {
            switch (m_BoardGameForm.CurrentTurn)
            {
                case 1:
                    m_BoardGameForm.Robot1PropertyCardList.Items.Remove(card);
                    break;
                case 2:
                    m_BoardGameForm.Robot1PropertyCardList.Items.Remove(card);
                    break;
                case 3:
                    m_BoardGameForm.Robot1PropertyCardList.Items.Remove(card);
                    break;
                default:
                    throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
            }
        }
    }
}
