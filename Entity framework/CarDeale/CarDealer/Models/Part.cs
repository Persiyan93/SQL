using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Models
{
    public class Part
    {

        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "price")]
        public decimal Price { get; set; }
        [XmlElement(ElementName = "quantity")]
        public int Quantity { get; set; }
        [XmlElement(ElementName = "supplierId")]
        public int SupplierId { get; set; }


        public Supplier Supplier { get; set; }
        [XmlIgnore]
        public ICollection<PartCar> PartCars { get; set; }
    }
}
