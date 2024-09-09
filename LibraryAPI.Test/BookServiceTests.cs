using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using LibraryAPI.Services;
using Moq;
using Xunit;

namespace LibraryAPI.Tests
{
    public class BookServiceTests
    {
        private readonly BookService _bookService;
        private readonly Mock<IBookRepository> _mockBookRepository;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnListOfBooks()
        {
            var mockBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Carrie", Genre = "Horror", Description = "A novel about a high school girl with telekinetic abilities", ISBN = "9780451166600", AuthorId = 1, ImagePath = "path/to/image1" },
                new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", Genre = "Fantasy", Description = "Second book in the Harry Potter series", ISBN = "9780439064873", AuthorId = 2, ImagePath = "path/to/image2" },
                new Book { Id = 3, Title = "To Kill a Mockingbird", Genre = "Southern Gothic", Description = "A novel about racial injustice in the American South", ISBN = "9780061120084", AuthorId = 3, ImagePath = "path/to/image3" }
            };

            _mockBookRepository.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(mockBooks);

            var result = await _bookService.GetAllBooksAsync();

            Assert.Equal(3, result.Count());
            Assert.Equal("Carrie", result.First(b => b.Id == 1).Title);
            Assert.Equal("Harry Potter and the Chamber of Secrets", result.First(b => b.Id == 2).Title);
            Assert.Equal("To Kill a Mockingbird", result.First(b => b.Id == 3).Title);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
        {
            var mockBook = new Book { Id = 1, Title = "Carrie", Genre = "Horror", Description = "A novel about a high school girl with telekinetic abilities", ISBN = "9780451166600", AuthorId = 1, ImagePath = "path/to/image1" };
            _mockBookRepository.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync(mockBook);

            var result = await _bookService.GetBookByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Carrie", result.Title);
            Assert.Equal("Horror", result.Genre);
            Assert.Equal("A novel about a high school girl with telekinetic abilities", result.Description);
            Assert.Equal("9780451166600", result.ISBN);
            Assert.Equal(1, result.AuthorId);
            Assert.Equal("path/to/image1", result.ImagePath);
        }

        [Fact]
        public async Task AddBookAsync_ShouldCallRepositoryAdd()
        {
            var newBook = new Book { Id = 4, Title = "New Book", Genre = "Genre", Description = "Description", ISBN = "1234567890", AuthorId = 1, ImagePath = "path/to/image4" };

            await _bookService.AddBookAsync(newBook);

            _mockBookRepository.Verify(repo => repo.AddBookAsync(newBook), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldCallRepositoryUpdate()
        {
            var updatedBook = new Book { Id = 1, Title = "Updated Book", Genre = "Updated Genre", Description = "Updated Description", ISBN = "0987654321", AuthorId = 1, ImagePath = "path/to/updated_image" };

            await _bookService.UpdateBookAsync(updatedBook);

            _mockBookRepository.Verify(repo => repo.UpdateBookAsync(updatedBook), Times.Once);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldCallRepositoryDelete_WhenBookExists()
        {
            var bookId = 1;

            _mockBookRepository
                .Setup(repo => repo.GetBookByIdAsync(bookId))
                .ReturnsAsync(new Book { Id = bookId, Title = "Carrie" });

            await _bookService.DeleteBookAsync(bookId);

            _mockBookRepository.Verify(repo => repo.DeleteBookAsync(bookId), Times.Once);
        }
    }
}
