using BoardGame.CardModelFromDatabase;
using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using BoardGame.InGameInformationForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BoardGame.BusinessLogics
{
    public class GameHandler
    {
        private BoardGame m_BoardGameForm;
        public Player player;
        public Robot robot1;
        public Robot robot2;
        public Robot robot3;        
        private readonly DataAccess m_DataAccess = new DataAccess();
        public Cards cards;
        private Level level;

        public GameHandler(Player Player, Robot Robot1, Robot Robot2, Robot Robot3, Level Levels)
        {            
            player = Player;
            robot1 = Robot1;
            robot2 = Robot2;
            robot3 = Robot3;
            level = Levels;
            cards = new Cards();
            m_BoardGameForm = new BoardGame(this, level);            

            GetDatabaseData();
        }

        public void StartGame()
        {
            m_BoardGameForm.ShowDialog();
        }

        public int RollTheDice()
        {
            int leftCube = Cube.ThrownValue;
            int rightCube = Cube.ThrownValue;
            int sumOfTheTwoValue = leftCube + rightCube;

            m_BoardGameForm.RightCubeValue.Text = rightCube.ToString();

            if (leftCube == 1)
            {
                m_BoardGameForm.LeftCubeValue.Text = "@";
                return (int)Math.Round((double)sumOfTheTwoValue / 2);
            }

            m_BoardGameForm.LeftCubeValue.Text = leftCube.ToString();
            return sumOfTheTwoValue;
        }

        public void JumpWithTheFigureToTheNextPosition(int thrownValue)
        {
            switch (m_BoardGameForm.CurrentTurn)
            {
                case 0: //player blue
                    m_BoardGameForm.StepFigure(m_BoardGameForm.player, thrownValue);
                    break;
                case 1: //robot1 green                    
                    m_BoardGameForm.StepFigure(m_BoardGameForm.robot1, thrownValue);
                    break;
                case 2: //robot2 yellow
                    m_BoardGameForm.StepFigure(m_BoardGameForm.robot2, thrownValue);
                    break;
                case 3: //robot3 red
                    m_BoardGameForm.StepFigure(m_BoardGameForm.robot3, thrownValue);
                    break;
                default:
                    throw new Exception("Nincs körön lévő játékos");
            }
        }

        public string ResultAddvertisement(Player player, Robot robot1, Robot robot2, Robot robot3)
        {
            string winner = "";
            string second = "";
            string third = "";
            string fourth = "";

            int[] Balances = new int[] { player.Balance, robot1.Balance, robot2.Balance, robot3.Balance };

            for (int i = Balances.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Balances[j] > Balances[j + 1])
                    {
                        int tmp = Balances[j];
                        Balances[j] = Balances[j + 1];
                        Balances[j + 1] = tmp;
                    }
                }
            }

            GetNameByPlace(Balances, 3, ref winner);
            GetNameByPlace(Balances, 2, ref second);
            GetNameByPlace(Balances, 1, ref third);
            GetNameByPlace(Balances, 0, ref fourth);

            return winner + "," + second + "," + third + "," + fourth + ",";
        }

        public void GameOver()
        {
            EndResult end = new EndResult(ResultAddvertisement(player, robot1, robot2, robot3));            
            if (end.ShowDialog() == DialogResult.OK)
            {
                m_BoardGameForm.Close();
                Application.Exit();
            }
        }

        private string GetNameByPlace(int[] balances,int index, ref string name) {

            if (balances[index] == player.Balance)
            {
                name = player.Name;
            }
            else if (balances[index] == robot1.Balance)
            {
                name = robot1.Name;
            }
            else if (balances[index] == robot2.Balance)
            {
                name = robot2.Name;
            }
            else
            {
                name = robot3.Name;
            }

            return name;
        }

        public void PlayerGoesIntoFailure()
        {
            player.Loser = true;
            DialogResult dialog = MessageBox.Show("Sajnáljuk " + player.Name + ", vége a játéknak...", "Vesztettél!", MessageBoxButtons.OK);
            if (dialog == DialogResult.OK)
            {
                m_BoardGameForm.Close();
                Application.Exit();
            }
        }

        public void ChangingTheSelectedRobotsBalanceTextValue(Robot robot)
        {
            switch (m_BoardGameForm.CurrentTurn)
            {
                case 1:
                    m_BoardGameForm.Robot1Balance.Text = robot.Balance.ToString();
                    break;
                case 2:
                    m_BoardGameForm.Robot2Balance.Text = robot.Balance.ToString();
                    break;
                case 3:
                    m_BoardGameForm.Robot3Balance.Text = robot.Balance.ToString();
                    break;
                default:
                    throw new Exception("Wrong turn");
            }
        }

        public void RobotGoesIntoFailure(Robot robot)
        {
            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.StartedField && field.FieldOwner == robot)
                {
                    field.StartedField = false;
                    field.FinishedField = true;
                    field.FieldBuildingTurns = 0;
                    m_BoardGameForm.FieldLabel[field.FieldLocation].Text = "Lezárt";
                }
            }

            switch (m_BoardGameForm.CurrentTurn)
            {
                case 1:
                    MessageBox.Show(robot.Name + " kiesett!");
                    ClearLoserFigure(robot);
                    m_BoardGameForm.Robot1Box.Visible = false;
                    robot1.Loser = true;
                    break;

                case 2:
                    MessageBox.Show(robot.Name + " kiesett!");
                    ClearLoserFigure(robot);
                    m_BoardGameForm.Robot2Box.Visible = false;
                    robot2.Loser = true;
                    break;

                case 3:
                    MessageBox.Show(robot.Name + " kiesett!");
                    ClearLoserFigure(robot);
                    m_BoardGameForm.Robot3Box.Visible = false;
                    robot3.Loser = true;
                    break;
                default:
                    throw new Exception("Nincs körön lévő Robot");
            }
        }

        private void ClearLoserFigure(Robot robot)
        {
            switch (robot.FigureColor)
            {
                case Figure.BlueFigure:
                    m_BoardGameForm.BlueFigure.Visible = false;
                    break;
                case Figure.RedFigure:
                    m_BoardGameForm.RedFigure.Visible = false;
                    break;
                case Figure.YellowFigure:
                    m_BoardGameForm.YellowFigure.Visible = false;
                    break;
                case Figure.GreenFigure:
                    m_BoardGameForm.GreenFigure.Visible = false;
                    break;
                default:
                    break;
            }
        }

        public void GiveThePAssiveAbilityForPlayer(int i, Robot OldOwnerRobot)
        {
            if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Kereskedelem.ToString())
            {
                player.CanTrade += 3;
                OldOwnerRobot.CanTrade = 0;

                m_BoardGameForm.Robot1TradeNumber.Visible = false;
                m_BoardGameForm.Robot2TradeNumber.Visible = false;
                m_BoardGameForm.Robot3TradeNumber.Visible = false;

                m_BoardGameForm.TradePropertyCard.Visible = true;
                m_BoardGameForm.TradeNumber.Visible = true;

                m_BoardGameForm.TradeNumber.Text += player.CanTrade;
            }
            else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Régész.ToString())
            {
                player.IsArchaeologist = true;
                OldOwnerRobot.IsArchaeologist = false;
            }
            else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Szárnysegéd.ToString())
            {
                player.HasAFriend = true;
                OldOwnerRobot.HasAFriend = false;
            }
            else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Talpraesett.ToString())
            {
                player.IsPointful = true;
                OldOwnerRobot.IsPointful = false;
            }
            else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Vállalkozó.ToString())
            {
                player.IsEntrepreneur = true;
                OldOwnerRobot.IsEntrepreneur = false;
            }
            else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Óvatos.ToString())
            {
                player.IsCareful = true;
                OldOwnerRobot.IsCareful = false;
            }
            else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Őrangyal.ToString())
            {
                player.HasAGuardien += 1;
                OldOwnerRobot.HasAGuardien = 0;
            }
        }

        private void GetDatabaseData()
        {
            player.MyPassiveCards = new List<CardModelFromDatabase.PassiveCard>();
            robot1.MyPassiveCards = new List<CardModelFromDatabase.PassiveCard>();
            robot2.MyPassiveCards = new List<CardModelFromDatabase.PassiveCard>();
            robot3.MyPassiveCards = new List<CardModelFromDatabase.PassiveCard>();

            player.MyPropertyCards = new List<CardModelFromDatabase.PropertyCard>();
            robot1.MyPropertyCards = new List<CardModelFromDatabase.PropertyCard>();
            robot2.MyPropertyCards = new List<CardModelFromDatabase.PropertyCard>();
            robot3.MyPropertyCards = new List<CardModelFromDatabase.PropertyCard>();

            cards.UsedHupszCard = new List<SadCard>();
            cards.UsedHurraCard = new List<HappyCard>();
            cards.PassiveCards = new List<CardModelFromDatabase.PassiveCard>();
            cards.HupszCard = new List<SadCard>();
            cards.HurraCard = new List<HappyCard>();
            cards.PropertyCards = new List<CardModelFromDatabase.PropertyCard>();

            foreach (SadCard card in m_DataAccess.GetAllHupszCard())
            {
                cards.HupszCard.Add(card);
            }

            foreach (HappyCard card in m_DataAccess.GetAllHurraCard())
            {
                cards.HurraCard.Add(card);
            }

            foreach (CardModelFromDatabase.PropertyCard card in m_DataAccess.GetAllPropertyCard())
            {
                cards.PropertyCards.Add(card);
            }

            foreach (CardModelFromDatabase.PassiveCard card in m_DataAccess.GetAllPassiveCard())
            {
                cards.PassiveCards.Add(card);
            }
        }
    }
}
