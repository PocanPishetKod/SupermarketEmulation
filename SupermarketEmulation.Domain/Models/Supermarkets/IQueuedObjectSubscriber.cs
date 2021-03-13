using SupermarketEmulation.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public interface IQueuedObjectSubscriber
    {
        void OnQueuedObjectAvailabled(object sender, ObjectAvailabledEventArgs e);
    }
}
