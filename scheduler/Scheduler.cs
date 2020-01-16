using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

using XGBoost;

namespace scheduler
{
    public class Scheduler
    {
        private Dictionary<Guid, Worker> _workers = new Dictionary<Guid, Worker>();

        private ConcurrentQueue<int> _pendingQueues = new ConcurrentQueue<int>();

        private ConcurrentDictionary<Guid, bool> _completedWork = new ConcurrentDictionary<Guid, bool>();

        public Scheduler()
        {
            var xgbClassifier = new XGBClassifier();

            foreach (var worker in _workers.Values)
            {
                worker.Initialize(xgbClassifier);

                worker.NotBusy += Worker_NotBusy;
            }
        }

        private void Worker_NotBusy(object sender, Guid e)
        {
            lock (_pendingQueues)
            {
                if (_pendingQueues.IsEmpty)
                {
                    return;
                }

                _pendingQueues.TryDequeue(out var res);

                _workers[e].Run(res);
            }
        }

        public bool Run(int x)
        {
            var id = Guid.NewGuid();

            while (!_completedWork.ContainsKey(id))
            {
                Thread.Sleep(50);
            }

            _completedWork.TryRemove(id, out var val);

            return val;
        }
    }
}