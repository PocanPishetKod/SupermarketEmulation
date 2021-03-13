using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Exceptions;
using SupermarketEmulation.Domain.Models.Buyers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public abstract class QueuedObject<T> : IReadOnlyQueuedObject<T> where T : IQueuedObjectNotifier
    {
        private SpinLock _spinLock;
        protected readonly ConcurrentQueue<IReadOnlyBuyer> _buyers;
        protected readonly List<IReadOnlyBuyer> _readyForService;

        public event EventHandler<ObjectAvailabledEventArgs> Availabled;

        public T Object { get; private set; }

        public IReadOnlyCollection<IReadOnlyBuyer> Buyers => _buyers;

        public int QueueLength => _buyers.Count;

        public QueuedObject(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            
            Object = obj;
            _buyers = new ConcurrentQueue<IReadOnlyBuyer>();
            _readyForService = new List<IReadOnlyBuyer>();
            Object.Availabled += OnObjectAvailabled;
            _spinLock = new SpinLock();
        }

        private void Notify(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (!_buyers.TryDequeue(out var buyer))
                {
                    break;
                }

                _readyForService.Add(buyer);
                Availabled?.Invoke(this, new ObjectAvailabledEventArgs(buyer.Id));
            }
        }

        private void OnObjectAvailabled(object sender, EventArgs e)
        {
            Notify(1);
        }

        public void ToGetInLine(IReadOnlyBuyer buyer)
        {
            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer));
            }

            if (_buyers.Any(b => b.Id == buyer.Id))
            {
                throw new BuyerAlreadyInQueueException();
            }

            var lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);
                _buyers.Enqueue(buyer);

                if (Object.IsAvailable(out var count))
                {
                    Notify(count);
                }
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit(false);
                }
            }
        }
    }
}
