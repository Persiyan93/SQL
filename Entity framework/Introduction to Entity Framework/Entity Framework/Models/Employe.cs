using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entity_Framework.Models
{
    public partial class Employe
    {
        public Employe()
        {
            Departments = new HashSet<Department>();
            EmployeesProjects = new HashSet<EmployeesProject>();
            InverseManager = new HashSet<Employe>();
        }

        [Key]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [Column("ManagerID")]
        public int? ManagerId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime HireDate { get; set; }
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }
        [Column("AddressID")]
        public int? AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Employees")]
        public virtual Addres Address { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Employees")]
        public virtual Department Department { get; set; }
        [ForeignKey(nameof(ManagerId))]
        [InverseProperty(nameof(Employe.InverseManager))]
        public virtual Employe Manager { get; set; }
        [InverseProperty("Manager")]
        public virtual ICollection<Department> Departments { get; set; }
        [InverseProperty(nameof(EmployeesProject.Employee))]
        public virtual ICollection<EmployeesProject> EmployeesProjects { get; set; }
        [InverseProperty(nameof(Employe.Manager))]
        public virtual ICollection<Employe> InverseManager { get; set; }
       
    }
}
