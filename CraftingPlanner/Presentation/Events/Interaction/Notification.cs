namespace CraftingPlanner.Presentation.Events.Interaction
{
    public interface INotification
    {
        string Title { get; }

        object? Content { get; }
    }

    public class Notification : INotification
    {
        public string Title { get; }

        public object? Content { get; }

        public Notification(string title, object? content)
        {
            Title = title;
            Content = content;
        }
    }
}
