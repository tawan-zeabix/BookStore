namespace API.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Writer { get; set; } = string.Empty;
    public int Price { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}