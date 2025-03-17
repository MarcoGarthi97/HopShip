using HopShip.Data.DTO.Repository;
using HopShip.Library.Database.Context;
using HopShip.Library.Database.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Repository.Product
{
    public interface IMdlProductRepository : IRepository<MdlProduct>
    {

    }

    public class MdlProductRepository : AbstractRepository<MdlProduct>, IMdlProductRepository
    {
        private readonly ILogger<MdlProductRepository> _logger;
        public MdlProductRepository(IContextForDb<ContextForDb> _context, ILogger<MdlProductRepository> logger) : base(_context)
        {
            _logger = logger;
        }
    }
}
