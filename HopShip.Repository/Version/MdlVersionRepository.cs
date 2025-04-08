using HopShip.Data.DTO.Repository;
using HopShip.Library.Database.Context;
using HopShip.Library.Database.Interface;
using HopShip.Repository.Product;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Repository.Version
{
    public interface IMdlVersionRepository : IRepository<MdlVersion>
    {

    }

    public class MdlVersionRepository : AbstractRepository<MdlVersion>, IMdlVersionRepository
    {
        private readonly ILogger<MdlVersionRepository> _logger;
        public MdlVersionRepository(IContextForDb<ContextForDb> _context, ILogger<MdlVersionRepository> logger) : base(_context)
        {
            _logger = logger;
        }
    }
}
