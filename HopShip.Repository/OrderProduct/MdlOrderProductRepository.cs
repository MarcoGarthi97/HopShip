using HopShip.Data.DTO.Repository;
using HopShip.Library.Database.Context;
using HopShip.Library.Database.Interface;
using HopShip.Repository.Order;
using Microsoft.Extensions.Logging;

namespace HopShip.Repository.OrderProduct
{
    public interface IMdlOrderProductRepository : IRepository<MdlOrderProduct>
    {

    }

    public class MdlOrderProductRepository : AbstractRepository<MdlOrderProduct>, IMdlOrderProductRepository
    {
        private readonly ILogger<MdlOrderProductRepository> _logger;
        public MdlOrderProductRepository(IContextForDb<ContextForDb> _context, ILogger<MdlOrderProductRepository> logger) : base(_context)
        {
            _logger = logger;
        }
    }
}
