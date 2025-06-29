using Application.Interfaces;
using OrderStockManagement.Domain.Entities; 
using System.Text.Json;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FakeApi");
        }
 
        public async Task<List<object>> CreateOrdersAsync(IEnumerable<Product> products)
        {
            var response = await _httpClient.GetAsync("/products");
            if (!response.IsSuccessStatusCode)
                return new();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // camelCase uyumu
            };

            var external = JsonSerializer.Deserialize<List<Order>>(content, options);

            return products.Select(product =>
            {
                var cheapest = external
                    .Where(x => x.Id.ToString() == product.ProductCode)
                    .OrderBy(x => x.Price)
                    .FirstOrDefault();

                return cheapest == null ? null : new
                {
                    Product = cheapest.Title,
                    OrderedQuantity = product.Threshold - product.Stock,
                    PricePerUnit = cheapest.Price
                };
            }).Where(x => x != null).Cast<object>().ToList();
        }

    }
}
