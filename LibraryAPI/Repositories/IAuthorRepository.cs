using LibraryAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> GetByNameAsync(string firstName, string lastName);
        Task AddAuthorAsync(Author author); 
        Task UpdateAuthorAsync(Author author); 
        Task DeleteAuthorAsync(Author author); 
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    }
}

