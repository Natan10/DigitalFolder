using DigitalFolder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFolder.Data
{
    public class AppDbContext : IdentityDbContext<CustomIdentityUser,IdentityRole<int>,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt)
        {

        }
        
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Um para muitos
            builder.Entity<Transaction>()
                .HasOne(transaction => transaction.Wallet)
                .WithMany(wallet => wallet.Transactions)
                .HasForeignKey(transaction => transaction.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

           
            // Um para muitos 
            builder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Wallet>().HasIndex(u => u.WalletName).IsUnique();

            base.OnModelCreating(builder);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
                }
                 ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
