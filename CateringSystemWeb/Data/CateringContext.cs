using Microsoft.EntityFrameworkCore;

using CateringSystem;

namespace CateringSystemWeb.Data
{
    public class CateringContext : DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public CateringContext() { }
        public CateringContext(DbContextOptions<CateringContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CateringSystem-2afded41-3b4c-47f2-8dfe-17ed883bc15b;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
    }
}