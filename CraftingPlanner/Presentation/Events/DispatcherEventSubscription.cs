using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CraftingPlanner.Presentation.Events
{
    public class DispatcherEventSubscription : EventSubscription
    {
        private readonly SynchronizationContext m_syncContext;

        public DispatcherEventSubscription(IDelegateReference actionReference, SynchronizationContext context)
            : base(actionReference)
        {
            m_syncContext = context;
        }

        public override void InvokeAction(Action action)
        {
            m_syncContext.Post((o) => action(), null);
        }
    }

    public class DispatcherEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        private readonly SynchronizationContext m_syncContext;

        public DispatcherEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference, SynchronizationContext context)
            : base(actionReference, filterReference)
        {
            m_syncContext = context;
        }

        public override void InvokeAction(Action<TPayload?> action, TPayload? argument)
        {
            m_syncContext.Post((x) =>
            {
                TPayload? payload = x == null ? default : (TPayload)x;
                action(payload);
            }, argument);
        }
    }
}
