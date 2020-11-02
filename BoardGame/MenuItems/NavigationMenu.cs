using BoardGame.BusinessLogics;
using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using System;
using System.IO;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class NavigationMenu : Form
    {
        private Player player;
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;
        private Level level;

        public NavigationMenu()
        {
            InitializeComponent();

            player = new Player("", 0, 0, 0);
            robot1 = new Robot("", 1, 0, 0);
            robot2 = new Robot("", 2, 0, 0);
            robot3 = new Robot("", 3, 0, 0);
            level = new Level();

            if (File.Exists("UserSettings.xml"))
            {
                WriteOrReadXMLFile.ImportData(player, robot1, robot2, robot3, level);
            }
            else
            {
                WriteOrReadXMLFile.ImportDefaultSettings(player, robot1, robot2, robot3, level);
            }

            ButtonTip.SetToolTip(GoToTheGame,"Játék");
            ButtonTip.SetToolTip(GoToSettings, "Beállítások");
            ButtonTip.SetToolTip(GoToRules, "Játékszabályzat");
            ButtonTip.SetToolTip(Exit, "Kilépés a Windowsba");
        }

        private void GoToTheGame_Click(object sender, EventArgs e)
        {
            if (File.Exists("UserSettings.xml"))
            {
                GameHandler gameHandler = new GameHandler(player,robot1,robot2,robot3,level);
                gameHandler.StartGame();
            }
            else
            {
                MessageBox.Show("Kérlek a beállításokban tölts ki mindent!");
            }
            
        }

        private void GoToRules_Click(object sender, EventArgs e)
        {
            GameRulesView gameRulesView = new GameRulesView();
            gameRulesView.Show();
        }

        private void GoToSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(player,robot1,robot2,robot3, level);
            if (settings.ShowDialog() == DialogResult.OK)
            {
                WriteOrReadXMLFile.ExportData(player, robot1, robot2, robot3, level);
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Biztos hogy kilépsz?", "Exit", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialog == DialogResult.No)
            {
                return;
            }
        }
    }
}
