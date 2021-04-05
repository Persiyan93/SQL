using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class TaskInputModel
    {
		[XmlElement(ElementName = "Name")]
		[Required]
		[StringLength(40,MinimumLength =2)]
		public string Name { get; set; }


		[XmlElement(ElementName = "OpenDate")]
		[Required]
		public string OpenDate { get; set; }


		[XmlElement(ElementName = "DueDate")]
		[Required]
		public string DueDate { get; set; }


		[XmlElement(ElementName = "ExecutionType")]
		[Required]
		[EnumDataType(typeof(ExecutionType))]
		public string ExecutionType { get; set; }


		[XmlElement(ElementName = "LabelType")]
		[Required]
		[EnumDataType(typeof(LabelType))]
		public string LabelType { get; set; }
	}


//    •	Id - integer, Primary Key
//•	Name - text with length[2, 40] (required)
//•	OpenDate - date and time(required)
//•	DueDate - date and time(required)
//•	ExecutionType - enumeration of type ExecutionType, with possible values(ProductBacklog, SprintBacklog, InProgress, Finished) (required)
//•	LabelType - enumeration of type LabelType, with possible values(Priority, CSharpAdvanced, JavaAdvanced, EntityFramework, Hibernate) (required)
//•	ProjectId - integer, foreign key(required)
//•	Project - Project 
//•	EmployeesTasks - collection of type EmployeeTask

}