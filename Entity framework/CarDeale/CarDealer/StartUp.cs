using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var json = File.ReadAllText(@"D:\Work\SQL\SQL\Entity framework\CarDeale\CarDealer\Datasets\cars.json");
            Console.WriteLine(ImportCars(context, json));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);
            var supplierIds = context.Suppliers.Select(s => s.Id).ToList();
            parts = parts.Where(p => supplierIds.Contains(p.SupplierId) == true).ToList();
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count}.";

        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var config = new MapperConfiguration(cfg =>
                  cfg.AddProfile<CarDealerProfile>());
            var mapper = config.CreateMapper();

            var inputCars = JsonConvert.DeserializeObject<List<CarInputModel>>(inputJson);
            foreach (var car in inputCars)
            {
                var dbCar = mapper.Map<Car>(car);
                context.Add(dbCar);
                context.SaveChanges();
                var carParts = new List<PartCar>();
                foreach (var partId in car.PartsId)
                {
                    var partCar = new PartCar { PartId = partId, CarId = dbCar.Id };
                    carParts.Add(partCar);
                }
                dbCar.PartCars = carParts;
                context.SaveChanges();



            }
           





            // var carTest = mapper.Map<ICollection<Car>>(inputCars);

            // context.AddRange(carTest);
            //context.SaveChanges();
            

            //var cars = inputCars.Select(c => new Car
            //{
            //    Make = c.Make,
            //    Model = c.Model,
            //    TravelledDistance = c.TravelledDistance,
            //    PartCars = c.PartsId.Select(x => new PartCar { PartId = x, }).ToList()


            //});


            //context.Cars.AddRange(inputCars.Select(c => new Car
            //{
            //    Make = c.Make,
            //    TravelledDistance = c.TravelledDistance,
            //    PartCars = c.PartsId.Select(x => new PartCar { PartId = x, }).ToList()


            //}).ToList());

            return null;
            //return $"Successfully imported {cars.Count()}.";
        }



}
}