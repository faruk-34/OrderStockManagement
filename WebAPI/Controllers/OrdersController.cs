

using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace StockManagement.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public OrdersController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        [HttpPost("check-and-place")]
        public async Task<IActionResult> CheckAndPlaceOrders()
        {
            var lowStock = _productService.LowStockProducts();
            var orders = await _orderService.CreateOrdersAsync(lowStock);
            return Ok(orders);
        }
    }
}
