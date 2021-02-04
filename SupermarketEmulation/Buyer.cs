using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SupermarketEmulation
{
    public class Buyer
    {
        private Thread _shoppingThread;
        private Supermarket _supermarket;
        private readonly ManualResetEvent _manualResetEvent;

        public string Name { get; private set; }

        public ShoppingList ShoppingList { get; private set; }

        public IReadOnlyCollection<Product> TakedProducts => ShoppingList.ShoppingListItems.Where(x => x.IsCompleted).Select(x => x.Product).ToList();

        public Buyer(IReadOnlyCollection<ShoppingListItem> items, string name)
        {
            ShoppingList = new ShoppingList(items);
            Name = name;
            _manualResetEvent = new ManualResetEvent(false);
        }

        private void DoShopping()
        {
            foreach (var item in ShoppingList.ShoppingListItems)
            {
                var shelf = _supermarket.GetShelf(item.Product.Name);
                shelf.GetInLine(this);
                _manualResetEvent.Reset();
                _manualResetEvent.WaitOne();
                shelf.WaitingObject.TakeProduct(item.Count, this);
                item.Complete();
            }

            var cashbox = _supermarket.GetOptimalCashbox();
            _manualResetEvent.Reset();
            cashbox.GetInLine(this);
            Console.WriteLine($"Покупатель {Name} встал в очередь на кассу -------. {DateTime.Now}");
            _manualResetEvent.WaitOne();
            Console.WriteLine($"Покупатель {Name} начал обслуживание на кассе -----------. {DateTime.Now}");
            cashbox.WaitingObject.Buy(TakedProducts);
            Console.WriteLine($"Покупатель {Name} закончил покупки -------------. {DateTime.Now}");
        }

        public void StartShopping(Supermarket supermarket)
        {
            _supermarket = supermarket;
            _shoppingThread = new Thread(DoShopping);
            _shoppingThread.Start();
            Console.WriteLine($"Покупатель {Name} вошел в супермаркет. {DateTime.Now}");
        }

        public void OnQueueCompleted(Buyer buyer)
        {
            if (buyer == this)
            {
                _manualResetEvent.Set();
            }
        }
    }
}
