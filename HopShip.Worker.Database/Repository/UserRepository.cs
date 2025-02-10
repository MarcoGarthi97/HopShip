using HopShip.Worker.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Worker.Database.Repository
{
    public interface IUserRepository
    {
        public void CreateTable();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDbContext<HopDbContext> _context;
        public UserRepository(IDbContext<HopDbContext> dbContext)
        {
            _context = dbContext;
        }

        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Users (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(100) NOT NULL,
                Email VARCHAR(100) NOT NULL UNIQUE,
                CreatedAt DATETIME NOT NULL
                );
            )";

            _context.ExceuteSqlRaw(query);
        }
    }
}
