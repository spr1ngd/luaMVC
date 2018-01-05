
namespace PureMVC.Patterns
{
    public interface IObserver
    {
        HandleNotification InvokeMethod { get; set; }
        void NotifyObserver(INotification notification);
    }

    public class Observer : IObserver
    {
        public Observer( HandleNotification invokeMethod )
        {
            this.InvokeMethod = invokeMethod;
        }
        public HandleNotification InvokeMethod { get; set; }

        public void NotifyObserver(INotification notification)
        {
            if (null != InvokeMethod)
                InvokeMethod(notification);
        }
    }
}