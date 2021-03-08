using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Models
{
    [XmlType("Supplier")]
    public class Supplier
    {
        public int Id { get; set; }

        [XmlElement(ElementName ="name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "isImporter")]
        public bool IsImporter { get; set; }

        [XmlIgnore]
        public ICollection<Part> Parts { get; set; }
    }
}
