using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Application.Algorithms
{
    public class CashboxSelectionAlgorithm : ICashboxSelectionAlgorithm
    {
        public IReadOnlyCashbox PickUp(Supermarket supermarket)
        {
            if (supermarket.Cashboxes.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return supermarket.Cashboxes.OrderBy(c => c.QueueLength).First().Object;
        }
    }
}
