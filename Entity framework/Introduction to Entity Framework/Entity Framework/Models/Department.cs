using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entity_Framework.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employe>();
        }

        [Key]
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("ManagerID")]
        public int ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        [InverseProperty(nameof(Employe.Departments))]
        public virtual Employe Manager { get; set; }
        [InverseProperty(nameof(Employe.Department))]
        public virtual ICollection<Employe> Employees { get; set; }
    }
}
