using Microsoft.EntityFrameworkCore;

namespace OrdersParser.Models
{
    internal class MSSQLContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public MSSQLContext(DbContextOptions<MSSQLContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
