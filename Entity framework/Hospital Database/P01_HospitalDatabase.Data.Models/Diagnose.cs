using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Diagnose
    {

        [Key]
        [Required]
        public int DiagnoseId { get; set; }

        [Required]
        [Column(TypeName ="NVARCHAR(50)")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        [MaxLength(250)]
        public string Comments { get; set; }

        [Required]
        public int PatientId { get; set; }

        public  virtual Patient Patient { get; set; }
    }
}
