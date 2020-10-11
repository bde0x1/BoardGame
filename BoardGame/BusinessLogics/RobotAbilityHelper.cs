using BoardGame.Domain_Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BoardGame.BusinessLogics
{
    public class RobotAbilityHelper
    {
        private GameHandler m_GameHandler;
        private BoardGame m_BoardGameForm;
        private UpdateListBoxes m_UpdateListBoxes;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;

        public RobotAbilityHelper(GameHandler gameHandler, BoardGame boardGame, UpdateListBoxes updateListBoxes)
        {
            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;
            m_UpdateListBoxes = updateListBoxes;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;
        }

        public void IfTheRobotIsAnEntrepreneur(Robot robot)
        {
            if (robot.IsEntrepreneur)
            {
                Random randomPassiveBalance = new Random();
                int myRandomPassiveBalance = randomPassiveBalance.Next(50, 201);
                robot.Balance += myRandomPassiveBalance;
                m_GameHandler.ChangingTheSelectedRobotsBalanceTextValue(robot);

                MessageBox.Show("Mivel vállalkozó vagy a körödvégén egy bizonyos jövedelem kerül az egyenlegedre, ez az egyenleg most " + myRandomPassiveBalance + " $ volt.", robot.Name);
            }
        }

        public void RobotWouldLikeToTradeOrNot(Robot robot)
        {
            Field robotSmallestProceedsFromProperty = null;
            Field SomebodysBiggestProceedsFromProperty = null;
            int min = 3000;
            int max = 0;
            bool noOption = false;

            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.StartedField)
                {
                    if (field.FieldOwner == robot)
                    {
                        if (field.FieldFinishedValue < min)
                        {
                            min = field.FieldFinishedValue;
                            robotSmallestProceedsFromProperty = field;
                        }
                    }
                    else if (field.FieldOwner != robot)
                    {
                        if (field.FieldFinishedValue > max)
                        {
                            max = field.FieldFinishedValue;
                            SomebodysBiggestProceedsFromProperty = field;
                        }
                    }
                    else
                    {
                        noOption = true;
                    }
                }
            }

            if (!noOption)
            {
                //robots wanna get the best option and trade his smallest proceeds property to the best proceeds property
                if (min >= max)
                {
                    MessageBox.Show("Nem szeretnék most cserélni!", robot.Name);
                }
                else if (min < max)
                {
                    if (robot.CanTrade > 0)
                    {
                        robot.CanTrade -= 1;
                        switch (m_BoardGameForm.CurrentTurn)
                        {
                            case 1:
                                m_BoardGameForm.Robot1TradeNumber.Text = "Ennyi cseréd van még:" + robot.CanTrade;
                                break;
                            case 2:
                                m_BoardGameForm.Robot2TradeNumber.Text = "Ennyi cseréd van még:" + robot.CanTrade;
                                break;
                            case 3:
                                m_BoardGameForm.Robot3TradeNumber.Text = "Ennyi cseréd van még:" + robot.CanTrade;
                                break;
                            default:
                                throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
                        }

                        MessageBox.Show(robotSmallestProceedsFromProperty.ToString() + " teruletemet szeretném elcserélni (" + SomebodysBiggestProceedsFromProperty.FieldOwner + ") : " + SomebodysBiggestProceedsFromProperty.ToString() + " területére.", robot.Name);

                        foreach (Field RobotField in m_BoardGameForm.OwnedProperties)
                        {
                            Player robotFieldOwner = null;

                            if (RobotField == robotSmallestProceedsFromProperty)
                            {
                                robotFieldOwner = RobotField.FieldOwner;

                                foreach (Field PlayerOrRobotField in m_BoardGameForm.OwnedProperties)
                                {
                                    if (PlayerOrRobotField == SomebodysBiggestProceedsFromProperty)
                                    {

                                        //robot's label text change to player and tha same with the player's label
                                        m_BoardGameForm.FieldLabelOwner[RobotField.FieldLocation].Text = PlayerOrRobotField.FieldOwner.Name;
                                        m_BoardGameForm.FieldLabelOwner[PlayerOrRobotField.FieldLocation].Text = RobotField.FieldOwner.Name;

                                        //property cards trade
                                        if (PlayerOrRobotField.FieldOwner == player)
                                        {
                                            for (int i = 0; i < player.MyPropertyCards.Count; i++)
                                            {
                                                if (player.MyPropertyCards[i].PropertyName == PlayerOrRobotField.FieldName)
                                                {
                                                    m_UpdateListBoxes.AddTheSelectedPropertytoRobotsPropertyListBox(player.MyPropertyCards[i]);
                                                    m_BoardGameForm.PlayerPropertyCardList.Items.Remove(player.MyPropertyCards[i]);

                                                    robot.MyPropertyCards.Add(player.MyPropertyCards[i]);
                                                    player.MyPropertyCards.Remove(player.MyPropertyCards[i]);
                                                }
                                            }

                                            for (int i = 0; i < robot.MyPropertyCards.Count; i++)
                                            {
                                                if (robot.MyPropertyCards[i].PropertyName == RobotField.FieldName)
                                                {
                                                    m_BoardGameForm.PlayerPropertyCardList.Items.Add(robot.MyPropertyCards[i]);
                                                    m_UpdateListBoxes.RemoveTheSelectedPropertytoRobotsPropertyListBox(robot.MyPropertyCards[i]);

                                                    player.MyPropertyCards.Add(robot.MyPropertyCards[i]);
                                                    robot.MyPropertyCards.Remove(robot.MyPropertyCards[i]);
                                                }
                                            }
                                        }
                                        else if (PlayerOrRobotField.FieldOwner == robot1)
                                        {
                                            for (int i = 0; i < robot1.MyPropertyCards.Count; i++)
                                            {
                                                if (robot1.MyPropertyCards[i].PropertyName == PlayerOrRobotField.FieldName)
                                                {
                                                    m_UpdateListBoxes.AddTheSelectedPropertytoRobotsPropertyListBox(robot1.MyPropertyCards[i]);
                                                    m_BoardGameForm.Robot1PropertyCardList.Items.Remove(robot1.MyPropertyCards[i]);

                                                    robot.MyPropertyCards.Add(robot1.MyPropertyCards[i]);
                                                    robot1.MyPropertyCards.Remove(robot1.MyPropertyCards[i]);
                                                }
                                            }

                                            for (int i = 0; i < robot.MyPropertyCards.Count; i++)
                                            {
                                                if (robot.MyPropertyCards[i].PropertyName == RobotField.FieldName)
                                                {
                                                    m_BoardGameForm.Robot1PropertyCardList.Items.Add(robot.MyPropertyCards[i]);
                                                    m_UpdateListBoxes.RemoveTheSelectedPropertytoRobotsPropertyListBox(robot.MyPropertyCards[i]);

                                                    robot1.MyPropertyCards.Add(robot.MyPropertyCards[i]);
                                                    robot.MyPropertyCards.Remove(robot.MyPropertyCards[i]);
                                                }
                                            }
                                        }
                                        else if (PlayerOrRobotField.FieldOwner == robot2)
                                        {
                                            for (int i = 0; i < robot2.MyPropertyCards.Count; i++)
                                            {
                                                if (robot2.MyPropertyCards[i].PropertyName == PlayerOrRobotField.FieldName)
                                                {
                                                    m_UpdateListBoxes.AddTheSelectedPropertytoRobotsPropertyListBox(robot2.MyPropertyCards[i]);
                                                    m_BoardGameForm.Robot2PropertyCardList.Items.Remove(robot2.MyPropertyCards[i]);

                                                    robot.MyPropertyCards.Add(robot2.MyPropertyCards[i]);
                                                    robot2.MyPropertyCards.Remove(robot2.MyPropertyCards[i]);
                                                }
                                            }

                                            for (int i = 0; i < robot.MyPropertyCards.Count; i++)
                                            {
                                                if (robot.MyPropertyCards[i].PropertyName == RobotField.FieldName)
                                                {
                                                    m_BoardGameForm.Robot2PropertyCardList.Items.Add(robot.MyPropertyCards[i]);
                                                    m_UpdateListBoxes.RemoveTheSelectedPropertytoRobotsPropertyListBox(robot.MyPropertyCards[i]);

                                                    robot2.MyPropertyCards.Add(robot.MyPropertyCards[i]);
                                                    robot.MyPropertyCards.Remove(robot.MyPropertyCards[i]);
                                                }
                                            }
                                        }
                                        else if (PlayerOrRobotField.FieldOwner == robot3)
                                        {
                                            for (int i = 0; i < robot3.MyPropertyCards.Count; i++)
                                            {
                                                if (robot3.MyPropertyCards[i].PropertyName == PlayerOrRobotField.FieldName)
                                                {
                                                    m_UpdateListBoxes.AddTheSelectedPropertytoRobotsPropertyListBox(robot3.MyPropertyCards[i]);
                                                    m_BoardGameForm.Robot3PropertyCardList.Items.Remove(robot3.MyPropertyCards[i]);

                                                    robot.MyPropertyCards.Add(robot3.MyPropertyCards[i]);
                                                    robot3.MyPropertyCards.Remove(robot3.MyPropertyCards[i]);
                                                }
                                            }

                                            for (int i = 0; i < robot.MyPropertyCards.Count; i++)
                                            {
                                                if (robot.MyPropertyCards[i].PropertyName == RobotField.FieldName)
                                                {
                                                    m_BoardGameForm.Robot3PropertyCardList.Items.Add(robot.MyPropertyCards[i]);
                                                    m_UpdateListBoxes.RemoveTheSelectedPropertytoRobotsPropertyListBox(robot.MyPropertyCards[i]);

                                                    robot3.MyPropertyCards.Add(robot.MyPropertyCards[i]);
                                                    robot.MyPropertyCards.Remove(robot.MyPropertyCards[i]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("there is no field owner like: " + RobotField.FieldOwner);
                                        }

                                        m_UpdateListBoxes.UpdateRobot3PropertyCardListBox();
                                        m_UpdateListBoxes.UpdateRobot2PropertyCardListBox();
                                        m_UpdateListBoxes.UpdateRobot1PropertyCardListBox();
                                        m_UpdateListBoxes.UpdatePlayerPropertyCardListBox();

                                        //owners change
                                        RobotField.FieldOwner = PlayerOrRobotField.FieldOwner;
                                        PlayerOrRobotField.FieldOwner = robotFieldOwner;
                                    }
                                }
                            }
                        }
                        //i dont know why is it here
                        //m_BoardGameForm.TradeNumber.Text = "Ennyi cseréd van még:" + robot.CanTrade;
                    }
                    else
                    {
                        MessageBox.Show("Sajnos már nem tudsz többet cserélni.", robot.Name);

                        switch (m_BoardGameForm.CurrentTurn)
                        {
                            case 1:
                                m_BoardGameForm.Robot1TradeNumber.Visible = false;
                                break;
                            case 2:
                                m_BoardGameForm.Robot2TradeNumber.Visible = false;
                                break;
                            case 3:
                                m_BoardGameForm.Robot3TradeNumber.Visible = false;
                                break;
                            default:
                                throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nem szeretnék most cserélni!", robot.Name);
            }
        }

        public void RobotWouldLikeToStealAPassiveAbilityOrNot(Robot robot)
        {
            List<CardModelFromDatabase.PassiveCard> AvailablePassiveCards = new List<CardModelFromDatabase.PassiveCard>();
            CardModelFromDatabase.PassiveCard SelectedPassiveAbility = null;

            switch (m_BoardGameForm.CurrentTurn)
            {
                case 1:

                    foreach (CardModelFromDatabase.PassiveCard Card in player.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }
                    foreach (CardModelFromDatabase.PassiveCard Card in robot2.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }
                    foreach (CardModelFromDatabase.PassiveCard Card in robot3.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }

                    break;
                case 2:

                    foreach (CardModelFromDatabase.PassiveCard Card in player.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }
                    foreach (CardModelFromDatabase.PassiveCard Card in robot1.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }
                    foreach (CardModelFromDatabase.PassiveCard Card in robot3.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }

                    break;
                case 3:

                    foreach (CardModelFromDatabase.PassiveCard Card in player.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }
                    foreach (CardModelFromDatabase.PassiveCard Card in robot1.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }
                    foreach (CardModelFromDatabase.PassiveCard Card in robot2.MyPassiveCards)
                    {
                        AvailablePassiveCards.Add(Card);
                    }

                    break;
                default:
                    throw new Exception("Wrong turn");
            }

            if (AvailablePassiveCards != null)
            {
                Random number = new Random();
                int RandomPassiveCard = number.Next(0, AvailablePassiveCards.Count);

                for (int i = 0; i < AvailablePassiveCards.Count; i++)
                {
                    if (i == RandomPassiveCard)
                    {
                        SelectedPassiveAbility = AvailablePassiveCards[i];
                    }
                }

                if (robot.CanStealAPassiveAbility > 0)
                {
                    robot.CanStealAPassiveAbility -= 1;
                    MessageBox.Show("(" + SelectedPassiveAbility + ") képességet loptam!", robot.Name);

                    switch (m_BoardGameForm.CurrentTurn)
                    {
                        case 1:

                            m_BoardGameForm.Robot1StealNumber.Text = "Ennyit lophatsz még:" + robot.CanStealAPassiveAbility;

                            break;
                        case 2:

                            m_BoardGameForm.Robot2StealNumber.Text = "Ennyit lophatsz még:" + robot.CanStealAPassiveAbility;

                            break;
                        case 3:

                            m_BoardGameForm.Robot3StealNumber.Text = "Ennyit lophatsz még:" + robot.CanStealAPassiveAbility;

                            break;
                        default:
                            throw new Exception("Wrong turn");
                    }

                    if (robot1.MyPassiveCards.Contains(SelectedPassiveAbility))
                    {
                        for (int i = 0; i < robot1.MyPassiveCards.Count; i++)
                        {
                            if (robot1.MyPassiveCards[i] == SelectedPassiveAbility)
                            {
                                GiveThePAssiveAbilityForRobot(i, robot, robot1, null);

                                robot.MyPassiveCards.Add(robot1.MyPassiveCards[i]);
                                switch (m_BoardGameForm.CurrentTurn)
                                {
                                    case 1:

                                        m_BoardGameForm.Robot1PassiveCardList.Items.Add(robot1.MyPassiveCards[i]);

                                        break;
                                    case 2:

                                        m_BoardGameForm.Robot2PassiveCardList.Items.Add(robot1.MyPassiveCards[i]);

                                        break;
                                    case 3:

                                        m_BoardGameForm.Robot3PassiveCardList.Items.Add(robot1.MyPassiveCards[i]);

                                        break;
                                    default:
                                        throw new Exception("Wrong turn");
                                }

                                m_BoardGameForm.Robot1PassiveCardList.Items.Remove(robot1.MyPassiveCards[i]);
                                robot1.MyPassiveCards.Remove(robot1.MyPassiveCards[i]);

                                m_UpdateListBoxes.UpdateRobot1PassiveCardListBox();
                            }
                        }
                    }
                    else if (robot2.MyPassiveCards.Contains(SelectedPassiveAbility))
                    {
                        for (int i = 0; i < robot2.MyPassiveCards.Count; i++)
                        {
                            if (robot2.MyPassiveCards[i] == SelectedPassiveAbility)
                            {
                                GiveThePAssiveAbilityForRobot(i, robot, robot2, null);

                                robot.MyPassiveCards.Add(robot2.MyPassiveCards[i]);
                                switch (m_BoardGameForm.CurrentTurn)
                                {
                                    case 1:

                                        m_BoardGameForm.Robot1PassiveCardList.Items.Add(robot2.MyPassiveCards[i]);

                                        break;
                                    case 2:

                                        m_BoardGameForm.Robot2PassiveCardList.Items.Add(robot2.MyPassiveCards[i]);

                                        break;
                                    case 3:

                                        m_BoardGameForm.Robot3PassiveCardList.Items.Add(robot2.MyPassiveCards[i]);

                                        break;
                                    default:
                                        throw new Exception("Wrong turn");
                                }

                                m_BoardGameForm.Robot2PassiveCardList.Items.Remove(robot2.MyPassiveCards[i]);
                                robot2.MyPassiveCards.Remove(robot2.MyPassiveCards[i]);

                                m_UpdateListBoxes.UpdateRobot2PassiveCardListBox();
                            }
                        }
                    }
                    else if (robot3.MyPassiveCards.Contains(SelectedPassiveAbility))
                    {
                        for (int i = 0; i < robot3.MyPassiveCards.Count; i++)
                        {
                            if (robot3.MyPassiveCards[i] == SelectedPassiveAbility)
                            {
                                GiveThePAssiveAbilityForRobot(i, robot, robot3, null);

                                robot.MyPassiveCards.Add(robot3.MyPassiveCards[i]);
                                switch (m_BoardGameForm.CurrentTurn)
                                {
                                    case 1:

                                        m_BoardGameForm.Robot1PassiveCardList.Items.Add(robot3.MyPassiveCards[i]);

                                        break;
                                    case 2:

                                        m_BoardGameForm.Robot2PassiveCardList.Items.Add(robot3.MyPassiveCards[i]);

                                        break;
                                    case 3:

                                        m_BoardGameForm.Robot3PassiveCardList.Items.Add(robot3.MyPassiveCards[i]);

                                        break;
                                    default:
                                        throw new Exception("Wrong turn");
                                }

                                m_BoardGameForm.Robot3PassiveCardList.Items.Remove(robot3.MyPassiveCards[i]);
                                robot3.MyPassiveCards.Remove(robot3.MyPassiveCards[i]);

                               m_UpdateListBoxes.UpdateRobot3PassiveCardListBox();
                            }
                        }
                    }
                    else if (player.MyPassiveCards.Contains(SelectedPassiveAbility))
                    {
                        for (int i = 0; i < player.MyPassiveCards.Count; i++)
                        {
                            if (player.MyPassiveCards[i] == SelectedPassiveAbility)
                            {
                                GiveThePAssiveAbilityForRobot(i, robot, null, player);

                                robot.MyPassiveCards.Add(player.MyPassiveCards[i]);
                                switch (m_BoardGameForm.CurrentTurn)
                                {
                                    case 1:

                                        m_BoardGameForm.Robot1PassiveCardList.Items.Add(player.MyPassiveCards[i]);

                                        break;
                                    case 2:

                                        m_BoardGameForm.Robot2PassiveCardList.Items.Add(player.MyPassiveCards[i]);

                                        break;
                                    case 3:

                                        m_BoardGameForm.Robot3PassiveCardList.Items.Add(player.MyPassiveCards[i]);

                                        break;
                                    default:
                                        throw new Exception("Wrong turn");
                                }

                                m_BoardGameForm.PlayerPassiveCardList.Items.Remove(player.MyPassiveCards[i]);
                                player.MyPassiveCards.Remove(player.MyPassiveCards[i]);

                                m_UpdateListBoxes.UpdatePlayerPassiveCardListBox();
                            }
                        }
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
                            throw new Exception("Wrong turn");
                    }
                }
                else
                {
                    MessageBox.Show("Sajnos már nem tudsz többet lopni.", robot.Name);
                    switch (m_BoardGameForm.CurrentTurn)
                    {
                        case 1:

                            m_BoardGameForm.Robot1StealNumber.Visible = false;

                            break;
                        case 2:

                            m_BoardGameForm.Robot2StealNumber.Visible = false;

                            break;
                        case 3:

                            m_BoardGameForm.Robot3StealNumber.Visible = false;

                            break;
                        default:
                            throw new Exception("Wrong turn");
                    }
                }
            }
            else
            {
                MessageBox.Show("Most nem lopok, majd legközelebb!", robot.Name);
            }
        }

        private void GiveThePAssiveAbilityForRobot(int i, Robot NewOwnerRobot, Robot OldOwnerRobot, Player OldOwnerPlayer)
        {
            if (OldOwnerRobot != null)
            {
                if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Kereskedelem.ToString())
                {
                    NewOwnerRobot.CanTrade += 3;
                    OldOwnerRobot.CanTrade = 0;

                    switch (m_BoardGameForm.CurrentTurn)
                    {
                        case 1:

                            m_BoardGameForm.Robot1TradeNumber.Visible = true;
                            m_BoardGameForm.Robot1TradeNumber.Text += NewOwnerRobot.CanTrade;

                            break;
                        case 2:

                            m_BoardGameForm.Robot2TradeNumber.Visible = true;
                            m_BoardGameForm.Robot2TradeNumber.Text += NewOwnerRobot.CanTrade;

                            break;
                        case 3:

                            m_BoardGameForm.Robot3TradeNumber.Visible = true;
                            m_BoardGameForm.Robot3TradeNumber.Text += NewOwnerRobot.CanTrade;

                            break;
                        default:
                            throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
                    }
                }
                else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Régész.ToString())
                {
                    NewOwnerRobot.IsArchaeologist = true;
                    OldOwnerRobot.IsArchaeologist = false;
                }
                else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Szárnysegéd.ToString())
                {
                    NewOwnerRobot.HasAFriend = true;
                    OldOwnerRobot.HasAFriend = false;
                }
                else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Talpraesett.ToString())
                {
                    NewOwnerRobot.IsPointful = true;
                    OldOwnerRobot.IsPointful = false;
                }
                else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Vállalkozó.ToString())
                {
                    NewOwnerRobot.IsEntrepreneur = true;
                    OldOwnerRobot.IsEntrepreneur = false;
                }
                else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Óvatos.ToString())
                {
                    NewOwnerRobot.IsCareful = true;
                    OldOwnerRobot.IsCareful = false;
                }
                else if (OldOwnerRobot.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Őrangyal.ToString())
                {
                    NewOwnerRobot.HasAGuardien += 1;
                    OldOwnerRobot.HasAGuardien = 0;
                }
            }
            else if (OldOwnerPlayer != null)
            {
                if (OldOwnerPlayer.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Kereskedelem.ToString())
                {
                    NewOwnerRobot.CanTrade += 3;
                    OldOwnerPlayer.CanTrade = 0;
                    m_BoardGameForm.TradeNumber.Text = "Ennyi cseréd van még:" + OldOwnerPlayer.CanTrade;

                    switch (m_BoardGameForm.CurrentTurn)
                    {
                        case 1:

                            m_BoardGameForm.Robot1TradeNumber.Visible = true;
                            m_BoardGameForm.Robot1TradeNumber.Text += NewOwnerRobot.CanTrade;

                            break;
                        case 2:

                            m_BoardGameForm.Robot2TradeNumber.Visible = true;
                            m_BoardGameForm.Robot2TradeNumber.Text += NewOwnerRobot.CanTrade;

                            break;
                        case 3:

                            m_BoardGameForm.Robot3TradeNumber.Visible = true;
                            m_BoardGameForm.Robot3TradeNumber.Text += NewOwnerRobot.CanTrade;

                            break;
                        default:
                            throw new Exception("This turn is not exist: " + m_BoardGameForm.CurrentTurn);
                    }
                }
                else if (OldOwnerPlayer.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Régész.ToString())
                {
                    NewOwnerRobot.IsArchaeologist = true;
                    OldOwnerPlayer.IsArchaeologist = false;
                }
                else if (OldOwnerPlayer.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Szárnysegéd.ToString())
                {
                    NewOwnerRobot.HasAFriend = true;
                    OldOwnerPlayer.HasAFriend = false;
                }
                else if (OldOwnerPlayer.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Talpraesett.ToString())
                {
                    NewOwnerRobot.IsPointful = true;
                    OldOwnerPlayer.IsPointful = false;
                }
                else if (OldOwnerPlayer.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Vállalkozó.ToString())
                {
                    NewOwnerRobot.IsEntrepreneur = true;
                    OldOwnerPlayer.IsEntrepreneur = false;
                }
                else if (OldOwnerPlayer.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Óvatos.ToString())
                {
                    NewOwnerRobot.IsCareful = true;
                    OldOwnerPlayer.IsCareful = false;
                }
                else if (OldOwnerPlayer.MyPassiveCards[i].SpecialPassiveAbility == Abilities.Őrangyal.ToString())
                {
                    NewOwnerRobot.HasAGuardien += 1;
                    OldOwnerPlayer.HasAGuardien = 0;
                }
            }
        }        
    }
}
