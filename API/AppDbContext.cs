using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BookModel> Books { get; set; }
}