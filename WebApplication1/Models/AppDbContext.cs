using Microsoft.EntityFrameworkCore;
namespace PostgresAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Actor> Actor { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<City> City { get; set; }

}
