using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();
            this.Prescriptions = new HashSet<PatientMedicament>();

        }
        public int PatientId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Lastname { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(80)")]
        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; }
        public ICollection<Diagnose> Diagnoses { get; set; }
        public ICollection<PatientMedicament> Prescriptions {get;set;}
    }
}
