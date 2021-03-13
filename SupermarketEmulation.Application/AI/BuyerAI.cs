using SupermarketEmulation.Application.Algorithms;
using SupermarketEmulation.Application.Repositories;
using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupermarketEmulation.Application.AI
{
    public class BuyerAI : IQueuedObjectSubscriber
    {
        private readonly ISupermarketRepository _supermarketRepository;
        private readonly IShelfSelectionAlgorithm _shelfSelectionAlgorithm;
        private readonly ICashboxSelectionAlgorithm _cashboxSelectionAlgorithm;
        private Task _executionTask;
        private ManualResetEvent _manualResetEvent;

        public Buyer Buyer { get; private set; }

        public Supermarket Supermarket { get; private set; }

        public BuyerAI(Buyer buyer, ISupermarketRepository supermarketRepository, IShelfSelectionAlgorithm shelfSelectionAlgorithm,
            ICashboxSelectionAlgorithm cashboxSelectionAlgorithm)
        {
            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer));
            }

            if (supermarketRepository == null)
            {
                throw new ArgumentNullException(nameof(supermarketRepository));
            }

            if (shelfSelectionAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(shelfSelectionAlgorithm));
            }

            if (cashboxSelectionAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(cashboxSelectionAlgorithm));
            }

            Buyer = buyer;
            _supermarketRepository = supermarketRepository;
            _shelfSelectionAlgorithm = shelfSelectionAlgorithm;
            _cashboxSelectionAlgorithm = cashboxSelectionAlgorithm;
            _manualResetEvent = new ManualResetEvent(false);
        }

        private void ProcessTakeProducts()
        {
            while (!Buyer.ShoppingList.IsCompleted)
            {
                var shelf = _shelfSelectionAlgorithm.PickUp(Supermarket, Buyer.ShoppingList);
                _manualResetEvent.Reset();
                Supermarket.ToGetInLineToShelf(shelf.Id, Buyer.Id, this);
                _manualResetEvent.WaitOne();
                var products = Supermarket.TakeProductsFromShelf(shelf.Id, Buyer.Id, Buyer.ShoppingList.GetProductsCount(shelf.ProductSpecification), this);
                Buyer.AddToBasket(products);
                Buyer.CompletePosition(shelf.ProductSpecification);
            }
        }

        private void ProcessBuy()
        {
            var cashbox = _cashboxSelectionAlgorithm.PickUp(Supermarket);
            _manualResetEvent.Reset();
            Supermarket.ToGetInLineToCashbox(cashbox.Id, Buyer.Id, this);
            _manualResetEvent.WaitOne();
            var receipt = Supermarket.BuyProducts(cashbox.Id, Buyer.Id, this);
        }

        private void Process()
        {
            try
            {
                Supermarket = _supermarketRepository.Get();
                Supermarket.Enter(Buyer);

                ProcessTakeProducts();
                ProcessBuy();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка");
            }
        }

        public void Start()
        {
            if (_executionTask != null)
            {
                throw new InvalidOperationException();
            }

            _executionTask = Task.Factory.StartNew(Process);
        }

        public void OnQueuedObjectAvailabled(object sender, ObjectAvailabledEventArgs e)
        {
            if (e.BuyerId == Buyer.Id)
            {
                _manualResetEvent.Set();
            }
        }
    }
}
