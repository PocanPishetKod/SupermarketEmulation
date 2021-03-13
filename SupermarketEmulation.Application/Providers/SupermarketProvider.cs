using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Providers
{
    public class SupermarketProvider : ISupermarketProvider
    {
        public Supermarket Provide(IReadOnlyCollection<ProductSpecification> productSpecifications, int cashboxesCount)
        {
            if (productSpecifications == null)
            {
                throw new ArgumentNullException(nameof(productSpecifications));
            }

            var result = new Supermarket();

            var index = 0;
            foreach (var spec in productSpecifications)
            {
                result.AddToAssortment(spec);
                var shelf = result.CreateShelf(spec, $"Полка_{index + 1}");
                shelf.ProductTaked += EventPrinter.PrintProductTaked;

                for (int i = 0; i < 5000;i++)
                {
                    result.AddToShelf(shelf.Id, new Product(spec));
                }

                index++;
            }

            for (int i = 0; i < cashboxesCount; i++)
            {
                var cashbox = result.CreateCashbox($"Касса_{i + 1}");
                cashbox.Scanned += EventPrinter.PrintProductScanned;
            }

            return result;
        }
    }
}
