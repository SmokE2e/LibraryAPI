using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryAPI.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);  
                entity.Property(a => a.FirstName)
                    .IsRequired()  
                    .HasMaxLength(100);  
                entity.Property(a => a.LastName)
                    .IsRequired()  
                    .HasMaxLength(100);  
                entity.Property(a => a.DateOfBirth)
                    .IsRequired();  
                entity.Property(a => a.Country)
                    .IsRequired()  
                    .HasMaxLength(100);  
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);  
                entity.Property(b => b.Title)
                    .IsRequired()  
                    .HasMaxLength(255);  
                entity.Property(b => b.Genre)
                    .IsRequired()  
                    .HasMaxLength(100);  
                entity.Property(b => b.Description)
                    .IsRequired()  
                    .HasMaxLength(1000);  
                entity.Property(b => b.ISBN)
                    .IsRequired()  
                    .HasMaxLength(13);  
                entity.Property(b => b.ImagePath)
                    .HasMaxLength(255); 

                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);  
            });

            
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" },
                new Author { Id = 2, FirstName = "J.K.", LastName = "Rowling", DateOfBirth = new DateTime(1965, 7, 31), Country = "United Kingdom" },
                new Author { Id = 3, FirstName = "Harper", LastName = "Lee", DateOfBirth = new DateTime(1926, 4, 28), Country = "United States" }
            );

          
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Carrie", Genre = "Horror", Description = "A novel about a high school girl with telekinetic abilities", ISBN = "9780451166600", AuthorId = 1, ImagePath = "path/to/image1" },
                new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", Genre = "Fantasy", Description = "Second book in the Harry Potter series", ISBN = "9780439064873", AuthorId = 2, ImagePath = "path/to/image2" },
                new Book { Id = 3, Title = "To Kill a Mockingbird", Genre = "Southern Gothic", Description = "A novel about racial injustice in the American South", ISBN = "9780061120084", AuthorId = 3, ImagePath = "path/to/image3" } 
            );
        }
    }
}
