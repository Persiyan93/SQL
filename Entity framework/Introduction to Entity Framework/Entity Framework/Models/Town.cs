using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entity_Framework.Models
{
    public partial class Town
    {
        public Town()
        {
            Addresses = new HashSet<Addres>();
        }

        [Key]
        [Column("TownID")]
        public int TownId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(Addres.Town))]
        public virtual ICollection<Addres> Addresses { get; set; }
    }
}
