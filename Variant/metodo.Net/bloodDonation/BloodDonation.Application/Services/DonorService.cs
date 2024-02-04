using BloodDonation.Application.Interfaces;
using BloodDonation.Core.Entities;
using BloodDonation.Core.Enums;
using BloodDonation.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class DonorService : IDonorService
    {
        private readonly BloodDonationDbContext _dbContext;
        public DonorService(BloodDonationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckDonorIsValid(Donator donator)
        {
            await _dbContext.Donators.AddAsync(donator);

            await _dbContext.SaveChangesAsync();

            if (donator.Weight < 50)
            {
                throw new Exception("Peso mínimo para doação é de 50Kg");
            }

            var age = DateTime.Now.Year - donator.BirthDate.Year;

            if (age < 18)
            {
                throw new Exception("A idade para doar precisa ser superior a 18 anos!");
            }

            if (donator != null)
            {
                var lastDonation = donator.Donations.OrderByDescending(d => d.DonationDate).Select(d => d.DonationDate).FirstOrDefault();

                if (lastDonation != null)
                {
                    //var period = (donator.Gender == Core.Enums.Gender ? 60 : 90);
                    var daysOfLasDonation = (DateTime.Today - lastDonation).Days;
                    //var daysLeft = period - daysOfLasDonation;

                    if (daysOfLasDonation < daysOfLasDonation)
                    {
                        throw new Exception($"{donator.FullName}, é necessário aguardar mais {daysOfLasDonation} dias para doar novamente!");
                    }
                }
            }

            return true;
        }
    }
}
