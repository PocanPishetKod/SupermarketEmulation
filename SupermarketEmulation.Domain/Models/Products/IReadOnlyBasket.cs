using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Products
{
    public interface IReadOnlyBasket
    {
        IReadOnlyCollection<Product> Products { get; }
    }
}
