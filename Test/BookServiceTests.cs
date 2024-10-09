using API.Controllers;
using API.DTOs.Response;
using API.Services.Interfaces;
using Moq;

namespace Test;

public class BookServiceTests
{
    private readonly Mock<IBookService> _bookServiceMock;
    public BookServiceTests()
    {
        _bookServiceMock = new Mock<IBookService>();
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
}