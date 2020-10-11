using BoardGame.Domain_Model;
using System.Collections.Generic;

namespace BoardGame
{
    public class Player
    {
        public Player(string name, int playerNumber, int canNotRoll, int balance)
        {
            this.Name = name;
            this.PlayerNumber = playerNumber;
            this.CanNotRollForXTurn = canNotRoll;
            this.Balance = balance;
        }

        public int PlayerNumber { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; }
        public int NextPosition { get; set; }
        public int Balance { get; set; }
        public int CanNotRollForXTurn { get; set; }
        public bool Loser { get; set; }
        public Figure FigureColor { get; set; }

        public List<CardModelFromDatabase.PassiveCard> MyPassiveCards { get; set; }
        public List<CardModelFromDatabase.PropertyCard> MyPropertyCards { get; set; }

        public bool IsArchaeologist { get; set; }
        public bool IsEntrepreneur { get; set; }
        public bool IsCareful { get; set; }
        public bool HasAFriend { get; set; }
        public bool IsPointful { get; set; }
        public int CanTrade { get; set; }
        public int CanStealAPassiveAbility { get; set; }
        public int HasAGuardien { get; set; }
    }
}
