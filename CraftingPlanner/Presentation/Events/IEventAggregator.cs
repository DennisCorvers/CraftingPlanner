using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingPlanner.Presentation.Events
{
    public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
    }
}
