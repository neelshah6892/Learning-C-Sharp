using Microsoft.EntityFrameworkCore;

namespace CRUDASPNETCOREandSQLITE.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
