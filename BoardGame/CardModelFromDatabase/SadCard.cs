namespace BoardGame.CardModelFromDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SadCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SadID { get; set; }

        [Required]
        [StringLength(255)]
        public string Situation { get; set; }

        public int Ammount { get; set; }
    }
}
