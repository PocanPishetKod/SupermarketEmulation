using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Products
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class Basket : IReadOnlyBasket
    {
        private readonly List<Product> _products;

        public IReadOnlyCollection<Product> Products => _products;

        public Basket()
        {
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _products.Remove(product);
        }
    }
}
