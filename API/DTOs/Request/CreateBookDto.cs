namespace API.DTOs.Request;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public int Price { get; set; }
}