using Microsoft.EntityFrameworkCore;
namespace PostgresAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Actor> Actor { get; set; }
}
