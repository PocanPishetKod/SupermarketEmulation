using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SupermarketEmulation
{
    public class BuyersQueue
    {
        private readonly ConcurrentQueue<Buyer> _queue;
        private readonly WaitingObject _waitingObject;
        private readonly ManualResetEvent _manualResetEvent;
        private Thread _processThread;

        public event Action<Buyer> Dequeued;

        public int Size => _queue.Count;

        public BuyersQueue(WaitingObject waitingObject)
        {
            _queue = new ConcurrentQueue<Buyer>();
            _waitingObject = waitingObject;
            _manualResetEvent = new ManualResetEvent(false);
        }

        private void DoWork()
        {
            while (true)
            {
                _manualResetEvent.WaitOne();

                if (_waitingObject.IsAvialable())
                {
                    if (_queue.TryDequeue(out var buyer))
                    {
                        Dequeued?.Invoke(buyer);
                    }
                    else
                    {
                        _manualResetEvent.Reset();
                        _manualResetEvent.WaitOne();
                    }
                }
            }
        }

        public void GetInLine(Buyer buyer)
        {
            _queue.Enqueue(buyer);
            Dequeued += buyer.OnQueueCompleted;
            _manualResetEvent.Set();
        }

        public void StartWorking()
        {
            _processThread = new Thread(DoWork);
            _processThread.Start();
        }
    }
}
