using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Events
{
    public class CashboxScanEventArgs : EventArgs
    {
        public IReadOnlyBuyer Buyer { get; private set; }

        public IReadOnlyCashbox Cashbox { get; private set; }

        public Product Product { get; private set; }

        public int RemainingAmount { get; private set; }

        public CashboxScanEventArgs(IReadOnlyBuyer buyer, IReadOnlyCashbox cashbox, Product product, int remainingAmount)
        {
            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer));
            }

            if (cashbox == null)
            {
                throw new ArgumentNullException(nameof(cashbox));
            }

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Buyer = buyer;
            Cashbox = cashbox;
            Product = product;
            RemainingAmount = remainingAmount;
        }
    }
}
