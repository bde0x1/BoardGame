using BoardGame.Domain_Model;
using System.Drawing;
using System.Windows.Forms;

namespace BoardGame.BusinessLogics
{
    public class CalculateNextPlayer
    {
        private BoardGame m_BoardGameForm;
        private GameHandler m_GameHandler;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;
        private NewTurnHelper m_NewTurnHelper;
        private RobotsTurn m_RobotTurns;

        public CalculateNextPlayer(RobotsTurn robotsTurn,GameHandler gameHandler, BoardGame boardGame, NewTurnHelper newTurnHelper)
        {
            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;
            m_NewTurnHelper = newTurnHelper;
            m_RobotTurns = robotsTurn;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;
        }

        // Checking who is the next;
        public void GetTheNextPlayersTurnStartingWithThePlayer()
        {
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

                        MessageBox.Show(robot2.Name + "kimaradt, " + robot3.Name + " következik.");

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

                            MessageBox.Show(robot3.Name + "kimaradt, " + player.Name + " következik.");

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
                                GetTheNextPlayersTurnStartingWithThePlayer();
                            }
                            else if (player.Loser == true)
                            {
                                GetTheNextPlayersTurnStartingWithRobot1();
                            }
                            else
                            {
                                m_NewTurnHelper.NewTurnWithPlayer();
                            }
                        }
                        else if (robot3.Loser == true)
                        {
                            GetTheNextPlayersTurnStartingWithThePlayer();
                        }
                        else
                        {
                            m_NewTurnHelper.NewTurnWithRobot3();
                        }
                    }
                    else if (robot2.Loser == true)
                    {
                        GetTheNextPlayersTurnStartingWithRobot3();
                    }
                    else
                    {
                        m_NewTurnHelper.NewTurnWithRobot2();
                    }
                }
                else if (robot1.Loser == true)
                {
                    GetTheNextPlayersTurnStartingWithRobot2();
                }
                else
                {
                    MessageBox.Show("Sajnos most kimaradsz, de talán majd legközelebb.", "Kimaradsz");

                    m_NewTurnHelper.NewTurnWithRobot1();
                }
            }
            else if (player.Loser == true)
            {
                GetTheNextPlayersTurnStartingWithRobot1();
            }
            else
            {
                m_NewTurnHelper.NewTurnWithPlayer();
            }
        }
        public void GetTheNextPlayersTurnStartingWithRobot1()
        {
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
                                GetTheNextPlayersTurnStartingWithRobot1();
                            }
                            else if (robot1.Loser == true)
                            {
                                GetTheNextPlayersTurnStartingWithRobot2();
                            }
                            else
                            {
                                m_NewTurnHelper.NewTurnWithRobot1();
                            }
                        }
                        else if (player.Loser == true)
                        {
                            GetTheNextPlayersTurnStartingWithRobot1();
                        }
                        else
                        {
                            m_NewTurnHelper.NewTurnWithPlayer();
                        }
                    }
                    else if (robot3.Loser == true)
                    {
                        GetTheNextPlayersTurnStartingWithThePlayer();
                    }
                    else
                    {
                        m_NewTurnHelper.NewTurnWithRobot3();
                        m_RobotTurns.RobotTurns();
                    }
                }
                else if (robot2.Loser == true)
                {
                    GetTheNextPlayersTurnStartingWithRobot3();
                }
                else
                {
                    m_NewTurnHelper.NewTurnWithRobot2();
                    m_RobotTurns.RobotTurns();
                }
            }
            else if (robot1.Loser == true)
            {
                GetTheNextPlayersTurnStartingWithRobot2();
            }
            else
            {
                m_NewTurnHelper.NewTurnWithRobot1();
            }
        }
        public void GetTheNextPlayersTurnStartingWithRobot2()
        {
            if (robot2.CanNotRollForXTurn > 0 && robot2.Loser == false)
            {
                robot2.CanNotRollForXTurn -= 1;

                MessageBox.Show(robot2.Name + "kimaradt, " + robot3.Name + " következik.");

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

                    MessageBox.Show(robot3.Name + "kimaradt, " + player.Name + " következik.");

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

                        MessageBox.Show(player.Name + "kimaradt, " + robot1.Name + " következik.");

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
                                GetTheNextPlayersTurnStartingWithRobot2();
                            }
                            else if (robot2.Loser == true)
                            {
                                GetTheNextPlayersTurnStartingWithRobot3();
                            }
                            else
                            {
                                m_NewTurnHelper.NewTurnWithRobot2();
                            }
                        }
                        else if (robot1.Loser == true)
                        {
                            GetTheNextPlayersTurnStartingWithRobot2();
                        }
                        else
                        {
                            MessageBox.Show("Sajnos most kimaradsz, de talán majd legközelebb.", "Kimaradsz");

                            m_NewTurnHelper.NewTurnWithRobot1();
                            m_RobotTurns.RobotTurns();
                        }
                    }
                    else if (player.Loser == true)
                    {
                        GetTheNextPlayersTurnStartingWithRobot1();
                    }
                    else
                    {
                        m_NewTurnHelper.NewTurnWithPlayer();
                    }
                }
                else if (robot3.Loser == true)
                {
                    GetTheNextPlayersTurnStartingWithThePlayer();
                }
                else
                {
                    m_NewTurnHelper.NewTurnWithRobot3();
                    m_RobotTurns.RobotTurns();
                }
            }
            else if (robot2.Loser == true)
            {
                GetTheNextPlayersTurnStartingWithRobot3();
            }
            else
            {
                m_NewTurnHelper.NewTurnWithRobot2();
            }
        }
        public void GetTheNextPlayersTurnStartingWithRobot3()
        {
            if (robot3.CanNotRollForXTurn > 0 && robot3.Loser == false)
            {
                robot3.CanNotRollForXTurn -= 1;

                MessageBox.Show(robot3.Name + "kimaradt, " + player.Name + " következik.");

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

                    MessageBox.Show(player.Name + "kimaradt, " + robot1.Name + " következik.");

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
                            robot2.CanNotRollForXTurn -= 1;

                            MessageBox.Show(robot2.Name + "kimaradt, " + robot3.Name + " következik.");

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
                                GetTheNextPlayersTurnStartingWithRobot3();
                            }
                            else if (robot3.Loser == true)
                            {
                                GetTheNextPlayersTurnStartingWithThePlayer();
                            }
                            else
                            {
                                m_NewTurnHelper.NewTurnWithRobot3();
                            }
                        }
                        else if (robot2.Loser == true)
                        {
                            GetTheNextPlayersTurnStartingWithRobot3();
                        }
                        else
                        {
                            m_NewTurnHelper.NewTurnWithRobot2();
                            m_RobotTurns.RobotTurns();
                        }
                    }
                    else if (robot1.Loser == true)
                    {
                        GetTheNextPlayersTurnStartingWithRobot2();
                    }
                    else
                    {
                        MessageBox.Show("Sajnos most kimaradsz, de talán majd legközelebb.", "Kimaradsz");

                        m_NewTurnHelper.NewTurnWithRobot1();
                        m_RobotTurns.RobotTurns();
                    }
                }
                else if (player.Loser == true)
                {
                    GetTheNextPlayersTurnStartingWithRobot1();
                }
                else
                {
                    m_NewTurnHelper.NewTurnWithPlayer();
                }
            }
            else if (robot3.Loser == true)
            {
                GetTheNextPlayersTurnStartingWithThePlayer();
            }
            else
            {
                m_NewTurnHelper.NewTurnWithRobot3();
            }
        }
    }
}
