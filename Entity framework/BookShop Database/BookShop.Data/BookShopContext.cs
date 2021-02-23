using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShop.Data
{
    public class BookShopContext:DbContext
    {
        public BookShopContext()
        {

        }

        public BookShopContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public  DbSet<Category > Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=BookShop;Integrated Security=true;");
            }
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(x => new { x.BookId, x.CategoryId });

                entity
                    .HasOne(x => x.Book)
                    .WithMany(b => b.BookCategories)
                    .HasForeignKey(x => x.BookId);

                entity
                    .HasOne(x => x.Category)
                    .WithMany(c => c.BookCategories)
                    .HasForeignKey(x => x.CategoryId);


            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(x => x.AgeRestriction)
                    .HasConversion<string>();

                entity.Property(x => x.EditionType)
                    .HasConversion<string>();

            });
        }

    }
}
