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
        if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Writer) || string.IsNullOrEmpty(book.Price.ToString()))
        {
            throw new BadRequestException("Book Not Valid");  
        }
            
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

    public async Task UpdateBookAsync(CreateBookDto book, int id)
    {
        if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Writer) || string.IsNullOrEmpty(book.Price.ToString()))
        {
            throw new BadRequestException("Book Not Valid");  
        }
        
        BookModel bookModel = await _bookRepository.GetByIdAsync(id);
        if (bookModel == null)
        {
            throw new NotFoundException("Book Not Found");
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
            throw new NotFoundException("Book not found");
        }
        await _bookRepository.DeleteAsync(bookModel);
    }
}