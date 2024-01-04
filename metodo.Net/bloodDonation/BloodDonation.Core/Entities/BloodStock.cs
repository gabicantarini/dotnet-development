using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Core.Entities
{
    public class BloodStock : BaseEntity //ter uma modelagem  para exibir o estoque???
    {
        public BloodStock(string bloodType, string rhFactor, decimal mLQuantity)
        {
            BloodType = bloodType;
            RhFactor = rhFactor;
            MLQuantity = mLQuantity;
        }

        public string BloodType { get; private set; }

        public string RhFactor { get; private set; }

        public decimal MLQuantity { get; private set; }
    }
}
