﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Medicament
    {
        
        public int MedicamentId { get; set; }
        [Required]
        [Column(TypeName ="NVARCHAR(50)")]
        public string Name { get; set; }
        public ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}
