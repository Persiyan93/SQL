using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Diagnose
    {
        public int DiagnoseId { get; set; }

        [Required]
        [Column(TypeName ="NVARCHAR(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        public string Comments { get; set; }

        public int ParientId { get; set; }

        public Patient Patient { get; set; }
    }
}
