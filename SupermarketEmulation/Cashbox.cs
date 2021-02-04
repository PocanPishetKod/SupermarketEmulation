using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SupermarketEmulation
{
    public class Cashbox : WaitingObject
    {
        private readonly object _locker;
        private int _copacity;

        public int SoldProducts { get; private set; }

        public Supermarket Supermarket { get; private set; }

        public Cashbox(Supermarket supermarket)
        {
            _locker = new object();
            SoldProducts = 0;
            Supermarket = supermarket;
            _copacity = 0;
        }

        public void Buy(IReadOnlyCollection<Product> products)
        {
            if (!IsAvialable())
            {
                return;
            }

            lock (_locker)
            {
                _copacity++;
                foreach (var product in products)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }

                SoldProducts += products.Count;
            }
        }

        public override bool IsAvialable()
        {
            return _copacity == 0;
        }
    }
}
