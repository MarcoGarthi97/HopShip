using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Worker.Database.Migrations
{
    public interface IMigrationOrchestrator
    {
        public Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public class MigrationOrchestrator : IMigrationOrchestrator
    {
        private readonly IMigrationVersion1 _version1;
        public MigrationOrchestrator(IMigrationVersion1 version1)
        {
            _version1 = version1;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //TODO: Modificarlo perché così ha poco senso

            await _version1.ExecuteVersion();
        }
    }
}
