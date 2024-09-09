using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(); 
        Task<Book> GetBookByIdAsync(int id); 
        Task<Book> GetBookByISBNAsync(string isbn);  
        Task AddBookAsync(Book book);  
        Task UpdateBookAsync(Book book);  
        Task DeleteBookAsync(int id);  
    }
}