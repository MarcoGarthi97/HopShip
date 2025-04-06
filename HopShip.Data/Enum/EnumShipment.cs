using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Data.Enum
{
    public enum EnumStatusShipment
    {
        Pending = 0,
        Processing = 1,
        Shipped = 2,
        InTransit = 3,
        OutForDelivery = 4,
        Delivered = 5,
        FailedDelivery = 6,
        Returned = 7,
        Cancelled = 8,
        Error = 9
    }
}
