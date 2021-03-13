using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Providers
{
    public interface IProductSpecificationsProvider
    {
        IReadOnlyCollection<ProductSpecification> Provide(int count);
    }
}
