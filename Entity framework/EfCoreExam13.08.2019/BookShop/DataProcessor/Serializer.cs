namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var exportModels = context
                .Authors
                .Select(a => new
                {

                    AuthorName = a.FirstName + " " + a.LastName,
                    Books = a.AuthorsBooks
                    .OrderByDescending(p=>p.Book.Price)
                    .Select(ab => new
                    {
                        BookName = ab.Book.Name,
                        BookPrice = ab.Book.Price.ToString("F2")
                    })
                    .ToList()


                }).ToList()
                .OrderByDescending(x => x.Books.Count())
                .ThenBy(x => x.AuthorName);


            var result = JsonConvert.SerializeObject(exportModels, Formatting.Indented);
            return result;

        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName() });


            var serializer = new XmlSerializer(typeof(List<BookDto>), new XmlRootAttribute("Books"));

            var books = context.Books
                .Where(b=>b.Genre==Genre.Science&&b.PublishedOn<date)
                .Select(b => new BookDto
                {

                    Name = b.Name,
                    Pages = b.Pages,
                    Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .OrderByDescending(b => b.Pages)
                .ThenByDescending(b => b.Date)
                .Take(10)
                .ToList();



            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, books,namespaces);
                return textWriter.ToString();
            }
        }
    }
}