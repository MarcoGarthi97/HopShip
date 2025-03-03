using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Worker.Database.Interface
{
    public interface IRepository
    {
        public void CreateTable();
    }

    public interface IMigrationVersion
    {
        public Task ExecuteVersion();
    }
}
