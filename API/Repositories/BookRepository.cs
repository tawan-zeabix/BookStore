using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories;

public class BookRepository : BaseRepository<BookModel>, IBookRepository
{
    public BookRepository(AppDbContext context) : base(context)
    {
        
    }
}