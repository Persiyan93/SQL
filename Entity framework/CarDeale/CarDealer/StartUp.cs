using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var xml = File.ReadAllText(@"D:\Work\SQL\SQL\Entity framework\CarDeale\CarDealer\Datasets\suppliers.xml");
            Console.WriteLine(xml);
            Console.WriteLine(ImportSuppliers(context, xml));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputxml)
        {
            var serializer = new XmlSerializer(typeof(Supplier[]), new XmlRootAttribute("Suppliers"));

            var suppliers = (Supplier[])serializer.Deserialize(new StringReader(inputxml));
            
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }






    }
}