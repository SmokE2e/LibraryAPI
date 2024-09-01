using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Data;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories
{
    // Реализация интерфейса репозитория для работы с книгами
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            // Возвращает все книги с информацией об авторе
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            // Возвращает книгу по Id с информацией об авторе
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> GetBookByISBNAsync(string isbn)
        {
            // Возвращает книгу по ISBN с информацией об авторе
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task AddBookAsync(Book book)
        {
            // Добавляет новую книгу в базу данных
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            // Обновляет информацию о книге
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            // Удаляет книгу из базы данных
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}