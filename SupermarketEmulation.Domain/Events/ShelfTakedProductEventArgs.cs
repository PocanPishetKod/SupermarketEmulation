using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Events
{
    public class ShelfTakedProductEventArgs : EventArgs
    {
        public IReadOnlyBuyer Buyer { get; private set; }

        public IReadOnlyShelf Shelf { get; private set; }

        public Product Product { get; private set; }

        public int RemainingAmount { get; private set; }

        public ShelfTakedProductEventArgs(IReadOnlyBuyer buyer, IReadOnlyShelf shelf, Product product, int remainingAmount)
        {
            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer));
            }

            if (shelf == null)
            {
                throw new ArgumentNullException(nameof(shelf));
            }

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Buyer = buyer;
            Shelf = shelf;
            Product = product;
            RemainingAmount = remainingAmount;
        }
    }
}
