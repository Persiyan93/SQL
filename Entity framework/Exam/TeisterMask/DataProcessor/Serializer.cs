namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });
            var serializer = new XmlSerializer(typeof(ProjectDTO[]), new XmlRootAttribute("Projects"));
            var projects = context.Projects.ToArray()
                .Where(p => p.Tasks.Count > 0)
                .Select(p => new ProjectDTO
                {
                    Name = p.Name,
                    TaskCount = p.Tasks.Count(),
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks
                        .Select(t => new TaskDTO
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        }).OrderBy(t => t.Name).ToArray()

                }).OrderByDescending(x => x.Tasks.Length).ThenBy(x => x.Name).ToArray();
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, projects, namespaces);
                return textWriter.ToString();
            }

        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees.ToList()
                .Where(x=>x.EmployeesTasks.Any(x=>DateTime.Compare(x.Task.OpenDate,date)>=0))
               
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks.Where(x => DateTime.Compare(x.Task.OpenDate, date) >= 0&&x.EmployeeId==e.Id)
                         .Select(x => new
                         {
                             TaskName = x.Task.Name,
                             OpenDate = x.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                             DueDate = x.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                             LabelType = x.Task.LabelType.ToString(),
                             ExecutionType = x.Task.ExecutionType.ToString()
                         }).OrderByDescending(x => DateTime.Parse(x.DueDate, CultureInfo.InvariantCulture))
                            .ThenBy(x => x.TaskName).ToList()

                }).ToList().OrderByDescending(x => x.Tasks.Count()).ThenBy(x => x.Username). Take(10);
            return JsonConvert.SerializeObject(employees, Formatting.Indented);
        }
    }
}