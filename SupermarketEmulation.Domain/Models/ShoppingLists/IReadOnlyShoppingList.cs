using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.ShoppingLists
{
    public interface IReadOnlyShoppingList
    {
        bool IsCompleted { get; }

        IReadOnlyCollection<IReadOnlyShoppingListPosition> Positions { get; }

        IReadOnlyCollection<IReadOnlyShoppingListPosition> NonCompletedPositions { get; }

        int GetProductsCount(ProductSpecification productSpecification);
    }
}
