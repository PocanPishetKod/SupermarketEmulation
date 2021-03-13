using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Exceptions;
using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Receipts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    /// <summary>
    /// Супермаркет
    /// </summary>
    public class Supermarket
    {
        private readonly List<QueuedShelf> _queuedShelves;
        private readonly List<QueuedCashbox> _queuedCashboxes;
        private readonly ConcurrentDictionary<Guid, Buyer> _buyers;
        private readonly Assortment _assortment;

        public IReadOnlyAssortment Assortment => _assortment;

        public IReadOnlyCollection<IReadOnlyQueuedObject<Shelf>> Shelves => _queuedShelves;

        public IReadOnlyCollection<IReadOnlyQueuedObject<Cashbox>> Cashboxes => _queuedCashboxes;

        public IReadOnlyCollection<IReadOnlyBuyer> Buyers => _buyers.Values.ToList();

        public Supermarket()
        {
            _queuedShelves = new List<QueuedShelf>();
            _queuedCashboxes = new List<QueuedCashbox>();
            _assortment = new Assortment();
            _buyers = new ConcurrentDictionary<Guid, Buyer>();
        }

        public IReadOnlyShelf CreateShelf(ProductSpecification productSpecification, string name)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!_assortment.ContainsSpecification(productSpecification))
            {
                throw new NoSuchAssortmentException();
            }

            var newShelf = new Shelf(productSpecification, name);
            _queuedShelves.Add(new QueuedShelf(newShelf));

            return newShelf;
        }

        public IReadOnlyCashbox CreateCashbox(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var newCashBox = new Cashbox(this, name);
            _queuedCashboxes.Add(new QueuedCashbox(newCashBox));

            return newCashBox;
        }

        public void AddToAssortment(ProductSpecification productSpecification)
        {
            if (productSpecification == null)
            {
                throw new ArgumentNullException(nameof(productSpecification));
            }

            if (_assortment.ContainsSpecification(productSpecification))
            {
                throw new ProdSpecExistsInAssortmentException();
            }

            _assortment.AddSpecification(productSpecification);
        }

        public void AddToShelf(Guid shelfId, Product product)
        {
            var shelf = _queuedShelves.FirstOrDefault(s => s.Object.Id == shelfId);
            if (shelf == null)
            {
                throw new NullReferenceException();
            }

            shelf.Object.AddProduct(product);
        }

        public IReadOnlyCollection<Product> TakeProductsFromShelf(Guid shelfId, Guid buyerId, int count, IQueuedObjectSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            if (!_buyers.TryGetValue(buyerId, out _))
            {
                throw new BuyerIsNotInSupermarketException();
            }
            
            var queuedShelf = _queuedShelves.FirstOrDefault(s => s.Object.Id == shelfId);
            if (queuedShelf == null)
            {
                throw new WorkerObjectNotFoundException();
            }

            queuedShelf.Availabled -= subscriber.OnQueuedObjectAvailabled;

            return queuedShelf.TakeAnyProducts(buyerId, count);
        }

        public Receipt BuyProducts(Guid cashboxId, Guid buyerId, IQueuedObjectSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            if (!_buyers.TryGetValue(buyerId, out var buyer))
            {
                throw new BuyerIsNotInSupermarketException();
            }

            var queuedCashbox = _queuedCashboxes.FirstOrDefault(c => c.Object.Id == cashboxId);
            if (queuedCashbox == null)
            {
                throw new WorkerObjectNotFoundException();
            }

            queuedCashbox.Availabled -= subscriber.OnQueuedObjectAvailabled;

            return queuedCashbox.MakePurchases(buyerId);
        }

        public void ToGetInLineToShelf(Guid shelfId, Guid buyerId, IQueuedObjectSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            if (!_buyers.TryGetValue(buyerId, out var buyer))
            {
                throw new BuyerIsNotInSupermarketException();
            }

            var queuedShelf = _queuedShelves.FirstOrDefault(s => s.Object.Id == shelfId);
            if (queuedShelf == null)
            {
                throw new WorkerObjectNotFoundException();
            }

            queuedShelf.Availabled += subscriber.OnQueuedObjectAvailabled;
            queuedShelf.ToGetInLine(buyer);
        }

        public void ToGetInLineToCashbox(Guid cashboxId, Guid buyerId, IQueuedObjectSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            if (!_buyers.TryGetValue(buyerId, out var buyer))
            {
                throw new BuyerIsNotInSupermarketException();
            }

            var queuedCashbox = _queuedCashboxes.FirstOrDefault(s => s.Object.Id == cashboxId);
            if (queuedCashbox == null)
            {
                throw new WorkerObjectNotFoundException();
            }

            queuedCashbox.Availabled += subscriber.OnQueuedObjectAvailabled;
            queuedCashbox.ToGetInLine(buyer);
        }

        public void Enter(Buyer buyer)
        {
            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer));
            }

            if (_buyers.TryGetValue(buyer.Id, out _))
            {
                throw new BuyerAlreadyEnterException();
            }

            _buyers.TryAdd(buyer.Id, buyer);
        }

        public void Exit(Guid buyerId)
        {
            if (!_buyers.TryGetValue(buyerId, out _))
            {
                throw new BuyerIsNotInSupermarketException();
            }

            _buyers.TryRemove(buyerId, out _);
        }
    }
}
