
using PureMVC.Patterns.Lua;

namespace PureMVC.Patterns
{
    using Core;

    public interface IFacade : INotifier
    {
        void NotifyObservers(INotification note);
        // command
        bool HasCommand(string notificationName);
        void RegisterCommand(string notificationName, ICommand command);
        void RemoveCommand(string notificationName);
        // mediator
        bool HasMediator(string mediatorName);
        void RegisterMediator(IMediator mediator);
        IMediator RetrieveMediator(string mediatorName);
        IMediator RemoveMediator(string mediatorName);
        // proxy
        bool HasProxy(string proxyName);
        void RegisterProxy(IProxy proxy);
        IProxy RemoveProxy(string proxyName);
        IProxy RetrieveProxy(string proxyName);
        // handler
        bool HasHandler(string handlerName);
        void RegisterHandler(IHandler handler);
        IHandler RemoveHandler(string handlerName);
        IHandler RetrieveHandler(string handlerName);
    }

    public class Facade : IFacade
    {
        protected static volatile IFacade m_instance;
        protected static readonly object m_staticSyncRoot = new object();
        protected IController m_controller;
        protected IModel m_model;
        protected IView m_view;
        protected IService m_service;
        
        public static IFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new Facade();
                        }
                    }
                }
                return m_instance;
            }
        }
        protected Facade()
        {
            this.InitializeFacade();
        }
        protected virtual void InitializeFacade()
        {
            this.InitializeModel();
            this.InitializeController();
            this.InitializeView();
            this.InitializeService();
        }
        public void NotifyObservers(INotification notification)
        {
            Observers.Instance.NotifyObservers(notification);
        }

        #region Command || Mediator || Proxy || Handler

        // command
        protected virtual void InitializeController()
        {
            if (this.m_controller == null)
            {
                this.m_controller = Controller.Instance;
            }
        }
        public void RegisterCommand( string notificationName,ICommand command )
        {
            this.m_controller.RegisterCommand(notificationName, command);
        }

        public void RemoveCommand(string notificationName)
        {
            this.m_controller.RemoveCommand(notificationName);
        }
        public bool HasCommand(string notificationName)
        {
            return this.m_controller.HasCommand(notificationName);
        }

        // mediator
        protected virtual void InitializeView()
        {
            if (this.m_view == null)
            {
                this.m_view = View.Instance;
            }
        }
        public void RegisterMediator(IMediator mediator)
        {
            this.m_view.RegisterMediator(mediator);
        }
        public IMediator RemoveMediator(string mediatorName)
        {
            return this.m_view.RemoveMediator(mediatorName);
        }
        public IMediator RetrieveMediator(string mediatorName)
        {
            return this.m_view.RetrieveMediator(mediatorName);
        }
        public bool HasMediator(string mediatorName)
        {
            return this.m_view.HasMediator(mediatorName);
        }

        // proxy
        protected virtual void InitializeModel()
        {
            if (this.m_model == null)
            {
                this.m_model = Model.Instance;
            }
        }
        public void RegisterProxy(IProxy proxy)
        {
            this.m_model.RegisterProxy(proxy);
        }
        public IProxy RemoveProxy(string proxyName)
        {
            return this.m_model.RemoveProxy(proxyName);
        }
        public IProxy RetrieveProxy(string proxyName)
        {
            return this.m_model.RetrieveProxy(proxyName);
        }
        public bool HasProxy(string proxyName)
        {
            return this.m_model.HasProxy(proxyName);
        }

        // handler
        protected virtual void InitializeService()
        {
            if (this.m_service == null)
            {
                this.m_service = Service.Instance;
            }
        }
        public void RegisterHandler( IHandler handler )
        {
            this.m_service.RegisterHandler(handler);
        }
        public IHandler RemoveHandler( string handlerName )
        {
            return this.m_service.RemoveHandler(handlerName);
        }
        public IHandler RetrieveHandler( string handlerName )
        {
            return this.m_service.RetrieveHandler(handlerName);
        }
        public bool HasHandler( string handlerName )
        {
            return this.m_service.HasHandler(handlerName);
        }

        // notifier
        public void SendNotification(string notificationName)
        {
            this.NotifyObservers(new Notification(notificationName));
        }
        public void SendNotification(string notificationName, object body)
        {
            this.NotifyObservers(new Notification(notificationName, body));
        }
        public void SendNotification(string notificationName, object body, string type)
        {
            this.NotifyObservers(new Notification(notificationName, body, type));
        }

        #endregion
    }
}