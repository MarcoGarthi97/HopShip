using HopShip.Library.Database.Context;
using HopShip.Worker.Database.Interface;

namespace HopShip.Worker.Database.Repository
{
    public interface IOrderRepository : IRepository { }
    public class OrderRepository : IOrderRepository
    {
        private readonly IContextForDb<ContextForDb> _context;

        public OrderRepository(IContextForDb<ContextForDb> context)
        {
            _context = context;
        }

        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Orders (
                Id SERIAL PRIMARY KEY,
                UserId INT NOT NULL,
                TotalAmount DECIMAL(10, 2) NOT NULL,
                Status SMALLINT NOT NULL,
                CreatedAt DATE NOT NULL
            );";

            _context.ExceuteSqlRaw(query);
        }
    }
}
