using Microsoft.EntityFrameworkCore;

namespace EDA_Inventory.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
        
    }

    public ProductDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    
    public DbSet<Products> Products { get; set; }
}