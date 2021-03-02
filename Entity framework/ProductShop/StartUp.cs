using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            Console.WriteLine(ImportUsers(context, @"D:\Work\SQL\SQL\Entity framework\ProductShop\Datasets\users.json"));
         
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(inputJson));
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }

    }
}