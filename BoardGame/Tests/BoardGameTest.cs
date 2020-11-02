using NUnit.Framework;
using BoardGame.BusinessLogics;
using BoardGame.Domain_Model;
using BoardGame.DomainModel;

namespace BoardGame.Tests
{
    [TestFixture]
    class BoardGameTest
    {
        Player player = new Player("Barna", 0, 0, 3600);
        Robot robot1 = new Robot("Bob", 1, 0, 12000);
        Robot robot2 = new Robot("Joe", 2, 0, 4500);
        Robot robot3 = new Robot("Meddy", 3, 0, 3200);
        Level level = new Level();

        [Test]
        public void TestGameOverMethod()
        {
            level.LevelType = Levels.Nehéz;
            var handler = new GameHandler(player, robot1, robot2, robot3, level);

            string finalResult = handler.ResultAddvertisement(player, robot1, robot2, robot3);

            string expectedResult = "Bob,Joe,Barna,Meddy,";

            Assert.AreEqual(expectedResult, finalResult);
        }

        [TestCase(10)]
        [TestCase(-10)]
        public void TestStepFigureMethd(int throwenValue)
        {
            level.LevelType = Levels.Nehéz;
            var handler = new GameHandler(player, robot1, robot2, robot3, level);
            var bGame = new BoardGame(handler,level);
            player.CurrentPosition = 0;
            bGame.StepFigure(player, throwenValue);

            var finalResult = player.CurrentPosition;            

            if (throwenValue == 10)
            {
                var expectedResult = 10;
                Assert.AreEqual(expectedResult, finalResult);
            }
            else if (throwenValue == -10)
            {
                var expectedResult = 30;
                Assert.AreEqual(expectedResult, finalResult);
            }           
        }

        [Test]
        public void TestRollTheDiceMethod()
        {
            level.LevelType = Levels.Nehéz;
            var handler = new GameHandler(player, robot1, robot2, robot3, level);
            var throwenValue = handler.RollTheDice();
            Assert.Positive(throwenValue);
            Assert.GreaterOrEqual(throwenValue,1);
            Assert.IsNotNull(throwenValue);
        }

        [Test]
        public void TestPlayerGoesIntoFailureMethod()
        {
            level.LevelType = Levels.Nehéz;
            var handler = new GameHandler(player, robot1, robot2, robot3, level);
            handler.PlayerGoesIntoFailure();
            Assert.IsTrue(player.Loser);
        }
    }
}
