using System;

namespace CraftingPlanner.Presentation.Events
{
    public class EventSubscription : IEventSubscription
    {
        private readonly IDelegateReference m_actionReference;

        public SubscriptionToken? SubscriptionToken { get; set; }

        public Action? Action
        {
            get
            {
                return m_actionReference.Target == null
                    ? throw new ArgumentNullException()
                    : (Action)m_actionReference.Target;
            }
        }

        ///<summary>
        /// Creates a new instance of <see cref="EventSubscription"/>.
        ///</summary>
        ///<param name="actionReference">A reference to a delegate of type <see cref="System.Action"/>.</param>
        ///<exception cref="ArgumentNullException">When <paramref name="actionReference"/> or <see paramref="filterReference"/> are <see langword="null" />.</exception>
        ///<exception cref="ArgumentException">When the target of <paramref name="actionReference"/> is not of type <see cref="System.Action"/>.</exception>
        public EventSubscription(IDelegateReference actionReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException(nameof(actionReference));

            if (!(actionReference.Target is Action))
                throw new ArgumentException("Invalid delegate reference type.", nameof(actionReference));

            m_actionReference = actionReference;
        }

        public virtual Action<object?[]>? GetExecutionStrategy()
        {
            var action = Action;
            if (action != null)
            {
                return arguments =>
                {
                    InvokeAction(action);
                };
            }
            return null;
        }

        public virtual void InvokeAction(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            action();
        }
    }

    /// <summary>
    /// Provides a way to retrieve a <see cref="Delegate"/> to execute an action depending
    /// on the value of a second filter predicate that returns true if the action should execute.
    /// </summary>
    /// <typeparam name="TPayload">The type to use for the generic <see cref="System.Action{TPayload}"/> and <see cref="Predicate{TPayload}"/> types.</typeparam>
    public class EventSubscription<TPayload> : IEventSubscription
    {
        private readonly IDelegateReference m_actionReference;
        private readonly IDelegateReference m_filterReference;

        public Action<TPayload?> Action
        {
            get
            {
                return m_actionReference.Target == null
                    ? throw new ArgumentNullException()
                    : (Action<TPayload?>)m_actionReference.Target;
            }
        }

        public Predicate<TPayload?> Filter
        {
            get
            {
                return m_filterReference.Target == null
                    ? throw new ArgumentNullException()
                    : (Predicate<TPayload?>)m_filterReference.Target;
            }
        }

        public SubscriptionToken? SubscriptionToken { get; set; }

        ///<summary>
        /// Creates a new instance of <see cref="EventSubscription{TPayload}"/>.
        ///</summary>
        ///<param name="actionReference">A reference to a delegate of type <see cref="System.Action{TPayload}"/>.</param>
        ///<param name="filterReference">A reference to a delegate of type <see cref="Predicate{TPayload}"/>.</param>
        ///<exception cref="ArgumentNullException">When <paramref name="actionReference"/> or <see paramref="filterReference"/> are <see langword="null" />.</exception>
        ///<exception cref="ArgumentException">When the target of <paramref name="actionReference"/> is not of type <see cref="System.Action{TPayload}"/>,
        ///or the target of <paramref name="filterReference"/> is not of type <see cref="Predicate{TPayload}"/>.</exception>
        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException(nameof(actionReference));
            if (!(actionReference.Target is Action<TPayload>))
                throw new ArgumentException("Invalid delegate reference type.", nameof(actionReference));
            if (filterReference == null)
                throw new ArgumentNullException(nameof(filterReference));
            if (!(filterReference.Target is Predicate<TPayload>))
                throw new ArgumentException("Invalid delegate reference type.", nameof(actionReference));

            m_actionReference = actionReference;
            m_filterReference = filterReference;
        }

        public virtual Action<object?[]>? GetExecutionStrategy()
        {
            Action<TPayload?> action = Action;
            Predicate<TPayload?> filter = Filter;
            if (action != null && filter != null)
            {
                return arguments =>
                {
                    var argument = default(TPayload);
                    if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                    {
                        argument = (TPayload)arguments[0]!;
                    }
                    if (filter(argument))
                    {
                        InvokeAction(action, argument);
                    }
                };
            }
            return null;
        }

        public virtual void InvokeAction(Action<TPayload?> action, TPayload? argument)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            action(argument);
        }
    }
}
