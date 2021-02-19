using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
   public   class Visitation
    {

        [Key]
        [Required]
        public int VisitationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Column(TypeName ="NVARCHAR(250)")]
        [MaxLength(250)]
        public string Comments { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    
       
    }
}
