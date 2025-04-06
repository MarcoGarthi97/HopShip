using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Data.Enum
{
    public enum EnumStatusOrder
    {
        OrderCreated = 0,
        OrderValidated = 1,
        OrderDeleted = 2,
        OrderNotValidated = 3,
        OrderFailed = 4,
    }
}
