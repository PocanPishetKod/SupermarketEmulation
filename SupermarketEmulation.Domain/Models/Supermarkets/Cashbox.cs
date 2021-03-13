using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Exceptions;
using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Receipts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    /// <summary>
    /// Касса
    /// </summary>
    public class Cashbox : IReadOnlyCashbox, IQueuedObjectNotifier
    {
        private int _maxBuyersCount;
        private SemaphoreSlim _semaphoreSlim;

        public event EventHandler Availabled;
        public event EventHandler<CashboxScanEventArgs> Scanned;

        public Guid Id { get; private set; }

        public Supermarket Supermarket { get; private set; }

        public string Name { get; private set; }

        public Cashbox(Supermarket supermarket, string name)
        {
            if (supermarket == null)
            {
                throw new ArgumentNullException(nameof(supermarket));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Supermarket = supermarket;
            _maxBuyersCount = 1;
            Id = Guid.NewGuid();
            Name = name;
            _semaphoreSlim = new SemaphoreSlim(_maxBuyersCount, _maxBuyersCount);
        }

        private void OnScanned(IReadOnlyBuyer buyer, Product product, int remainingAmount)
        {
            Scanned?.Invoke(this, new CashboxScanEventArgs(buyer, this, product, remainingAmount));
        }

        private void PunchProduct(Product product)
        {
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        }

        private void MakePurchasesInternal(IReadOnlyBuyer buyer)
        {
            var remainingAmount = buyer.Basket.Products.Count;
            foreach (var product in buyer.Basket.Products)
            {
                PunchProduct(product);
                remainingAmount--;
                OnScanned(buyer, product, remainingAmount);
            }
        }

        public Receipt MakePurchases(IReadOnlyBuyer buyer)
        {
            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer));
            }

            if (!IsAvailable())
            {
                throw new CashboxIsBusyException();
            }

            _semaphoreSlim.Wait();

            MakePurchasesInternal(buyer);

            var result = new Receipt(Supermarket, buyer.Basket.Products);

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
