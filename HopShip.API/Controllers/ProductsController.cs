using AutoMapper;
using HopShip.Data.DTO.Request;
using HopShip.Data.DTO.Service;
using HopShip.Service.Product;
using Microsoft.AspNetCore.Mvc;

namespace HopShip.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly ISrvProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IMapper mapper, ISrvProductService srvProductService)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = srvProductService;
        }

        [HttpGet("GetProducts", Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<SrvProduct>>> GetProductsAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<SrvProduct> srvProducts = await _productService.GetProductAsync(cancellationToken);

                return Ok(srvProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw ex;
            }
        }

        [HttpPost("InsertProducts", Name = "InsertProducts")]
        public async Task<ActionResult<bool>> InsertProductsAsync(IEnumerable<InsertProductRequest> orders, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<SrvProduct> srvProducts = _mapper.Map<IEnumerable<SrvProduct>>(orders);
                await _productService.InsertProductsAsync(srvProducts, cancellationToken);

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw ex;
            }
        }
    }
}
