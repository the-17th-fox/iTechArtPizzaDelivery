using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Constants.OrderStatuses
{
    public enum OrderStatuses
    {
        InProccesOfCreating,
        IsNotPaid,
        IsPaid,
        CookingInProgress,
        Delivering,
        Delivered
    }
}
