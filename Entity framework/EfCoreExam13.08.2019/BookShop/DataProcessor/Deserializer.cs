namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var officers = new List<Book>();

            var serializer = new XmlSerializer(typeof(List<BookInputModel>), new XmlRootAttribute("Books"));
            var inputModels = (List<BookInputModel>)serializer.Deserialize(new StringReader(xmlString));
            foreach (var inputModel in inputModels)
            {
                var isDateValid = DateTime.TryParseExact(inputModel.PublishedOn, "MM/dd/yyyy",
                                   CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
                if (!IsValid(inputModel)||!isDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var book = new Book
                {
                    Name = inputModel.Name,
                    Genre = (Genre)Enum.Parse(typeof(Genre), inputModel.Genre),
                    Price = inputModel.Price,
                    Pages = inputModel.Pages,
                    PublishedOn = date

                };
                sb.AppendLine(String.Format(SuccessfullyImportedBook,  book.Name, book.Price ));
                context.Books.Add(book);
                context.SaveChanges();

            }
            return sb.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            var models = JsonConvert
                .DeserializeObject<List<AuthorInputModel>>(jsonString,settings);

            var booksIds = context.Books.Select(x => x.Id).ToList();

            foreach (var model in models)
            {
                var emails = context.Authors.Select(a => a.Email).ToList();

                if (!IsValid(model)||emails.Contains(model.Email) )
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var author = new Author
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email
                };
                
                foreach (var book in model.Books)
                {
                   if (!IsValid(book))
                    {
                        continue;
                    }
                    else if (!booksIds.Contains((int)book.Id))
                    {
                        continue;
                    }
                    author.AuthorsBooks.Add(new AuthorBook { BookId = (int)book.Id });
                }
                if (author.AuthorsBooks.Count==0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                context.Authors.Add(author);
                context.SaveChanges();
                sb.AppendLine($"Successfully imported author - {author.FirstName + " " + author.LastName} with {author.AuthorsBooks.Count} books.");
            }
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}