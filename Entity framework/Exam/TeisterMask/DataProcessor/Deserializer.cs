namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Data.Models;
    using Data;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
    using System.Linq;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var sb = new StringBuilder();
           

            var serializer = new XmlSerializer(typeof(List<ProjectInputModel>), new XmlRootAttribute("Projects"));
            var inputModels = (List<ProjectInputModel>)serializer.Deserialize(new StringReader(xmlString));
            foreach (var model in inputModels)
            {
                var isOpenDateValid = DateTime.TryParseExact(model.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture
                    , DateTimeStyles.None, out DateTime openDate);
                var isDueDateValid= DateTime.TryParseExact(model.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture
                    , DateTimeStyles.None, out DateTime dueDate);
               
                if (!IsValid(model)||!isOpenDateValid)
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }
                var project = new Project
                {
                    Name = model.Name,
                    OpenDate = openDate,
                    DueDate = isDueDateValid ? (DateTime?)dueDate : null,
                };
                foreach (var inputTask in model.Tasks)
                {
                    var isTaskOpenDateValid = DateTime.TryParseExact(inputTask.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture
                  , DateTimeStyles.None, out DateTime taskOpenDate);
                    var isTaskDueDateValid = DateTime.TryParseExact(inputTask.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture
                        , DateTimeStyles.None, out DateTime taskDueDate);
                    bool isDatevalid=false;
                    if (project.DueDate==null)
                    {
                        isDatevalid = true;
                    }
                    else if (project.DueDate>taskDueDate)
                    {
                        isDatevalid = true;
                    }
                    

                    if (!IsValid(inputTask)||!isTaskOpenDateValid||!isTaskDueDateValid||project.OpenDate>taskOpenDate||!isDatevalid)
                    {
                        sb.AppendLine("Invalid data!");
                        continue;
                    }
                    var task = new Task
                    {
                        Name = inputTask.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), inputTask.ExecutionType),
                        LabelType = (LabelType)Enum.Parse(typeof(LabelType), inputTask.LabelType)
                    };
                    project.Tasks.Add(task);
                    
                   

                }
                sb.AppendLine($"Successfully imported project - {project.Name} with {project.Tasks.Count()} tasks.");
                context.Projects.Add(project);
                context.SaveChanges();
               

            }
            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var sb = new StringBuilder();


            var inputModels = JsonConvert.DeserializeObject<List<EmployeeInputModel>>(jsonString);
            foreach (var inputmodel in inputModels)
            {
                if (!IsValid(inputmodel))
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }
                var employee = new Employee
                {
                    Username = inputmodel.Username,
                    Phone = inputmodel.Phone,
                    Email = inputmodel.Email
                };
                var tasksIds = context.Tasks.Select(x => x.Id);
                inputmodel.Tasks = inputmodel.Tasks.Distinct().ToList();
                foreach (var taskId in inputmodel.Tasks)
                {
                    if(!tasksIds.Contains(taskId))
                    {
                        sb.AppendLine("Invalid data!");
                        continue;
                    }
                    employee.EmployeesTasks.Add(new EmployeeTask { TaskId =taskId });
                }
                context.Employees.Add(employee);
                context.SaveChanges();
                sb.AppendLine($"Successfully imported employee - {employee.Username} with {employee.EmployeesTasks.Count()} tasks.");
            }
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}