using API.Controllers;
using API.DTOs.Request;
using API.DTOs.Response;
using API.Helpers;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Moq;

namespace Test;

public class BookServiceTests
{
    private readonly Mock<IBookService> _bookServiceMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    public BookServiceTests()
    {
        _bookServiceMock = new Mock<IBookService>();
        _bookRepositoryMock = new Mock<IBookRepository>();
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook()
    {
        // Arrange
        int bookId = 1;
        BookDto book = new BookDto()
        {
            Title = "Book1",
            Writer = "Admin",
            Price = 300
        };
        
        _bookServiceMock.Setup(service => service.GetBookByIdAsync(bookId)).ReturnsAsync(book);
        BookController bookController = new BookController(_bookServiceMock.Object);
        
        // Act
        BookDto result = await bookController.GetBook(bookId);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Book1", result.Title);
        Assert.Equal("Admin", result.Writer);
    }
    
    [Fact]
    public async Task AddBookAsync_ShouldAddBook_WhenBookIsValid()
    {
        // Arrange
        CreateBookDto validBookDto = new CreateBookDto()
        {
            Title = "Book number 1",
            Writer = "Admin",
            Price = 20
        };

        BookDto bookCreated = new BookDto()
        {
            Title = "Book number 1",
            Writer = "Admin",
            Price = 20
        };
        
        _bookServiceMock.Setup(service => service.AddBookAsync(validBookDto)).ReturnsAsync(bookCreated);
        BookController bookController = new BookController(_bookServiceMock.Object);

        // Act
        BookDto book = await bookController.AddBook(validBookDto);

        // Assert
        Assert.NotNull(book);
        Assert.Equal("Book number 1", book.Title);
        Assert.Equal("Admin", book.Writer);
        Assert.Equal(20, book.Price);
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldUpdateBook_WhenBookIsValid()
    {
        // Arrange
        CreateBookDto validBookDto = new CreateBookDto()
        {
            Title = "Book number 2",
            Writer = "Admin",
            Price = 20
        };
        
        BookDto bookUpdated = new BookDto()
        {
            Title = "Book number 2",
            Writer = "Admin",
            Price = 20
        };
        _bookServiceMock.Setup(service => service.UpdateBookAsync(validBookDto, 1)).ReturnsAsync(bookUpdated);
        BookController bookController = new BookController(_bookServiceMock.Object);
        
        // Act
        BookDto book = await bookController.UpdateBook(1, validBookDto);

        // Assert
        Assert.NotNull(book);
        Assert.Equal("Book number 2", book.Title);
        Assert.Equal("Admin", book.Writer);
        Assert.Equal(20, book.Price);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldCallDelete_WhenBookExists()
    {
        // Arrange
        int bookId = 1;
        var bookModel = new BookModel() {Id=1, Title = "Sample Book", Writer = "Author", Price = 20 };
        
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(bookModel);
        BookController bookController = new BookController(_bookServiceMock.Object);

        // Act
        BookDto book = await bookController.GetBook(bookId);
        BookModel findBook = await _bookRepositoryMock.Object.GetByIdAsync(bookId);
        await bookController.DeleteBook(bookId);

        // Assert
        _bookRepositoryMock.Verify(repo => repo.DeleteAsync(findBook));
    }
    
}

    