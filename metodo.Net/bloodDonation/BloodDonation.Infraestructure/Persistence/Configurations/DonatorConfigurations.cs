using BloodDonation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Infraestructure.Persistence.Configurations
{
    public class DonatorConfigurations : IEntityTypeConfiguration<Donator>
    {

        public void Configure(EntityTypeBuilder<Donator> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .HasMany(d => d.Donations)
                .WithOne()
                .HasForeignKey(d => d.Id);

            builder
                .Property(d => d.FullName)
                .HasMaxLength(30);

            builder
                .Property(d => d.Email)
                .HasMaxLength(30);

        }
    }
}
