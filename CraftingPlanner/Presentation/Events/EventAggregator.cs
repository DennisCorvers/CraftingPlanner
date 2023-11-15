using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CraftingPlanner.Presentation.Events
{
    public class EventAggregator : IEventAggregator
    {
        private static IEventAggregator? m_current;

        /// <summary>
        /// Gets or Sets the Current Instance of the <see cref="IEventAggregator"/>
        /// </summary>
        public static IEventAggregator Current
        {
            get => m_current ??= new EventAggregator();
            set => m_current = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="EventAggregator"/>
        /// </summary>
        public EventAggregator()
        {
            if (m_current is null)
            {
                m_current = this;
            }
        }

        private readonly Dictionary<Type, EventBase> events = new Dictionary<Type, EventBase>();
        private readonly SynchronizationContext? syncContext = SynchronizationContext.Current;

        /// <summary>
        /// Gets the single instance of the event managed by this EventAggregator. Multiple calls to this method with the same <typeparamref name="TEventType"/> returns the same event instance.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to get. This must inherit from <see cref="EventBase"/>.</typeparam>
        /// <returns>A singleton instance of an event object of type <typeparamref name="TEventType"/>.</returns>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            lock (events)
            {
                EventBase? existingEvent = null;

                if (!events.TryGetValue(typeof(TEventType), out existingEvent))
                {
                    TEventType newEvent = new TEventType();
                    newEvent.SynchronizationContext = syncContext;
                    events[typeof(TEventType)] = newEvent;

                    return newEvent;
                }
                else
                {
                    return (TEventType)existingEvent;
                }
            }
        }
    }
}
