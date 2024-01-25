using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Core.Entities
{
    public class BaseEntity //classe para ser herdada
    {
        protected BaseEntity() { }
        public int Id { get; private set; }
    }
}
