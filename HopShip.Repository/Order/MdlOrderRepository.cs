using HopShip.Data.DTO.Repository;
using HopShip.Library.Database.Context;
using HopShip.Library.Database.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Repository.Order
{
    public interface IMdlOrderRepository : IRepository<MdlOrder>
    {

    }

    public class MdlOrderRepository : AbstractRepository<MdlOrder>, IMdlOrderRepository
    {
        private readonly ILogger<MdlOrderRepository> _logger;
        public MdlOrderRepository(IContextForDb<ContextForDb> _context, ILogger<MdlOrderRepository> logger) : base(_context)
        {
            _logger = logger;
        }
    }
}
