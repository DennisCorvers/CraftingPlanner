using System;
using System.Reflection;

namespace CraftingPlanner.Presentation.Events
{
    public class DelegateReference : IDelegateReference
    {
        private readonly Delegate? m_delegate;
        private readonly WeakReference? m_weakReference;
        private readonly MethodInfo? m_method;
        private readonly Type? m_delegateType;

        public Delegate? Target
        {
            get
            {
                if (m_delegate != null)
                {
                    return m_delegate;
                }
                else
                {
                    return TryGetDelegate();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateReference"/>.
        /// </summary>
        /// <param name="delegate">The original <see cref="Delegate"/> to create a reference for.</param>
        /// <param name="keepReferenceAlive">If <see langword="false" /> the class will create a weak reference to the delegate, allowing it to be garbage collected. Otherwise it will keep a strong reference to the target.</param>
        /// <exception cref="ArgumentNullException">If the passed <paramref name="delegate"/> is not assignable to <see cref="Delegate"/>.</exception>
        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null)
                throw new ArgumentNullException("delegate");

            if (keepReferenceAlive)
            {
                m_delegate = @delegate;
            }
            else
            {
                m_weakReference = new WeakReference(@delegate.Target);
                m_method = @delegate.GetMethodInfo();
                m_delegateType = @delegate.GetType();
            }
        }

        public bool TargetEquals(Delegate myDelegate)
        {
            if (m_delegate != null)
            {
                return m_delegate == myDelegate;
            }
            if (myDelegate == null)
            {
                return !m_method!.IsStatic && !m_weakReference!.IsAlive;
            }
            return m_weakReference!.Target == myDelegate.Target && Equals(m_method, myDelegate.GetMethodInfo());
        }

        private Delegate? TryGetDelegate()
        {
            if (m_method?.IsStatic == true)
            {
                return m_method.CreateDelegate(m_delegateType!, null);
            }
            var target = m_weakReference?.Target;
            if (target != null)
            {
                return m_method?.CreateDelegate(m_delegateType!, target);
            }
            return null;
        }
    }
}
