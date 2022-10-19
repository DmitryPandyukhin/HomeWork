using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Доступ к функционалу EF Core
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : 
            base(options)
        {

        }
        // Доступ к объектам Product в БД
        public DbSet<Product> Products { get; set; }
    }
}
