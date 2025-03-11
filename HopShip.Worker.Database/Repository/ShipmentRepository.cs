using HopShip.Library.Database.Context;
using HopShip.Worker.Database.Interface;

namespace HopShip.Worker.Database.Repository
{
    public interface IShipmentRepository : IRepository { }
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly IContextForDb<ContextForDb> _context;

        public ShipmentRepository(IContextForDb<ContextForDb> context)
        {
            _context = context;
        }

        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Shipments (
                Id SERIAL PRIMARY KEY,
                OrderId INT NOT NULL,
                ShipmentStatus SMALLINT NOT NULL,
                ShipmentDate DATE NOT NULL,
                TrackingNumber VARCHAR(100),
                CreatedAt DATE NOT NULL
            )";

            _context.ExceuteSqlRaw(query);
        }
    }
}
