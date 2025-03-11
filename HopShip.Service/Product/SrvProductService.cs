using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;
using HopShip.Repository.Product;
using Microsoft.Extensions.Logging;

namespace HopShip.Service.Product
{
    public interface ISrvProductService
    {
        public Task InsertProductsAsync(IEnumerable<SrvProduct> srvProducts, CancellationToken cancellationToken);
        public Task<IEnumerable<SrvProduct>> GetProductAsync(CancellationToken cancellationToken);
    }

    public class SrvProductService : ISrvProductService
    {
        private readonly ILogger<SrvProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IMdlProductRepository _productRepository;
        public SrvProductService(ILogger<SrvProductService> logger, IMapper mapper, IMdlProductRepository mdlProductRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = mdlProductRepository;
        }

        public async Task<IEnumerable<SrvProduct>> GetProductAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GerOrders");

            IEnumerable<MdlProduct> mdlProducts = await _productRepository.FindAsync(x => x.IsActive, cancellationToken);
            IEnumerable<SrvProduct> srvProducts = _mapper.Map<IEnumerable<SrvProduct>>(mdlProducts);

            _logger.LogInformation("End GerOrders");

            return srvProducts;
        }

        public async Task InsertProductsAsync(IEnumerable<SrvProduct> srvProducts, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start InsertProducts");

            IEnumerable<MdlProduct> mdlProducts = _mapper.Map<IEnumerable<MdlProduct>>(srvProducts);
            mdlProducts.ToList().ForEach(x => x.IsActive = true);

            await _productRepository.BulkInsertAsync(mdlProducts, cancellationToken);

            _logger.LogInformation("End InsertProducts");
        }
    }
}
