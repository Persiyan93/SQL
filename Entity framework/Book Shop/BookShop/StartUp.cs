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
            var comand = Console.ReadLine();
             var result=GetBooksReleasedBefore(db,comand);
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
                    .Where(x => (int)x.ReleaseDate.Value.Year  != year)
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
                    .Where(b => b.ReleaseDate < dateOfrelease)
                     .OrderByDescending(b => b.ReleaseDate)
                    .Select(b => new { b.Title, EditionType = b.EditionType.ToString(), b.Price })
                    .ToList();

            foreach (var book in books)
            {
                resultString.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }
            Console.WriteLine(resultString.ToString().TrimEnd().Length);
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

            var authors = context.Authors
                    .Select(a => new
                    {

                        FullName = a.FirstName +" "+ a.LastName,

                        BookCount = a.Books.Sum(b => b.Copies)



                    })
                    .OrderByDescending(x=>x.BookCount)
                    .ToList();
            foreach (var author in authors)
            {
                resultString.AppendLine($"{author.FullName} - {author.BookCount}");
            }
            return resultString.ToString();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var resultString = new StringBuilder();
            var categories = context.Categories
                     .Select(x => new
                     {
                         Name = x.Name,

                         Profit = x.CategoryBooks.Sum(cb => cb.Book.Price * cb.Book.Copies)

                     })

                     .OrderByDescending(x=>x.Profit)
                     .ThenBy(x=>x.Name)
                     .ToList();
            foreach (var item in categories)
            {
                resultString.AppendLine($"{item.Name} ${item.Profit:F2}");
            }
            return resultString.ToString();
         
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {

            var resultString = new StringBuilder();
            var catogories = context.Categories
                    .Select(c => new
                    {

                        c.Name,

                        Books = c.CategoryBooks.OrderByDescending(cb => cb.Book.ReleaseDate)
                                .Select(b => b.Book.Title + " (" + b.Book.ReleaseDate.Value.Year+")")
                                .Take(3)
                                .ToList()

                    })
                    .OrderBy(x => x.Name);
            foreach (var category in catogories)
            {
                resultString.AppendLine($"--{category.Name}");
                resultString.AppendLine(string.Join(Environment.NewLine, category.Books));
                
                
            }

            return resultString.ToString();
        }
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                    .Where(x => x.ReleaseDate.Value.Year < 2010)
                    .ToList();
            foreach (var book in books)
            {
                book.Price += 5;
            }
            context.SaveChanges();
            
        }

    }
}
