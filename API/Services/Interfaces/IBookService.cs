using API.DTOs.Request;
using API.DTOs.Response;
using API.Models;

namespace API.Services.Interfaces;

public interface IBookService
{
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<List<BookDto>> GetAllBooksAsync();
    Task AddBookAsync(CreateBookDto book);
    Task UpdateBookAsync(CreateBookDto book, int id);
    Task DeleteBookAsync(int id);
}