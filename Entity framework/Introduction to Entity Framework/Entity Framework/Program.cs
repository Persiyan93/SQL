using Entity_Framework.Data;
using Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
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
            Console.WriteLine(IncreaseSalaries(context));



        }
        public static string GetEmloyeesFullInformation(SoftUniContext context)
        {
            var resultString = new StringBuilder();
            var employees = context.Employees
                .OrderBy(x => x.EmployeeId)
                .Select(x => new { x.FirstName, x.LastName, x.JobTitle, x.MiddleName, Salary = Math.Round(x.Salary, 2) }).ToList();
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
                .Select(x => new { x.FirstName, x.LastName, DepartmentName = x.Department.Name, x.Salary })
                .OrderBy(x => x.Salary);
            foreach (var e in employees)
            {
                resultString.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:F2}");
            }
            return resultString.ToString();
        }
        public static string AddNewAdressToEmployee(SoftUniContext context)
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

            var employees = context.Employees.
                Where(x => x.EmployeesProjects
                    .Any(y => y.Project.StartDate.Year >= 2001 && y.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    ManageFirstName = x.Manager.FirstName,
                    ManagerLastName = x.Manager.LastName,
                    Projects = x.EmployeesProjects
                    .Select(x => new
                    {
                        ProjectName = x.Project.Name,
                        StartDate = x.Project.StartDate
                                .ToString("M / d / yyyy h: mm:ss tt", CultureInfo.InvariantCulture),
                        EndDate = x.Project.EndDate.HasValue ? x.Project.EndDate
                                .Value.
                                 ToString("M / d / yyyy h: mm:ss tt", CultureInfo.InvariantCulture) : "not finished"
                    }).ToList()
                }).ToList();
            foreach (var employe in employees)
            {
                resultString.AppendLine($"{employe.FirstName} {employe.LastName} – Manager: {employe.ManageFirstName} {employe.ManagerLastName}");
                foreach (var projects in employe.Projects)
                {
                    resultString.AppendLine($"--{projects.ProjectName} - {projects.StartDate} - {projects.EndDate}");

                }

            }
            return resultString.ToString();
        }
        public static string AddressesByTown(SoftUniContext context)
        {
            var resultString = new StringBuilder();

            var addresses = context.Addresses
                 .OrderByDescending(x => x.Employees.Count)
                 .ThenBy(x => x.Town.Name)
                 .ThenBy(x => x.AddressText)
                 .Take(10)
                 .Select(x => new { x.AddressText, x.Town.Name, x.Employees.Count })
                 .ToList();
            foreach (var address in addresses)
            {
                resultString.AppendLine($"{address.AddressText}, {address.Name} - {address.Count} employees");
            }




            return resultString.ToString();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var resultString = new StringBuilder();
            var employe = context.Employees
                .Where(x => x.EmployeeId == 147)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    Projects = x.EmployeesProjects
                        .Select(
                             x => x.Project.Name

                            )
                        .OrderBy(x => x)
                        .ToList()
                }).FirstOrDefault();
            resultString.AppendLine($"{employe.FirstName} {employe.LastName} - {employe.JobTitle}");
            foreach (var project in employe.Projects)
            {
                resultString.AppendLine(project);
            }

            int a = 2;
             int b = 3;
            b += 2;

            return resultString.ToString();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var resultString = new StringBuilder();

            var departments = context.Departments
                .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name).
                Select(x => new
                {
                    x.Name,
                    x.Manager.FirstName,
                    x.Manager.LastName,
                    Employees = x.Employees.Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        x.JobTitle

                    })
                        .OrderBy(x => x.FirstName)
                        .ThenBy(x => x.LastName)
                        .ToList()

                }).ToList();

            foreach (var department in departments)
            {
                resultString.AppendLine($"{department.Name} - {department.FirstName}  {department.LastName}");
                foreach (var employe in department.Employees)
                {
                    resultString.AppendLine($"{employe.FirstName} {employe.LastName} - {employe.JobTitle}");
                }
            }

            return resultString.ToString();
        }
        public static string GetLatestProjects(SoftUniContext context)
        {
            var resultString = new StringBuilder();

            var projects = context.Projects
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                    StartDate = x.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)

                }).ToList();

            foreach (var project in projects)
            {
                resultString.AppendLine(project.Name);
                resultString.AppendLine(project.Description);
                resultString.AppendLine(project.StartDate);
            }
            return resultString.ToString();

        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var resultSting = new StringBuilder();
            var promotedEmployees = context.Employees
                .Where(x => x.Department.Name == "Engineering" || x.Department.Name == "Tool Design"
                        || x.Department.Name == "Marketing" || x.Department.Name == "Information Services");

            foreach (var employe in promotedEmployees)
            {
                employe.Salary = employe.Salary * 1.12m;
            }

          

            var employeesInfo = promotedEmployees
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    Salary = Math.Round(x.Salary, 2)

                }).
                OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();
            foreach (var info in employeesInfo)
            {
                resultSting.AppendLine($"{info.FirstName} {info.LastName} (${info.Salary:F2})");
            }




            return resultSting.ToString();
        }

    }

}




