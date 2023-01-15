using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options)
        {

        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasKey(u => u.Id);
        }
    }
}
