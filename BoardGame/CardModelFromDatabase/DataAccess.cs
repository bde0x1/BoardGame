using System.Collections.Generic;
using System.Linq;

namespace BoardGame.CardModelFromDatabase
{
    class DataAccess
    {
        private readonly CardModel m_Database = new CardModel();

        public List<PassiveCard> GetAllPassiveCard()
        {
            return m_Database.PassiveCards.Select(e => e).ToList();
        }

        public List<SadCard> GetAllHupszCard()
        {
            return m_Database.SadCards.Select(e => e).ToList();
        }

        public List<HappyCard> GetAllHurraCard()
        {
            return m_Database.HappyCards.Select(e => e).ToList();
        }

        public List<PropertyCard> GetAllPropertyCard()
        {
            return m_Database.PropertyCards.Select(e => e).ToList();
        }
    }
}
