using HopShip.Library.Database.Context;
using HopShip.Worker.Database.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Worker.Database.Repository
{
    public interface IUserRepository : IRepository
    {
    }

    public class UserRepository : IUserRepository
    {
        private readonly IContextForDb<ContextForDb> _context;
        public UserRepository(IContextForDb<ContextForDb> dbContext)
        {
            _context = dbContext;
        }

        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Users (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(100) NOT NULL,
                Email VARCHAR(100) NOT NULL UNIQUE,
                CreatedAt DATE NOT NULL
                );";

            _context.ExceuteSqlRaw(query);
        }
    }
}
