using HopShip.Data.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace HopShip.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "InsertOrders")]
        public async Task<ActionResult<bool>> InsertOrders(IEnumerable<InsertOrderRequest> orders)
        {
            try
            {
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw ex;
            }
        }
    }
}
