using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public interface IReadOnlyAssortment
    {
        IReadOnlyCollection<ProductSpecification> ProductSpecifications { get; }
    }
}
