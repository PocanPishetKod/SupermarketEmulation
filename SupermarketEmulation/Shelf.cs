using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SupermarketEmulation
{
    public class Shelf : WaitingObject
    {
        private readonly object _locker;
        private int _takersCount;

        public string ProductName { get; private set; }

        public int TakedProducts { get; private set; }

        public Supermarket Supermarket { get; private set; }

        public Shelf(string productName, Supermarket supermarket)
        {
            _locker = new object();
            TakedProducts = 0;
            _takersCount = 0;
            ProductName = productName;
            Supermarket = supermarket;
        }

        public void TakeProduct(int count, Buyer buyer)
        {
            lock (_locker)
            {
                if (_takersCount == 2)
                {
                    return;
                }

                _takersCount++;
                TakedProducts += count;
            }

            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine($"Покупатель {buyer.Name} взял с полки товар {ProductName}. {DateTime.Now}");
            }

            lock (_locker)
            {
                _takersCount--;
            }
        }

        public override bool IsAvialable()
        {
            return _takersCount < 2;
        }
    }
}
