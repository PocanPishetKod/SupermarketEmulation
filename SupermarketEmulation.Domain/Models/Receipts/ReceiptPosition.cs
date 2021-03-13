using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Receipts
{
    /// <summary>
    /// Позиция чека
    /// </summary>
    public class ReceiptPosition
    {
        public ProductSpecification ProductSpecification { get; private set; }

        public int Count { get; set; }

        public ReceiptPosition(ProductSpecification productSpecification, int count)
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
    }
}
