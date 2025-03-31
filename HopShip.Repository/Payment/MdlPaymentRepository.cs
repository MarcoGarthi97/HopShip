using HopShip.Data.DTO.Repository;
using HopShip.Library.Database.Context;
using HopShip.Library.Database.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Repository.Payment
{
    public interface IMdlPaymentRepository : IRepository<MdlPayment>
    {

    }

    public class MdlPaymentRepository : AbstractRepository<MdlPayment>, IMdlPaymentRepository
    {
        private readonly ILogger<MdlPaymentRepository> _logger;
        public MdlPaymentRepository(IContextForDb<ContextForDb> _context, ILogger<MdlPaymentRepository> logger): base(_context)
        {
            _logger = logger;
        }
    }
}
