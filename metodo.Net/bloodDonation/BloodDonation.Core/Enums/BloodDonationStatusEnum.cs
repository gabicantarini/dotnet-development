using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Core.Enums
{
    public enum BloodDonationStatusEnum
    {
        Created = 0,//ProfileCreated
        Scheduled = 1, //ScheduledDonation
        Cancelled = 2, //doa~ção cancelada
        Finished = 3 //doação concluída

    }
}
