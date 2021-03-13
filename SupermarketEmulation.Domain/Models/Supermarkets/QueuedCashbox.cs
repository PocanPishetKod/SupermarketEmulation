using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Exceptions;
using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public class QueuedCashbox : QueuedObject<Cashbox>
    {
        public QueuedCashbox(Cashbox cashbox) : base(cashbox) { }

        public Receipt MakePurchases(Guid buyerId)
        {
            var buyer = _readyForService.FirstOrDefault(b => b.Id == buyerId);
            if (buyer == null)
            {
                throw new ActionNotAvailableForBuyerException();
            }

            _readyForService.Remove(buyer);

            return Object.MakePurchases(buyer);
        }
    }
}
