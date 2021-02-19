using P01_StudentSystem.Data.Models.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR (MAX)")]
        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
