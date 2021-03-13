using SupermarketEmulation.Domain.Models.Buyers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public interface IReadOnlyQueuedObject<T>
    {
        T Object { get; }

        IReadOnlyCollection<IReadOnlyBuyer> Buyers { get; }

        public int QueueLength { get; }
    }
}
