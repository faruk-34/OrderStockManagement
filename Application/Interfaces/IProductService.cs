
using OrderStockManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface   IProductService
    {
        void AddProduct(Product product);
        IEnumerable<string> GetLowStockProducts();
        IEnumerable<Product> LowStockProducts();
        List<Product> GetAll();
    }
}
