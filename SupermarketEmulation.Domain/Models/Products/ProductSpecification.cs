using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Products
{
    /// <summary>
    /// Спецификация продукта
    /// </summary>
    public class ProductSpecification
    {
        public string Name { get; private set; }

        public ProductSpecification(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}
