
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public struct AsyncActionOnePara
    {
        public Action<object> action;
        public object para1;
    }

    public struct AsyncActionTwoPara
    {
        public Action<object,object> action;
        public object para1;
        public object para2;
    }

    public struct AsyncActionThreePara
    {
        public Action<object, object, object> action;
        public object para1;
        public object para2;
        public object para3;
    }

    public class Loom : MonoBehaviour
    {
        //todo 关于初始化这里可以再优化一波
        public static Loom Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private List<Action> asyncActions = new List<Action>();
        private List<AsyncActionOnePara> asyncActionsOnePara = new List<AsyncActionOnePara>();
        private List<AsyncActionTwoPara> asyncActionTwoPara = new List<AsyncActionTwoPara>();
        private List<AsyncActionThreePara> asyncActionThreePara = new List<AsyncActionThreePara>();

        private void Update()
        {
            lock (asyncActions)
            {
                foreach (Action action in asyncActions)
                    action();
                asyncActions.Clear();
            }
            lock (asyncActionsOnePara)
            {
                foreach (AsyncActionOnePara aa in asyncActionsOnePara)
                    aa.action(aa.para1);
                asyncActionsOnePara.Clear();
            }
            lock (asyncActionTwoPara)
            {
                foreach (AsyncActionTwoPara twoPara in asyncActionTwoPara)
                    twoPara.action(twoPara.para1,twoPara.para2);
                asyncActionTwoPara.Clear();
            }
            lock (asyncActionThreePara)
            {
                foreach (AsyncActionThreePara threePara in asyncActionThreePara)
                    threePara.action(threePara.para1,threePara.para2,threePara.para3);
                asyncActionThreePara.Clear();
            }
        }

        public static void InvokeAsync( Action callback )
        {
            lock (Instance.asyncActions)
            {
                Instance.asyncActions.Add(callback);
            }
        }

        public static void InvokeAsync( Action<object> callback ,object para1 ) 
        {
            lock (Instance.asyncActionsOnePara)
            {
                Instance.asyncActionsOnePara.Add(new AsyncActionOnePara() { action = callback, para1 = para1 });
            }
        }

        public static void InvokeAsync( Action<object, object> callback,object para1,object para2,object para3)
        {
            lock (Instance.asyncActionTwoPara)
            {
                Instance.asyncActionTwoPara.Add(new AsyncActionTwoPara() { action = callback, para1 = para1, para2 = para2 });
            }
        }

        public static void InvokeAsync( Action<object, object, object> callback ,object para1,object para2,object para3)
        {
            lock (Instance.asyncActionThreePara)
            {
                Instance.asyncActionThreePara.Add(new AsyncActionThreePara() { action = callback, para1 = para1, para2 = para2, para3 = para3 });
            }
        }
    }
}