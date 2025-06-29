using Application.Interfaces;
using OrderStockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _catalog = new();
        private readonly IRomanConverterService _romanService;

        public ProductService(IRomanConverterService romanService)
        {
            _romanService = romanService;
        }

        public void AddProduct(Product product)
        {
            _catalog.Add(product);
        }

        public List<Product> GetAll()
        {
            return _catalog.ToList();
        }

        public IEnumerable<string> GetLowStockProducts()
        {
            var result = new List<string>();

            var lowStockProducts = LowStockProducts();

            foreach (var product in lowStockProducts)
            {
                string romanStock = _romanService.ToRoman(product.Stock);

                result.Add($"Product: {product.Name}, Stock: {romanStock} Adet");
            }

            return result;
        }

        public IEnumerable<Product> LowStockProducts()
        {
            return _catalog.Where(p => p.Stock < p.Threshold).ToList();
        }
    }
}