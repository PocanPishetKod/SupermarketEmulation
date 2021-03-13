using SupermarketEmulation.Domain.Exceptions;
using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Domain.Models.ShoppingLists
{
    /// <summary>
    /// Список покупок
    /// </summary>
    public class ShoppingList : IReadOnlyShoppingList
    {
        private readonly List<ShoppingListPosition> _positions;

        public IReadOnlyCollection<IReadOnlyShoppingListPosition> Positions => _positions;

        public bool IsCompleted => _positions.Count == 0 ? true : _positions.All(p => p.IsCompleted);

        public IReadOnlyCollection<IReadOnlyShoppingListPosition> NonCompletedPositions => _positions.Where(p => !p.IsCompleted).ToList();

        public ShoppingList()
        {
            _positions = new List<ShoppingListPosition>();
        }

        public bool Contains(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            return _positions.FirstOrDefault(p => p.ProductSpecification.Name == productSpecification.Name) != null;
        }

        public void AddPosition(ProductSpecification productSpecification, int count)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            if (Contains(productSpecification))
            {
                throw new PositionAlreadyExistsException();
            }

            _positions.Add(new ShoppingListPosition(productSpecification, count));
        }

        public void RemovePosition(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            var position = _positions.FirstOrDefault(p => p.ProductSpecification.Name == productSpecification.Name);
            if (position == null)
            {
                throw new PositionNotFoundException();
            }

            _positions.Remove(position);
        }

        public void CompletePosition(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            var position = _positions.FirstOrDefault(p => p.ProductSpecification.Name == productSpecification.Name);
            if (position == null)
            {
                throw new PositionNotFoundException();
            }

            position.Complete();
        }

        public int GetProductsCount(ProductSpecification productSpecification)
        {
            return _positions.First(p => p.ProductSpecification.Name == productSpecification.Name).Count;
        }
    }
}
