
namespace PureMVC.Patterns
{
    public interface INotifier
    {
        void SendNotification(string notificationName);
        void SendNotification(string notificationName, object body);
        void SendNotification(string notificationName, object body, string type);
    }

    public class Notifier : INotifier
    {
        private IFacade m_facade = Patterns.Facade.Instance;
        protected IFacade Facade
        {
            get { return this.m_facade; }
        }

        public void SendNotification(string notificationName)
        {
            this.m_facade.SendNotification(notificationName);
        }
        public void SendNotification(string notificationName, object body)
        {
            this.m_facade.SendNotification(notificationName, body);
        }
        public void SendNotification(string notificationName, object body, string type)
        {
            this.m_facade.SendNotification(notificationName, body, type);
        }
    }
}