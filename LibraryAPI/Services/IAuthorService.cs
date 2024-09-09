﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();  
        Task<Author> GetAuthorByIdAsync(int id); 
        Task AddAuthorAsync(Author author);  
        Task UpdateAuthorAsync(Author author);  
        Task DeleteAuthorAsync(int id);  
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);  
    }
}
