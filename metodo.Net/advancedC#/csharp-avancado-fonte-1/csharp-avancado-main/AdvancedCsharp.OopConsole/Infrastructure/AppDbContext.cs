using System;
using AdvancedCsharp.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdvancedCsharp.OopConsole.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(c => c.Id);
        }
    }
}

