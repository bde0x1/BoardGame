namespace BoardGame
{
    public class Field
    {
        public string FieldName { get; set; }
        public Player FieldOwner { get; set; }
        public int FieldLocation { get; set; }
        public int FieldValue { get; set; }
        public int FieldBuildingTurns { get; set; }
        public int FieldFinishedValue { get; set; }
        public bool OwnedField { get; set; }
        public bool StartedField { get; set; }
        public bool FinishedField { get; set; }

        public override string ToString()
        {
            return "["+FieldOwner+"] " + FieldName + ", jövedelem: "+ FieldFinishedValue;
        }
    }
}
