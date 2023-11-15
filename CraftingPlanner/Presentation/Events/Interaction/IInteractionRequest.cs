using System;

namespace CraftingPlanner.Presentation.Events.Interaction
{
    public interface IInteractionRequest
    {
        event EventHandler<InteractionRequestedEventArgs> Raised;
    }
}
