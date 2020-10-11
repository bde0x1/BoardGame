using BoardGame.Domain_Model;

namespace BoardGame.BusinessLogics
{
    public class NewTurnHelper
    {
        private BoardGame m_BoardGameForm;
        private GameHandler m_GameHandler;
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;

        public NewTurnHelper(GameHandler gameHandler, BoardGame boardGame)
        {
            m_GameHandler = gameHandler;
            m_BoardGameForm = boardGame;
            player = gameHandler.player;
            robot1 = gameHandler.robot1;
            robot2 = gameHandler.robot2;
            robot3 = gameHandler.robot3;
        }

        // Next turns reduces the property building turns
        public void NewTurnWithPlayer()
        {
            int finishedProperties = 0;

            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.FinishedField)
                {
                    finishedProperties += 1;
                }
            }

            if (finishedProperties == 24)
            {
                m_GameHandler.GameOver();
            }
            else
            {
                m_BoardGameForm.CurrentTurn = 0;

                foreach (Field field in m_BoardGameForm.OwnedProperties)
                {
                    if (field.StartedField == true && field.FieldOwner == player)
                    {
                        field.FieldBuildingTurns -= 1;

                        if (field.FieldBuildingTurns > 0)
                        {
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = field.FieldBuildingTurns.ToString();
                        }
                        else
                        {
                            field.StartedField = false;
                            field.FinishedField = true;
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = "Kész";
                            player.Balance += field.FieldFinishedValue;
                            m_BoardGameForm.PlayerBalance.Text = player.Balance.ToString();
                        }
                    }
                }

                m_BoardGameForm.ThrowWithTheCubes.Enabled = true;
                m_BoardGameForm.EndTurn.Enabled = false;

                if (player.CanTrade > 0)
                {
                    m_BoardGameForm.TradePropertyCard.Enabled = true;
                }

                if (player.CanStealAPassiveAbility > 0)
                {
                    m_BoardGameForm.StealPassiveAbility.Enabled = true;
                }
            }
        }
        public void NewTurnWithRobot1()
        {
            int finishedProperties = 0;

            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.FinishedField)
                {
                    finishedProperties += 1;
                }
            }

            if (finishedProperties == 24)
            {
                m_GameHandler.GameOver();
            }
            else
            {
                m_BoardGameForm.CurrentTurn = 1;

                foreach (Field field in m_BoardGameForm.OwnedProperties)
                {
                    if (field.StartedField == true && field.FieldOwner == robot1)
                    {
                        field.FieldBuildingTurns -= 1;

                        if (field.FieldBuildingTurns > 0)
                        {
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = field.FieldBuildingTurns.ToString();
                        }
                        else
                        {
                            field.StartedField = false;
                            field.FinishedField = true;
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = "Kész";
                            m_BoardGameForm.robot1.Balance += field.FieldFinishedValue;
                            m_BoardGameForm.Robot1Balance.Text = robot1.Balance.ToString();
                        }
                    }
                }
            }
        }
        public void NewTurnWithRobot2()
        {
            int finishedProperties = 0;

            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.FinishedField)
                {
                    finishedProperties += 1;
                }
            }

            if (finishedProperties == 24)
            {
                m_GameHandler.GameOver();
            }
            else
            {
                m_BoardGameForm.CurrentTurn = 2;

                foreach (Field field in m_BoardGameForm.OwnedProperties)
                {
                    if (field.StartedField == true && field.FieldOwner == robot2)
                    {
                        field.FieldBuildingTurns -= 1;

                        if (field.FieldBuildingTurns > 0)
                        {
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = field.FieldBuildingTurns.ToString();
                        }
                        else
                        {
                            field.StartedField = false;
                            field.FinishedField = true;
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = "Kész";
                            m_BoardGameForm.robot2.Balance += field.FieldFinishedValue;
                            m_BoardGameForm.Robot2Balance.Text = robot2.Balance.ToString();
                        }
                    }
                }
            }
        }
        public void NewTurnWithRobot3()
        {
            int finishedProperties = 0;

            foreach (Field field in m_BoardGameForm.OwnedProperties)
            {
                if (field.FinishedField)
                {
                    finishedProperties += 1;
                }
            }

            if (finishedProperties == 24)
            {
                m_GameHandler.GameOver();
            }
            else
            {
                m_BoardGameForm.CurrentTurn = 3;

                foreach (Field field in m_BoardGameForm.OwnedProperties)
                {
                    if (field.StartedField == true && field.FieldOwner == robot3)
                    {
                        field.FieldBuildingTurns -= 1;

                        if (field.FieldBuildingTurns > 0)
                        {
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = field.FieldBuildingTurns.ToString();
                        }
                        else
                        {
                            field.StartedField = false;
                            field.FinishedField = true;
                            m_BoardGameForm.FieldLabel[field.FieldLocation].Text = "Kész";
                            robot3.Balance += field.FieldFinishedValue;
                            m_BoardGameForm.Robot3Balance.Text = robot3.Balance.ToString();
                        }
                    }
                }
            }
        }
    }
}
