using Microsoft.EntityFrameworkCore;
using SuperStore.Domain.Entities;

namespace SuperStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor receives DbContextOptions from DI
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Price)
                      .HasPrecision(18, 2);

                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("NOW()");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(150);
            });
        }
    }
}
