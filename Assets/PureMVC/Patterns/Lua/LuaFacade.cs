
namespace PureMVC.Patterns.Lua
{
    using Core.Lua;
    using XLua;

    public interface ILuaFacade 
    {
        void InitializeLuaFacade();
        // luaObserver
        void NotifyObservers(INotification notification);
        // luaMediator 
        void InitializeLuaMediator();
        void RegisterLuaMediator(ILuaMediator luaMediator);
        void RemoveLuaMediator(string luaMediatorName);
        ILuaMediator RetrieveLuaMediator(string luaMediatorName);
        bool HasLuaMediator(string luaMediatorName);
        // luaCommand
        void InitializeLuaCommand();
        void RegisterLuaCommand(ILuaCommnad luaCommand);
        void RemoveLuaCommand(string luaCommandName);
        bool HasLuaCommand(string luaCommand);
        // luaProxy
        void InitializeLuaProxy();
        void RegisterLuaProxy(ILuaProxy luaProxy);
        void RemoveLuaProxy(string luaProxyName);
        ILuaProxy RetrieveLuaProxy(string luaProxyName);
        bool HasLuaProxy(string luaProxyName);
        // luaHandler
        void InitializeLuaHandler();
        void RegisterLuaHandler(ILuaHandler luaHandler);
        void RemoveLuaHandler(string luaHandlerName);
        ILuaHandler RetrieveLuaHandler(string luaHandlerName);
        bool HasLuaHandler(string luaHandlerName);
    }
    
    [LuaCallCSharp]
    public class LuaFacade : ILuaFacade
    {
        private static volatile ILuaFacade m_luaFacade = null;
        private static object m_syncStaticRoot = new object();
        protected ILuaView m_luaView = null;
        protected ILuaModel m_luaModel = null;
        protected ILuaController m_luaController = null;
        protected ILuaService m_luaService = null;
        
        public static ILuaFacade Instance 
        {
            get
            {
                if (null == m_luaFacade)
                {
                    lock (  m_syncStaticRoot)
                    {
                        m_luaFacade = new LuaFacade();
                    }
                }
                return m_luaFacade;
            }
        }
        protected LuaFacade()
        {
            InitializeLuaFacade();
        }
        public virtual void InitializeLuaFacade()
        {
            InitializeLuaMediator();
            InitializeLuaProxy();
            InitializeLuaCommand();
            InitializeLuaHandler();
        }
        public void NotifyObservers(INotification notification)
        {
            Observers.Instance.NotifyObservers(notification);
        }

        #region SendNotification

        public static void SendNotification(string notificationName)
        {
            Facade.Instance.NotifyObservers(new Notification(notificationName));
        }
        public static void SendNotification(string notificationName, object body)
        {
            Facade.Instance.NotifyObservers(new Notification(notificationName, body));
        }
        public static void SendNotification(string notificationName, object body, string type)
        {
            Facade.Instance.NotifyObservers(new Notification(notificationName, body, type));
        }

        #endregion

        #region LuaMediator
        public virtual void InitializeLuaMediator()
        {
            m_luaView = LuaView.Instance;
        }
        public void RegisterLuaMediator(ILuaMediator luaMediator)
        {
            m_luaView.RegisterMediator(luaMediator);
        }
        public void RemoveLuaMediator(string luaMediatorName)
        {
            m_luaView.RemoveMediator(luaMediatorName);
        }
        public ILuaMediator RetrieveLuaMediator(string luaMediatorName)
        {
            return m_luaView.RetrieveMediator(luaMediatorName);
        }
        public bool HasLuaMediator(string luaMediatorName)
        {
            return m_luaView.HasMediator(luaMediatorName);
        }
        #endregion

        #region LuaCommand
        public virtual void InitializeLuaCommand()
        {
            m_luaController = LuaController.Instance;
        }
        public void RegisterLuaCommand(ILuaCommnad luaCommand)
        {
            m_luaController.RegisterCommand(luaCommand);
        }
        public void RemoveLuaCommand(string luaCommandName)
        {
            m_luaController.RemoveCommand(luaCommandName);
        }
        public bool HasLuaCommand(string luaCommand)
        {
            return m_luaController.HasCommnad(luaCommand);
        }
        #endregion

        #region LuaProxy
        public virtual void InitializeLuaProxy()
        {
            m_luaModel = LuaModel.Instance;
        }
        public void RegisterLuaProxy(ILuaProxy luaProxy)
        {
            m_luaModel.RegisterProxy(luaProxy);
        }
        public void RemoveLuaProxy(string luaProxyName)
        {
            m_luaModel.RemoveProxy(luaProxyName);
        }
        public ILuaProxy RetrieveLuaProxy(string luaProxyName)
        {
            return m_luaModel.RetrieveProxy(luaProxyName);
        }
        public bool HasLuaProxy(string luaProxyName)
        {
            return m_luaModel.HasProxy(luaProxyName);
        }
        #endregion

        #region LuaHandler
        public virtual void InitializeLuaHandler()
        {
            m_luaService = LuaSerivce.Instance;
        }
        public void RegisterLuaHandler(ILuaHandler luaHandler)
        {
            m_luaService.RegisterHandler(luaHandler);
        }
        public void RemoveLuaHandler(string luaHandlerName)
        {
            m_luaService.RemoveHandler(luaHandlerName);
        }
        public ILuaHandler RetrieveLuaHandler(string luaHandlerName)
        {
            return m_luaService.RetrieveHandler(luaHandlerName);
        }
        public bool HasLuaHandler(string luaHandlerName)
        {
            return m_luaService.HasHandler(luaHandlerName);
        }
        #endregion
    }
}