using HopShip.Data.DTO.Repository;
using HopShip.Library.Database.Context;
using HopShip.Worker.Database.Interface;

namespace HopShip.Worker.Database.Repository
{
    public interface IVersionRepository : IRepository
    {
        public Task<string> GetVersion();
    }

    public class VersionRepository : IVersionRepository
    {
        private readonly IContextForDb<ContextForDb> _context;

        public VersionRepository(IContextForDb<ContextForDb> context)
        {
            _context = context;
        }

        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Version (
                Version VARCHAR(100) NOT NULL,
                RUD DATE NOT NULL
            )";

            _context.ExceuteSqlRaw(query);

            InsertRecord();
        }

        public async Task<string> GetVersion()
        {
            var mdlVersion = await _context.FirstOrDefaultAsync<MdlVersion>(x => true);

            return mdlVersion?.Version;
        }

        private void InsertRecord()
        {
            string query = @"INSERT INTO Version (Version, RUD) VALUES ('1.0.0', '" + DateTime.Now + "')";

            _context.ExceuteSqlRaw(query);
        }
    }
}
