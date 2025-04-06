using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Data.Enum
{
    public enum EnumStatusPayment
    {
        InQueue = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3,
        Canceled = 4
    }

    public enum EnumMethodPayment
    {
        None = 0,
        Card = 1,
        Cash = 2,
        PayPal = 3,
        BankTransfer = 4,
    }
}
