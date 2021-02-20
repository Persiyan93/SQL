using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using System;

namespace P03_SalesDatabase.Data
{
    public class SelectContext:DbContext
    {
        public SelectContext()
        {

        }
        public SelectContext(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;DataBase=SalesDatabase;Integrated security=true");
            }
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity
                    .HasOne(x => x.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(s => s.ProductId);

                entity
                  .HasOne(x => x.Customer)
                  .WithMany(c => c.Sales)
                  .HasForeignKey(s => s.CustomerId);

                entity
                  .HasOne(x => x.Store)
                  .WithMany(s => s.Sales)
                  .HasForeignKey(x => x.StoreId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity
                    .Property(x => x.Description)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(250)
                    .HasDefaultValue("No Description");


            });
        }
    }
}
