using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
          


            var context = new CarDealerContext();
            var xml = File.ReadAllText(@"D:\Work\SQL\SQL\Entity framework\CarDeale\CarDealer\bin\Debug\netcoreapp2.1\cars.xml");

            Console.WriteLine(ImportCars(context, xml));
        }

        private static void RemoveDuplicateParts()
        {
            var xmldoc = XDocument.Load(@"D:\Work\SQL\SQL\Entity framework\CarDeale\CarDealer\Datasets\cars.xml");
            var cars = xmldoc.Root.Elements();
            foreach (var car in cars)
            {
                var parts = car.Element("parts").Elements("partId");
                var existingParts = new List<int>();
                foreach (var part in parts)
                {
                    var currpart = part.Attribute("id").Value;
                    if (existingParts.Contains(int.Parse(currpart)))
                    {
                        part.Remove();
                    }
                    existingParts.Add(int.Parse(currpart));
                }

              
              

            }
            Console.WriteLine(xmldoc);
            xmldoc.Save("cars.xml");
        }

        public static string ImportSuppliers(CarDealerContext context, string inputxml)
        {
            var serializer = new XmlSerializer(typeof(Supplier[]), new XmlRootAttribute("Suppliers"));
            var suppliers = (Supplier[])serializer.Deserialize(new StringReader(inputxml));

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }
        public static string ImportParts(CarDealerContext context, string inputXml)
        {

            var serializer = new XmlSerializer(typeof(Part[]), new XmlRootAttribute("Parts"));
            var parts = (Part[])serializer.Deserialize(new StringReader(inputXml));
            var supplierIds = context.Suppliers.Select(s => s.Id).ToList();
            parts = parts.Where(p => supplierIds.Any(x => x == p.SupplierId)).ToArray();
            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}.";


        }


        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(Car[]), new XmlRootAttribute("Cars"));
            var cars = (Car[])serializer.Deserialize(new StringReader(inputXml));

            foreach (var car in cars)
            {
                Co
            }
            return null;
        }






    }
}