
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Patterns.Lua;

namespace PureMVC.Core.Lua
{
    public interface ILuaService
    {
        void RegisterHandler(ILuaHandler luaHandler);
        ILuaHandler RetrieveHandler(string luaHandlerName);
        bool HasHandler(string luaHandlerName);
        void RemoveHandler(string luaHandlerName);
        void RegisterObserver(string notificationName, IObserver luaObserver);
    }
    
    public class LuaSerivce : ILuaService
    {
        private volatile static ILuaService m_instance = null;
        private static readonly object m_syncStaticRoot = new object();
        public static ILuaService Instance
        {
            get
            {
                if (null == m_instance)
                {
                    lock ( m_syncStaticRoot )
                    {
                        m_instance = new LuaSerivce();
                    }
                }
                return m_instance;
            }
        }
        private readonly object m_syncRoot = new object();
        private IDictionary<string, ILuaHandler> m_luaHandlers = new Dictionary<string, ILuaHandler>();

        public void RegisterHandler(ILuaHandler luaHandler)
        {
            lock (m_syncRoot)
            {
                if (m_luaHandlers.ContainsKey(luaHandler.NAME))
                    return;
                m_luaHandlers.Add(luaHandler.NAME, luaHandler);
                if (null != luaHandler.OnRegister)
                    luaHandler.OnRegister();
                if (luaHandler.ListNotificationInterests.Count > 0)
                {
                    for (int i = 0; i < luaHandler.ListNotificationInterests.Count; i++)
                        RegisterObserver(luaHandler.ListNotificationInterests[i], new Observer(luaHandler.Request));
                }
            }
        }
        public ILuaHandler RetrieveHandler(string luaHandlerName)
        {
            lock (m_syncRoot)
            {
                ILuaHandler luaHandler = null;
                if (m_luaHandlers.TryGetValue(luaHandlerName, out luaHandler)) { }
                return luaHandler;
            }
        }
        public bool HasHandler(string luaHandlerName)
        {
            lock (m_syncRoot)
            {
                if (m_luaHandlers.ContainsKey(luaHandlerName))
                    return true;
                return false;
            }
        }
        public void RemoveHandler(string luaHandlerName)
        {
            lock (m_syncRoot)
            {
                ILuaHandler luaHandler = null;
                if (m_luaHandlers.TryGetValue(luaHandlerName, out luaHandler))
                {
                    Observers.Instance.RemoveObserver(luaHandlerName,new Observer(luaHandler.Request));
                    if (null != luaHandler.OnRemove)
                        luaHandler.OnRemove();
                    m_luaHandlers.Remove(luaHandlerName);
                }
            }
        }
        public void RegisterObserver(string notificationName, IObserver luaObserver)
        {
            Observers.Instance.RegisterObserver(notificationName, luaObserver);
        }
    }
}