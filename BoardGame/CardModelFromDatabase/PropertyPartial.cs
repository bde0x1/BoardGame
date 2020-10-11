namespace BoardGame.CardModelFromDatabase
{
    public partial class PropertyCard
    {
        public override string ToString()
        {
            return "Lelet: " + PropertyName + ", Ár: " + PropertyValue + ", Jövedelem: " + PropertyFinishedValue;
        }
    }
}
