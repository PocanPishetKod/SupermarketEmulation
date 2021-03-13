using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Products
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product
    {
        public Guid Id { get; private set; }

        public ProductSpecification ProductSpecification { get; private set; }

        public Product(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            ProductSpecification = productSpecification;
            Id = Guid.NewGuid();
        }
    }
}
