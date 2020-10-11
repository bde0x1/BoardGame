namespace BoardGame.DomainModel
{
    public enum Levels {Könnyű, Nehéz };
    
    public class Level
    {
        public Levels LevelType { get; set; }
    }
}
