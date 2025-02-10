using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.Database.Context
{
    public class HopDbContext : DbContext
    {
        public HopDbContext(DbContextOptions<HopDbContext> options) : base(options) { }
    }
}
