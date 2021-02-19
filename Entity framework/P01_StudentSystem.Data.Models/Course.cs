using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public Course()
        {

        }
        public int CourseId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(80)]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(300)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Resource> Resources { get; set; }

    }
}
