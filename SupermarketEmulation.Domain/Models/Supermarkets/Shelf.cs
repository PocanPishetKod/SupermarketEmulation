using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Exceptions;
using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    /// <summary>
    /// Полка для продуктов
    /// </summary>
    public class Shelf : IReadOnlyShelf, IQueuedObjectNotifier
    {
        private int _maxBuyersCount;
        private ConcurrentBag<Product> _products;
        private SemaphoreSlim _semaphoreSlim;

        public event EventHandler Availabled;
        public event EventHandler<ShelfTakedProductEventArgs> ProductTaked;

        public Guid Id { get; private set; }

        public ProductSpecification ProductSpecification { get; private set; }

        public IReadOnlyCollection<Product> Products => _products;

        public string Name { get; private set; }

        public Shelf(ProductSpecification productSpecification, string name)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            ProductSpecification = productSpecification;
            _products = new ConcurrentBag<Product>();
            _maxBuyersCount = 2;
            Id = Guid.NewGuid();
            Name = name;
            _semaphoreSlim = new SemaphoreSlim(_maxBuyersCount, _maxBuyersCount);
        }

        private void OnProductTaked(IReadOnlyBuyer buyer, Product product, int remainingAmount)
        {
            ProductTaked?.Invoke(this, new ShelfTakedProductEventArgs(buyer, this, product, remainingAmount));
        }

        private List<Product> TakeProductInternal(int count, IReadOnlyBuyer buyer)
        {
            if (_products.IsEmpty)
            {
                throw new ShelfIsEmptyException();
            }

            var result = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                if (!_products.TryTake(out var product))
                {
                    return result;
                }

                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                result.Add(product);
                OnProductTaked(buyer, product, count - (i + 1));
            }

            return result;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (product.ProductSpecification.Name != ProductSpecification.Name)
            {
                throw new ProductSpecificationNotMatchingException(ProductSpecification.Name, product.ProductSpecification.Name);
            }

            _products.Add(product);
        }

        public IReadOnlyCollection<Product> TakeAnyProducts(int count, IReadOnlyBuyer buyer)
        {
            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer));
            }

            if (!IsAvailable())
            {
                throw new ShelfIsBusyException();
            }

            _semaphoreSlim.Wait();

            var result = TakeProductInternal(count, buyer);

            _semaphoreSlim.Release();

            Availabled?.Invoke(this, EventArgs.Empty);

            return result;
        }

        public bool IsAvailable(out int count)
        {
            count = _semaphoreSlim.CurrentCount;
            return count > 0;
        }

        public bool IsAvailable()
        {
            return _semaphoreSlim.CurrentCount > 0;
        }
    }
}
