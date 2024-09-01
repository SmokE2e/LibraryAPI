using System;

namespace LibraryAPI.Models
{
    // Модель для представления книги в системе
    public class Book
    {
        // Идентификатор книги
        public int Id { get; set; }

        // ISBN книги
        public string ISBN { get; set; }

        // Название книги
        public string Title { get; set; }

        // Жанр книги
        public string Genre { get; set; }

        // Описание книги
        public string Description { get; set; }

        // Внешний ключ на автора книги
        public int AuthorId { get; set; }

        // Связь с моделью автора
        public Author Author { get; set; }

        // Время, когда книгу взяли
        public DateTime? TakenAt { get; set; }

        // Время, когда книгу нужно вернуть
        public DateTime? ReturnBy { get; set; }

        // Путь к изображению книги
        public string ImagePath { get; set; }
    }
}
