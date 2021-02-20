using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {

        public Doctor()
        {
            this.Visitations = new HashSet<Visitation>();
        }
        public int DoctorId { get; set; }



        [Required]
        [Column(TypeName ="NVARCHAR(100)")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        [MaxLength(100)]
        public string Specialty { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }

    }
}
