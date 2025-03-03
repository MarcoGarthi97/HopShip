using HopShip.DatabaseService.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Worker.Database.Database
{
    public interface IHopShipDb
    {
        public void CreateDatabase();
    }
    public class HopShipDb : IHopShipDb
    {
        private readonly IContextForDb<ContextForDb> _context;

        public HopShipDb(IContextForDb<ContextForDb> context)
        {
            _context = context;
        }

        public void CreateDatabase()
        {
            string query = @"CREATE DATABASE HopShipDB;";

            _context.ExceuteSqlRaw(query);
        }
    }
}
