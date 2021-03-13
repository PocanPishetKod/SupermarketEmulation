using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Providers
{
    public class ProductSpecificationsProvider : IProductSpecificationsProvider
    {
        public IReadOnlyCollection<ProductSpecification> Provide(int count)
        {
            var result = new List<ProductSpecification>(count);
            for (int i = 0; i < count; i++)
            {
                result.Add(new ProductSpecification($"Product_{i + 1}"));
            }

            return result;
        }
    }
}
