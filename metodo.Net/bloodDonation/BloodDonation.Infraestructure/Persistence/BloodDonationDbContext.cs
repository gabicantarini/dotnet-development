using BloodDonation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml.Linq;

namespace BloodDonation.Infraestructure.Persistence
{
    public class BloodDonationDbContext : DbContext
    {
        public BloodDonationDbContext(DbContextOptions<BloodDonationDbContext> options) : base(options)  
        {
            //fill db in memory
            //Donators = new List<Donator>
            //{
            //    new Donator("Gabriela Cantarini", "gab@hotmail.com", new DateTime(1984, 08, 23), "F", 70, "O+", "RHFACTOR"),
            //    new Donator("Le Cantarini", "leb@hotmail.com", new DateTime(1986, 07, 09), "F", 93.5, "O+", "RHFACTOR"),
            //    new Donator("Ceci Cantarini", "ce@hotmail.com", new DateTime(19884, 01, 10), "F", 15.3, "O+", "RHFACTOR"),

            //};

            //Donations = new List<Donation>
            //{
            //    new Donation(1, new DateTime(2023, 12, 20), 1000),
            //    new Donation(1, new DateTime(2023, 12, 26), 900),
            //    new Donation(1, new DateTime(2023, 12, 28), 200),

            //};

            //BloodStock = new List<BloodStock>
            //{

            //};
        }

        public DbSet<Donator> Donators { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<BloodStock> BloodStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}
