using HopShip.Library.Database.Context;
using HopShip.Worker.Database.Interface;

namespace HopShip.Worker.Database.Repository
{
    public interface IPaymentRepository : IRepository { }
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IContextForDb<ContextForDb> _context;

        public PaymentRepository(IContextForDb<ContextForDb> context)
        {
            _context = context;
        }

        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS Payments (
            Id SERIAL PRIMARY KEY,
            OrderId INT NOT NULL,
            PaymentStatus SMALLINT NOT NULL,
            PaymentDate DATE NOT NULL,
            Amount DECIMAL(10, 2) NOT NULL, 
            PaymentMethod SMALLINT NOT NULL,
            CreatedAt SMALLINT NOT NULL
                );";

            _context.ExceuteSqlRaw(query);
        }
    }
}
