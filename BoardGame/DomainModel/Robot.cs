namespace BoardGame.Domain_Model
{
    public class Robot : Player
    {
        public Robot(string name, int playerNumber, int canNotRoll, int balance) : base(name, playerNumber, canNotRoll, balance)
        {
            this.Name = name;
            this.PlayerNumber = playerNumber;
            this.CanNotRollForXTurn = canNotRoll;
            this.Balance = balance;
        }
    }
}
