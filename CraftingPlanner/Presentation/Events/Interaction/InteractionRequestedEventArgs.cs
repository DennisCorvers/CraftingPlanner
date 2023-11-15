using System;

namespace CraftingPlanner.Presentation.Events.Interaction
{
    public class InteractionRequestedEventArgs : EventArgs
    {
        public INotification Context { get; }
        public Action? Callback { get; }

        public InteractionRequestedEventArgs(INotification context, Action? callback)
        {
            this.Context = context;
            this.Callback = callback;
        }
    }
}
