using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    /// <summary>
    /// Ассортимент
    /// </summary>
    public class Assortment : IReadOnlyAssortment
    {
        private readonly List<ProductSpecification> _productSpecifications;
        public IReadOnlyCollection<ProductSpecification> ProductSpecifications => _productSpecifications;

        public Assortment()
        {
            _productSpecifications = new List<ProductSpecification>();
        }

        public void AddSpecification(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            _productSpecifications.Add(productSpecification);
        }

        public void RemoveSpecification(ProductSpecification productSpecification)
        {
            _productSpecifications.Remove(productSpecification);
        }

        public bool ContainsSpecification(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            return _productSpecifications.FirstOrDefault(ps => ps.Name == productSpecification.Name) != null;
        }
    }
}
