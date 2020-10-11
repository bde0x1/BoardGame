using System.Collections.Generic;

namespace BoardGame.Domain_Model
{
    public class Cards
    {
        public List<CardModelFromDatabase.PassiveCard> PassiveCards { get; set; }
        public List<CardModelFromDatabase.HappyCard> HurraCard { get; set; }
        public List<CardModelFromDatabase.SadCard> HupszCard { get; set; }
        public List<CardModelFromDatabase.HappyCard> UsedHurraCard { get; set; }
        public List<CardModelFromDatabase.SadCard> UsedHupszCard { get; set; }
        public List<CardModelFromDatabase.PropertyCard> PropertyCards { get; set; }
    }
}
