using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LibraryAPI.Controllers;
using LibraryAPI.Models;
using LibraryAPI.Repositories;

namespace LibraryAPI.Tests
{
    public class BooksControllerTests
    {
        private List<Book> GetTestBooks()
        {
            return new List<Book>
            {
                new Book { Id = 1, Title = "Carrie", ISBN = "9780451166600", AuthorId = 1 },
                new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", ISBN = "9780439064873", AuthorId = 2 },
                new Book { Id = 3, Title = "To Kill a Mockingbird", ISBN = "9780061120084", AuthorId = 3 }
            };
        }

        [Fact]
        public async Task GetBookById_ReturnsCorrectBook_WhenBookExists()
        {
            // Arrange
            var mockRepo = new Mock<IBookRepository>();
            var book = new Book
            {
                Id = 1,
                Title = "Carrie",
                Genre = "Horror",
                Description = "A novel about a high school girl with telekinetic abilities",
                ISBN = "9780451166600",
                AuthorId = 1,
                ImagePath = "path/to/image1"
            };
            mockRepo.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync(book);

            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = await controller.GetBook(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Carrie", returnValue.Title);
        }

        [Fact]
        public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.GetBookByIdAsync(999)).ReturnsAsync((Book)null);

            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = await controller.GetBook(999);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
