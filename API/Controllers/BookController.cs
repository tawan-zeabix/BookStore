using API.DTOs.Request;
using API.DTOs.Response;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<List<BookDto>> GetBooks()
    {
        return await _bookService.GetAllBooksAsync();
    }

    [HttpGet("{id}")]
    public async Task<BookDto> GetBook(int id)
    {
        return await _bookService.GetBookByIdAsync(id);
    }

    [HttpPost]
    public async Task<BookDto> AddBook([FromBody] CreateBookDto book)
    {
        return await _bookService.AddBookAsync(book);
        
    }

    [HttpPut("{id}")]
    public async Task<BookDto> UpdateBook(int id, [FromBody] CreateBookDto book)
    {
        return await _bookService.UpdateBookAsync(book, id);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteBook(int id)
    {
        return await _bookService.DeleteBookAsync(id);
    }
}