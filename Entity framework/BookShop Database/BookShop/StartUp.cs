using BookShop.Data;
using BookShop.Models.Enums;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BookShop
{
    class StartUp
    {
        static void Main(string[] args)
        {

            var context = new BookShopContext();
            decimal price = decimal.Parse("24,40");
            var check = Enum.IsDefined(typeof(AgeRestriction), "teen");
            double b = 3.444;
            Console.WriteLine(check);
            var comand = Console.ReadLine();
            GetBooksByAgeRestriction(context, comand);

        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string comand)
        {

            var books = context.Books
                    .Where(b => (int)b.AgeRestriction == 3)
                    .OrderBy(x => x.Title)
                    .Select(x => x.Title)
                    .ToList();
            var result = string.Join("/n", books);


            return result;
        }


        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                    .Where(x => Enum.IsDefined(x.EditionType.GetType(), "Gold") && x.Copies < 5000)
                    .Select(x => x.Title)
                    .ToList();
            return string.Join("/n", books);


        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var resultString = new StringBuilder();

            var books = context.Books
                    .Where(x => x.Price > 40)
                    .OrderByDescending(x => x.Price)
                    .Select(x => new { x.Title, x.Price })
                    .ToList();
            foreach (var item in books)
            {
                resultString.AppendLine($"{item.Title} - ${item.Price:F2}");

            }

            return resultString.ToString();



        }
    }
}
