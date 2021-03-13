using SupermarketEmulation.Application.AI;
using SupermarketEmulation.Application.Algorithms;
using SupermarketEmulation.Application.Providers;
using SupermarketEmulation.Application.Repositories;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketEmulation
{
    class Program
    {
        private static Supermarket _supermarket;
        private static IBuyerProvider _buyerProvider;
        private static ISupermarketRepository _supermarketRepository;
        private static IShelfSelectionAlgorithm _shelfSelectionAlgorithm;
        private static ICashboxSelectionAlgorithm _cashboxSelectionAlgorithm;

        static void Main(string[] args)
        {
            _supermarketRepository = new SupermarketRepository();
            var supermarketProvider = new SupermarketProvider();
            var productSpecProvider = new ProductSpecificationsProvider();
            _buyerProvider = new BuyerProvider();
            _shelfSelectionAlgorithm = new ShelfSelectionAlgorithm();
            _cashboxSelectionAlgorithm = new CashboxSelectionAlgorithm();

            _supermarket = supermarketProvider.Provide(productSpecProvider.Provide(30), 5);
            _supermarketRepository.Save(_supermarket);

            EnterBuyers(10);
            Console.ReadLine();
        }

        private static void EnterBuyers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var buyer = _buyerProvider.Provide(_supermarket.Assortment, $"Покупатель_{i + 1}");
                var buyerAI = new BuyerAI(buyer, _supermarketRepository, _shelfSelectionAlgorithm, _cashboxSelectionAlgorithm);
                buyerAI.Start();
            }
        }
    }
}
