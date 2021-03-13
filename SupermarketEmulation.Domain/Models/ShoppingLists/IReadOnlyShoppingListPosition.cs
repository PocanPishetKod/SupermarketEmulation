using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.ShoppingLists
{
    public interface IReadOnlyShoppingListPosition
    {
        ProductSpecification ProductSpecification { get; }

        int Count { get; }

        bool IsCompleted { get; }
    }
}
