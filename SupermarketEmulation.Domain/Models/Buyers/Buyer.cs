using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.ShoppingLists;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Buyers
{
    /// <summary>
    /// Покупатель
    /// </summary>
    public class Buyer : IReadOnlyBuyer
    {
        private readonly ShoppingList _shoppingList;
        private readonly Basket _basket;

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyShoppingList ShoppingList => _shoppingList;

        public IReadOnlyBasket Basket => _basket;

        public Buyer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Id = Guid.NewGuid();
            _shoppingList = new ShoppingList();
            _basket = new Basket();
        }

        public void AddShoppingListPosition(ProductSpecification productSpecification, int count)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            _shoppingList.AddPosition(productSpecification, count);
        }

        public void AddToBasket(IReadOnlyCollection<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            foreach (var product in products)
            {
                _basket.AddProduct(product);
            }
        }

        public void CompletePosition(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            _shoppingList.CompletePosition(productSpecification);
        }
    }
}
