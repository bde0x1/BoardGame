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
            if (robot1.PlayerNumber == m_BoardGameForm.CurrentTurn && robot1.CanNotRollForXTurn == 0 && robot1.Loser == false)
            {
                CurrentRobotPlayHisTurn(robot1);

                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot2();
            }
            else if (robot1.PlayerNumber == m_BoardGameForm.CurrentTurn && robot1.Loser == true)
            {
                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot2();
            }

            System.Threading.Thread.Sleep(200);

            //robot 2 turn
            if (robot2.PlayerNumber == m_BoardGameForm.CurrentTurn && robot2.CanNotRollForXTurn == 0 && robot2.Loser == false)
            {
                CurrentRobotPlayHisTurn(robot2);

                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot3();
            }
            else if (robot2.PlayerNumber == m_BoardGameForm.CurrentTurn && robot2.Loser == true)
            {
                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithRobot3();
            }

            System.Threading.Thread.Sleep(200);

            //robot 3 turn
            if (robot3.PlayerNumber == m_BoardGameForm.CurrentTurn && robot3.CanNotRollForXTurn == 0 && robot3.Loser == false)
            {
                CurrentRobotPlayHisTurn(robot3);

                m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithThePlayer();
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

        private void CurrentRobotPlayHisTurn(Robot robot)
        {
            m_BoardGameForm.TurnText.Text = robot.Name + turnText;

            if (robot.CanTrade > 0)
            {
                m_RobotAbilityHelper.RobotWouldLikeToTradeOrNot(robot);
            }

            if (robot.CanStealAPassiveAbility > 0)
            {
                m_RobotAbilityHelper.RobotWouldLikeToStealAPassiveAbilityOrNot(robot);
            }

            m_GameHandler.JumpWithTheFigureToTheNextPosition(m_GameHandler.RollTheDice());

            m_RobotActionControl.RobotAfterChangeHisPositionAction(robot);

            m_RobotAbilityHelper.IfTheRobotIsAnEntrepreneur(robot);
        }
    }
}
