using SupermarketEmulation.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application
{
    public static class EventPrinter
    {
        public static void PrintProductScanned(object sender, CashboxScanEventArgs e)
        {
            Console.WriteLine($"Просканировано. Касса: {e.Cashbox.Name}, Продукт: {e.Product.ProductSpecification.Name}, Покупатель: {e.Buyer.Name}, Осталось просканировать: {e.RemainingAmount}");
        }

        public static void PrintProductTaked(object sender, ShelfTakedProductEventArgs e)
        {
            Console.WriteLine($"Продукт взят. Полка: {e.Shelf.Name}, Продукт: {e.Product.ProductSpecification.Name}, Покупатель: {e.Buyer.Name}, Осталось взять: {e.RemainingAmount}");
        }
    }
}
