using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class AppContext : DbContext
{
    public DbSet<Administrator> Administrators { get; set; } = default!;

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }
}
