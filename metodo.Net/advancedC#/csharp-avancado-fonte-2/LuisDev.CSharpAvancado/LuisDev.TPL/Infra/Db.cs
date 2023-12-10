using Microsoft.EntityFrameworkCore;

namespace LuisDev.TPL.Infra
{
    internal class Db : DbContext
    {
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=AulaLuisDev;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
