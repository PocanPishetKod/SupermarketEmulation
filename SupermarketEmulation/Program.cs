using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketEmulation
{
    class Program
    {
        private static Supermarket _supermarket;

        static void Main(string[] args)
        {
            _supermarket = new Supermarket();
            _supermarket.StartWorking();
            LaunchPeople(_supermarket);
            Console.ReadLine();
        }

        private static void LaunchPeople(Supermarket supermarket)
        {
            var random = new Random();
            var buyers = GenerateBuyers(random.Next(10, 31));
            foreach (var item in buyers)
            {
                supermarket.Enter(item);
                item.StartShopping(supermarket);
            }
        }

        private static IReadOnlyCollection<ShoppingListItem> ProvideShoppingItems()
        {
            var random = new Random();
            var result = new List<ShoppingListItem>();
            foreach (var item in ProductsProvider.Provide())
            {
                result.Add(new ShoppingListItem(new Product(item), random.Next(0, 11)));
            }

            return result.Where(x => x.Count > 0).ToList();
        }

        private static IReadOnlyCollection<Buyer> GenerateBuyers(int count)
        {
            var result = new List<Buyer>();

            for (int i = 0; i < count; i++)
            {
                result.Add(new Buyer(ProvideShoppingItems(), $"Buyer_{i + 1}"));
            }

            return result;
        }
    }
}
