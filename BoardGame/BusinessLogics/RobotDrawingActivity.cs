using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoardGame.BusinessLogics
{
    public class RobotDrawingActivity
    {
        private BoardGame m_BoardGameForm;
        private GameHandler m_GameHandler;
        private RobotActionControl m_RobotActionControl;

        private UpdateListBoxes m_UpdateListBoxes;
        private Level m_Level;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;

        public RobotDrawingActivity(RobotActionControl robotActionControl, GameHandler gameHandler, BoardGame boardGame, UpdateListBoxes updateListBoxes, Level level)
        {

            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;            
            m_RobotActionControl = robotActionControl;
            m_UpdateListBoxes = updateListBoxes;
            m_Level = level;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;
        }

        public void RobotDrawingACard(Robot robot)
        {
            //Drawing a passive card
            if (robot.CurrentPosition == 5 || robot.CurrentPosition == 15 || robot.CurrentPosition == 25 || robot.CurrentPosition == 35)
            {
                PassiveCard passiveCardForm = new PassiveCard(m_BoardGameForm.CurrentTurn, player, robot1, robot2, robot3, m_GameHandler.cards);
                if (passiveCardForm.ShowDialog() == DialogResult.OK)
                {
                    if (!passiveCardForm.NoPassiveCard)
                    {
                        switch (passiveCardForm.PassiveAbility)
                        {
                            case Abilities.Kereskedelem:

                                robot.CanTrade = passiveCardForm.AbilityValue;
                                switch (m_BoardGameForm.CurrentTurn)
                                {
                                    case 1:

                                        m_BoardGameForm.Robot1TradeNumber.Visible = true;
                                        m_BoardGameForm.Robot1TradeNumber.Text += robot.CanTrade;

                                        break;
                                    case 2:

                                        m_BoardGameForm.Robot2TradeNumber.Visible = true;
                                        m_BoardGameForm.Robot2TradeNumber.Text += robot.CanTrade;

                                        break;
                                    case 3:

                                        m_BoardGameForm.Robot3TradeNumber.Visible = true;
                                        m_BoardGameForm.Robot3TradeNumber.Text += robot.CanTrade;

                                        break;
                                    default:
                                        throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
                                }

                                MessageBox.Show("Mostantól kereskedő lettél! Csak a köröd elején cserélhetsz, azaz ha dobtál már nem teheted meg!", robot.Name);

                                break;
                            case Abilities.Régész:

                                robot.IsArchaeologist = true;
                                MessageBox.Show("Mostantól régész vagy! Ha feltársz valamit az ásatás ideje a felére csökken.", robot.Name);

                                break;
                            case Abilities.Vállalkozó:

                                robot.IsEntrepreneur = true;
                                MessageBox.Show("Mostantól vállalkozó vagy! Miután befejezted a körödet egy bizonyos összeg kerül az egyenlegedre.", robot.Name);

                                break;
                            case Abilities.Óvatos:

                                robot.IsCareful = true;
                                MessageBox.Show("Mostantól óvatos lettél! Minden egyes hupszkártyából húzott balesetet elkerülsz, így nem maradsz ki.", robot.Name);

                                break;
                            case Abilities.Őrangyal:

                                robot.HasAGuardien = passiveCardForm.AbilityValue;
                                MessageBox.Show("Mostantól vane EGY őrangyalod! Ha netán csődbekerülnél ő kisegít.", robot.Name);

                                break;
                            case Abilities.Szárnysegéd:

                                robot.HasAFriend = true;
                                MessageBox.Show("Mostantól van egy szárnysegéded! Jöhet bármi a szárnysegéded fizet mindent, kivéve ha befejezett ásatási területre lépsz. Az neki is drága.", robot.Name);

                                break;
                            case Abilities.Talpraesett:

                                robot.IsPointful = true;
                                MessageBox.Show("Mostantól talpraesett vagy! Mindig tudod mi a dolgod.", robot.Name);

                                break;
                            case Abilities.Tolvaj:

                                robot.CanStealAPassiveAbility = passiveCardForm.AbilityValue;

                                switch (m_BoardGameForm.CurrentTurn)
                                {
                                    case 1:

                                        m_BoardGameForm.Robot1StealNumber.Visible = true;
                                        m_BoardGameForm.Robot1StealNumber.Text += robot.CanStealAPassiveAbility;

                                        break;
                                    case 2:

                                        m_BoardGameForm.Robot2StealNumber.Visible = true;
                                        m_BoardGameForm.Robot2StealNumber.Text += robot.CanStealAPassiveAbility;

                                        break;
                                    case 3:

                                        m_BoardGameForm.Robot3StealNumber.Visible = true;
                                        m_BoardGameForm.Robot3StealNumber.Text += robot.CanStealAPassiveAbility;

                                        break;
                                    default:
                                        throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
                                }

                                MessageBox.Show("Mostantól tolvaj vagy! Ellophatsz egy képességet valakitől.", robot.Name);

                                break;
                            default:
                                throw new Exception("This ability is not exists: " + passiveCardForm.PassiveAbility);
                        }

                        switch (m_BoardGameForm.CurrentTurn)
                        {
                            case 1:
                                m_UpdateListBoxes.UpdateRobot1PassiveCardListBox();
                                break;
                            case 2:
                                m_UpdateListBoxes.UpdateRobot2PassiveCardListBox();
                                break;
                            case 3:
                                m_UpdateListBoxes.UpdateRobot3PassiveCardListBox();
                                break;
                            default:
                                throw new Exception("Nincs körön lévő Robot");
                        }
                    }
                }
            }
            //Drawing a hupsz card
            else if (robot.CurrentPosition == 2 || robot.CurrentPosition == 12 || robot.CurrentPosition == 22 || robot.CurrentPosition == 32)
            {
                HupszCard hupszCardForm = new HupszCard(m_BoardGameForm.CurrentTurn, player, robot1, robot2, robot3, m_GameHandler.cards);
                if (hupszCardForm.ShowDialog() == DialogResult.OK)
                {
                    switch (hupszCardForm.ActionType)
                    {
                        case HupszAction.Jump:
                            m_GameHandler.JumpWithTheFigureToTheNextPosition(-hupszCardForm.Amount);
                            m_RobotActionControl.RobotAfterChangeHisPositionAction(robot);
                            break;
                        case HupszAction.Accident:
                            HupszAccident(robot, hupszCardForm);
                            break;
                        case HupszAction.Balance:
                            HupszBalance(robot, hupszCardForm);
                            break;
                        default:
                            throw new Exception("Nincs ilyen Action");
                    }
                }
            }
            //Drawing a Hurra card
            else if (robot.CurrentPosition == 8 || robot.CurrentPosition == 18 || robot.CurrentPosition == 28 || robot.CurrentPosition == 38)
            {
                HurraCard hurraCardForm = new HurraCard(m_BoardGameForm.CurrentTurn, player, robot1, robot2, robot3, m_GameHandler.cards);
                if (hurraCardForm.ShowDialog() == DialogResult.OK)
                {
                    switch (hurraCardForm.ActionType)
                    {
                        case HurraAction.Jump:
                            m_GameHandler.JumpWithTheFigureToTheNextPosition(hurraCardForm.Amount);
                            m_RobotActionControl.RobotAfterChangeHisPositionAction(robot);
                            break;
                        case HurraAction.Roll:

                            m_GameHandler.JumpWithTheFigureToTheNextPosition(m_GameHandler.RollTheDice());
                            m_RobotActionControl.RobotAfterChangeHisPositionAction(robot);
                            break;
                        case HurraAction.Balance:
                            robot.Balance += hurraCardForm.Amount;
                            m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);
                            break;
                        default:
                            throw new Exception("Nincs ilyen Action");
                    }
                }
            }
            //Drawing a Property card
            else
            {
                PropertyCardForRobot PropertyCardForm = new PropertyCardForRobot(m_BoardGameForm,player, robot1, robot2, robot3, m_GameHandler.cards, m_Level);
                if (PropertyCardForm.ShowDialog() == DialogResult.OK)
                {
                    if (PropertyCardForm.IDontWannaBuy)
                    {
                        MessageBox.Show("Nem akarom megvenni!", robot.Name);
                    }
                    else if (PropertyCardForm.CantBuy)
                    {
                        MessageBox.Show("Nem tudom megvenni!", robot.Name);
                    }
                    else
                    {
                        robot.Balance -= PropertyCardForm.FieldValue;

                        m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);


                        switch (m_BoardGameForm.CurrentTurn)
                        {
                            case 1:
                                m_UpdateListBoxes.UpdateRobot1PropertyCardListBox();
                                break;
                            case 2:
                                m_UpdateListBoxes.UpdateRobot2PropertyCardListBox();
                                break;
                            case 3:
                                m_UpdateListBoxes.UpdateRobot3PropertyCardListBox();
                                break;
                            default:
                                throw new Exception("Nincs körön lévő Robot");
                        }

                        Field field = new Field();
                        field.FieldName = PropertyCardForm.FieldName;
                        field.OwnedField = true;
                        field.FieldOwner = robot;
                        field.FieldValue = PropertyCardForm.FieldValue;
                        field.FieldFinishedValue = PropertyCardForm.FieldFinishedValue;
                        field.FieldLocation = robot.CurrentPosition;

                        //the robot is an Archaeologist so he/she can do it 2x faster
                        if (robot.IsArchaeologist)
                        {
                            double fieldBuildingTurns = PropertyCardForm.FieldBuildingTurns;
                            field.FieldBuildingTurns = (int)Math.Round(fieldBuildingTurns / 2);
                        }
                        else
                        {
                            field.FieldBuildingTurns = PropertyCardForm.FieldBuildingTurns;
                        }

                        m_BoardGameForm.OwnedProperties.Add(field);

                        foreach (Field currentField in m_BoardGameForm.OwnedProperties)
                        {
                            if (robot.CurrentPosition == currentField.FieldLocation && currentField.FieldOwner == robot)
                            {
                                currentField.StartedField = true;
                                m_BoardGameForm.FieldLabel[robot.CurrentPosition].Text = currentField.FieldBuildingTurns.ToString();
                                m_BoardGameForm.FieldLabel[robot.CurrentPosition].Visible = true;
                                m_BoardGameForm.FieldLabelOwner[robot.CurrentPosition].Text = field.FieldOwner.Name;
                                m_BoardGameForm.FieldLabelOwner[robot.CurrentPosition].Visible = true;
                                double Bill = field.FieldFinishedValue;
                                m_BoardGameForm.PropertyInfo.SetToolTip(m_BoardGameForm.Field[robot.CurrentPosition], "Lelet: " + field.FieldName + ",\n Fizetendő a telek befejezése esetén: " + Math.Round(Bill * 0.4) + "$,\n Fizetendő megkezdett lelet esetén: " + Math.Round(Bill * 0.25) + "$");
                            }
                        }
                    }
                }
            }
        }

        private void HupszAccident(Robot robot, HupszCard hupszCardForm)
        {
            if (robot.IsCareful)
            {
                MessageBox.Show("Óvatos voltál ezért megúsztad. Nem maradsz ki.", robot.Name);
            }
            else
            {
                robot.CanNotRollForXTurn += hupszCardForm.Amount;

                switch (m_BoardGameForm.CurrentTurn)
                {
                    case 1:

                        m_BoardGameForm.Robot1CanRoll.Text = robot.CanNotRollForXTurn + "-körig kimaradsz!";
                        m_BoardGameForm.Robot1CanRoll.ForeColor = Color.Red;

                        break;
                    case 2:

                        m_BoardGameForm.Robot2CanRoll.Text = robot.CanNotRollForXTurn + "-körig kimaradsz!";
                        m_BoardGameForm.Robot2CanRoll.ForeColor = Color.Red;

                        break;
                    case 3:

                        m_BoardGameForm.Robot3CanRoll.Text = robot.CanNotRollForXTurn + "-körig kimaradsz!";
                        m_BoardGameForm.Robot3CanRoll.ForeColor = Color.Red;

                        break;
                    default:
                        throw new Exception("Wrong turn");
                }
            }
        }

        private void HupszBalance(Robot robot, HupszCard hupszCardForm)
        {
            if (robot.HasAFriend)
            {
                MessageBox.Show("Szerencsés vagy, hogy ilyen barátod van! Kifizette helyetted.", robot.Name);
            }
            else
            {
                robot.Balance -= hupszCardForm.Amount;

                if (robot.Balance < 0)
                {
                    if (robot.HasAGuardien > 0)
                    {
                        robot.HasAGuardien -= 1;
                        MessageBox.Show("Az őrangyalod most megmentett, de többször már nem tud!", robot.Name);
                        robot.Balance += hupszCardForm.Amount;
                        m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);
                    }
                    m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);
                    m_GameHandler.RobotGoesIntoFailure(robot);
                }
                else
                {
                    m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);
                }
            }
        }
    }
}
