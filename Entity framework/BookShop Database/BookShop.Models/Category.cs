using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookShop.Models
{
    public class Category
    {
        public Category()
        {
            this.BookCategories = new HashSet<BookCategory>();
        }
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName ="NVARCHAR(50)")]
        public string Name { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
