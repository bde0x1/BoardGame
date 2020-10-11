using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System.Drawing;
using System.Windows.Forms;

namespace BoardGame.BusinessLogics
{
    public class RobotsTurn
    {
        private BoardGame m_BoardGameForm;
        private GameHandler m_GameHandler;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;
        private RobotActionControl m_RobotActionControl;
        private RobotAbilityHelper m_RobotAbilityHelper;
        private NewTurnHelper m_NewTurnHelper;
        private CalculateNextPlayer m_CalculateNextPlayer;
        private UpdateListBoxes m_UpdateListBoxes;
        private Level m_Level;
        private string turnText = " te vagy a soronlévő játékos! Dobj a kockával!";

        public RobotsTurn(GameHandler gameHandler, BoardGame boardGame, NewTurnHelper newTurnHelper, UpdateListBoxes updateListBoxes, Level level)
        {
            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;
            m_NewTurnHelper = newTurnHelper;
            m_UpdateListBoxes = updateListBoxes;
            m_Level = level;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;

            m_CalculateNextPlayer = new CalculateNextPlayer(this, m_GameHandler, m_BoardGameForm, m_NewTurnHelper);
            m_RobotActionControl = new RobotActionControl(m_GameHandler, m_BoardGameForm, m_UpdateListBoxes,m_Level);
            m_RobotAbilityHelper = new RobotAbilityHelper(m_GameHandler, m_BoardGameForm, m_UpdateListBoxes);
        }

        public void RobotTurns()
        {
            //robot 1 turn
            if (robot1.PlayerNumber == m_BoardGameForm.CurrentTurn && robot1.CanNotRollForXTurn == 0 && robot1.Loser == false) //green
            {
                m_BoardGameForm.TurnText.Text = robot1.Name + turnText;

                if (robot1.CanTrade > 0)
                {
                    m_RobotAbilityHelper.RobotWouldLikeToTradeOrNot(robot1);
                }

                if (robot1.CanStealAPassiveAbility > 0)
                {
                    m_RobotAbilityHelper.RobotWouldLikeToStealAPassiveAbilityOrNot(robot1);
                }

                m_GameHandler.JumpWithTheFigureToTheNextPosition(m_GameHandler.RollTheDice());

                m_RobotActionControl.RobotAfterChangeHisPositionAction(robot1);

                m_RobotAbilityHelper.IfTheRobotIsAnEntrepreneur(robot1);

                if (robot2.CanNotRollForXTurn > 0 && robot2.Loser == false)
                {
                    robot2.CanNotRollForXTurn -= 1;

                    MessageBox.Show(robot2.Name + " kimaradt, " + robot3.Name + " következik.");

                    if (robot2.CanNotRollForXTurn == 0)
                    {
                        m_BoardGameForm.Robot2CanRoll.Text = "Léphetsz!";
                        m_BoardGameForm.Robot2CanRoll.ForeColor = Color.Green;
                    }
                    else
                    {
                        m_BoardGameForm.Robot2CanRoll.Text = robot2.CanNotRollForXTurn + "-körig kimarad!";
                    }

                    if (robot3.CanNotRollForXTurn > 0 && robot3.Loser == false)
                    {
                        robot3.CanNotRollForXTurn -= 1;

                        MessageBox.Show(robot3.Name + " kimaradt, " + player.Name + " következik.");

                        if (robot3.CanNotRollForXTurn == 0)
                        {
                            m_BoardGameForm.Robot3CanRoll.Text = "Léphetsz!";
                            m_BoardGameForm.Robot3CanRoll.ForeColor = Color.Green;
                        }
                        else
                        {
                            m_BoardGameForm.Robot3CanRoll.Text = robot3.CanNotRollForXTurn + "-körig kimarad!";
                        }

                        if (player.CanNotRollForXTurn > 0 && player.Loser == false)
                        {
                            player.CanNotRollForXTurn -= 1;

                            MessageBox.Show(player.Name + " kimaradt, " + robot1.Name + " következik.");

                            if (player.CanNotRollForXTurn == 0)
                            {
                                m_BoardGameForm.PlayerCanRoll.Text = "Léphetsz!";
                                m_BoardGameForm.PlayerCanRoll.ForeColor = Color.Green;
                            }
                            else
                            {
                                m_BoardGameForm.PlayerCanRoll.Text = player.CanNotRollForXTurn + "-körig kimarad!";
                            }

                            if (robot1.CanNotRollForXTurn > 0 && robot1.Loser == false)
                            {
                                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot1();
                            }
                            else if (robot1.Loser == true)
                            {
                                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot2();
                            }
                            else
                            {
                                MessageBox.Show("Sajnos most kimaradsz, de talán majd legközelebb.", "Kimaradsz");

                                m_NewTurnHelper.NewTurnWithRobot1();
                                RobotTurns();
                            }
                        }
                        else if (player.Loser == true)
                        {
                            m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot1();
                        }
                        else
                        {
                            m_NewTurnHelper.NewTurnWithPlayer();
                        }
                    }
                    else if (robot3.Loser == true)
                    {
                        m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithThePlayer();
                    }
                    else
                    {
                        m_NewTurnHelper.NewTurnWithRobot3();
                    }
                }
                else if (robot2.Loser == true)
                {
                    m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot3();
                }
                else
                {
                    m_NewTurnHelper.NewTurnWithRobot2();
                }
            }
            else if (robot1.PlayerNumber == m_BoardGameForm.CurrentTurn && robot1.Loser == true)
            {
                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot2();
            }

            System.Threading.Thread.Sleep(200);

            //robot 2 turn
            if (robot2.PlayerNumber == m_BoardGameForm.CurrentTurn && robot2.CanNotRollForXTurn == 0 && robot2.Loser == false) //yellow
            {
                m_BoardGameForm.TurnText.Text = robot2.Name + turnText;

                if (robot2.CanTrade > 0)
                {
                    m_RobotAbilityHelper.RobotWouldLikeToTradeOrNot(robot2);
                }

                if (robot2.CanStealAPassiveAbility > 0)
                {
                    m_RobotAbilityHelper.RobotWouldLikeToStealAPassiveAbilityOrNot(robot2);
                }

                m_GameHandler.JumpWithTheFigureToTheNextPosition(m_GameHandler.RollTheDice());

                m_RobotActionControl.RobotAfterChangeHisPositionAction(robot2);

                m_RobotAbilityHelper.IfTheRobotIsAnEntrepreneur(robot2);

                if (robot3.CanNotRollForXTurn > 0 && robot3.Loser == false)
                {
                    robot3.CanNotRollForXTurn -= 1;

                    MessageBox.Show(robot3.Name + " kimaradt, " + player.Name + " következik.");

                    if (robot3.CanNotRollForXTurn == 0)
                    {
                        m_BoardGameForm.Robot3CanRoll.Text = "Léphetsz!";
                        m_BoardGameForm.Robot3CanRoll.ForeColor = Color.Green;
                    }
                    else
                    {
                        m_BoardGameForm.Robot3CanRoll.Text = robot3.CanNotRollForXTurn + "-körig kimarad!";
                    }

                    if (player.CanNotRollForXTurn > 0 && player.Loser == false)
                    {
                        player.CanNotRollForXTurn -= 1;

                        MessageBox.Show(player.Name + " kimaradt, " + robot1.Name + " következik.");

                        if (player.CanNotRollForXTurn == 0)
                        {
                            m_BoardGameForm.PlayerCanRoll.Text = "Léphetsz!";
                            m_BoardGameForm.PlayerCanRoll.ForeColor = Color.Green;
                        }
                        else
                        {
                            m_BoardGameForm.PlayerCanRoll.Text = player.CanNotRollForXTurn + "-körig kimarad!";
                        }

                        if (robot1.CanNotRollForXTurn > 0 && robot1.Loser == false)
                        {
                            robot1.CanNotRollForXTurn -= 1;

                            MessageBox.Show(robot1.Name + " kimaradt, " + robot2.Name + " következik.");

                            if (robot1.CanNotRollForXTurn == 0)
                            {
                                m_BoardGameForm.Robot1CanRoll.Text = "Léphetsz!";
                                m_BoardGameForm.Robot1CanRoll.ForeColor = Color.Green;
                            }
                            else
                            {
                                m_BoardGameForm.Robot1CanRoll.Text = robot1.CanNotRollForXTurn + "-körig kimarad!";
                            }

                            if (robot2.CanNotRollForXTurn > 0 && robot2.Loser == false)
                            {
                                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot2();
                            }
                            else if (robot2.Loser == true)
                            {
                                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot3();
                            }
                            else
                            {
                                m_NewTurnHelper.NewTurnWithRobot2();
                                RobotTurns();
                            }

                        }
                        else if (robot1.Loser == true)
                        {
                            m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot2();
                        }
                        else
                        {
                            MessageBox.Show("Sajnos most kimaradsz, de talán majd legközelebb.", "Kimaradsz");

                            m_NewTurnHelper.NewTurnWithRobot1();
                            RobotTurns();
                        }
                    }
                    else if (player.Loser == true)
                    {
                        m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot1();
                    }
                    else
                    {
                        m_NewTurnHelper.NewTurnWithPlayer();
                    }
                }
                else if (robot3.Loser == true)
                {
                    m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithThePlayer();
                }
                else
                {
                    m_NewTurnHelper.NewTurnWithRobot3();
                }
            }
            else if (robot2.PlayerNumber == m_BoardGameForm.CurrentTurn && robot2.Loser == true)
            {
                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot3();
            }

            System.Threading.Thread.Sleep(200);

            //robot 3 turn
            if (robot3.PlayerNumber == m_BoardGameForm.CurrentTurn && robot3.CanNotRollForXTurn == 0 && robot3.Loser == false) //red
            {
                m_BoardGameForm.TurnText.Text = robot3.Name + turnText;

                if (robot3.CanTrade > 0)
                {
                    m_RobotAbilityHelper.RobotWouldLikeToTradeOrNot(robot3);
                }

                if (robot3.CanStealAPassiveAbility > 0)
                {
                    m_RobotAbilityHelper.RobotWouldLikeToStealAPassiveAbilityOrNot(robot3);
                }

                m_GameHandler.JumpWithTheFigureToTheNextPosition(m_GameHandler.RollTheDice());

                m_RobotActionControl.RobotAfterChangeHisPositionAction(robot3);

                m_RobotAbilityHelper.IfTheRobotIsAnEntrepreneur(robot3);

                if (player.CanNotRollForXTurn > 0 && player.Loser == false)
                {
                    player.CanNotRollForXTurn -= 1;

                    if (player.CanNotRollForXTurn == 0)
                    {
                        m_BoardGameForm.PlayerCanRoll.Text = "Léphetsz!";
                        m_BoardGameForm.PlayerCanRoll.ForeColor = Color.Green;
                    }
                    else
                    {
                        m_BoardGameForm.PlayerCanRoll.Text = player.CanNotRollForXTurn + "-körig kimaradsz!";
                    }

                    if (robot1.CanNotRollForXTurn > 0 && robot1.Loser == false)
                    {
                        robot1.CanNotRollForXTurn -= 1;

                        MessageBox.Show(robot1.Name + " kimaradt, " + robot2.Name + " következik.");

                        if (robot1.CanNotRollForXTurn == 0)
                        {
                            m_BoardGameForm.Robot1CanRoll.Text = "Léphetsz!";
                            m_BoardGameForm.Robot1CanRoll.ForeColor = Color.Green;
                        }
                        else
                        {
                            m_BoardGameForm.Robot1CanRoll.Text = robot1.CanNotRollForXTurn + "-körig kimarad!";
                        }

                        if (robot2.CanNotRollForXTurn > 0 && robot2.Loser == false)
                        {
                            robot2.CanNotRollForXTurn -= 1;

                            MessageBox.Show(robot2.Name + " kimaradt, " + robot3.Name + " következik.");

                            if (robot2.CanNotRollForXTurn == 0)
                            {
                                m_BoardGameForm.Robot2CanRoll.Text = "Léphetsz!";
                                m_BoardGameForm.Robot2CanRoll.ForeColor = Color.Green;
                            }
                            else
                            {
                                m_BoardGameForm.Robot2CanRoll.Text = robot2.CanNotRollForXTurn + "-körig kimarad!";
                            }

                            if (robot3.CanNotRollForXTurn > 0 && robot3.Loser == false)
                            {
                                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot3();
                            }
                            else if (robot3.Loser == false)
                            {
                                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithThePlayer();
                            }
                            else
                            {
                                m_NewTurnHelper.NewTurnWithRobot3();
                                RobotTurns();
                            }
                        }
                        else if (robot2.Loser == true)
                        {
                            m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot3();
                        }
                        else
                        {
                            m_NewTurnHelper.NewTurnWithRobot2();
                            RobotTurns();
                        }
                    }
                    else if (robot1.Loser == true)
                    {
                        m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot2();
                    }
                    else
                    {
                        MessageBox.Show("Sajnos most kimaradsz, de talán majd legközelebb.", "Kimaradsz");

                        m_NewTurnHelper.NewTurnWithRobot1();
                        RobotTurns();
                    }
                }
                else if (player.Loser == true)
                {
                    m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot1();
                }
                else
                {
                    m_NewTurnHelper.NewTurnWithPlayer();
                }
            }
            else if (robot3.PlayerNumber == m_BoardGameForm.CurrentTurn && robot3.Loser == true)
            {
                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithThePlayer();
            }

            System.Threading.Thread.Sleep(200);

            if (m_BoardGameForm.CurrentTurn > 0)
            {
                RobotTurns();
            }

            if (robot1.Loser && robot2.Loser && robot3.Loser)
            {
                MessageBox.Show("Gratulálok te nyertél!");
                m_BoardGameForm.Close();
                Application.Exit();
            }
        }
    }
}
