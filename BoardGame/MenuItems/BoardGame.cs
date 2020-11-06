using BoardGame.BusinessLogics;
using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class BoardGame : Form
    {

        #region Local Variables 

        private string turnText = " te vagy a soronlévő játékos! Dobj a kockával!";
        public int CurrentTurn = 0;
        public Player player;
        public Robot robot1;
        public Robot robot2;
        public Robot robot3;
        public PictureBox[] Field;
        public Label[] FieldLabel;
        public Label[] FieldLabelOwner;
        private GameHandler m_GameHandler;
        private ActionControl m_ActionControl;
        public List<Field> OwnedProperties = new List<Field>();
        private UpdateListBoxes m_UpdateListBoxes;
        private NewTurnHelper m_NewTurnHelper;
        private RobotsTurn m_RobotTurns;
        private CalculateNextPlayer m_CalculateNextPlayer;
        private Level m_Level;

        #endregion         

        public BoardGame(GameHandler gameHandler, Level level)
        {
            InitializeComponent();

            m_GameHandler = gameHandler;
            m_Level = level;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;

            m_ActionControl = new ActionControl(m_GameHandler,this);
            m_UpdateListBoxes = new UpdateListBoxes(m_GameHandler, this);
            m_NewTurnHelper = new NewTurnHelper(m_GameHandler, this);
            m_RobotTurns = new RobotsTurn(m_GameHandler, this, m_NewTurnHelper, m_UpdateListBoxes,m_Level);
            m_CalculateNextPlayer = new CalculateNextPlayer(m_RobotTurns, m_GameHandler, this, m_NewTurnHelper);

            FieldLabel = new Label[] { l0, l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11, l12, l13, l14, l15, l16, l17, l18, l19, l20, l21, l22, l23, l24, l25, l26, l27, l28, l29, l30, l31, l32, l33, l34, l35, l36, l37, l38, l39 };
            FieldLabelOwner = new Label[] { WhoHaveThis0, WhoHaveThis1, WhoHaveThis2, WhoHaveThis3, WhoHaveThis4, WhoHaveThis5, WhoHaveThis6, WhoHaveThis7, WhoHaveThis8, WhoHaveThis9, WhoHaveThis10, WhoHaveThis11, WhoHaveThis12, WhoHaveThis13, WhoHaveThis14, WhoHaveThis15, WhoHaveThis16, WhoHaveThis17, WhoHaveThis18, WhoHaveThis19, WhoHaveThis20, WhoHaveThis21, WhoHaveThis22, WhoHaveThis23, WhoHaveThis24, WhoHaveThis25, WhoHaveThis26, WhoHaveThis27, WhoHaveThis28, WhoHaveThis29, WhoHaveThis30, WhoHaveThis31, WhoHaveThis32, WhoHaveThis33, WhoHaveThis34, WhoHaveThis35, WhoHaveThis36, WhoHaveThis37, WhoHaveThis38, WhoHaveThis39 };
            Field = new PictureBox[] { Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17, Field18, Field19, Field20, Field21, Field22, Field23, Field24, Field25, Field26, Field27, Field28, Field29, Field30, Field31, Field32, Field33, Field34, Field35, Field36, Field37, Field38, Field39 };

            PlayerBox.Text = player.Name;
            Robot1Box.Text = robot1.Name;
            Robot2Box.Text = robot2.Name;
            Robot3Box.Text = robot3.Name;

            PlayerBalance.Text = player.Balance.ToString();
            Robot1Balance.Text = robot1.Balance.ToString();
            Robot2Balance.Text = robot2.Balance.ToString();
            Robot3Balance.Text = robot3.Balance.ToString();

            ButtonTip.SetToolTip(Exit, "Kilépés a menübe");
            ButtonTip.SetToolTip(ThrowWithTheCubes, "Dobás a kockákkal");
            ButtonTip.SetToolTip(DrawTheCard, "Kártya felhúzása");
            ButtonTip.SetToolTip(LetsDoIt, "Ásatás elvégzése");
            ButtonTip.SetToolTip(EndTurn, "Végeztél a köröddel");
            ButtonTip.SetToolTip(TradePropertyCard, "Lelet csere");
            ButtonTip.SetToolTip(StealPassiveAbility, "Képesség lopás");
        }

        private void BoardGame_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            WindowState = FormWindowState.Maximized;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Biztos hogy kilépsz?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
            else if (dialog == DialogResult.No)
            {
                return;
            }
        }

        private void ThrowWithTheCubes_Click(object sender, EventArgs e)
        {
            if (player.CanTrade > 0)
            {
                TradePropertyCard.Enabled = false;
            }

            if (player.CanStealAPassiveAbility > 0)
            {
                StealPassiveAbility.Enabled = false;
            }

            m_GameHandler.JumpWithTheFigureToTheNextPosition(m_GameHandler.RollTheDice());

            m_ActionControl.PlayerAfterChangeHisPositionAction();
        }

        private void DrawTheCard_Click(object sender, EventArgs e)
        {
            //Drawing a passive card
            if (player.CurrentPosition == 5 || player.CurrentPosition == 15 || player.CurrentPosition == 25 || player.CurrentPosition == 35)
            {
                PassiveCard passiveCardForm = new PassiveCard(CurrentTurn, player, robot1, robot2, robot3, m_GameHandler.cards);
                if (passiveCardForm.ShowDialog() == DialogResult.OK)
                {
                    if (!passiveCardForm.NoPassiveCard)
                    {
                        switch (passiveCardForm.PassiveAbility)
                        {
                            case Abilities.Kereskedelem: //kész, kivéve a gomb actiont

                                TradePropertyCard.Visible = true;
                                TradeNumber.Visible = true;
                                player.CanTrade = passiveCardForm.AbilityValue;

                                TradeNumber.Text += player.CanTrade;

                                MessageBox.Show("Mostantól kereskedő lettél! Csak a köröd elején cserélhetsz, azaz ha dobtál már nem teheted meg!", player.Name);

                                break;
                            case Abilities.Régész:

                                player.IsArchaeologist = true;
                                MessageBox.Show("Mostantól régész vagy! Ha feltársz valamit az ásatás ideje a felére csökken.", player.Name);

                                break;
                            case Abilities.Vállalkozó:

                                player.IsEntrepreneur = true;
                                MessageBox.Show("Mostantól vállalkozó vagy! Miután befejezted a körödet egy bizonyos összeg kerül az egyenlegedre.", player.Name);

                                break;
                            case Abilities.Óvatos:

                                player.IsCareful = true;
                                MessageBox.Show("Mostantól óvatos lettél! Minden egyes hupszkártyából húzott balesetet elkerülsz, így nem maradsz ki.", player.Name);

                                break;
                            case Abilities.Őrangyal:

                                player.HasAGuardien = passiveCardForm.AbilityValue;
                                MessageBox.Show("Mostantól vane EGY őrangyalod! Ha netán csődbekerülnél ő kisegít.", player.Name);

                                break;
                            case Abilities.Szárnysegéd:

                                player.HasAFriend = true;
                                MessageBox.Show("Mostantól van egy szárnysegéded! Jöhet bármi a szárnysegéded fizet mindent, kivéve ha befejezett ásatási területre lépsz. Az neki is drága.", player.Name);

                                break;
                            case Abilities.Talpraesett:

                                player.IsPointful = true;
                                MessageBox.Show("Mostantól talpraesett vagy! Mindig tudod mi a dolgod.", player.Name);

                                break;
                            case Abilities.Tolvaj:

                                StealPassiveAbility.Visible = true;
                                StealNumber.Visible = true;
                                player.CanStealAPassiveAbility = passiveCardForm.AbilityValue;
                                StealNumber.Text += player.CanStealAPassiveAbility;
                                MessageBox.Show("Mostantól tolvaj vagy! Ellophatsz egy képességet valakitől.", player.Name);

                                break;
                            default:
                                throw new Exception("This ability is not exists: " + passiveCardForm.PassiveAbility);
                        }

                        m_UpdateListBoxes.UpdatePlayerPassiveCardListBox();

                        DrawTheCard.Enabled = false;
                        EndTurn.Enabled = true;
                    }
                    else
                    {
                        DrawTheCard.Enabled = false;
                        EndTurn.Enabled = true;
                    }
                }
            }
            //Drawing a hupsz card
            else if (player.CurrentPosition == 2 || player.CurrentPosition == 12 || player.CurrentPosition == 22 || player.CurrentPosition == 32)
            {
                HupszCard hupszCardForm = new HupszCard(CurrentTurn, player, robot1, robot2, robot3, m_GameHandler.cards);
                if (hupszCardForm.ShowDialog() == DialogResult.OK)
                {
                    switch (hupszCardForm.ActionType)
                    {
                        case HupszAction.Jump:

                            m_GameHandler.JumpWithTheFigureToTheNextPosition(-hupszCardForm.Amount);

                            m_ActionControl.PlayerAfterChangeHisPositionAction();

                            break;
                        case HupszAction.Accident:

                            if (player.IsCareful)
                            {
                                MessageBox.Show("Óvatos voltál ezért megúsztad. Nem maradsz ki.", player.Name);

                                DrawTheCard.Enabled = false;
                                EndTurn.Enabled = true;
                            }
                            else
                            {
                                player.CanNotRollForXTurn += hupszCardForm.Amount;

                                PlayerCanRoll.Text = player.CanNotRollForXTurn + "-körig kimaradsz!";
                                PlayerCanRoll.ForeColor = Color.Red;

                                DrawTheCard.Enabled = false;
                                EndTurn.Enabled = true;
                            }

                            break;
                        case HupszAction.Balance:

                            if (player.HasAFriend)
                            {
                                MessageBox.Show("Szerencsés vagy, hogy ilyen barátod van! Kifizette helyetted.", player.Name);

                                DrawTheCard.Enabled = false;
                                EndTurn.Enabled = true;
                            }
                            else
                            {
                                player.Balance -= hupszCardForm.Amount;

                                if (player.Balance < 0)
                                {
                                    if (player.HasAGuardien > 0)
                                    {
                                        player.HasAGuardien -= 1;
                                        MessageBox.Show("Az őrangyalod most megmentett, de többször már nem tud!", player.Name);
                                        player.Balance += hupszCardForm.Amount;
                                        PlayerBalance.Text = player.Balance.ToString();
                                    }
                                    else
                                    {
                                        PlayerBalance.Text = player.Balance.ToString();
                                        m_GameHandler.PlayerGoesIntoFailure();
                                    }
                                }
                                else
                                {
                                    PlayerBalance.Text = player.Balance.ToString();
                                }

                                DrawTheCard.Enabled = false;
                                EndTurn.Enabled = true;
                            }

                            break;
                        default:
                            throw new Exception("This Action is not exists: " + hupszCardForm.ActionType);
                    }
                }
            }
            //Drawing a Hurra card
            else if (player.CurrentPosition == 8 || player.CurrentPosition == 18 || player.CurrentPosition == 28 || player.CurrentPosition == 38)
            {
                HurraCard hurraCardForm = new HurraCard(CurrentTurn, player, robot1, robot2, robot3, m_GameHandler.cards);
                if (hurraCardForm.ShowDialog() == DialogResult.OK)
                {
                    switch (hurraCardForm.ActionType)
                    {
                        case HurraAction.Jump:

                            m_GameHandler.JumpWithTheFigureToTheNextPosition(hurraCardForm.Amount);

                            m_ActionControl.PlayerAfterChangeHisPositionAction();

                            break;
                        case HurraAction.Roll:

                            ThrowWithTheCubes.Enabled = true;
                            DrawTheCard.Enabled = false;
                            EndTurn.Enabled = false;


                            break;
                        case HurraAction.Balance:

                            player.Balance += hurraCardForm.Amount;

                            PlayerBalance.Text = player.Balance.ToString();

                            DrawTheCard.Enabled = false;
                            EndTurn.Enabled = true;

                            break;
                        default:
                            throw new Exception("This Action is not exists: " + hurraCardForm.ActionType);
                    }
                }
            }
            //Drawing a Property card
            else
            {
                PropertyCard PropertyCardForm = new PropertyCard(player, m_GameHandler.cards);
                if (PropertyCardForm.ShowDialog() == DialogResult.OK)
                {
                    m_UpdateListBoxes.UpdatePlayerPropertyCardListBox();

                    player.Balance -= PropertyCardForm.FieldValue;
                    PlayerBalance.Text = player.Balance.ToString();

                    Field field = new Field();
                    field.FieldName = PropertyCardForm.FieldName;
                    field.OwnedField = true;
                    field.FieldOwner = player;
                    field.FieldValue = PropertyCardForm.FieldValue;
                    field.FieldFinishedValue = PropertyCardForm.FieldFinishedValue;
                    field.FieldLocation = player.CurrentPosition;

                    //tha player is an Archaeologist so he/she can do it 2x faster
                    if (player.IsArchaeologist)
                    {
                        double fieldBuildingTurns = PropertyCardForm.FieldBuildingTurns;
                        field.FieldBuildingTurns = (int)Math.Round(fieldBuildingTurns / 2);
                    }
                    else
                    {
                        field.FieldBuildingTurns = PropertyCardForm.FieldBuildingTurns;
                    }


                    OwnedProperties.Add(field);

                    DrawTheCard.Enabled = false;
                    LetsDoIt.Enabled = true;
                }
                else
                {
                    DrawTheCard.Enabled = false;
                    EndTurn.Enabled = true;
                }
            }
        }

        private void LetsDoIt_Click(object sender, EventArgs e)
        {
            foreach (Field field in OwnedProperties)
            {
                if (player.CurrentPosition == field.FieldLocation && field.FieldOwner == player)
                {
                    if (player.IsArchaeologist)
                    {
                        MessageBox.Show("Mivel régész vagy ezért kétszer olyan gyorsan végezheted el az ásatást!", player.Name);
                    }

                    field.StartedField = true;
                    FieldLabel[player.CurrentPosition].Text = field.FieldBuildingTurns.ToString();
                    FieldLabel[player.CurrentPosition].Visible = true;
                    FieldLabelOwner[player.CurrentPosition].Text = field.FieldOwner.Name;
                    FieldLabelOwner[player.CurrentPosition].Visible = true;
                    double Bill = field.FieldFinishedValue;
                    PropertyInfo.SetToolTip(Field[player.CurrentPosition], "Lelet: " + field.FieldName +
                        ",\n Fizetendő a telek befejezése esetén: " 
                        + Math.Round(Bill * 0.4) + "$,\n Fizetendő megkezdett lelet esetén: " 
                        + Math.Round(Bill * 0.25) + "$");
                }
            }

            LetsDoIt.Enabled = false;
            EndTurn.Enabled = true;
        }

        private void TradePropertyCard_Click(object sender, EventArgs e)
        {
            TradeWithRobotsView tradeView = new TradeWithRobotsView(player, OwnedProperties);
            if (tradeView.ShowDialog() == DialogResult.OK)
            {
                if (player.CanTrade > 0)
                {
                    player.CanTrade -= 1;
                    TradeNumber.Text = "Ennyi cseréd van még:" + player.CanTrade;

                    foreach (Field PlayerField in OwnedProperties)
                    {
                        Player playerFieldOwner = null;

                        if (PlayerField == tradeView.PlayersPropertyForTrade)
                        {
                            playerFieldOwner = PlayerField.FieldOwner;

                            foreach (Field RobotField in OwnedProperties)
                            {
                                if (RobotField == tradeView.RobotsPropertyForTrade)
                                {
                                    //player's label text change to robot and tha same with the robot's label
                                    FieldLabelOwner[PlayerField.FieldLocation].Text = RobotField.FieldOwner.Name;
                                    FieldLabelOwner[RobotField.FieldLocation].Text = PlayerField.FieldOwner.Name;

                                    //property cards trade
                                    if (RobotField.FieldOwner == robot1)
                                    {
                                        for (int i = 0; i < robot1.MyPropertyCards.Count; i++)
                                        {
                                            if (robot1.MyPropertyCards[i].PropertyName == RobotField.FieldName)
                                            {
                                                PlayerPropertyCardList.Items.Add(robot1.MyPropertyCards[i]);
                                                Robot1PropertyCardList.Items.Remove(robot1.MyPropertyCards[i]);

                                                player.MyPropertyCards.Add(robot1.MyPropertyCards[i]);
                                                robot1.MyPropertyCards.Remove(robot1.MyPropertyCards[i]);
                                            }
                                        }

                                        for (int i = 0; i < player.MyPropertyCards.Count; i++)
                                        {
                                            if (player.MyPropertyCards[i].PropertyName == PlayerField.FieldName)
                                            {
                                                Robot1PropertyCardList.Items.Add(player.MyPropertyCards[i]);
                                                PlayerPropertyCardList.Items.Remove(player.MyPropertyCards[i]);

                                                robot1.MyPropertyCards.Add(player.MyPropertyCards[i]);
                                                player.MyPropertyCards.Remove(player.MyPropertyCards[i]);
                                            }
                                        }

                                        m_UpdateListBoxes.UpdateRobot1PropertyCardListBox();
                                    }
                                    else if (RobotField.FieldOwner == robot2)
                                    {
                                        for (int i = 0; i < robot2.MyPropertyCards.Count; i++)
                                        {
                                            if (robot2.MyPropertyCards[i].PropertyName == RobotField.FieldName)
                                            {
                                                PlayerPropertyCardList.Items.Add(robot2.MyPropertyCards[i]);
                                                Robot2PropertyCardList.Items.Remove(robot2.MyPropertyCards[i]);

                                                player.MyPropertyCards.Add(robot2.MyPropertyCards[i]);
                                                robot2.MyPropertyCards.Remove(robot2.MyPropertyCards[i]);
                                            }
                                        }

                                        for (int i = 0; i < player.MyPropertyCards.Count; i++)
                                        {
                                            if (player.MyPropertyCards[i].PropertyName == PlayerField.FieldName)
                                            {
                                                Robot2PropertyCardList.Items.Add(player.MyPropertyCards[i]);
                                                PlayerPropertyCardList.Items.Remove(player.MyPropertyCards[i]);

                                                robot2.MyPropertyCards.Add(player.MyPropertyCards[i]);
                                                player.MyPropertyCards.Remove(player.MyPropertyCards[i]);
                                            }
                                        }

                                        m_UpdateListBoxes.UpdateRobot2PropertyCardListBox();
                                    }
                                    else if (RobotField.FieldOwner == robot3)
                                    {
                                        for (int i = 0; i < robot3.MyPropertyCards.Count; i++)
                                        {
                                            if (robot3.MyPropertyCards[i].PropertyName == RobotField.FieldName)
                                            {
                                                PlayerPropertyCardList.Items.Add(robot3.MyPropertyCards[i]);
                                                Robot3PropertyCardList.Items.Remove(robot3.MyPropertyCards[i]);

                                                player.MyPropertyCards.Add(robot3.MyPropertyCards[i]);
                                                robot3.MyPropertyCards.Remove(robot3.MyPropertyCards[i]);
                                            }
                                        }

                                        for (int i = 0; i < player.MyPropertyCards.Count; i++)
                                        {
                                            if (player.MyPropertyCards[i].PropertyName == PlayerField.FieldName)
                                            {
                                                Robot3PropertyCardList.Items.Add(player.MyPropertyCards[i]);
                                                PlayerPropertyCardList.Items.Remove(player.MyPropertyCards[i]);

                                                robot3.MyPropertyCards.Add(player.MyPropertyCards[i]);
                                                player.MyPropertyCards.Remove(player.MyPropertyCards[i]);
                                            }
                                        }

                                        m_UpdateListBoxes.UpdateRobot3PropertyCardListBox();
                                    }
                                    else
                                    {
                                        throw new Exception("there is no field owner like: " + RobotField.FieldOwner.Name);
                                    }

                                    m_UpdateListBoxes.UpdatePlayerPropertyCardListBox();

                                    //owners change
                                    PlayerField.FieldOwner = RobotField.FieldOwner;
                                    RobotField.FieldOwner = playerFieldOwner;
                                }
                            }
                        }
                    }

                    TradeNumber.Text = "Ennyi cseréd van még:" + player.CanTrade;
                    TradePropertyCard.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Sajnos már nem tudsz többet cserélni.", player.Name);
                    TradePropertyCard.Visible = false;
                    TradeNumber.Visible = false;
                    TradePropertyCard.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("A kereskedési lehetőséged megmaradt, felhasználhatod legközelebb!", player.Name);
                TradePropertyCard.Enabled = false;
            }
        }

        private void StealPassiveAbility_Click(object sender, EventArgs e)
        {
            StealPassiveAbilityFromRobotsView stealPassiveAbilityFromRobots = new StealPassiveAbilityFromRobotsView(player, robot1, robot2, robot3);
            if (stealPassiveAbilityFromRobots.ShowDialog() == DialogResult.OK)
            {
                if (player.CanStealAPassiveAbility > 0)
                {
                    player.CanStealAPassiveAbility -= 1;
                    StealNumber.Text = "Ennyit lophatsz még:" + player.CanStealAPassiveAbility;

                    if (robot1.MyPassiveCards.Contains(stealPassiveAbilityFromRobots.RobotSelectedPassiveAbility))
                    {
                        for (int i = 0; i < robot1.MyPassiveCards.Count; i++)
                        {
                            if (robot1.MyPassiveCards[i] == stealPassiveAbilityFromRobots.RobotSelectedPassiveAbility)
                            {
                               m_GameHandler.GiveThePAssiveAbilityForPlayer(i, robot1);

                                player.MyPassiveCards.Add(robot1.MyPassiveCards[i]);
                                //PlayerPassiveCardList.Items.Add(robot1.MyPassiveCards[i]);

                                robot1.MyPassiveCards.Remove(robot1.MyPassiveCards[i]);
                                //Robot1PassiveCardList.Items.Remove(robot1.MyPassiveCards[i]);

                                m_UpdateListBoxes.UpdateRobot1PassiveCardListBox();
                            }
                        }
                    }
                    else if (robot2.MyPassiveCards.Contains(stealPassiveAbilityFromRobots.RobotSelectedPassiveAbility))
                    {
                        for (int i = 0; i < robot2.MyPassiveCards.Count; i++)
                        {
                            if (robot2.MyPassiveCards[i] == stealPassiveAbilityFromRobots.RobotSelectedPassiveAbility)
                            {
                                m_GameHandler.GiveThePAssiveAbilityForPlayer(i, robot2);

                                player.MyPassiveCards.Add(robot2.MyPassiveCards[i]);
                                //PlayerPassiveCardList.Items.Add(robot2.MyPassiveCards[i]);

                                robot2.MyPassiveCards.Remove(robot2.MyPassiveCards[i]);
                                //Robot2PassiveCardList.Items.Remove(robot2.MyPassiveCards[i]);

                                m_UpdateListBoxes.UpdateRobot2PassiveCardListBox();
                            }
                        }
                    }
                    else if (robot3.MyPassiveCards.Contains(stealPassiveAbilityFromRobots.RobotSelectedPassiveAbility))
                    {
                        for (int i = 0; i < robot3.MyPassiveCards.Count; i++)
                        {
                            if (robot3.MyPassiveCards[i] == stealPassiveAbilityFromRobots.RobotSelectedPassiveAbility)
                            {
                                m_GameHandler.GiveThePAssiveAbilityForPlayer(i, robot3);

                                player.MyPassiveCards.Add(robot3.MyPassiveCards[i]);
                                //PlayerPassiveCardList.Items.Add(robot3.MyPassiveCards[i]);

                                robot3.MyPassiveCards.Remove(robot3.MyPassiveCards[i]);
                                //Robot3PassiveCardList.Items.Remove(robot3.MyPassiveCards[i]);

                                m_UpdateListBoxes.UpdateRobot3PassiveCardListBox();
                            }
                        }
                    }
                    m_UpdateListBoxes.UpdatePlayerPassiveCardListBox();
                }
                else
                {
                    MessageBox.Show("Sajnos már nem tudsz többet lopni.", player.Name);
                    StealPassiveAbility.Visible = false;
                    StealNumber.Visible = false;
                    StealPassiveAbility.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("A lopási lehetőséged megmaradt, felhasználhatod legközelebb!", player.Name);
                StealPassiveAbility.Enabled = false;
            }
        }

        private void EndTurn_Click(object sender, EventArgs e)
        {
            if (player.IsEntrepreneur)
            {
                Random randomPassiveBalance = new Random();
                int myRandomPassiveBalance = randomPassiveBalance.Next(50, 201);
                player.Balance += myRandomPassiveBalance;
                PlayerBalance.Text = player.Balance.ToString();

                MessageBox.Show("Mivel vállalkozó vagy a körödvégén egy bizonyos jövedelem kerül az egyenlegedre, ez az egyenleg most " + myRandomPassiveBalance + " $ volt.", player.Name);
            }

            if (player.PlayerNumber == CurrentTurn) //blue
            {
                ThrowWithTheCubes.Enabled = false;
                EndTurn.Enabled = false;

                GetNextRobot();
            }

            System.Threading.Thread.Sleep(200);

            m_RobotTurns.RobotTurns();

            TurnText.Text = player.Name + turnText;
        }

        private void GetNextRobot()
        {
            if (robot1.CanNotRollForXTurn > 0)
            {
                robot1.CanNotRollForXTurn -= 1;

                MessageBox.Show(robot1.Name + " kimaradt, " + robot2.Name + " következik.");

                if (robot1.CanNotRollForXTurn == 0)
                {
                    ChangeLabelColor(Robot1CanRoll);
                }
                else
                {
                    Robot1CanRoll.Text = robot1.CanNotRollForXTurn + "-körig kimarad!";
                }

                if (robot2.CanNotRollForXTurn > 0)
                {
                    robot2.CanNotRollForXTurn -= 1;

                    MessageBox.Show(robot2.Name + " kimaradt, " + robot3.Name + " következik.");

                    if (robot2.CanNotRollForXTurn == 0)
                    {
                        ChangeLabelColor(Robot2CanRoll);
                    }
                    else
                    {
                        Robot2CanRoll.Text = robot2.CanNotRollForXTurn + "-körig kimarad!";
                    }

                    if (robot3.CanNotRollForXTurn > 0)
                    {
                        robot3.CanNotRollForXTurn -= 1;

                        MessageBox.Show(robot3.Name + " kimaradt, " + player.Name + " következik.");

                        if (robot3.CanNotRollForXTurn == 0)
                        {
                            ChangeLabelColor(Robot3CanRoll);
                        }
                        else
                        {
                            Robot3CanRoll.Text = robot3.CanNotRollForXTurn + "-körig kimarad!";
                        }

                        if (player.CanNotRollForXTurn > 0)
                        {
                            m_CalculateNextPlayer.GetTheNextPlayersTurnStartingWithThePlayer();
                        }
                        else
                        {
                            m_NewTurnHelper.NewTurnWithPlayer();

                            ThrowWithTheCubes.Enabled = true;
                            EndTurn.Enabled = false;
                        }
                    }
                    else
                    {
                        m_NewTurnHelper.NewTurnWithRobot3();
                    }
                }
                else
                {
                    m_NewTurnHelper.NewTurnWithRobot2();
                }
            }
            else
            {
                m_NewTurnHelper.NewTurnWithRobot1();
            }
        }

        private void ChangeLabelColor(Label label)
        {
            label.Text = "Léphetsz!";
            label.ForeColor = Color.Green;
        }

        public void StepFigure(Player player, int thrownValue)
        {
            int x, y;
            int xOffset = 0;
            int yOffset = 0;

            switch (player.FigureColor)
            {
                case Figure.BlueFigure:
                    break;
                case Figure.RedFigure:
                    xOffset = 25;
                    yOffset = 25;
                    break;
                case Figure.YellowFigure:
                    yOffset = 25;
                    break;
                case Figure.GreenFigure:
                    xOffset = 25;
                    break;
                default:
                    break;
            }

            player.NextPosition = player.CurrentPosition + thrownValue;

            if (player.NextPosition >= 40)
            {
                player.NextPosition -= 40;
            }
            else if (player.NextPosition < 0)
            {
                player.NextPosition += 40;
            }

            x = Field[player.NextPosition].Location.X;
            y = Field[player.NextPosition].Location.Y;

            switch (player.FigureColor)
            {
                case Figure.BlueFigure:
                    BlueFigure.Location = new Point(x + xOffset, y + yOffset);
                    break;
                case Figure.RedFigure:
                    RedFigure.Location = new Point(x + xOffset, y + yOffset);
                    break;
                case Figure.YellowFigure:
                    YellowFigure.Location = new Point(x + xOffset, y + yOffset);
                    break;
                case Figure.GreenFigure:
                    GreenFigure.Location = new Point(x + xOffset, y + yOffset);
                    break;
                default:
                    break;
            }

            player.CurrentPosition = player.NextPosition;
            player.NextPosition = 0;
        }
    }
}