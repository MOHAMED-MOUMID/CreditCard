using Microsoft.EntityFrameworkCore;
using CreditCardApi.Models;

namespace CreditCardApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}
