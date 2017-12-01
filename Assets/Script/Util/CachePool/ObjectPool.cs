
// 是否启用PoolManager自动管理ObjectPool
#define ENABLE_POOL_MANAGER

using System;
using System.Collections;
using System.Collections.Generic;

namespace LuaMVC
{
    public interface IPool
    {
        string name { get; set; }
        void OnInitialze();
        void OnRelease();
    }

    public interface IObjectPool<T> : IPool
    {
        void Push(T obj);
        void Push(IList<T> objs);
        void Push(int count);
        T Pop();
        IList<T> Pop(int count);
        Func<T> New { get; set; }
        Action pushAction { get; set; }
        Action popAction { get; set; }
    }

    public class ObjectPool<T> : IObjectPool<T>, IEnumerable
    {
        public string name { get; set; }
        public IList<T> busyList { get; private set; }
        public IList<T> idleList { get; private set; }
        public Action pushAction { get; set; }
        public Action popAction { get; set; }
        public Func<T> New { get; set; }

        public ObjectPool(string name)
        {
            this.name = name;
            OnInitialze();
        }

        // 压入空闲队列
        public virtual void Push(T obj)
        {
            busyList.Remove(obj);
            idleList.Add(obj);
            if (null != pushAction)
                pushAction();
        }
        public void Push(IList<T> objs)
        {
            for (int i = 0; i < objs.Count; i++)
                Push(objs[i]);
        }
        public void Push(int count)
        {
            if (count > busyList.Count)
                count = busyList.Count;
            for (int i = count - 1; i >= 0; i--)
                Push(busyList[i]);
        }

        // 取到工作队列
        public virtual T Pop()
        {
            T resultT;
            if (idleList.Count > 0)
            {
                resultT = idleList[0];
                idleList.RemoveAt(0);
            }
            else
                resultT = New();
            if (null != popAction)
                popAction();
            busyList.Add(resultT);
            return resultT;
        }
        public IList<T> Pop(int count)
        {
            IList<T> resultList = new List<T>();
            for (int i = 0; i < count; i++)
                resultList.Add(Pop());
            return resultList;
        }

        public virtual void OnInitialze()
        {
            busyList = new List<T>();
            idleList = new List<T>();
#if ENABLE_POOL_MANAGER
            PoolManager.Instance.AddPool(this);
#endif
        }
        public virtual void OnRelease()
        {
            busyList.Clear();
            idleList.Clear();
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < busyList.Count; i++)
                yield return busyList[i];
        }
    }
}