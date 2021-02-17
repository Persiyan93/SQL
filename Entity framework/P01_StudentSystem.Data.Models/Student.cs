using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {

        [Key]
        public int StudentId { get; set; }


        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "CHAR")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }


        public DateTime? RegisterOn { get; set; }

        public DateTime? BirthDay { get; set; }
    }
}
