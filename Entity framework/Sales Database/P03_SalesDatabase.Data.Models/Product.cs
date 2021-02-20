using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }
        public int ProductId { get; set; }


        [Required]
        [Column(TypeName ="NVARCHAR(50)")]
        public string Name { get; set; }

        [Required]
     
        public double Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Sale> Sales{ get; set; }


    }
}
