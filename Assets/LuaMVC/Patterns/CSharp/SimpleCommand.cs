namespace PureMVC.Patterns
{
    public interface ICommand
    {
        void Execute(INotification notification);
    }

    public class SimpleCommand : Notifier, ICommand
    {
        public virtual void Execute(INotification notification)
        {

        }
    }
}