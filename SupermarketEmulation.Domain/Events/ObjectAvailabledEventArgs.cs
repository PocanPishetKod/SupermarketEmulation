using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Events
{
    public class ObjectAvailabledEventArgs : EventArgs
    {
        public Guid BuyerId { get; private set; }

        public ObjectAvailabledEventArgs(Guid buyerId)
        {
            BuyerId = buyerId;
        }
    }
}
