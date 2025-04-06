using HopShip.Data.DTO.Repository;
using HopShip.Library.Database.Context;
using HopShip.Library.Database.Interface;
using Microsoft.Extensions.Logging;

namespace HopShip.Repository.Shipment
{
    public interface IMdlShipmentRepository : IRepository<MdlShipment>
    {

    }

    public class MdlShipmentRepository : AbstractRepository<MdlShipment>, IMdlShipmentRepository
    {
        private readonly ILogger<MdlShipmentRepository> _logger;
        public MdlShipmentRepository(IContextForDb<ContextForDb> _context, ILogger<MdlShipmentRepository> logger) : base(_context)
        {
            _logger = logger;
        }
    }
}
