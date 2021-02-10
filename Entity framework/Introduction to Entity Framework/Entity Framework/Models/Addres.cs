using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entity_Framework.Models
{
    public partial class Addres
    {
        public Addres()
        {
            Employees = new HashSet<Employe>();
        }

        [Key]
        [Column("AddressID")]
        public int AddressId { get; set; }
        [Required]
        [StringLength(100)]
        public string AddressText { get; set; }
        [Column("TownID")]
        public int? TownId { get; set; }

        [ForeignKey(nameof(TownId))]
        [InverseProperty("Addresses")]
        public virtual Town Town { get; set; }
        [InverseProperty(nameof(Employe.Address))]
        public virtual ICollection<Employe> Employees { get; set; }
    }
}
