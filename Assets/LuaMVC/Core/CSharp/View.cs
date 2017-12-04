namespace PureMVC.Core
{
    using Patterns;
    using System.Collections.Generic;

    public interface IView
    {
        bool HasMediator(string mediatorName);
        void RegisterMediator(IMediator mediator);
        IMediator RemoveMediator(string mediatorName);
        IMediator RetrieveMediator(string mediatorName);
        void RegisterObserver(string notificationName, IObserver observer);
        void RemoveObserver(string notificationName, IObserver observer);
    }

    public class View : IView
    {
        protected static volatile IView m_instance;
        protected IDictionary<string, IMediator> m_mediatorMap = new Dictionary<string, IMediator>();
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();
        public static IView Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new View();
                        }
                    }
                }
                return m_instance;
            }
        }

        protected View()
        {
            this.InitializeView();
        }
        protected virtual void InitializeView()
        {
        }
        public virtual bool HasMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_mediatorMap.ContainsKey(mediatorName);
            }
        }
        public virtual void RegisterMediator(IMediator mediator)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_mediatorMap.ContainsKey(mediator.MediatorName))
                {
                    return;
                }
                this.m_mediatorMap[mediator.MediatorName] = mediator;
                IList<string> list = mediator.ListNotificationInterests();
                if (list.Count > 0)
                {
                    IObserver observer = new Observer(mediator.HandleNotification);
                    for (int i = 0; i < list.Count; i++)
                        RegisterObserver(list[i], observer);
                }
            }
            mediator.OnRegister();
        }
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            IMediator notifyContext = null;
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                notifyContext = this.m_mediatorMap[mediatorName];
                IList<string> list = notifyContext.ListNotificationInterests();
                for (int i = 0; i < list.Count; i++)
                    RemoveObserver(list[i], new Observer(notifyContext.HandleNotification));
                this.m_mediatorMap.Remove(mediatorName);
            }
            if (notifyContext != null)
                notifyContext.OnRemove();
            return notifyContext;
        }
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                return this.m_mediatorMap[mediatorName];
            }
        }

        public virtual void RegisterObserver(string notificationName, IObserver observer)
        {
            Observers.Instance.RegisterObserver(notificationName, observer);
        }
        public void RemoveObserver(string notificationName, IObserver observer)
        {
            Observers.Instance.RemoveObserver(notificationName, observer);
        }
    }
}