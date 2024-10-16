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
    public async Task AddBook([FromBody] CreateBookDto book)
    {
        await _bookService.AddBookAsync(book);
    }

    [HttpPut("{id}")]
    public async Task UpdateBook(int id, [FromBody] CreateBookDto book)
    {
        await _bookService.UpdateBookAsync(book, id);
    }

    [HttpDelete("{id}")]
    public async Task DeleteBook(int id)
    {
        await _bookService.DeleteBookAsync(id);
    }
}