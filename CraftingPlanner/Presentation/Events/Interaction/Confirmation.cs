namespace CraftingPlanner.Presentation.Events.Interaction
{
    public interface IConfirmation : INotification
    {
        bool Confirmed { get; }
    }

    public class Confirmation : IConfirmation
    {
        public bool Confirmed { get; }

        public string Title { get; }

        public object? Content { get; }

        public Confirmation(bool confirmed, string title, object? content)
        {
            Confirmed = confirmed;
            Title = title;
            Content = content;
        }
    }
}
