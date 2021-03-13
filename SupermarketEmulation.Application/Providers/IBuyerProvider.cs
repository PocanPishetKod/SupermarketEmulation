using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Providers
{
    public interface IBuyerProvider
    {
        Buyer Provide(IReadOnlyAssortment assortment, string name);
    }
}
