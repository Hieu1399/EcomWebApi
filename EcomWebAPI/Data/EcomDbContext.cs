using EcomWebAPI.Models;
using EcomWebAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace EcomWebAPI.Data
{
    public class EcomDbContext : DbContext
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options)
            : base(options)
        {

        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> UserModels { get; set; }
    }
}
