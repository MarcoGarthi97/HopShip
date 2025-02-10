using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Worker.Database.Context
{
    public interface IDbContext<TContext> where TContext : DbContext
    {
        int ExceuteSqlRaw(string sql);
    }

    public class HopDbContext : DbContext, IDbContext<HopDbContext>
    {
        public HopDbContext(DbContextOptions<HopDbContext> options) : base(options) { }

        public int ExceuteSqlRaw(string sql)
        {
            return Database.ExecuteSqlRaw(sql);
        }
    }
}
