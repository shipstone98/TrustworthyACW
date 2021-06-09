using Microsoft.EntityFrameworkCore;

using CateringSystem;
using CateringSystem.PayPal;

namespace CateringSystemWeb.Data
{
    public class CateringContext : DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PayPalOrder> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public CateringContext() { }
        public CateringContext(DbContextOptions<CateringContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CateringSystem-2afded41-3b4c-47f2-8dfe-17ed883bc15b;Trusted_Connection=True;MultipleActiveResultSets=true;");
            optionsBuilder.UseSqlServer("Server=tcp:cateringsystemdb.database.windows.net,1433;Database=coreDB;User ID=chris;Password=Carrot31;Encrypt=true;Connection Timeout=30;MultipleActiveResultSets=true;");
        }
    }
}