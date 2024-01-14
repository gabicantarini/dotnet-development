using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepository;
        public DonorService(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<bool> CheckDonorIsValid(Donor donor)
        {
            if (donor.Weight < 50)
            {
                throw new Exception("Peso mínimo para doação é de 50Kg");
            }

            var age = DateTime.Now.Year - donor.BirthDate.Year;

            if (age < 18)
            {
                throw new Exception("A idade para doar precisa ser superior a 18 anos!");
            }

            if (donor != null)
            {
                var lastDonation = donor.Donations.OrderByDescending(d => d.DonationDate).Select(d => d.DonationDate).FirstOrDefault();

                if (lastDonation != null)
                {
                    var period = (donor.Gender == Core.Enums.Gender.Female ? 60 : 90);
                    var daysOfLasDonation = (DateTime.Today - lastDonation).Days;
                    var daysLeft = period - daysOfLasDonation;

                    if (daysOfLasDonation < period)
                    {
                        throw new Exception($"{donor.FullName}, é necessário aguardar mais {daysLeft} dias para doar novamente!");
                    }
                }
            }

            return true;
        }
    }
}
