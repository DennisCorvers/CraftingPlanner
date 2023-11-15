using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingPlanner.Presentation.Events
{
    public class SubscriptionToken : IEquatable<SubscriptionToken>, IDisposable
    {
        private readonly Guid m_token;
        private Action<SubscriptionToken>? m_unsubscribeAction;

        /// <summary>
        /// Initializes a new instance of <see cref="SubscriptionToken"/>.
        /// </summary>
        public SubscriptionToken(Action<SubscriptionToken> unsubscribeAction)
        {
            m_unsubscribeAction = unsubscribeAction;
            m_token = Guid.NewGuid();
        }

        public bool Equals(SubscriptionToken? other)
        {
            if (other == null) return false;
            return Equals(m_token, other.m_token);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SubscriptionToken);
        }

        public override int GetHashCode()
        {
            return m_token.GetHashCode();
        }

        public virtual void Dispose()
        {
            if (m_unsubscribeAction != null)
            {
                m_unsubscribeAction(this);
                m_unsubscribeAction = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
