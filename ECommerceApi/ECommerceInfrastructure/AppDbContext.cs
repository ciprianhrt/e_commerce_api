using ECommerceDomain;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInfrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}