using System;
using System.Collections.Generic;

namespace LibraryAPI.Models
{
    // Модель для представления автора в системе
    public class Author
    {
        // Идентификатор автора
        public int Id { get; set; }

        // Имя автора
        public string FirstName { get; set; }

        // Фамилия автора
        public string LastName { get; set; }

        // Дата рождения автора
        public DateTime DateOfBirth { get; set; }

        // Страна, откуда родом автор
        public string Country { get; set; }

        // Связь с моделью книги (один автор может иметь много книг)
        public ICollection<Book> Books { get; set; }
    }
}