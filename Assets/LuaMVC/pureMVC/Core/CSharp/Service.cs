
using System.Collections.Generic;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    public interface IService
    {
        void RegisterHandler(IHandler handler);
        IHandler RemoveHandler(string handlerName);
        IHandler RetrieveHandler(string handlerName);
        bool HasHandler(string handlerName);
        void RegisterObserver(string notificationName, IObserver observer);
        void RemoveObserver(string notificationName, IObserver observer);
    }

    public class Service : IService
    {
        private static IService m_instance = null;
        private IDictionary<string, IHandler> m_handlers = new Dictionary<string, IHandler>();
        private static readonly object m_staticSyncRoot = new object();
        private readonly object m_syncRoot = new object();
        public static IService Instance
        {
            get
            {
                if (null == m_instance)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (null == m_instance)
                        {
                            m_instance = new Service();
                        }
                    }
                }
                return m_instance;
            }
        }

        public Service()
        {
            this.InitializeService();
        }
        protected virtual void InitializeService()
        { 

        }
        public void RegisterHandler(IHandler handler)
        {
            lock (this.m_syncRoot)
            {
                if (m_handlers.ContainsKey(handler.HandlerName))
                {
                    return;
                }
                this.m_handlers[handler.HandlerName] = handler;
                var handlers = handler.HandleNotification();
                if (handlers.Count > 0)
                {
                    IObserver observer = new Observer(handler.Request);
                    for (int i = 0; i < handlers.Count; i++)
                        RegisterObserver(handlers[i], observer);
                }
            }
            handler.OnRegister();
        }
        public IHandler RemoveHandler(string handlerName)
        {
            IHandler handler = null;
            lock (m_syncRoot)
            {
                if (!this.m_handlers.ContainsKey(handlerName))
                    return null;
                handler = m_handlers[handlerName];
                RemoveObserver(handlerName,new Observer(handler.Request));
                this.m_handlers.Remove(handlerName);
            }
            if (null != handler)
                handler.OnRemove();
            return handler;
        }
        public IHandler RetrieveHandler(string handlerName)
        {
            lock (m_syncRoot)
            {
                if (!m_handlers.ContainsKey(handlerName))
                    return null;
                return m_handlers[handlerName];
            }
        }
        public bool HasHandler(string handlerName)
        {
            if (null == RetrieveHandler(handlerName))
                return false;
            return true;
        }

        public void RegisterObserver(string notificationName, IObserver observer)
        {
            Observers.Instance.RegisterObserver(notificationName, observer);
        }
        public void RemoveObserver(string notificationName, IObserver observer)
        {
            Observers.Instance.RemoveObserver(notificationName, observer);
        }
    }
}