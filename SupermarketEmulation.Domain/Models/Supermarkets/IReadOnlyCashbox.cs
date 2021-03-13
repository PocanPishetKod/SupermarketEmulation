using SupermarketEmulation.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public interface IReadOnlyCashbox
    {
        Guid Id { get; }

        string Name { get; }

        Supermarket Supermarket { get; }

        event EventHandler<CashboxScanEventArgs> Scanned;
    }
}
