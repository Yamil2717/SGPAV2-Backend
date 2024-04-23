using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class InventoryContext(DbContextOptions<InventoryContext> options) : DbContext(options)
    {
        public DbSet<Products> Products { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<SaleDetail> SaleDetail { get; set; }

        public DbSet<Sale> Sale { get; set; }
    }
}
