using BookShop.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace BookShop.DataProcessor.ImportDto
{
	[XmlType("Book")]
    public class BookInputModel
    {
		[XmlElement(ElementName = "Name")]
		[Required]
		[StringLength(30,MinimumLength =3)]
		public string Name { get; set; }


		[XmlElement(ElementName = "Genre")]
		[EnumDataType(typeof(Genre))]
		[Required]
		public string Genre { get; set; }


		[XmlElement(ElementName = "Price")]
		[Range(0.01,double.MaxValue)]
		public decimal Price { get; set; }


		[XmlElement(ElementName = "Pages")]
		[Range(50,5000)]
		public int Pages { get; set; }


		[XmlElement(ElementName = "PublishedOn")]
		[Required]
		public string PublishedOn { get; set; }
	}
}
