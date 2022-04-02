using DigitalFolder.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalFolder.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt)
        {

        }
        
        public DbSet<Wallet> Wallets { get; set; }
    }
}
