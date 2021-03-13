using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Application.Providers
{
    public class BuyerProvider : IBuyerProvider
    {
        public Buyer Provide(IReadOnlyAssortment assortment, string name)
        {
            if (assortment == null)
            {
                throw new ArgumentNullException(nameof(assortment));
            }

            var result = new Buyer(name);

            var random = new Random();

            var shoppingListLength = random.Next(0, 31);
            var startSpecIndex = random.Next(0, assortment.ProductSpecifications.Count - shoppingListLength);
            foreach (var spec in assortment.ProductSpecifications.Skip(startSpecIndex))
            {
                result.AddShoppingListPosition(spec, random.Next(1, 11));
            }

            return result;
        }
    }
}
