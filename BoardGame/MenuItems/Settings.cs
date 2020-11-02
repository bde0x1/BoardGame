using BoardGame.BusinessLogics;
using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System;
using System.IO;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class Settings : Form
    {
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;
        private Level m_Level;

        public Settings(Player Player, Robot Robot1, Robot Robot2, Robot Robot3, Level level)
        {
            InitializeComponent();

            ButtonTip.SetToolTip(Cancel, "Kilépés a Menübe");
            ButtonTip.SetToolTip(SaveSettings, "Mentés, és kilépés a menübe");

            this.player = Player;
            this.robot1 = Robot1;
            this.robot2 = Robot2;
            this.robot3 = Robot3;
            this.m_Level = level;

            foreach (var llevels in Enum.GetValues(typeof(Figure)))
            {
                LevelSelection.Items.Add(llevels);
            }

            foreach (var figure in Enum.GetValues(typeof(Figure)))
            {
                PlayerFigureColor.Items.Add(figure);
                Robot1FigureColor.Items.Add(figure);
                Robot2FigureColor.Items.Add(figure);
                Robot3FigureColor.Items.Add(figure);
            }

            FillTheSettingsNodes();
        }

        private void FillTheSettingsNodes()
        {
            if (File.Exists("UserSettings.xml"))
            {
                WriteOrReadXMLFile.ImportData(player, robot1, robot2, robot3, m_Level);

                InitializeUIElements();
            }
            else
            {
                WriteOrReadXMLFile.ImportDefaultSettings(player, robot1, robot2, robot3, m_Level);

                InitializeUIElements();
            }
        }

        private void InitializeUIElements()
        {
            PlayerName.Text = player.Name;
            Robot1Name.Text = robot1.Name;
            Robot2Name.Text = robot2.Name;
            Robot3Name.Text = robot3.Name;

            PlayerFigureColor.SelectedItem = player.FigureColor;
            Robot1FigureColor.SelectedItem = robot1.FigureColor;
            Robot2FigureColor.SelectedItem = robot2.FigureColor;
            Robot3FigureColor.SelectedItem = robot3.FigureColor;

            StarterMoney.Text = player.Balance.ToString();

            LevelSelection.SelectedItem = m_Level.LevelType;
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            if (CheckThatUSerFilledEveryTextBox())
            {
                if (CheckThatPlayerSelectedTheFigures())
                {
                    if (CheckThatTheUserDidNotChoseTwoOrMoreSameFigure())
                    {
                        player.Name = PlayerName.Text;
                        robot1.Name = Robot1Name.Text;
                        robot2.Name = Robot2Name.Text;
                        robot3.Name = Robot3Name.Text;

                        player.FigureColor = (Figure)PlayerFigureColor.SelectedItem;
                        robot1.FigureColor = (Figure)Robot1FigureColor.SelectedItem;
                        robot2.FigureColor = (Figure)Robot2FigureColor.SelectedItem;
                        robot3.FigureColor = (Figure)Robot3FigureColor.SelectedItem;

                        m_Level.LevelType = (Levels)LevelSelection.SelectedItem;

                        if (int.TryParse(StarterMoney.Text, out int starterMoneys))
                        {
                            player.Balance = starterMoneys;
                            robot1.Balance = starterMoneys;
                            robot2.Balance = starterMoneys;
                            robot3.Balance = starterMoneys;

                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kérlek töltsd ki számokkal a pénz mezőt!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kérlek minden játékosnak válassz különböző figurát!");
                    }
                }
                else
                {
                    MessageBox.Show("Kérlek minden játékosnak válassz egy figurát!");
                }
            }
            else
            {
                MessageBox.Show("Kérlek töltsd ki a mezőket!");
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckThatUSerFilledEveryTextBox()
        {
            if (PlayerName.Text != "" && Robot1Name.Text != "" && Robot2Name.Text != "" && Robot3Name.Text != "" && StarterMoney.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckThatTheUserDidNotChoseTwoOrMoreSameFigure()
        {
            if ((Figure)PlayerFigureColor.SelectedItem != (Figure)Robot1FigureColor.SelectedItem && (Figure)PlayerFigureColor.SelectedItem != (Figure)Robot2FigureColor.SelectedItem && (Figure)PlayerFigureColor.SelectedItem != (Figure)Robot3FigureColor.SelectedItem && (Figure)Robot1FigureColor.SelectedItem != (Figure)Robot2FigureColor.SelectedItem && (Figure)Robot1FigureColor.SelectedItem != (Figure)Robot3FigureColor.SelectedItem && (Figure)Robot2FigureColor.SelectedItem != (Figure)Robot3FigureColor.SelectedItem)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckThatPlayerSelectedTheFigures()
        {
            string defaultString = "Válassz egyet";

            if (PlayerFigureColor.Text != defaultString && Robot1FigureColor.Text != defaultString && Robot2FigureColor.Text != defaultString && Robot3FigureColor.Text != defaultString)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
