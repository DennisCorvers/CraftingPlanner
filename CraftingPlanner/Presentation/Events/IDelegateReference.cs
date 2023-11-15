using System;

namespace CraftingPlanner.Presentation.Events
{
    public interface IDelegateReference
    {
        Delegate? Target { get; }
    }
}