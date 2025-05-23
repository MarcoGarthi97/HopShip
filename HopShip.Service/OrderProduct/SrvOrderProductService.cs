﻿using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;
using HopShip.Data.Enum;
using HopShip.Repository.OrderProduct;
using HopShip.Service.Product;
using Microsoft.Extensions.Logging;

namespace HopShip.Service.OrderProduct
{
    public interface ISrvOrderProductService
    {
        public Task<IEnumerable<SrvOrderProduct>> GerOrderProductsByOrderIdAsync(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<SrvOrderProduct>> CheckOrderProductsBeforeAsync(List<SrvOrderProduct> srvOrderProducts, CancellationToken cancellationToken);
        public EnumStatusOrder CheckOrderProductsAfter(IEnumerable<SrvOrderProduct> srvOrderProducts, CancellationToken cancellationToken);
        public Task InsertOrderProductAsync(IEnumerable<SrvOrderProduct> srvOrderProducts, CancellationToken cancellationToken);
    }

    public class SrvOrderProductService : ISrvOrderProductService
    {
        private readonly ILogger<SrvOrderProductService> _logger;
        private readonly IMapper _mapper;
        private readonly ISrvProductService _serviceProduct;
        private readonly IMdlOrderProductRepository _repositoryOrderProduct;

        public SrvOrderProductService(ILogger<SrvOrderProductService> logger, IMapper mapper, IMdlOrderProductRepository repositoryOrderProduct, ISrvProductService srvProductService)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryOrderProduct = repositoryOrderProduct;
            _serviceProduct = srvProductService;
        }

        public async Task<IEnumerable<SrvOrderProduct>> GerOrderProductsByOrderIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GerOrderProductsByOrderIdAsync");

            IEnumerable<MdlOrderProduct> mdlOrderProducts = await _repositoryOrderProduct.FindAsync(x => x.OrderId == id, cancellationToken);
            IEnumerable<SrvOrderProduct> srvOrderProducts = _mapper.Map<IEnumerable<SrvOrderProduct>>(mdlOrderProducts);

            _logger.LogInformation("End GerOrderProductsByOrderIdAsync");

            return srvOrderProducts;
        }

        public async Task InsertOrderProductAsync(IEnumerable<SrvOrderProduct> srvOrderProducts, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start InsertOrderProductAsync");

            IEnumerable<MdlOrderProduct> mdlOrderProducts = _mapper.Map<IEnumerable<MdlOrderProduct>>(srvOrderProducts);
            mdlOrderProducts.ToList().ForEach(x => x.CreateDate = DateTime.UtcNow);
            await _repositoryOrderProduct.BulkInsertAsync(mdlOrderProducts, cancellationToken);

            _logger.LogInformation("End InsertOrderProductAsync");
        }

        public async Task<IEnumerable<SrvOrderProduct>> CheckOrderProductsBeforeAsync(List<SrvOrderProduct> srvOrderProducts, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start CheckOrdersBeforeAsync");

            IEnumerable<SrvProduct> products = await _serviceProduct.GetProductByIdsAsync(srvOrderProducts.Select(x => x.ProductId), cancellationToken);

            foreach (var product in products)
            {
                SrvOrderProduct orderProduct = srvOrderProducts.First(x => x.ProductId == product.Id);

                if (!product.IsActive)
                {
                    srvOrderProducts.Remove(orderProduct);
                }

                if (product.Stock < orderProduct.Stock)
                {
                    srvOrderProducts.Remove(orderProduct);
                }
            }

            _logger.LogInformation("End CheckOrdersBeforeAsync");

            return srvOrderProducts;
        }

        public EnumStatusOrder CheckOrderProductsAfter(IEnumerable<SrvOrderProduct> srvOrderProducts, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start CheckOrderProductsAfterAsync");

            try
            {
                foreach (var orderProduct in srvOrderProducts)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return EnumStatusOrder.OrderFailed;
                    }

                    decimal discount = (100 - orderProduct.Discount) / 100;
                    decimal total = orderProduct.Stock * orderProduct.UnitPrice;
                    decimal totalPrice = total * discount;

                    if((int)totalPrice != (int)orderProduct.TotalPrice)
                    {
                        return EnumStatusOrder.OrderNotValidated;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return EnumStatusOrder.OrderFailed;
            }

            _logger.LogInformation("End CheckOrderProductsAfterAsync");

            return EnumStatusOrder.OrderValidated;
        }
    }
}
