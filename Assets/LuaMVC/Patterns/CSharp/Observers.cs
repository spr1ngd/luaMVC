
using System.Collections.Generic;
using XLua;

namespace PureMVC.Patterns
{
    [CSharpCallLua]
    public delegate void HandleNotification(INotification notification);

    public interface IObservers
    {
        void NotifyObservers(INotification notification);
        void RegisterObserver(string notificationName, IObserver observer);
        void RemoveObserver(string notificationName, IObserver observer);
    }

    public class Observers : IObservers
    {
        private readonly object m_syncRoot = new object();
        private readonly static object m_syncStaticRoot = new object();
        private static IObservers m_instance = null;
        public static IObservers Instance
        {
            get
            {
                if (null == m_instance)
                {
                    lock ( m_syncStaticRoot )
                    {
                        m_instance = new Observers();
                    }
                }
                return m_instance;
            }
        }
        private IDictionary<string,IList<IObserver>> m_observers = new Dictionary<string, IList<IObserver>>();

        public void NotifyObservers(INotification notification)
        {
            lock (m_syncRoot)
            {
                IList<IObserver> luaObservers = null;
                if (m_observers.TryGetValue(notification.Name, out luaObservers))
                {
                    for (int i = 0; i < luaObservers.Count; i++)
                        luaObservers[i].NotifyObserver(notification);
                }
            }
        }
        public void RegisterObserver(string notificationName, IObserver observer)
        {
            lock (m_syncRoot)
            {
                if (!m_observers.ContainsKey(notificationName))
                    m_observers[notificationName] = new List<IObserver>();
                m_observers[notificationName].Add(observer);
            }
        }
        public void RemoveObserver(string notificationName, IObserver observer)
        {
            lock (m_syncRoot)
            {
                if (m_observers.ContainsKey(notificationName))
                    m_observers[notificationName].Remove(observer);
            }
        }
    }
}