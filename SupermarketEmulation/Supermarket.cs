using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SupermarketEmulation
{
    public class Supermarket
    {
        private readonly List<WorkingStructure<Shelf>> _shelfs;
        private readonly List<WorkingStructure<Cashbox>> _cashboxes;

        public Supermarket()
        {
            _shelfs = new List<WorkingStructure<Shelf>>();
            _cashboxes = new List<WorkingStructure<Cashbox>>();

            Initialize();
        }

        private void Initialize()
        {
            foreach (var item in ProductsProvider.Provide())
            {
                var shelf = new Shelf(item, this);
                _shelfs.Add(new WorkingStructure<Shelf>(shelf, new BuyersQueue(shelf)));
            }

            for (int i = 0; i < 5; i++)
            {
                var cashbox = new Cashbox(this);
                _cashboxes.Add(new WorkingStructure<Cashbox>(cashbox, new BuyersQueue(cashbox)));
            }
        }

        public void StartWorking()
        {
            foreach (var item in _shelfs)
            {
                item.StartWorking();
            }

            foreach (var item in _cashboxes)
            {
                item.StartWorking();
            }
        }

        public void Enter(Buyer buyer)
        {

        }

        public WorkingStructure<Shelf> GetShelf(string productName)
        {
            return _shelfs.First(x => x.WaitingObject.ProductName.Equals(productName, StringComparison.CurrentCultureIgnoreCase));
        }

        public WorkingStructure<Cashbox> GetOptimalCashbox()
        {
            return _cashboxes.OrderBy(x => x.Queue.Size).First();
        }

        public WorkingStructure<Shelf> GetOptimalShelf()
        {
            return _shelfs.OrderBy(x => x.Queue.Size).First();
        }
    }
}
