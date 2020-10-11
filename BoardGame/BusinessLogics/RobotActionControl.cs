using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoardGame.BusinessLogics
{
    public class RobotActionControl
    {
        private BoardGame m_BoardGameForm;
        private GameHandler m_GameHandler;
        private RobotDrawingActivity m_RobotDrawingActivity;
        private UpdateListBoxes m_UpdateListBoxes;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;
        private Level m_Level;

        public RobotActionControl(GameHandler gameHandler, BoardGame boardGame, UpdateListBoxes updateListBoxes, Level level)
        {
            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;
            m_UpdateListBoxes = updateListBoxes;
            m_Level = level;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;

            m_RobotDrawingActivity = new RobotDrawingActivity(this,m_GameHandler,m_BoardGameForm,m_UpdateListBoxes, m_Level);
        }

        public void RobotAfterChangeHisPositionAction(Robot robot)
        {
            int Bill = 0;
            string ownedText = "";
            bool ownedField = false;

            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.FieldLocation == robot.CurrentPosition)
                {
                    ownedField = true;

                    if (field.StartedField && field.FieldOwner != robot)
                    {
                        Bill = (int)Math.Round(field.FieldFinishedValue * 0.25);

                        if (TheFieldOwnerIsLost(field))
                        {
                            ownedText = "Nem kell fizetned, mivel" + field.FieldOwner.Name + " már kiesett!";
                        }
                        else
                        {
                            if (robot.HasAFriend)
                            {
                                MessageBox.Show("Szerencsés vagy, hogy ilyen barátod van! Kifizette helyetted.", robot.Name);
                            }
                            else
                            {
                                ChangeRobotBalanceIfItIsNeccessery(field, robot, Bill, ref ownedText);
                            }
                        }

                        UpdatePlayerssBalance(field, Bill);
                    }
                    else if (!field.StartedField && field.FieldOwner != robot)
                    {
                        Bill = (int)Math.Round(field.FieldFinishedValue * 0.4);

                        if (TheFieldOwnerIsLost(field))
                        {
                            ownedText = "Nem kell fizetned, mivel" + field.FieldOwner + " már kiesett!";
                        }
                        else
                        {
                            ChangeRobotBalanceIfItIsNeccessery(field, robot, Bill, ref ownedText);
                        }

                        UpdatePlayerssBalance(field, Bill);
                    }
                    else if (field.StartedField && field.FieldOwner == robot)
                    {
                        ownedText = "Sajnáljuk " + robot.Name + ", ezen a teruleten már ásatást végzel.";
                    }
                    else if (!field.StartedField && field.FieldOwner == robot)
                    {
                        ownedText = "Sajnáljuk " + robot.Name + ", ezen a teruleten már ásatást végeztél.";
                    }

                }
            }

            if (ownedField)
            {
                MessageBox.Show(ownedText, "Magánterület!");
            }
            else
            {
                FourCornerAction(robot);
            }
        }

        private bool TheFieldOwnerIsLost(Field field)
        {
            if ((field.FieldOwner == robot1 && robot1.Loser) || (field.FieldOwner == robot2 && robot2.Loser) || (field.FieldOwner == robot3 && robot3.Loser))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ChangeRobotBalanceIfItIsNeccessery(Field field, Robot robot, int Bill, ref string ownedText)
        {
            robot.Balance -= Bill;
            ownedText = "Sajnáljuk " + robot.Name + ", ezen a teruleten " + field.FieldOwner.Name + " már ásatást végez. Az itt tartózkodásod nem ingyenes. A tulajdonosnak a terület jövedelmének(" + field.FieldFinishedValue + " $) a 25%-t ki kell fizetned, ami " + Bill + " $";

            // if the robot ballance less then 0 after the bill
            if (robot.Balance < 0)
            {
                if (robot.HasAGuardien > 0)
                {
                    robot.HasAGuardien -= 1;
                    MessageBox.Show("Az őrangyalod most megmentett, de többször már nem tud!", robot.Name);
                    robot.Balance += Bill;
                    m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);
                }
                else
                {
                    m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);
                    m_GameHandler.RobotGoesIntoFailure(robot);
                }
            }
            else
            {
                m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);
            }
        }

        private void UpdatePlayerssBalance(Field field, int Bill)
        {
            if (field.FieldOwner == robot1 && !robot1.Loser)
            {
                robot1.Balance += Bill;
                m_BoardGameForm.Robot1Balance.Text = robot1.Balance.ToString();
            }
            else if (field.FieldOwner == robot2 && !robot2.Loser)
            {
                robot2.Balance += Bill;
                m_BoardGameForm.Robot2Balance.Text = robot2.Balance.ToString();
            }
            else if (field.FieldOwner == robot3 && !robot3.Loser)
            {
                robot3.Balance += Bill;
                m_BoardGameForm.Robot3Balance.Text = robot3.Balance.ToString();
            }
            else
            {
                player.Balance += Bill;
                m_BoardGameForm.PlayerBalance.Text = player.Balance.ToString();
            }
        }

        private void FourCornerAction(Robot robot)
        {
            if (robot.CurrentPosition != 0 && robot.CurrentPosition != 10 && robot.CurrentPosition != 20 && robot.CurrentPosition != 30)
            {
                m_RobotDrawingActivity.RobotDrawingACard(robot);
            }
            else
            {
                if (robot.CurrentPosition == 10)
                {
                    if (robot.IsPointful)
                    {
                        MessageBox.Show("Talpraesettségednek köszönhetően megúsztad a hegyomlást.", robot.Name);
                    }
                    else
                    {
                        MessageBox.Show(robot.Name + " beszorult, a mentőalakulatok mindjárt itt vannak. Kimarad 2 körböl.");
                        RobotCantRoll(robot, 2);
                    }
                }
                else if (robot.CurrentPosition == 20)
                {
                    MessageBox.Show(robot.Name + " éhes volt, tart egy kis szünetet. Kimarad egy körböl.");
                    RobotCantRoll(robot, 1);
                }
                else if (robot.CurrentPosition == 30)
                {
                    MessageBox.Show(robot.Name + " szórakozott kicsit, dob egyet a két kockával és annyit vissza kell lépjen.");
                    m_GameHandler.JumpWithTheFigureToTheNextPosition(-m_GameHandler.RollTheDice());
                    RobotAfterChangeHisPositionAction(robot);
                }
            }
        }

        private void RobotCantRoll(Robot robot,int howManyTurnHeWillMiss)
        {
            robot.CanNotRollForXTurn += howManyTurnHeWillMiss;

            switch (m_BoardGameForm.CurrentTurn)
            {
                case 1:

                    m_BoardGameForm.Robot1CanRoll.Text = robot.CanNotRollForXTurn + "-körig kimarad!";
                    m_BoardGameForm.Robot1CanRoll.ForeColor = Color.Red;

                    break;
                case 2:

                    m_BoardGameForm.Robot2CanRoll.Text = robot.CanNotRollForXTurn + "-körig kimarad!";
                    m_BoardGameForm.Robot2CanRoll.ForeColor = Color.Red;

                    break;
                case 3:

                    m_BoardGameForm.Robot3CanRoll.Text = robot.CanNotRollForXTurn + "-körig kimarad!";
                    m_BoardGameForm.Robot3CanRoll.ForeColor = Color.Red;

                    break;
                default:
                    throw new Exception("Wrong turn");
            }
        }
    }
}
