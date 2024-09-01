using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    // Интерфейс сервиса для работы с книгами
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();  // Получение всех книг
        Task<Book> GetBookByIdAsync(int id);  // Получение книги по Id
        Task<Book> GetBookByISBNAsync(string isbn);  // Получение книги по ISBN
        Task AddBookAsync(Book book);  // Добавление новой книги
        Task UpdateBookAsync(Book book);  // Обновление существующей книги
        Task DeleteBookAsync(int id);  // Удаление книги
    }
}