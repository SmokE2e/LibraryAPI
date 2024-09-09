using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using LibraryAPI.Data;
using System;

namespace LibraryAPI.Tests
{
    public class AuthorRepositoryTests
    {

        private List<Author> GetTestAuthors()
        {
            return new List<Author>
            {
                new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" },
                new Author { Id = 2, FirstName = "J.K.", LastName = "Rowling", DateOfBirth = new DateTime(1965, 7, 31), Country = "United Kingdom" },
                new Author { Id = 3, FirstName = "Harper", LastName = "Lee", DateOfBirth = new DateTime(1926, 4, 28), Country = "United States" }
            };
        }

      
        private DbContextOptions<LibraryContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllAuthors()
        {
            var options = CreateInMemoryOptions();

            using (var context = new LibraryContext(options))
            {
                context.Authors.AddRange(GetTestAuthors());
                context.SaveChanges();
            }

            using (var context = new LibraryContext(options))
            {
                var repository = new AuthorRepository(context);

                var result = await repository.GetAllAsync();
                Assert.Equal(3, result.Count()); 
            }
        }

        
        [Fact]
        public async Task GetByIdAsync_ReturnsAuthor_WhenAuthorExists()
        {
            var options = CreateInMemoryOptions();

            using (var context = new LibraryContext(options))
            {
                context.Authors.AddRange(GetTestAuthors());
                context.SaveChanges();
            }

            using (var context = new LibraryContext(options))
            {
                var repository = new AuthorRepository(context);

              
                var result1 = await repository.GetByIdAsync(1);
                Assert.NotNull(result1);
                Assert.Equal("Stephen", result1.FirstName);
                Assert.Equal("King", result1.LastName);

                              var result2 = await repository.GetByIdAsync(2);
                Assert.NotNull(result2);
                Assert.Equal("J.K.", result2.FirstName);
                Assert.Equal("Rowling", result2.LastName);

               
                var result3 = await repository.GetByIdAsync(3);
                Assert.NotNull(result3);
                Assert.Equal("Harper", result3.FirstName);
                Assert.Equal("Lee", result3.LastName);
            }
        }

       
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenAuthorDoesNotExist()
        {
            var options = CreateInMemoryOptions();

            using (var context = new LibraryContext(options))
            {
                context.Authors.AddRange(GetTestAuthors());
                context.SaveChanges();
            }

            using (var context = new LibraryContext(options))
            {
                var repository = new AuthorRepository(context);

                var result = await repository.GetByIdAsync(999);
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task AddAuthorAsync_AddsNewAuthor()
        {
            var options = CreateInMemoryOptions();

            using (var context = new LibraryContext(options))
            {
                context.Authors.AddRange(GetTestAuthors());
                context.SaveChanges();
            }

            using (var context = new LibraryContext(options))
            {
                var repository = new AuthorRepository(context);

                var newAuthor = new Author { FirstName = "George", LastName = "Orwell", DateOfBirth = new DateTime(1903, 6, 25), Country = "United Kingdom" };
                await repository.AddAuthorAsync(newAuthor);

                var result = await repository.GetAllAsync();
                Assert.Equal(4, result.Count()); 

                Assert.Contains(result, a => a.FirstName == "George" && a.LastName == "Orwell");
            }
        }

        
        [Fact]
        public async Task UpdateAuthorAsync_UpdatesExistingAuthor()
        {
            var options = CreateInMemoryOptions();

            using (var context = new LibraryContext(options))
            {
                context.Authors.AddRange(GetTestAuthors());
                context.SaveChanges();
            }

            using (var context = new LibraryContext(options))
            {
                var repository = new AuthorRepository(context);

                var author = await repository.GetByIdAsync(1);
                author.FirstName = "UpdatedFirstName";
                await repository.UpdateAuthorAsync(author);

                var updatedAuthor = await repository.GetByIdAsync(1);
                Assert.Equal("UpdatedFirstName", updatedAuthor.FirstName);
            }
        }

    
        [Fact]
        public async Task DeleteAuthorAsync_RemovesAuthor()
        {
            var options = CreateInMemoryOptions();

            using (var context = new LibraryContext(options))
            {
                context.Authors.AddRange(GetTestAuthors());
                context.SaveChanges();
            }

            using (var context = new LibraryContext(options))
            {
                var repository = new AuthorRepository(context);

                var author = await repository.GetByIdAsync(1);
                await repository.DeleteAuthorAsync(author);

                var result = await repository.GetAllAsync();
                Assert.Equal(2, result.Count()); 
                Assert.DoesNotContain(result, a => a.Id == 1);
            }
        }

      
        [Fact]
        public async Task GetBooksByAuthorAsync_ReturnsBooks_WhenBooksExist()
        {
            var options = CreateInMemoryOptions();

            using (var context = new LibraryContext(options))
            {
                var author = new Author { Id = 1, FirstName = "Stephen", LastName = "King" };
                context.Authors.Add(author);
                context.Books.Add(new Book { Id = 1, Title = "Carrie", AuthorId = 1 });
                context.Books.Add(new Book { Id = 2, Title = "The Shining", AuthorId = 1 });
                context.SaveChanges();
            }

            using (var context = new LibraryContext(options))
            {
                var repository = new AuthorRepository(context);

                var result = await repository.GetBooksByAuthorAsync(1);
                Assert.Equal(2, result.Count()); 
            }
        }
    }
}
