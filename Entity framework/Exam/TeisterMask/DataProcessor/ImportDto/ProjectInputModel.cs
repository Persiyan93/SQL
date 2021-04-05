using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ProjectInputModel
    {

        [XmlElement(ElementName = "Name")]
        [Required]
        [StringLength(40,MinimumLength =2)]
        public string Name { get; set; }


        [XmlElement(ElementName = "OpenDate")]
        [Required]
        public string OpenDate { get; set; }


        [XmlElement(ElementName = "DueDate")]
        public string DueDate { get; set; }


        

        [XmlArray("Tasks")]
        public TaskInputModel[] Tasks { get; set; }


    }
//    •	Id - integer, Primary Key
//•	Name - text with length[2, 40] (required)
//•	OpenDate - date and time(required)
//•	DueDate - date and time(can be null)
//•	Tasks - collection of type Task

}
