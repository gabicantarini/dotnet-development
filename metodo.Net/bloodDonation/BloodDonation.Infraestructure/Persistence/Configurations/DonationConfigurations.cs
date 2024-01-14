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
    public class DonationConfigurations : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .HasOne(d => d.Donator)
                .WithMany()
                .HasForeignKey(d => d.DonatorId);
        }
    }
}
