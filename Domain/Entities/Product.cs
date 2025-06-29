
namespace OrderStockManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Threshold { get; set; }
        public int Stock { get; set; }
   
    }
}
