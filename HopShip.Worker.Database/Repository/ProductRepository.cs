using HopShip.Library.Database.Context;
using HopShip.Worker.Database.Interface;

namespace HopShip.Worker.Database.Repository
{
    public interface IProductRepository : IRepository { }
    public class ProductRepository : IProductRepository
    {
        private readonly IContextForDb<ContextForDb> _context;

        public ProductRepository(IContextForDb<ContextForDb> context)
        {
            _context = context;
        }

        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Products (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(100) NOT NULL,
                Description TEXT,
                Price DECIMAL(10, 2) NOT NULL,
                Stock INT NOT NULL,
                Category SMALLINT NOT NULL,
                CreatedAt DATE NOT NULL
            );";

            _context.ExceuteSqlRaw(query);
        }
    }
}
