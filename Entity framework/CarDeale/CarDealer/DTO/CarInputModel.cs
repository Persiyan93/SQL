using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DTO
{
    public class CarInputModel
    {
        public CarInputModel()
        {
            
        }

        [XmlElement(ElementName = "make")]
        public string Make { get; set; }

        [XmlElement(ElementName = "model")]
        public string Model { get; set; }

        public long TravelledDistance { get; set; }


        public List<int> PartsId{ get; set; }


    }
}
