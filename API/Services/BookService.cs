using API.DTOs.Request;
using API.DTOs.Response;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.Helpers;

namespace API.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        BookModel book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new NotFoundException("Book Not Found");
        }

        BookDto bookDto = new BookDto()
        {
            Title = book.Title,
            Writer = book.Writer,
            Price = book.Price,
        };
            
        return bookDto;
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        try
        {
            List<BookModel> books = await _bookRepository.GetAllAsync();
            List<BookDto> bookDtos = new List<BookDto>();
            foreach (BookModel book in books)
            {
                bookDtos.Add(new BookDto()
                {
                    Title = book.Title,
                    Writer = book.Writer,
                    Price = book.Price,
                });
            }
            
            return bookDtos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task AddBookAsync(CreateBookDto book)
    {
        try
        {
            BookModel newBook = new BookModel()
            {
                Title = book.Title,
                Writer = book.Writer,
                Price = book.Price,
                CreatedBy = "Admin",
                IsActive = true
            };
            
            await _bookRepository.AddAsync(newBook);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateBookAsync(CreateBookDto book, int id)
    {
        BookModel bookModel = await _bookRepository.GetByIdAsync(id);
        if (bookModel == null)
        {
            throw new Exception("Book not found");
        }
        bookModel.Title = book.Title;
        bookModel.Writer = book.Writer;
        bookModel.Price = book.Price;
        await _bookRepository.UpdateAsync(bookModel);
    }

    public async Task DeleteBookAsync(int id)
    {
        BookModel bookModel = await _bookRepository.GetByIdAsync(id);
        if (bookModel == null)
        {
            throw new Exception("Book not found");
        }
        await _bookRepository.DeleteAsync(bookModel);
    }
}