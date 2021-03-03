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
            string inputjason = File.ReadAllText(@"D:\Work\SQL\SQL\Entity framework\ProductShop\Datasets\categories-products.json");
            // Console.WriteLine(ImportCategoryProducts(context, inputjason));
            GetUsersWithProducts(context);

        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);
            foreach (var product in products)
            {
                context.Products.Add(product);


            }
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson, settings)
                                        .Where(x => x.Name != null)
                                        .ToList();
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProduct = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);
            context.AddRange(categoriesProduct);
            context.SaveChanges();
            return $"Successfully imported {categoriesProduct.Count}";

        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                        .Where(x => x.Price >= 500 && x.Price <= 1000)
                        .OrderBy(x => x.Price)
                        .Select(x => new { Name = x.Name, Price = x.Price, Seller = x.Seller.FirstName + " " + x.Seller.LastName ?? $"{x.Seller.LastName}" })
                        .ToList();
            var result = JsonConvert.SerializeObject(products, Formatting.Indented);

            Console.WriteLine(result);

            return null;

        }
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                        .Where(x => x.ProductsSold.Where(z => z.BuyerId != null).Count() > 0)
                        .Select(x =>
                        new
                        {

                            FirstName = x.FirstName
                                ,
                            LastName = x.LastName
                                ,
                            SoldProducts = x.ProductsSold.Where(p => p.BuyerId != null)
                                        .Select(p => new
                                        {
                                            Name = p.Name,
                                            Price = p.Price,
                                            BuyerFirstName = p.Buyer.FirstName,
                                            BuyerLastName = p.Buyer.LastName

                                        })
                        });
            var result = JsonConvert.SerializeObject(users, Formatting.Indented);
            Console.WriteLine(result);
            return null;


        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                            .OrderByDescending(c => c.CategoryProducts.Count)
                            .Select(c => new
                            {
                                Category = c.Name,
                                ProductsCount = c.CategoryProducts.Count,
                                AveragePrice = Math.Round(c.CategoryProducts.Average(cp => cp.Product.Price), 2),
                                TotalRevenue = Math.Round(c.CategoryProducts.Sum(cp => cp.Product.Price), 2)
                            });
            var result = JsonConvert.SerializeObject(categories, Formatting.Indented);
            Console.WriteLine(result);
            return null;

        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {




            var template = new
            {
                UserCount = context.Users.Where(u => u.ProductsSold.Where(p => p.Buyer != null).Count() > 0).Count(),
                Users = context.Users
                    .Where(u => u.ProductsSold.Where(p => p.Buyer != null).Count() > 0)
                    .Select(u => new
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new
                        {
                            Count = u.ProductsSold.Count(),
                            Products = u.ProductsSold.Select(p => new
                            {
                                Name = p.Name,
                                Price = p.Price

                            })

                        }

                    })
                    .OrderByDescending(t=>t.SoldProducts.Count)
            };

            var result = JsonConvert.SerializeObject(template, Formatting.Indented, new JsonSerializerSettings
            {

                NullValueHandling = NullValueHandling.Ignore
            }) ;
            Console.WriteLine(result);
            return null;

        }




    }
}