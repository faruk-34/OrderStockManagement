
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderStockManagement.Domain.Entities;

namespace OrderStockManagement.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return Ok(product);
        }
        [HttpGet("low-stock")]
        public IActionResult GetLowStock() => Ok(_productService.GetLowStockProducts());

        [HttpGet]
        public IActionResult GetAll() => Ok(_productService.GetAll());
    }
}
