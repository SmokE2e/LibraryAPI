using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibraryAPI.Tests
{
    public class BookRepositoryTests
    {
        private DbContextOptions<LibraryContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: $"TestLibraryDb_{Guid.NewGuid()}")
                .Options;
        }

        private void InitializeDatabase(LibraryContext context)
        {
            context.Authors.AddRange(
                new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" },
                new Author { Id = 2, FirstName = "J.K.", LastName = "Rowling", DateOfBirth = new DateTime(1965, 7, 31), Country = "United Kingdom" },
                new Author { Id = 3, FirstName = "Harper", LastName = "Lee", DateOfBirth = new DateTime(1926, 4, 28), Country = "United States" }
            );

            context.Books.AddRange(
                new Book { Id = 1, Title = "Carrie", Genre = "Horror", Description = "A novel about a high school girl with telekinetic abilities", ISBN = "9780451166600", AuthorId = 1, ImagePath = "path/to/image1" },
                new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", Genre = "Fantasy", Description = "Second book in the Harry Potter series", ISBN = "9780439064873", AuthorId = 2, ImagePath = "path/to/image2" },
                new Book { Id = 3, Title = "To Kill a Mockingbird", Genre = "Southern Gothic", Description = "A novel about racial injustice in the American South", ISBN = "9780061120084", AuthorId = 3, ImagePath = "path/to/image3" }
            );

            context.SaveChanges();
        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsBook_WhenBookExists()
        {
            var options = CreateInMemoryOptions();
            Book result;

            using (var context = new LibraryContext(options))
            {
                InitializeDatabase(context);  
                var repository = new BookRepository(context);
                result = await repository.GetBookByIdAsync(1); 
            }

            Assert.NotNull(result);
            Assert.Equal("Carrie", result.Title);
        }

        [Fact]
        public async Task GetBookByISBNAsync_ReturnsBook_WhenBookExists()
        {
      
            var options = CreateInMemoryOptions();
            Book result;

            using (var context = new LibraryContext(options))
            {
                InitializeDatabase(context);  
                var repository = new BookRepository(context);
                result = await repository.GetBookByISBNAsync("9780439064873"); 
            }

            Assert.NotNull(result);
            Assert.Equal("Harry Potter and the Chamber of Secrets", result.Title);
        }

        [Fact]
        public async Task GetBookByISBNAsync_ReturnsNull_WhenBookDoesNotExist()
        {
            var options = CreateInMemoryOptions();
            Book result;

            using (var context = new LibraryContext(options))
            {
                InitializeDatabase(context);
                var repository = new BookRepository(context);
                result = await repository.GetBookByISBNAsync("NonExistentISBN"); 
            }

            Assert.Null(result);
        }
    }
}
