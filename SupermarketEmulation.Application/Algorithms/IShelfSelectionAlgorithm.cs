using SupermarketEmulation.Domain.Models.ShoppingLists;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Algorithms
{
    public interface IShelfSelectionAlgorithm
    {
        IReadOnlyShelf PickUp(Supermarket supermarket, IReadOnlyShoppingList shoppingList);
    }
}
