﻿using OrderStockManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<object>> CreateOrdersAsync(IEnumerable<Product> products);
    }
}
