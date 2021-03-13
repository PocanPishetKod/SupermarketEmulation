using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public interface IQueuedObjectNotifier
    {
        event EventHandler Availabled;

        bool IsAvailable(out int count);

        bool IsAvailable();
    }
}
