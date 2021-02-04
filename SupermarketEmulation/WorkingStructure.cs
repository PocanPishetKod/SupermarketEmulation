using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation
{
    public class WorkingStructure<T> where T : WaitingObject
    {
        public T WaitingObject { get; private set; }

        public BuyersQueue Queue { get; private set; }

        public WorkingStructure(T waitingObject, BuyersQueue buyersQueue)
        {
            WaitingObject = waitingObject;
            Queue = buyersQueue;
        }

        public void StartWorking()
        {
            Queue.StartWorking();
        }

        public void GetInLine(Buyer buyer)
        {
            Queue.GetInLine(buyer);
        }
    }
}
