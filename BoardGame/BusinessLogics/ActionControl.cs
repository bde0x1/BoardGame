using BoardGame.Domain_Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoardGame.BusinessLogics
{
    public class ActionControl
    {
        private BoardGame m_BoardGameForm;
        private GameHandler m_GameHandler;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;

        public ActionControl(GameHandler gameHandler, BoardGame boardGame)
        {
            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;
        }

        public void PlayerAfterChangeHisPositionAction()
        {
            int Bill = 0;
            string ownedText = "";
            bool ownedField = false;

            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.FieldLocation == player.CurrentPosition)
                {
                    ownedField = true;

                    if (field.StartedField && field.FieldOwner != player)
                    {
                        Bill = (int)Math.Round(field.FieldFinishedValue * 0.25);

                        if (TheFieldOwnerIsLost(field))
                        {
                            ownedText = "Nem kell fizetned, mivel" + field.FieldOwner.Name + " már kiesett!";
                        }
                        else
                        {
                            if (player.HasAFriend)
                            {
                                MessageBox.Show("Szerencsés vagy, hogy ilyen barátod van! Kifizette helyetted.", player.Name);
                            }
                            else
                            {
                                ChangePlayerBalanceIfItIsNeccessery(field, Bill, ref ownedText);
                            }
                        }

                        UpdateRobotsBalance(field, Bill);
                    }
                    else if (!field.StartedField && field.FieldOwner != player)
                    {
                        Bill = (int)Math.Round(field.FieldFinishedValue * 0.4);

                        if (TheFieldOwnerIsLost(field))
                        {
                            ownedText = "Nem kell fizetned, mivel" + field.FieldOwner.Name + " már kiesett!";
                        }
                        else
                        {
                            ChangePlayerBalanceIfItIsNeccessery(field, Bill, ref ownedText);
                        }

                        UpdateRobotsBalance(field, Bill);
                    }
                    else if (field.StartedField && field.FieldOwner == player)
                    {
                        ownedText = "Sajnáljuk " + player.Name + ", ezen a teruleten már ásatást végzel.";
                    }
                    else if (!field.StartedField && field.FieldOwner == player)
                    {
                        ownedText = "Sajnáljuk " + player.Name + ", ezen a teruleten már ásatást végeztél.";
                    }
                }
            }

            if (ownedField)
            {
                MessageBox.Show(ownedText, player.Name + " ez Magánterület!");

                m_BoardGameForm.DrawTheCard.Enabled = false;
                m_BoardGameForm.ThrowWithTheCubes.Enabled = false;
                m_BoardGameForm.EndTurn.Enabled = true;
            }
            else
            {
                FourCornerAction();
            }
        }

        private void ButtonEnableChangeForTheEndOfTheTurn()
        {
            m_BoardGameForm.EndTurn.Enabled = true;
            m_BoardGameForm.ThrowWithTheCubes.Enabled = false;
        }

        private void UpdateRobotsBalance(Field field, int Bill)
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
        }

        private void ChangePlayerBalanceIfItIsNeccessery(Field field, int Bill, ref string ownedText)
        {
            player.Balance -= Bill;
            ownedText = "Sajnáljuk " + player.Name + ", ezen a teruleten " + field.FieldOwner.Name + " már ásatást végez. Az itt tartózkodásod nem ingyenes. A tulajdonosnak a terület jövedelmének(" + field.FieldFinishedValue + " $) a 25%-t ki kell fizetned, ami " + Bill + " $";

            // if the player ballance less then 0 after the bill
            if (player.Balance < 0)
            {
                if (player.HasAGuardien > 0)
                {
                    player.HasAGuardien -= 1;
                    MessageBox.Show("Az őrangyalod most megmentett, de többször már nem tud!", player.Name);
                    player.Balance += Bill;
                    m_BoardGameForm.PlayerBalance.Text = player.Balance.ToString();
                }
                else
                {
                    m_BoardGameForm.PlayerBalance.Text = player.Balance.ToString();
                    m_GameHandler.PlayerGoesIntoFailure();
                }
            }
            else
            {
                m_BoardGameForm.PlayerBalance.Text = player.Balance.ToString();
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

        private void FourCornerAction()
        {
            if (player.CurrentPosition != 0 && player.CurrentPosition != 10 && player.CurrentPosition != 20 && player.CurrentPosition != 30)
            {
                m_BoardGameForm.DrawTheCard.Enabled = true;
                m_BoardGameForm.ThrowWithTheCubes.Enabled = false;
                m_BoardGameForm.EndTurn.Enabled = false;
            }
            else
            {
                if (player.CurrentPosition == 0)
                {
                    ButtonEnableChangeForTheEndOfTheTurn();
                }
                else if (player.CurrentPosition == 10)
                {
                    if (player.IsPointful)
                    {
                        MessageBox.Show("Talpraesettségednek köszönhetően megúsztad a hegyomlást.", player.Name);
                        ButtonEnableChangeForTheEndOfTheTurn();
                    }
                    else
                    {
                        MessageBox.Show("Beszorultál, a mentőalakulatok mindjárt itt vannak. Kimaradsz 2 körböl.", player.Name);
                        PlayerCanNotRoll(2);
                    }
                }
                else if (player.CurrentPosition == 20)
                {
                    MessageBox.Show("Enni is kell valamit, tarts egy kis szünetet. Kimaradsz egy körböl.", player.Name);
                    PlayerCanNotRoll(1);
                }
                else if (player.CurrentPosition == 30)
                {
                    MessageBox.Show("Szórakoztál kicsit, dobj egyet a két kockával és annyit vissza kell lépned.", player.Name);
                    m_GameHandler.JumpWithTheFigureToTheNextPosition(-m_GameHandler.RollTheDice());
                    PlayerAfterChangeHisPositionAction();
                }
            }
        }

        private void PlayerCanNotRoll(int howManyTurnHeWillMiss)
        {
            player.CanNotRollForXTurn += howManyTurnHeWillMiss;
            m_BoardGameForm.PlayerCanRoll.Text = player.CanNotRollForXTurn + "-körig kimaradsz!";
            m_BoardGameForm.PlayerCanRoll.ForeColor = Color.Red;
            ButtonEnableChangeForTheEndOfTheTurn();
        }
    }
}
