using BoardGame.Domain_Model;

namespace BoardGame.CardModelFromDatabase
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PassiveCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PassiveID { get; set; }

        [Required]
        [StringLength(255)]
        public string SpecialPassiveAbility { get; set; }

        public int AbilityValue { get; set; }
    }
}
