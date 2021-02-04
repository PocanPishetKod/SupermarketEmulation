using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation
{
    public class ShoppingList
    {
        public ICollection<ShoppingListItem> ShoppingListItems { get; private set; }

        public ShoppingList(IReadOnlyCollection<ShoppingListItem> items)
        {
            ShoppingListItems = items.ToList();
        }
    }
}
