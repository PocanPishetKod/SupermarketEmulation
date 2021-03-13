using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.ShoppingLists
{
    /// <summary>
    /// Позиция списка покупок
    /// </summary>
    public class ShoppingListPosition : IReadOnlyShoppingListPosition
    {
        public ProductSpecification ProductSpecification { get; private set; }

        public int Count { get; private set; }

        public bool IsCompleted { get; private set; }

        public ShoppingListPosition(ProductSpecification productSpecification, int count)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            ProductSpecification = productSpecification;
            Count = count;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}
