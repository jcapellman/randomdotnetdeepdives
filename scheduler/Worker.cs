using System;
using XGBoost;

namespace scheduler
{
    public class Worker
    {
        private XGBClassifier _classifier;

        public Guid ID { get; set; }

        public void Initialize(XGBClassifier classifier)
        {
            _classifier = classifier;
            ID = Guid.NewGuid();
        }

        public bool Run(int x)
        {
            return x % 2 == 0;
        }

        
        public event EventHandler<Guid> NotBusy;
    }
}
