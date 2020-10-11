using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class PropertyCardForRobot : Form
    {
        public string FieldName { get; set; }
        public int FieldValue { get; set; }
        public int FieldFinishedValue { get; set; }
        public int FieldBuildingTurns { get; set; }
        public bool CantBuy { get; set; }
        public bool IDontWannaBuy { get; set; }

        private BoardGame m_BoardGame;
        private Cards m_Cards;
        private Player m_Player;
        private Robot m_Robot1;
        private Robot m_Robot2;
        private Robot m_Robot3;
        private Level m_Level;

        private int m_Turn;


        public PropertyCardForRobot(BoardGame boardGame, Player player, Robot robot1, Robot robot2, Robot robot3, Cards card, Level level)
        {
            InitializeComponent();

            this.m_Level = level;
            this.m_BoardGame = boardGame;
            this.m_Player = player;
            this.m_Robot1 = robot1;
            this.m_Robot2 = robot2;
            this.m_Robot3 = robot3;
            this.m_Turn = boardGame.CurrentTurn;
            this.m_Cards = card;

            IDontWannaBuy = false;
            CantBuy = false;

            Random randomCard = new Random();
            int randomCardNumber = randomCard.Next(0, m_Cards.PropertyCards.Count);

            switch (m_Turn)
            {
                case 1: //robot1 turn

                    DrawingACardAndChangeTheTexts(randomCardNumber, m_Robot1);

                    break;

                case 2: //robot2 turn

                    DrawingACardAndChangeTheTexts(randomCardNumber, m_Robot2);

                    break;

                case 3: //robot3 turn

                    DrawingACardAndChangeTheTexts(randomCardNumber, m_Robot3);

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

        private void DrawingACardAndChangeTheTexts(int randomCardNumber, Robot m_Robot)
        {
            PlayerOrRobotName.Text = m_Robot.Name;

            for (int i = 0; i < m_Cards.PropertyCards.Count; i++)
            {
                if (i == randomCardNumber)
                {
                    CardText.Text = "Gratulálok! Felfedeztél egy " + m_Cards.PropertyCards[i].PropertyName + "-t.";
                    TurnText.Text = "A feltárásának idelye: " + m_Cards.PropertyCards[i].BuildingTurns + " kör.";
                    FinishedValue.Text = "Ammennyiben elvégezted " + m_Cards.PropertyCards[i].PropertyFinishedValue + "$-t  kapsz.";
                    Price.Text = m_Cards.PropertyCards[i].PropertyValue + "$";

                    if (ImSmartRobot())
                    {
                        if (IsItARightDecession(m_Robot, m_Cards.PropertyCards[i].PropertyValue, m_Cards.PropertyCards[i].PropertyFinishedValue))
                        {
                            BuyProperty(i, m_Robot);
                        }
                        else
                        {
                            IDontWannaBuy = true;
                        }
                    }
                    else
                    {
                        if (m_Robot.Balance >= m_Cards.PropertyCards[i].PropertyValue)
                        {
                            BuyProperty(i, m_Robot);
                        }
                        else
                        {
                            CantBuy = true;
                        }
                    }
                }                
            }
        }

        private bool ImSmartRobot()
        {
            if (m_Level.LevelType == Levels.Nehéz)
            {
                return true;
            }
            else if (m_Level.LevelType == Levels.Könnyű)
            {
                return false;
            }
            else
            {
                throw new Exception("This level is not exists: " + m_Level.LevelType.ToString());
            }
        }

        public bool IsItARightDecession(Robot currentRobot, int cardPrice, int cardFinishedValue)
        {
            double playerCanPayMe = PlayerCanPayMe(currentRobot, cardFinishedValue);//avg
            double robotsCanPayMe = RobotsCanPayMe(currentRobot, cardFinishedValue);//avg
            double iMightWinXCash = playerCanPayMe + robotsCanPayMe; //the robot might win ? cash, the others can pay him (the next 1-12 property) 
            double iMightLoseXCash = ICanLose(currentRobot); //the robot might lose ? cash in his next turn (the next 1-12 property)
            int theMaxValueThatIShouldPay = GetTheMaximumBill(currentRobot);
            int theNumberOfNotFreeDestinations = CountNotFreeDestinations(currentRobot);

            if (theNumberOfNotFreeDestinations <= 6)
            {
                if (theMaxValueThatIShouldPay <= (currentRobot.Balance - cardPrice))
                {
                    if (iMightLoseXCash <= iMightWinXCash)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void BuyProperty(int i, Robot m_Robot)
        {
            FieldFinishedValue = m_Cards.PropertyCards[i].PropertyFinishedValue;
            FieldValue = m_Cards.PropertyCards[i].PropertyValue;
            FieldName = m_Cards.PropertyCards[i].PropertyName;
            FieldBuildingTurns = m_Cards.PropertyCards[i].BuildingTurns;

            m_Robot.MyPropertyCards.Add(m_Cards.PropertyCards[i]);

            m_Cards.PropertyCards.RemoveAt(i);
        }

        public double PlayerCanPayMe(Robot robot, int cardFinishedValue)
        {
            return GetCalculationFor(m_Player, robot, cardFinishedValue);
        }

        public double RobotsCanPayMe(Robot robot, int cardFinishedValue)
        {
            double sum = 0;

            if (robot.Name == m_Robot1.Name)
            {
                sum += GetCalculationFor(m_Robot2, robot, cardFinishedValue);
                sum += GetCalculationFor(m_Robot3, robot, cardFinishedValue);
            }
            else if (robot.Name == m_Robot2.Name)
            {
                sum += GetCalculationFor(m_Robot1, robot, cardFinishedValue);
                sum += GetCalculationFor(m_Robot3, robot, cardFinishedValue);
            }
            else if (robot.Name == m_Robot3.Name)
            {
                sum += GetCalculationFor(m_Robot1, robot, cardFinishedValue);
                sum += GetCalculationFor(m_Robot2, robot, cardFinishedValue);
            }

            return sum;
        }

        public double GetCalculationFor(Player selectedPlayerOrRobot, Robot robot, int cardFinishedValue)
        {
            int value = 0;
            int counter = 0;
            for (int i = 0; i < m_BoardGame.OwnedProperties.Count; i++)
            {
                if (TheOwnedFieldLocationIsBetweenThePlayerCurrentPositionAndThePlayerCurrentPositionPlus12(selectedPlayerOrRobot.CurrentPosition, i))
                {
                    if (m_BoardGame.OwnedProperties[i].FieldOwner == robot)
                    {
                        value += GetValueInCaseOfPlayers(value, i, robot);
                        counter++;
                    }
                }
            }

            if (TheOwnedCurrentPropertyLocationIsBetweenThePlayerCurrentPositionAndThePlayerCurrentPositionPlus12(selectedPlayerOrRobot.CurrentPosition,robot.CurrentPosition))
            {
                value += (int)Math.Round(cardFinishedValue * 0.25);
                counter++;
            }            

            if (!selectedPlayerOrRobot.Loser && selectedPlayerOrRobot.CanNotRollForXTurn == 0)
            {
                if (counter > 0)
                {
                    double get = value / counter;
                    return get;
                }
                else
                {
                    return 0;
                }
            }
                
            else
            {
                return 0;
            }
        }

        public int GetValueInCaseOfPlayers(int value, int i, Robot robot)
        {
            if (m_BoardGame.OwnedProperties[i].FinishedField)
            {
                value += (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.4);
            }
            else if (m_BoardGame.OwnedProperties[i].StartedField)
            {
                value += (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.25);
            }
            else
            {
                value = 0;
            }

            return value;
        }

        public int CountNotFreeDestinations(Robot robot)
        {
            int notFreeDestinationsCounter = 0;

            for (int i = 0; i < m_BoardGame.OwnedProperties.Count; i++)
            {
                if (TheOwnedFieldLocationIsBetweenThePlayerCurrentPositionAndThePlayerCurrentPositionPlus12(robot.CurrentPosition, i))
                {
                    if (m_BoardGame.OwnedProperties[i].FieldOwner != robot)
                    {
                        notFreeDestinationsCounter++;
                    }
                }
            }

            return notFreeDestinationsCounter;
        }

        public int GetTheMaximumBill(Robot robot)
        {
            int maxBill = 0;

            for (int i = 0; i < m_BoardGame.OwnedProperties.Count; i++)
            {
                if (TheOwnedFieldLocationIsBetweenThePlayerCurrentPositionAndThePlayerCurrentPositionPlus12(robot.CurrentPosition, i))
                {
                    maxBill = GetMax(i, maxBill, robot);
                }
            }

            return maxBill;
        }

        public int GetMax(int i, int maxBill, Robot robot)
        {
            if (m_BoardGame.OwnedProperties[i].FieldOwner != robot)
            {
                if (m_BoardGame.OwnedProperties[i].FinishedField)
                {
                    if (maxBill < (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.4))
                    {
                        maxBill = (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.4);
                    }
                }
                else if (m_BoardGame.OwnedProperties[i].StartedField)
                {
                    if (maxBill < (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.25))
                    {
                        maxBill = (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.25);
                    }
                }
                else
                {
                    maxBill = 0;
                }
            }

            return maxBill;
        }

        public double ICanLose(Robot robot)
        {
            int value = 0;
            int counter = 0;
            for (int i = 0; i < m_BoardGame.OwnedProperties.Count; i++)
            {
                if (TheOwnedFieldLocationIsBetweenThePlayerCurrentPositionAndThePlayerCurrentPositionPlus12(robot.CurrentPosition, i))
                {
                    value += GetValueInCaseOfCurrentRobot(value, i, robot);
                    counter++;
                }
            }

            if (counter>0)
            {
                return value / counter;
            }
            else
            {
                return 0;
            }
        }

        private int GetValueInCaseOfCurrentRobot(int value, int i, Robot robot)
        {
            if (m_BoardGame.OwnedProperties[i].FieldOwner != robot)
            {
                if (m_BoardGame.OwnedProperties[i].FinishedField)
                {
                    value += (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.4);
                }
                else if (m_BoardGame.OwnedProperties[i].StartedField)
                {
                    value += (int)Math.Round(m_BoardGame.OwnedProperties[i].FieldFinishedValue * 0.25);
                }
                else
                {
                    value = 0;
                }
            }

            return value;
        }

        private bool TheOwnedFieldLocationIsBetweenThePlayerCurrentPositionAndThePlayerCurrentPositionPlus12(int playerCurrentPosition, int i)
        {
            if (playerCurrentPosition + 12 >= m_BoardGame.OwnedProperties[i].FieldLocation && playerCurrentPosition < m_BoardGame.OwnedProperties[i].FieldLocation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TheOwnedCurrentPropertyLocationIsBetweenThePlayerCurrentPositionAndThePlayerCurrentPositionPlus12(int playerCurrentPosition, int robotCurrentPosition) 
        {
            if (playerCurrentPosition + 12 >= robotCurrentPosition && playerCurrentPosition < robotCurrentPosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
