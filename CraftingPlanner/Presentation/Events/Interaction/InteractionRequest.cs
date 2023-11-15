using System;

namespace CraftingPlanner.Presentation.Events.Interaction
{
    public class InteractionRequest<T> : IInteractionRequest
          where T : INotification
    {
        public event EventHandler<InteractionRequestedEventArgs>? Raised;

        public void Raise(T context)
        {
            this.Raise(context, c => { });
        }

        public void Raise(T context, Action<T> callback)
        {
            var handler = this.Raised;
            if (handler != null)
            {
                var interactionRequestArgs = new InteractionRequestedEventArgs(context, () =>
                {
                    callback?.Invoke(context);
                });

                handler(this, interactionRequestArgs);
            }
        }
    }
}
