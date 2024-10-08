using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    public enum EnumOrderStatus
    {
        Undefined       = 0,
        OrderCreated    = 1,
        OrderPrepared   = 2,
        OrderSended     = 3,
        OrderAccepted   = 4,
        OrderCanceled   = 5,
        OrderDeleted    = 6,
    }
}
