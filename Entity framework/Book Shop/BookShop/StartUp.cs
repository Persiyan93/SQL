namespace BookShop
{
    using Data;
    using Initializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {

            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            // var comand = int.Parse(Console.ReadLine());
            var result = GetTotalProfitByCategory(db);
            Console.WriteLine(result);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string comand)
        {

            var books = context.Books
                    .AsEnumerable()
                    .Where(b => b.AgeRestriction.ToString().ToLower() == comand.ToLower())
                    .OrderBy(x => x.Title)
                    .Select(x => x.Title)
                    .ToList();
            var result = string.Join(Environment.NewLine, books);


            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                    .AsEnumerable()
                    .Where(b => b.EditionType.ToString() == "Gold" && b.Copies < 5000)
                    .OrderBy(b => b.BookId)
                    .Select(b => b.Title)
                    .ToList();

            return string.Join(Environment.NewLine, books);


        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var resultString = new StringBuilder();
            var books = context.Books
                    .Where(x => x.Price > 40)
                    .OrderByDescending(x => x.Price)
                    .Select(x => new { x.Title, x.Price })
                    .ToList();
            foreach (var book in books)
            {
                resultString.AppendLine($"{book.Title} - ${book.Price:F2}");
            }
            return resultString.ToString();

        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                    .Where(x => x.ReleaseDate.Value.Year != year)
                    .OrderBy(x => x.BookId)
                    .Select(x => x.Title)
                    .ToList();
            return string.Join(Environment.NewLine, books);

        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.ToLower())
                    .ToList();
            var result = new List<string>();
            foreach (var cur in categories)
            {
                var books = context.Books
                  .Where(x => x.BookCategories.Any(x => x.Category.Name == cur.ToLower()))
                  .Select(x => x.Title)
                  .ToList();
                result.AddRange(books);
            }
            result = result.OrderBy(x => x).ToList();
            return string.Join(Environment.NewLine, result);
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateOfrelease;
            DateTime.TryParse(date, out dateOfrelease);

            var resultString = new StringBuilder();
            var books = context.Books
                    .Where(b => b.ReleaseDate.Value < dateOfrelease)
                     .OrderByDescending(b => b.ReleaseDate)
                    .Select(b => new { b.Title, EditionType = b.EditionType.ToString(), b.Price })
                    .ToList();

            foreach (var book in books)
            {
                resultString.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }
            return resultString.ToString();


        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {

            var authors = context.Authors
                    .Where(a => a.FirstName.EndsWith(input))
                    .Select(a => a.FirstName + " " + a.LastName)
                    .OrderBy(a => a)
                    .ToList();
            return string.Join(Environment.NewLine, authors);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                    .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                    .Select(b => b.Title)
                    .OrderBy(b => b)
                    .ToList();

            return string.Join(Environment.NewLine, books);
        }


        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var resultString = new StringBuilder();
            var books = context.Books
                    .Where(b => b.Author.LastName
                               .ToLower()
                               .StartsWith(input.ToLower()))
                    .OrderBy(b => b.BookId)
                    .Select(b => new { b.Title, b.Author.FirstName, b.Author.LastName })
                    .ToList();
            foreach (var book in books)
            {
                resultString.AppendLine($"{book.Title} ({book.FirstName + " " + book.LastName})");
            }
            return resultString.ToString();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var count = context.Books
                    .Where(b => b.Title.Length > lengthCheck)
                    .Count();

            return count;

        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var resultString = new StringBuilder();

            var Authors = context.Books
                    .GroupBy(b => new { b.Author.FirstName, b.Author.LastName })
                    .Select(g => new { g.Key.FirstName, g.Key.LastName, CopiesCount = g.Sum(x => x.Copies) })
                    .OrderByDescending(g => g.CopiesCount)
                    .ToList();
            foreach (var author in Authors)
            {
                resultString.AppendLine($"{author.FirstName + " " + author.LastName} - {author.CopiesCount}");
            }
            return resultString.ToString();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var resultString = new StringBuilder();
            var category = context.BooksCategories
                    .GroupBy(b => new {b.Category.Name,b.CategoryId })
                    .Select(g => new { g.Key.Name,g.Key.CategoryId,Count=g.Min(b=>b.Book.Price) })
                    .ToList();
            foreach (var item in category)
            {
                resultString.AppendLine($"{item.CategoryId} ");
            }
            return resultString.ToString();
            //Profit = g.Sum(x => x.Copies * x.Price)   Profit=g.Sum(x=>x.Book.Copies*x.Book.Price) 
        }
    }
}
