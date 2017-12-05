
using PureMVC.Patterns;

namespace PureMVC.Core.Lua
{
    using System.Collections.Generic;
    using Patterns.Lua;

    public interface ILuaView
    {
        bool HasMediator(string mediatorName);
        void RegisterMediator(ILuaMediator luaMediator);
        void RemoveMediator(string mediatorName);
        ILuaMediator RetrieveMediator(string mediatorName);
        void RegisterObserver(string notificationName, IObserver luaObserver);
        void RemoveObserver(string notificationName, IObserver luaObserver);
    }
    
    public class LuaView : ILuaView
    {
        private volatile static ILuaView m_instance;
        private static readonly object m_syncStaticRoot = new object();
        private readonly object m_syncRoot = new object();
        private IDictionary<string,ILuaMediator> m_luaMediators = new Dictionary<string, ILuaMediator>();
        public static ILuaView Instance
        {
            get
            {
                if (null == m_instance)
                {
                    lock (m_syncStaticRoot)
                    {
                        m_instance = new LuaView();
                    }
                }
                return m_instance;
            }
        }

        public void RegisterObserver(string notificationName, IObserver luaObserver)
        {
            Observers.Instance.RegisterObserver(notificationName, luaObserver);
        }
        public void RemoveObserver(string notificationName, IObserver luaObserver)
        {
            Observers.Instance.RemoveObserver(notificationName, luaObserver);
        }

        public void RegisterMediator(ILuaMediator luaMediator)
        {
            lock (m_syncRoot)
            {
                if (m_luaMediators.ContainsKey(luaMediator.NAME))
                    return;
                m_luaMediators.Add(luaMediator.NAME,luaMediator);
                if( null != luaMediator.OnRegister)
                    luaMediator.OnRegister();
                if (luaMediator.ListNotificationInterests.Count > 0)
                {
                    for (int i = 0; i < luaMediator.ListNotificationInterests.Count; i++)
                        RegisterObserver(luaMediator.ListNotificationInterests[i], new Observer(luaMediator.HandleNotification));
                }
            }
        }
        public void RemoveMediator(string mediatorName)
        {
            ILuaMediator luaMediator = null;
            if (m_luaMediators.TryGetValue(mediatorName, out luaMediator))
            {
                RemoveObserver(luaMediator.NAME,new Observer(luaMediator.HandleNotification));
                if (null != luaMediator.OnRemove)
                    luaMediator.OnRemove();
                m_luaMediators.Remove(mediatorName);
            }
        }
        public ILuaMediator RetrieveMediator(string mediatorName)
        {
            if (!m_luaMediators.ContainsKey(mediatorName))
                return null;
            return m_luaMediators[mediatorName];
        }
        public bool HasMediator(string mediatorName)
        {
            if (!m_luaMediators.ContainsKey(mediatorName))
                return false;
            return true;
        }
    }
}