using HopShip.Data.DTO.Repository;
using HopShip.Repository.Order;
using HopShip.Repository.OrderProduct;
using HopShip.Repository.Payment;
using HopShip.Repository.Product;
using HopShip.Repository.Shipment;
using HopShip.Repository.Version;
using HopShip.Service.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.Database
{
    public interface ISrvDatabaseService
    {
        public Task BuildDatabaseAsync(CancellationToken cancellationToken);
        public Task InsertDataAsync(CancellationToken cancellationToken);
    }

    public class SrvDatabaseService : ISrvDatabaseService
    {
        private readonly IMdlOrderProductRepository _orderProductRepository;
        private readonly IMdlOrderRepository _orderRepository;
        private readonly IMdlPaymentRepository _paymentRepository;
        private readonly IMdlProductRepository _productRepository;
        private readonly IMdlShipmentRepository _shipmentRepository;
        private readonly IMdlVersionRepository _versionRepository;
        private readonly ILogger<SrvDatabaseService> _logger;

        public SrvDatabaseService(IMdlOrderProductRepository mdlOrderProductRepository, IMdlOrderRepository mdlOrderRepository, IMdlPaymentRepository mdlPaymentRepository, IMdlProductRepository mdlProductRepository, IMdlShipmentRepository mdlShipmentRepository, IMdlVersionRepository mdlVersionRepository, ILogger<SrvDatabaseService> logger)
        {
            _orderProductRepository = mdlOrderProductRepository;
            _orderRepository = mdlOrderRepository;
            _paymentRepository = mdlPaymentRepository;
            _productRepository = mdlProductRepository;
            _shipmentRepository = mdlShipmentRepository;
            _versionRepository = mdlVersionRepository;
            _logger = logger;
        }

        public async Task BuildDatabaseAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start BuildDatabaseAsync");

            string version = await GetVersionAsync(cancellationToken);
            if(string.IsNullOrEmpty(version))
            {
                await Version_1_0_0(cancellationToken);
            }

            _logger.LogInformation("End BuildDatabaseAsync");
        }

        private async Task Version_1_0_0(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Version_1_0_0");

            string query = @"CREATE TABLE IF NOT EXISTS version (
                version VARCHAR(100) NOT NULL,
                rud DATE NOT NULL
            )"
            ;

            await _versionRepository.ExecuteSqlRawAsync(query);

            query = @"INSERT INTO version (Version, RUD) VALUES ('1.0.0', '" + DateTime.Now + "')";

            await _versionRepository.ExecuteSqlRawAsync(query);

            query = @"CREATE TABLE IF NOT EXISTS orders (
                id SERIAL PRIMARY KEY,
                userid INT NOT NULL,
                totalamount DECIMAL(10, 2) NOT NULL,
                status SMALLINT NOT NULL,
                createdat DATE NOT NULL
            );";

            await _orderRepository.ExecuteSqlRawAsync(query);

            query = @"CREATE TABLE IF NOT EXISTS Payments (
            id SERIAL PRIMARY KEY,
            orderid INT NOT NULL,
            paymentstatus SMALLINT NOT NULL,
            paymentdate DATE NOT NULL,
            amount DECIMAL(10, 2) NOT NULL, 
            paymentmethod SMALLINT NOT NULL,
            createdat DATE NOT NULL
                );";

            await _paymentRepository.ExecuteSqlRawAsync(query);

            query = @"CREATE TABLE IF NOT EXISTS Products (
                id SERIAL PRIMARY KEY,
                name VARCHAR(100) NOT NULL,
                description TEXT,
                price DECIMAL(10, 2) NOT NULL,
                discount DECIMAL(10, 2) NOT NULL,
                stock INT NOT NULL,
                category SMALLINT NOT NULL,
                isactive BOOLEAN NOT NULL,
                createdat DATE NOT NULL
            );";

            await _productRepository.ExecuteSqlRawAsync(query);

            query = @"CREATE TABLE IF NOT EXISTS orderproducts (
                id SERIAL PRIMARY KEY,
                orderid INT NOT NULL,
                productid INT NOT NULL,
                stock INT NOT NULL,
                unitprice DECIMAL(10, 2) NOT NULL,
                discount DECIMAL(10, 2) NOT NULL,
                totalprice DECIMAL(10, 2) NOT NULL,
                createddate DATE NOT NULL
            );";

            await _orderProductRepository.ExecuteSqlRawAsync(query);

            query = @"CREATE TABLE IF NOT EXISTS Shipments (
                id SERIAL PRIMARY KEY,
                orderid INT NOT NULL,
                shipmentstatus SMALLINT NOT NULL,
                shipmentdate DATE NOT NULL,
                trackingnumber VARCHAR(100),
                createdat DATE NOT NULL
            )";

            await _shipmentRepository.ExecuteSqlRawAsync(query);

            _logger.LogInformation("End Version_1_0_0");
        }

        private async Task<string> GetVersionAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GetVersionAsync");

            try
            {
                MdlVersion version = await _versionRepository.FirstAsync(x => true, cancellationToken: cancellationToken);

                _logger.LogInformation("End GetVersionAsync");

                return version.Version;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return string.Empty;
            }
        }

        public async Task InsertDataAsync(CancellationToken cancellationToken)
        {
            var products = SrvStorageData.GetProducts();

            await _productRepository.BulkInsertAsync(products, cancellationToken);
        }
    }
}
