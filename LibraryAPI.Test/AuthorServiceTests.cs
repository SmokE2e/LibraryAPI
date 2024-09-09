using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using LibraryAPI.Services;
using Moq;
using Xunit;

namespace LibraryAPI.Tests
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _mockAuthorRepository;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_mockAuthorRepository.Object);
        }

        [Fact]
        public async Task GetAllAuthorsAsync_ShouldReturnAllAuthors()
        {
            var authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" },
                new Author { Id = 2, FirstName = "J.K.", LastName = "Rowling", DateOfBirth = new DateTime(1965, 7, 31), Country = "United Kingdom" },
                new Author { Id = 3, FirstName = "Harper", LastName = "Lee", DateOfBirth = new DateTime(1926, 4, 28), Country = "United States" }
            };

            _mockAuthorRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(authors);

            var result = await _authorService.GetAllAuthorsAsync();

            Assert.Equal(3, ((List<Author>)result).Count);
            _mockAuthorRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ShouldReturnAuthor_WhenAuthorExists()
        {
            var author = new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" };

            _mockAuthorRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(author);

            var result = await _authorService.GetAuthorByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Stephen", result.FirstName);
            Assert.Equal("King", result.LastName);
            Assert.Equal(new DateTime(1947, 9, 21), result.DateOfBirth);
            Assert.Equal("United States", result.Country);
            _mockAuthorRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task AddAuthorAsync_ShouldCallAddAuthorOnce()
        {
            var newAuthor = new Author { Id = 4, FirstName = "George", LastName = "Orwell", DateOfBirth = new DateTime(1903, 6, 25), Country = "United Kingdom" };
            await _authorService.AddAuthorAsync(newAuthor);

            _mockAuthorRepository.Verify(repo => repo.AddAuthorAsync(newAuthor), Times.Once);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldCallUpdateAuthorOnce()
        {
            var updatedAuthor = new Author { Id = 1, FirstName = "Stephen", LastName = "King Updated", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" };

            await _authorService.UpdateAuthorAsync(updatedAuthor);

            _mockAuthorRepository.Verify(repo => repo.UpdateAuthorAsync(updatedAuthor), Times.Once);
        }

        [Fact]
        public async Task DeleteAuthorAsync_ShouldCallDeleteAuthor_WhenAuthorExists()
        {
            var author = new Author { Id = 1, FirstName = "Stephen", LastName = "King", DateOfBirth = new DateTime(1947, 9, 21), Country = "United States" };

            _mockAuthorRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(author);

            await _authorService.DeleteAuthorAsync(1);

            _mockAuthorRepository.Verify(repo => repo.DeleteAuthorAsync(author), Times.Once);
        }

        [Fact]
        public async Task GetBooksByAuthorAsync_ShouldReturnBooks()
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Carrie", Genre = "Horror", Description = "A novel about a high school girl with telekinetic abilities", ISBN = "9780451166600", AuthorId = 1, ImagePath = "path/to/image1" },
                new Book { Id = 2, Title = "The Shining", Genre = "Horror", Description = "A novel about a family staying in an isolated hotel with a haunted past", ISBN = "9780307743657", AuthorId = 1, ImagePath = "path/to/image2" }
            };

            _mockAuthorRepository
                .Setup(repo => repo.GetBooksByAuthorAsync(1))
                .ReturnsAsync(books);

            var result = await _authorService.GetBooksByAuthorAsync(1);

            Assert.Equal(2, ((List<Book>)result).Count);
            _mockAuthorRepository.Verify(repo => repo.GetBooksByAuthorAsync(1), Times.Once);
        }
    }
}
