using Microsoft.EntityFrameworkCore;

namespace EDA_Customer.Data;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext()
    {
        
    }

    public CustomerDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
        
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
}