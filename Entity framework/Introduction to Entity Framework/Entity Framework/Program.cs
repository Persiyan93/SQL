using Entity_Framework.Data;
using Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace Entity_Framework
{
    class Program
    {
        static void Main(string[] args)
        {

            var optionBuilder = new DbContextOptionsBuilder<SoftUniContext>();
            optionBuilder.UseSqlServer("Server=.;Database=SoftUni;Integrated Security=True;");
          var context = new SoftUniContext(optionBuilder.Options);
            Console.WriteLine( GetEmployeesInPeriod(context));
            

          
        }
        public static string GetEmloyeesFullInformation(SoftUniContext context)
        {
            var resultString = new StringBuilder();
            var employees = context.Employees
                .OrderBy(x=>x.EmployeeId)
                .Select(x => new { x.FirstName, x.LastName, x.JobTitle, x.MiddleName,Salary = Math.Round(x.Salary, 2) }).ToList();
            foreach (var employe in employees)
            {
                resultString.AppendLine($"{employe.FirstName} {employe.LastName} {employe.MiddleName} {employe.JobTitle} {employe.Salary:F2}");

            }
            return resultString.ToString();
        }
        public static string GetEmployeeWithSalaryOver50000(SoftUniContext context)
        {
            var result = new StringBuilder();
            var employees = context.Employees
                .Where(x => x.Salary > 50000)
                .Select(x => new { x.FirstName, Salary = Math.Round(x.Salary, 2) })
                .OrderBy(x => x.FirstName)
                .ToList();
            foreach (var emploey in employees)
            {
                result.AppendLine($"{emploey.FirstName} - {emploey.Salary:F2}");
            }
            return result.ToString();
        }

        public static string GetEmployeesFromResarchAndDevelopment(SoftUniContext context)
        {
            var resultString = new StringBuilder();

            var employees = context.Employees
                .Where(x => x.Department.Name == "Research and Development")
                .Select(x => new { x.FirstName, x.LastName, DepartmentName=x.Department.Name, x.Salary })
                .OrderBy(x => x.Salary);
            foreach (var e in employees)
            {
                resultString.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:F2}");
            }
            return resultString.ToString();
        }
        public static string AddNewAdressToEmployee (SoftUniContext context)
        {
            var resultString = new StringBuilder();
            var address = new Addres();
            address.AddressText = "Vitoshka 15";
            address.TownId = 4;
            var employe = context.Employees.Where(x => x.LastName == "Nakov").Single();
            employe.Address = address;
            context.SaveChanges();
            var employeesAddresses = context.Employees
                .OrderByDescending(x => x.EmployeeId)
                .Take(10)
                .Select(x => x.Address.AddressText)
                .ToList();
            resultString.Append(string.Join("\n", employeesAddresses));
            return resultString.ToString();
            
        }
       public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var resultString = new StringBuilder();
            
            var employees = context.Employees
                .Any(x=>x.EmployeesProjects)
                
                (x => x.Project.StartDate.Year >= 2001 && x.Project.StartDate.Year <= 2003)
                
                .Select(x => new { x.Employee.FirstName, x.Employee.LastName, ManagerFirstName = x.Employee.Manager.FirstName, managerLastName = x.Employee.Manager.LastName })
                .Distinct()
                .Take(10)
                .ToList();
            foreach (var employe in employees)
            {
                resultString.AppendLine(employe.ToString());
            }

            return resultString.ToString();
        } 
    }

}
