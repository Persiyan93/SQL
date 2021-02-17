using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class Player
    {
        public Player()
        {
            this.PlayerStatistic = new HashSet<PlayerStatistic>();
            this.Bets = new HashSet<Bet>();
        }
        public int PlayerId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int SquadNumber { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        public bool IsInjured { get; set; }
        public  virtual ICollection< PlayerStatistic> PlayerStatistic { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }

    }
}
