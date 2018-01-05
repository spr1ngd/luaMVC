
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

namespace LuaMVC
{ 
    public struct SyncActionOnePara
    {
        public Action<object> action;
        public object para1;
    }
    public struct SyncActionTwoPara
    {
        public Action<object,object> action;
        public object para1;
        public object para2;
    }
    public struct SyncActionThreePara
    {
        public Action<object, object, object> action;
        public object para1;
        public object para2;
        public object para3;
    }

    public struct DelayAction
    {
        public Action action;
        public float time;
    }
    public struct DelayActionOnePara
    {
        public Action<object> action;
        public object para1;
        public float time;
    }

    public struct AsyncActionOnePara
    {
        public Action<object> action;
        public object para1;
    }

    public class Loom : MonoBehaviour
    {
        private static Loom m_instance = null;
        public static Loom Instance
        {
            get
            {
                if (null == m_instance)
                {
                    // if unity throw an error "xxxxx can only be called from the main thread." . Please init this tool from application start up. 
                    GameObject Loom = new GameObject("Loom");
                    m_instance = Loom.AddComponent<Loom>();
                }
                return m_instance;
            }
        }

        public void Init()
        {

        }

        private void Update()
        {
            RunSyncMethod();
            RunDelay();
        }

        #region Sync method

        private List<Action> syncActions = new List<Action>();
        private List<SyncActionOnePara> syncActionsOnePara = new List<SyncActionOnePara>();
        private List<SyncActionTwoPara> syncActionTwoPara = new List<SyncActionTwoPara>();
        private List<SyncActionThreePara> syncActionThreePara = new List<SyncActionThreePara>();

        private void RunSyncMethod()
        {
            lock (syncActions)
            {
                foreach (Action action in syncActions)
                    action();
                syncActions.Clear();
            }
            lock (syncActionsOnePara)
            {
                foreach (SyncActionOnePara aa in syncActionsOnePara)
                    aa.action(aa.para1);
                syncActionsOnePara.Clear();
            }
            lock (syncActionTwoPara)
            {
                foreach (SyncActionTwoPara twoPara in syncActionTwoPara)
                    twoPara.action(twoPara.para1, twoPara.para2);
                syncActionTwoPara.Clear();
            }
            lock (syncActionThreePara)
            {
                foreach (SyncActionThreePara threePara in syncActionThreePara)
                    threePara.action(threePara.para1, threePara.para2, threePara.para3);
                syncActionThreePara.Clear();
            }
        }
        public static void InvokeSync( Action callback )
        {
            lock (Instance.syncActions)
            {
                Instance.syncActions.Add(callback);
            }
        }
        public static void InvokeSync( Action<object> callback ,object para1 ) 
        {
            lock (Instance.syncActionsOnePara)
            {
                Instance.syncActionsOnePara.Add(new SyncActionOnePara() { action = callback, para1 = para1 });
            }
        }
        public static void InvokeSync( Action<object, object> callback,object para1,object para2)
        {
            lock (Instance.syncActionTwoPara)
            {
                Instance.syncActionTwoPara.Add(new SyncActionTwoPara() { action = callback, para1 = para1, para2 = para2 });
            }
        }
        public static void InvokeSync( Action<object, object, object> callback ,object para1,object para2,object para3)
        {
            lock (Instance.syncActionThreePara)
            {
                Instance.syncActionThreePara.Add(new SyncActionThreePara() { action = callback, para1 = para1, para2 = para2, para3 = para3 });
            }
        }

        #endregion

        #region Delay method

        private List<DelayAction> delayActions = new List<DelayAction>();
        private List<DelayActionOnePara> delayActionsOnePara = new List<DelayActionOnePara>();

        private void RunDelay()
        {

        }
        public static void InvokeDelay( Action callback , float time )
        {
            Instance.delayActions.Add(new DelayAction() { action = callback, time = time });
        }
        public static void InvokeDelay( Action<object> callback ,float time , object para1)
        {
            Instance.delayActionsOnePara.Add(new DelayActionOnePara() { action = callback, time = time });
        }

        #endregion

        #region Async method

        private static int maxThreads = 8;
        private static int numThreads = 0;

        private static void RunAsync( object state )
        {
            try
            {
                ((Action) state)();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Interlocked.Decrement(ref numThreads);
            }
        }
        private static void RunAsyncOnePara(object state)
        {
            try
            {
                AsyncActionOnePara action = (AsyncActionOnePara) state;
                action.action(action.para1);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Interlocked.Decrement(ref numThreads);
            }
        }
        public static void InvokeAsync( Action callback )
        {
            while (numThreads > maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            ThreadPool.QueueUserWorkItem(RunAsync, callback);
        }
        public static void InvokeAsync( Action<object> callback , object para1 )
        {
            while (numThreads > maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            ThreadPool.QueueUserWorkItem(RunAsyncOnePara, new AsyncActionOnePara() { action = callback, para1 = para1 });
        }

        #endregion
    }
}