
using System.Collections.Generic;
using PureMVC.Patterns.Lua;

namespace PureMVC.Core.Lua
{
    public interface ILuaModel
    {
        void RegisterProxy(ILuaProxy luaProxy);
        ILuaProxy RetrieveProxy(string luaProxyName);
        void RemoveProxy(string luaProxyName);
        bool HasProxy(string luaProxyName);
    }
    
    public class LuaModel : ILuaModel
    {
        private volatile static ILuaModel m_instance = null;
        private static readonly object m_syncStaticRoot = new object();
        public static ILuaModel Instance
        {
            get
            {
                if (null == m_instance)
                {
                    lock (m_syncStaticRoot)
                    {
                        m_instance = new LuaModel();
                    }
                }
                return m_instance;
            }
        }
        private readonly object m_syncRoot = new object();
        private IDictionary<string,ILuaProxy> m_luaProxys = new Dictionary<string, ILuaProxy>();

        public void RegisterProxy(ILuaProxy luaProxy)
        {
            lock (m_syncRoot)
            {
                if (!m_luaProxys.ContainsKey(luaProxy.NAME))
                {
                    m_luaProxys.Add(luaProxy.NAME, luaProxy);
                    luaProxy.OnRegister();
                }
            }
        }
        public ILuaProxy RetrieveProxy(string luaProxyName)
        {
            lock (m_syncRoot)
            {
                if (m_luaProxys.ContainsKey(luaProxyName))
                    return m_luaProxys[luaProxyName];
                return null;
            }
        }
        public void RemoveProxy(string luaProxyName)
        {
            lock (m_syncRoot)
            {
                if (m_luaProxys.ContainsKey(luaProxyName))
                {
                    m_luaProxys[luaProxyName].OnRemove();
                    m_luaProxys.Remove(luaProxyName);
                }
            }
        }
        public bool HasProxy(string luaProxyName)
        {
            lock (m_syncRoot)
            {
                if (m_luaProxys.ContainsKey(luaProxyName))
                    return true;
                return false;
            }
        }
    }
}