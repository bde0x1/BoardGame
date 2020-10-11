namespace BoardGame.CardModelFromDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PropertyID { get; set; }

        [Required]
        [StringLength(255)]
        public string PropertyName { get; set; }

        public int BuildingTurns { get; set; }

        public int PropertyFinishedValue { get; set; }

        public int PropertyValue { get; set; }
    }
}
