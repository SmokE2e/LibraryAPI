using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    // Интерфейс сервиса для работы с авторами
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();  // Получение всех авторов
        Task<Author> GetAuthorByIdAsync(int id);  // Получение автора по Id
        Task AddAuthorAsync(Author author);  // Добавление нового автора
        Task UpdateAuthorAsync(Author author);  // Обновление информации об авторе
        Task DeleteAuthorAsync(int id);  // Удаление автора
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);  // Получение всех книг по автору
    }
}
