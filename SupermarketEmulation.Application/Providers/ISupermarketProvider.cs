using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Providers
{
    public interface ISupermarketProvider
    {
        Supermarket Provide(IReadOnlyCollection<ProductSpecification> productSpecifications, int cashboxesCount);
    }
}
