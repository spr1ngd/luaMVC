using PureMVC.Patterns;

namespace PureMVC.Core
{
    using System.Collections.Generic;

    public interface IModel
    {
        bool HasProxy(string proxyName);
        void RegisterProxy(IProxy proxy);
        IProxy RemoveProxy(string proxyName);
        IProxy RetrieveProxy(string proxyName);
    }

    public class Model : IModel
    {
        protected static volatile IModel m_instance;
        protected IDictionary<string, IProxy> m_proxyMap = new Dictionary<string, IProxy>();
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();
        public static IModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new Model();
                        }
                    }
                }
                return m_instance;
            }
        }

        protected Model()
        {
            this.InitializeModel();
        }
        protected virtual void InitializeModel()
        {
        }
        public virtual bool HasProxy(string proxyName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_proxyMap.ContainsKey(proxyName);
            }
        }
        public virtual void RegisterProxy(IProxy proxy)
        {
            lock (this.m_syncRoot)
            {
                this.m_proxyMap[proxy.ProxyName] = proxy;
            }
            proxy.OnRegister();
        }
        public virtual IProxy RemoveProxy(string proxyName)
        {
            IProxy proxy = null;
            lock (this.m_syncRoot)
            {
                if (this.m_proxyMap.ContainsKey(proxyName))
                {
                    proxy = this.RetrieveProxy(proxyName);
                    this.m_proxyMap.Remove(proxyName);
                }
            }
            if (proxy != null)
            {
                proxy.OnRemove();
            }
            return proxy;
        }
        public virtual IProxy RetrieveProxy(string proxyName)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_proxyMap.ContainsKey(proxyName))
                {
                    return null;
                }
                return this.m_proxyMap[proxyName];
            }
        }
    }
}