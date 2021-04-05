﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectDTO
    {
        [XmlAttribute("TasksCount")]
        public int TaskCount { get; set; }
        [XmlElement("ProjectName")]
        public string Name { get; set; }
        
        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }
        [XmlArray("Tasks")]
        public TaskDTO[] Tasks { get; set; }

    }
}
