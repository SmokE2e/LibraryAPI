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

        // Определение сущностей (DbSet)
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация сущности Author
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);  // Установка первичного ключа
                entity.Property(a => a.FirstName)
                    .IsRequired()  // Обязательно для заполнения
                    .HasMaxLength(100);  // Максимальная длина
                entity.Property(a => a.LastName)
                    .IsRequired()  // Обязательно для заполнения
                    .HasMaxLength(100);  // Максимальная длина
                entity.Property(a => a.DateOfBirth)
                    .IsRequired();  // Обязательно для заполнения
                entity.Property(a => a.Country)
                    .IsRequired()  // Обязательно для заполнения
                    .HasMaxLength(100);  // Максимальная длина
            });

            // Конфигурация сущности Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);  // Установка первичного ключа
                entity.Property(b => b.Title)
                    .IsRequired()  // Обязательно для заполнения
                    .HasMaxLength(255);  // Максимальная длина
                entity.Property(b => b.Genre)
                    .IsRequired()  // Обязательно для заполнения
                    .HasMaxLength(100);  // Максимальная длина
                entity.Property(b => b.Description)
                    .IsRequired()  // Обязательно для заполнения
                    .HasMaxLength(1000);  // Максимальная длина
                entity.Property(b => b.ISBN)
                    .IsRequired()  // Обязательно для заполнения
                    .HasMaxLength(13);  // Максимальная длина ISBN
                entity.Property(b => b.ImagePath)
                    .HasMaxLength(255);  // Максимальная длина для пути к изображению

                // Установка внешнего ключа для связи с Author
                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);  // Удаление книг при удалении автора
            });

            // Добавление начальных данных для авторов
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "George", LastName = "Orwell", DateOfBirth = new DateTime(1903, 6, 25), Country = "United Kingdom" },
                new Author { Id = 2, FirstName = "J.K.", LastName = "Rowling", DateOfBirth = new DateTime(1965, 7, 31), Country = "United Kingdom" },
                new Author { Id = 3, FirstName = "Harper", LastName = "Lee", DateOfBirth = new DateTime(1926, 4, 28), Country = "United States" }
            );

            // Добавление начальных данных для книг
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "1984", Genre = "Dystopian", Description = "A novel about totalitarianism.", ISBN = "9780451524935", AuthorId = 1, ImagePath = "path/to/image1" },
                new Book { Id = 2, Title = "Animal Farm", Genre = "Political satire", Description = "A novel about a farm.", ISBN = "9780451526342", AuthorId = 1, ImagePath = "path/to/image2" },
                new Book { Id = 3, Title = "Harry Potter and the Philosopher's Stone", Genre = "Fantasy", Description = "A novel about a young wizard.", ISBN = "9780747532699", AuthorId = 2, ImagePath = "path/to/image3" },
                new Book { Id = 4, Title = "To Kill a Mockingbird", Genre = "Southern Gothic", Description = "A novel about racial injustice.", ISBN = "9780061120084", AuthorId = 3, ImagePath = "path/to/image4" }
            );
        }
    }
}
