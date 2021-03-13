using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.ShoppingLists;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Buyers
{
    public interface IReadOnlyBuyer
    {
        Guid Id { get; }

        string Name { get; }

        IReadOnlyShoppingList ShoppingList { get; }

        IReadOnlyBasket Basket { get; }
    }
}
