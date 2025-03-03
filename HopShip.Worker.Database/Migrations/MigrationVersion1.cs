using HopShip.Worker.Database.Database;
using HopShip.Worker.Database.Interface;
using HopShip.Worker.Database.Repository;
using System.Xml.Serialization;

namespace HopShip.Worker.Database.Migrations
{
    public interface IMigrationVersion1 : IMigrationVersion { }

    public class MigrationVersion1 : IMigrationVersion1
    {
        private readonly IHopShipDb _hopShipDb;
        private readonly IUserRepository _userRepository;
        private readonly IShipmentRepository  _shipmentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IVersionRepository _versionRepository;
        public MigrationVersion1(IHopShipDb hopShipDb, IUserRepository userRepository, IShipmentRepository shipmentRepository, IProductRepository productRepository, IPaymentRepository paymentRepository, IOrderRepository orderRepository, IVersionRepository versionRepository)
        {
            _hopShipDb = hopShipDb;
            _userRepository = userRepository;
            _shipmentRepository = shipmentRepository;
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _versionRepository = versionRepository;
        }

        public async Task ExecuteVersion()
        {
            //TODO: Modificarlo perché così ha poco senso
            var version = await _versionRepository.GetVersion();
            if (version == "1.0.0")
                return;

            //_hopShipDb.CreateDatabase();

            _userRepository.CreateTable();
            _shipmentRepository.CreateTable();
            _productRepository.CreateTable();
            _paymentRepository.CreateTable();
            _orderRepository.CreateTable();
            _versionRepository.CreateTable();
        }
    }
}
