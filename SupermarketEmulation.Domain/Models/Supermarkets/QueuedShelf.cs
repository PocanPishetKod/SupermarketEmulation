using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Exceptions;
using SupermarketEmulation.Domain.Models.Buyers;
using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public class QueuedShelf : QueuedObject<Shelf>
    {
        public QueuedShelf(Shelf shelf) : base(shelf) { }

        public IReadOnlyCollection<Product> TakeAnyProducts(Guid buyerId, int count)
        {
            var buyer = _readyForService.FirstOrDefault(b => b.Id == buyerId);
            if (buyer == null)
            {
                throw new ActionNotAvailableForBuyerException();
            }

            _readyForService.Remove(buyer);

            return Object.TakeAnyProducts(count, buyer);
        }
    }
}
