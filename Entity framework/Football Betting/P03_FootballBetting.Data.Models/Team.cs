﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Team
    {
        public Team()
        {
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
            this.Players = new HashSet<Player>();
        }
        public int TeamId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Initials { get; set; }

        public decimal Budget { get; set; }

        [ForeignKey("Color")]
        public int PrimaryKitColorId { get; set; }
        public Color PrimaryKitColor { get; set; }

        [ForeignKey("Color")]      
        public int SecondaryKitColorId { get; set; }
        public Color SecondaryKitColor { get; set; }


        [ForeignKey("Town")]
        public int TownId { get; set; }
        public Town Town { get; set; }
        [InverseProperty("HomeTeam")]
        public virtual ICollection<Game>  HomeGames { get; set; }
        [InverseProperty("AwayTeam")]
        public virtual ICollection<Game>  AwayGames { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}