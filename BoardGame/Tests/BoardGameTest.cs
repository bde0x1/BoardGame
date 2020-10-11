using NUnit.Framework;
using BoardGame.BusinessLogics;
using BoardGame.Domain_Model;
using BoardGame.DomainModel;
using Moq;
using System.Collections.Generic;

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

        [TestCase]
        public void TestGameOverMethod()
        {
            level.LevelType = Levels.Nehéz;
            GameHandler handler = new GameHandler(player, robot1, robot2, robot3, level);

            string FinalResult = handler.ResultAddvertisement(player, robot1, robot2, robot3);

            string expectedResult = "Bob,Joe,Barna,Meddy";

            Assert.AreEqual(expectedResult, FinalResult);
        }

        //[TestCase]
        //public void TestICanLose()
        //{
        //    level.LevelType = Levels.Nehéz;
        //    GameHandler handler = new GameHandler(player, robot1, robot2, robot3, level);
        //    BoardGame bg = new BoardGame(handler, level);
        //    bg.CurrentTurn = 1;

        //    var listOfPropertyCards = new List<CardModelFromDatabase.PropertyCard>();
        //    listOfPropertyCards.Add(new CardModelFromDatabase.PropertyCard { BuildingTurns  = 1, PropertyFinishedValue = 100, PropertyID=1, PropertyName="a", PropertyValue=50 });
        //    Mock<GameHandler> mockG = new Mock<GameHandler>();
        //    Mock<Cards> mockC = new Mock<Cards>();
        //    mockG.Setup(cards => cards).Equals(mockC.Object);
        //    mockC.Setup(cards => cards.PropertyCards).Returns(listOfPropertyCards);

        //    bg.OwnedProperties.Add(GetField(player, 100, 3));
        //    bg.OwnedProperties.Add(GetField(player, 100, 7));
        //    bg.OwnedProperties.Add(GetField(player, 100, 16));
        //    bg.OwnedProperties.Add(GetField(player, 100, 27));
        //    bg.OwnedProperties.Add(GetField(robot2, 100, 17));
        //    bg.OwnedProperties.Add(GetField(robot2, 100, 21));
        //    bg.OwnedProperties.Add(GetField(robot2, 100, 31));
        //    bg.OwnedProperties.Add(GetField(robot2, 100, 33));
        //    bg.OwnedProperties.Add(GetField(robot1, 100, 4));
        //    bg.OwnedProperties.Add(GetField(robot1, 100, 6));
        //    bg.OwnedProperties.Add(GetField(robot1, 100, 29));
        //    bg.OwnedProperties.Add(GetField(robot1, 100, 37));
        //    bg.OwnedProperties.Add(GetField(robot3, 100, 9));
        //    bg.OwnedProperties.Add(GetField(robot3, 100, 39));
        //    bg.OwnedProperties.Add(GetField(robot3, 100, 34));


        //    player.CurrentPosition = 1;
        //    robot2.CurrentPosition = 25;
        //    robot3.CurrentPosition = 36;
        //    robot1.CurrentPosition = 13;

        //    PropertyCardForRobot PCFR = new  PropertyCardForRobot(bg, player, robot1, robot2, robot3, mockG.Object.cards, level);

        //    double FinalResult = PCFR.ICanLose(robot1);

        //    double expectedResult = 100;

        //    Assert.AreEqual(expectedResult, FinalResult);
        //}

        //private Field GetField(Player owner, int price, int location)
        //{
        //    Field field = new Field();
        //    field.OwnedField = true;
        //    field.FieldOwner = owner;
        //    field.FieldValue = 200;
        //    field.FieldFinishedValue = price;
        //    field.FieldLocation = location;

        //    return field;
        //}
    }
}
