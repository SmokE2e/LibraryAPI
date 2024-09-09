using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryAPI.Controllers;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryAPI.Tests
{
    public class AuthorsControllerTests
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

        private List<Book> GetTestBooks()
        {
            return new List<Book>
            {
                new Book { Id = 1, Title = "Carrie", Genre = "Horror", Description = "A novel about a high school girl with telekinetic abilities", ISBN = "9780451166600", AuthorId = 1, ImagePath = "path/to/image1" },
                new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", Genre = "Fantasy", Description = "Second book in the Harry Potter series", ISBN = "9780439064873", AuthorId = 2, ImagePath = "path/to/image2" },
                new Book { Id = 3, Title = "To Kill a Mockingbird", Genre = "Southern Gothic", Description = "A novel about racial injustice in the American South", ISBN = "9780061120084", AuthorId = 3, ImagePath = "path/to/image3" }
            };
        }

        [Fact]
        public async Task GetAuthors_ReturnsAllAuthors()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestAuthors());

            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.GetAuthors();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Author>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<Author>>(okResult.Value);
            Assert.Equal(3, returnValue.Count);
        }

        [Fact]
        public async Task GetAuthor_ReturnsAuthor_WhenAuthorExists()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var author = new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" };

            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(author);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.GetAuthor(1);

            var actionResult = Assert.IsType<ActionResult<Author>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Author>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Stephen", returnValue.FirstName);
            Assert.Equal("King", returnValue.LastName);
            Assert.Equal(new DateTime(1947, 9, 21), returnValue.DateOfBirth);
            Assert.Equal("United States", returnValue.Country);
        }

        [Fact]
        public async Task GetAuthor_ReturnsNotFound_WhenAuthorDoesNotExist()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Author)null);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.GetAuthor(999);

            var actionResult = Assert.IsType<ActionResult<Author>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostAuthor_CreatesNewAuthor()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var newAuthor = new Author { Id = 4, FirstName = "George", LastName = "Orwell", DateOfBirth = new DateTime(1903, 6, 25), Country = "United Kingdom" };

            mockRepo.Setup(repo => repo.AddAuthorAsync(newAuthor)).Returns(Task.CompletedTask);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.PostAuthor(newAuthor);

            var actionResult = Assert.IsType<ActionResult<Author>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Author>(createdAtActionResult.Value);
            Assert.Equal(newAuthor.Id, returnValue.Id);
            Assert.Equal(newAuthor.FirstName, returnValue.FirstName);
            Assert.Equal(newAuthor.LastName, returnValue.LastName);
        }

        [Fact]
        public async Task PutAuthor_UpdatesExistingAuthor()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var updatedAuthor = new Author { Id = 1, FirstName = "Stephen", LastName = "King Updated", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" };

            mockRepo.Setup(repo => repo.UpdateAuthorAsync(updatedAuthor)).Returns(Task.CompletedTask);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.PutAuthor(1, updatedAuthor);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutAuthor_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var updatedAuthor = new Author { Id = 2, FirstName = "J.K.", LastName = "Rowling Updated", DateOfBirth = new DateTime(1965, 7, 31), Country = "United Kingdom" };
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.PutAuthor(1, updatedAuthor);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteAuthor_DeletesExistingAuthor()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var author = new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" };

            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(author);
            mockRepo.Setup(repo => repo.DeleteAuthorAsync(author)).Returns(Task.CompletedTask);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.DeleteAuthor(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAuthor_ReturnsNotFound_WhenAuthorDoesNotExist()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Author)null);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.DeleteAuthor(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetBooksByAuthor_ReturnsBooks_WhenBooksExist()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Carrie", Genre = "Horror", AuthorId = 1, ISBN = "9780451166600", ImagePath = "path/to/image1" },
                new Book { Id = 2, Title = "The Shining", Genre = "Horror", AuthorId = 1, ISBN = "9780307743657", ImagePath = "path/to/image2" }
            };

            mockRepo.Setup(repo => repo.GetBooksByAuthorAsync(1)).ReturnsAsync(books);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.GetBooksByAuthor(1);

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Book>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetBooksByAuthor_ReturnsNotFound_WhenNoBooksExist()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            mockRepo.Setup(repo => repo.GetBooksByAuthorAsync(999)).ReturnsAsync((List<Book>)null);
            var controller = new AuthorsController(mockRepo.Object);

            var result = await controller.GetBooksByAuthor(999);

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Book>>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
