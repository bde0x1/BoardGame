namespace BoardGame.CardModelFromDatabase
{
    using System.Data.Entity;

    public partial class CardModel : DbContext
    {
        public CardModel()
            : base("name=CardsFromDatabase")
        {
        }

        public virtual DbSet<HappyCard> HappyCards { get; set; }
        public virtual DbSet<PassiveCard> PassiveCards { get; set; }
        public virtual DbSet<PropertyCard> PropertyCards { get; set; }
        public virtual DbSet<SadCard> SadCards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
