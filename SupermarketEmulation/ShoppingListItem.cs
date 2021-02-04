using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation
{
    public class ShoppingListItem
    {
        public Product Product { get; private set; }

        public int Count { get; private set; }

        public bool IsCompleted { get; private set; }

        public ShoppingListItem(Product product, int count)
        {
            Product = product;
            Count = count;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}
