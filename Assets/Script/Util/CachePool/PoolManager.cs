
using System.Collections.Generic;

namespace LuaMVC
{
    public class PoolManager
    {
        private volatile static PoolManager m_instance;
        private readonly object m_syncRoot = new object();
        private IDictionary<string,IPool> m_pools = new Dictionary<string, IPool>();
        public static PoolManager Instance
        {
            get
            {
                if (null == m_instance)
                    m_instance = new PoolManager();
                return m_instance;
            }
        }

        public void AddPool( IPool pool)
        {
            lock (m_syncRoot)
            {
                if (!m_pools.ContainsKey(pool.name))
                    m_pools.Add(pool.name, pool);
            }
        }
        public IObjectPool<T> GetPool<T>( string name )
        {
            lock ( m_syncRoot )
            {
                if (m_pools.ContainsKey(name))
                    return m_pools[name] as IObjectPool<T>;
            }
            return null;
        }
        public void RemovePool( string name )
        {
            lock ( m_syncRoot )
            {
                if (m_pools.ContainsKey(name))
                {
                    var objPool = m_pools[name];
                    if( null != objPool)
                        objPool.OnRelease();
                    m_pools.Remove(name);
                }
            }
        }
        // todo PoolManager的释放需要luaMVC框架来统一管理
        public void OnRelease()
        {
            lock (m_syncRoot)
            {
                foreach (var pair in m_pools)
                    pair.Value.OnRelease();
                m_pools.Clear();
            }
        }
    }
}