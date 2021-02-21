using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Column(TypeName ="NVARCHAR(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string LastName { get; set; }


    }
}
