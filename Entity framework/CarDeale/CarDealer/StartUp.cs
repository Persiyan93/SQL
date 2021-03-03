using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            var json = File.ReadAllText(@"D:\Work\SQL\SQL\Entity framework\CarDeale\CarDealer\Datasets\suppliers.json");
            ImportSuppliers(context, json);
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);
            foreach (var supplier in suppliers)
            {
                Console.WriteLine(supplier.Name);
            }

           return $"Successfully imported {suppliers.Count}.";
        }
    }
}