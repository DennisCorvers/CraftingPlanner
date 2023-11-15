using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CraftingPlanner.Presentation.Events
{
    public class PubSubEvent : EventBase
    {
        public SubscriptionToken Subscribe(Action action)
            => Subscribe(action, ThreadOption.PublisherThread);

        public SubscriptionToken Subscribe(Action action, ThreadOption threadOption)
            => Subscribe(action, threadOption, false);

        public SubscriptionToken Subscribe(Action action, bool keepSubscriberReferenceAlive)
            => Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);

        public virtual SubscriptionToken Subscribe(Action action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);

            EventSubscription subscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    subscription = new EventSubscription(actionReference);
                    break;
                case ThreadOption.BackgroundThread:
                    subscription = new BackgroundEventSubscription(actionReference);
                    break;
                case ThreadOption.UIThread:
                    if (SynchronizationContext == null)
                        throw new InvalidOperationException("EventAggregator not constructed on UI thread");
                    subscription = new DispatcherEventSubscription(actionReference, SynchronizationContext);
                    break;
                default:
                    subscription = new EventSubscription(actionReference);
                    break;
            }

            return InternalSubscribe(subscription);
        }

        public virtual void Invoke()
            => InternalInvoke();

        public virtual void Unsubscribe(Action subscriber)
        {
            lock (Subscriptions)
            {
                var eventSubscription = Subscriptions.Cast<EventSubscription>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        public virtual bool Contains(Action subscriber)
        {
            var eventSubscription = default(EventSubscription);
            lock (Subscriptions)
            {
                eventSubscription = Subscriptions.Cast<EventSubscription>().FirstOrDefault(evt => evt.Action == subscriber);
            }
            return eventSubscription != null;
        }
    }

    /// <summary>
    /// Defines a class that manages publication and subscription to events.
    /// </summary>
    /// <typeparam name="TPayload">The type of message that will be passed to the subscribers.</typeparam>
    public class PubSubEvent<TPayload> : EventBase
    {
        public SubscriptionToken Subscribe(Action<TPayload> action)
            => Subscribe(action, ThreadOption.PublisherThread);

        public virtual SubscriptionToken Subscribe(Action<TPayload> action, Predicate<TPayload> filter)
            => Subscribe(action, ThreadOption.PublisherThread, false, filter);

        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption)
            => Subscribe(action, threadOption, false);

        public SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive)
            => Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);

        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
            => Subscribe(action, threadOption, keepSubscriberReferenceAlive, null);

        public virtual SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TPayload>? filter)
        {
            var actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            var filterReference = filter != null
                ? new DelegateReference(filter, keepSubscriberReferenceAlive)
                : (IDelegateReference)new DelegateReference(new Predicate<TPayload>(delegate { return true; }), true);

            EventSubscription<TPayload> subscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    subscription = new EventSubscription<TPayload>(actionReference, filterReference);
                    break;
                case ThreadOption.BackgroundThread:
                    subscription = new BackgroundEventSubscription<TPayload>(actionReference, filterReference);
                    break;
                case ThreadOption.UIThread:
                    if (SynchronizationContext == null)
                        throw new InvalidOperationException("EventAggregator not constructed on UI thread");
                    subscription = new DispatcherEventSubscription<TPayload>(actionReference, filterReference, SynchronizationContext);
                    break;
                default:
                    subscription = new EventSubscription<TPayload>(actionReference, filterReference);
                    break;
            }

            return InternalSubscribe(subscription);
        }

        public virtual void Invoke(TPayload? payload)
            => InternalInvoke(payload);

        public virtual void Unsubscribe(Action<TPayload> subscriber)
        {
            lock (Subscriptions)
            {
                var eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        public virtual bool Contains(Action<TPayload> subscriber)
        {
            var eventSubscription = default(IEventSubscription);
            lock (Subscriptions)
            {
                eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
            }
            return eventSubscription != null;
        }
    }
}
