using SupermarketEmulation.Domain.Models.ShoppingLists;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Application.Algorithms
{
    public class ShelfSelectionAlgorithm : IShelfSelectionAlgorithm
    {
        private IReadOnlyShelf FindOptimal(Supermarket supermarket, IReadOnlyShoppingList shoppingList, IReadOnlyCollection<IReadOnlyShelf> excluded = null)
        {
            var baseQuery = supermarket.Shelves
                .Where(s => shoppingList.NonCompletedPositions.Any(p => p.ProductSpecification.Name == s.Object.ProductSpecification.Name));

            if (excluded == null)
            {
                return baseQuery.OrderBy(s => s.QueueLength).First().Object;
            }
            
            return baseQuery.Where(s => !excluded.Any(e => e.ProductSpecification.Name == s.Object.ProductSpecification.Name))
                .OrderBy(s => s.QueueLength)
                .First().Object;
        }

        public IReadOnlyShelf PickUp(Supermarket supermarket, IReadOnlyShoppingList shoppingList)
        {
            if (supermarket.Shelves.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var result = FindOptimal(supermarket, shoppingList);
            if (shoppingList.Positions.Any(p => p.ProductSpecification.Name == result.ProductSpecification.Name))
            {
                return result;
            }

            var excluded = new List<IReadOnlyShelf>() { result };

            while (excluded.Count < shoppingList.Positions.Count)
            {
                result = FindOptimal(supermarket, shoppingList, excluded);
                if (shoppingList.Positions.Any(p => p.ProductSpecification.Name == result.ProductSpecification.Name))
                {
                    return result;
                }

                excluded.Add(result);
            }

            return null;
        }
    }
}
