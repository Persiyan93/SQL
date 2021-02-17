using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Initials { get; set; }

        public decimal Budget { get; set; }

        [ForeignKey("Color")]
        public int PrimaryKitColorId { get; set; }
        [ForeignKey("Color")]
        public int SecondaryKitColorId { get; set; }

        [ForeignKey("Town")]
        public int TownId { get; set; }
        public Town Town { get; set; }
    }
}
