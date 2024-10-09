namespace API.DTOs.Response;

public class BookDto
{
    public string Title { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public int Price { get; set; }
}