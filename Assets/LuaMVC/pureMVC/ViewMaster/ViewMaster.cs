using System;

namespace LuaMVC
{
    using XLua;
    using System.Collections.Generic;

    /// <summary>
    /// View master. 方便Lua代码进行调用 设置为静态类型跟随.Net Framework CLR加载，程序的生命周期只加载一次
    /// </summary>
    [LuaCallCSharp]
    public static class ViewMaster //: Singleton<ViewMaster>
    {
        private static IDictionary<string, IBaseView> m_views = new Dictionary<string, IBaseView>();
        private static IDictionary<string, ILuaBaseView> m_luaViews = new Dictionary<string, ILuaBaseView>();

        public static void AddView(IBaseView view)
        {
            string viewName = view.ViewName.ToString();
            if (!m_views.ContainsKey(viewName))
                m_views.Add(viewName, view);
        }
        public static void AddView(IBaseView[] views)
        {
            for (int i = 0; i < views.Length; i++)
                AddView(views[i]);
        } 
        public static void AddView( ILuaBaseView view )
        {
            string viewName = view.ViewName;
            if (!m_luaViews.ContainsKey(viewName))
                m_luaViews.Add(viewName, view); 
        }
        public static void OpenView( string viewName,bool closeOthers )
        {
            //todo 这个地方需要继续改进viewMaster的功能
            if(closeOthers)
            {
                foreach (var view in m_views.Values)
                {
                    if (view.ViewName.ToString() != viewName)
                        view.Close();
                }
                foreach (var view in m_luaViews.Values)
                {
                    if (view.ViewName != viewName)
                        view.Close();
                }
            }
            OpenView(viewName);
            //todo 这玩意也要继续改进一下
            OpenView("FunctionBarView");
        }

        public static void OpenView( string viewName )
        {
            IBaseView view = null; 
            if( m_views.TryGetValue(viewName,out view) )
            {
                view.Open();
                return;
            }
            ILuaBaseView luaView = null;
            if (m_luaViews.TryGetValue(viewName, out luaView))
                luaView.Open(); 
        }
        public static void OpenView( string[] viewNames )
        {
            for( int i = 0 ; i < viewNames.Length;i++ )
                OpenView(viewNames[i]); 
        }

        public static void CloseView( string viewName )
        {
            IBaseView view = null;
            if( m_views.TryGetValue(viewName,out view) )
            {
                view.Close();
                return;
            }
            UnityEngine.Debug.Log(viewName);
            ILuaBaseView luaView = null;
            if (m_luaViews.TryGetValue(viewName, out luaView))
                luaView.Close(); 
        }
        public static void CloseView(string[] viewNames)
        {
            for (int i = 0; i < viewNames.Length; i++)
                CloseView(viewNames[i]);
        }

        public static void RemoveView( string viewName )
        {
            if (m_views.ContainsKey(viewName))
            {
                m_views[viewName].Close();
                m_views.Remove(viewName);
            }
            if (m_luaViews.ContainsKey(viewName))
            {
                m_luaViews[viewName].Close();
                m_luaViews.Remove(viewName);
            }
        }
        public static void RemoveView(string[] viewNames )
        {
            for( int i = 0 ; i < viewNames.Length;i++ )
                RemoveView(viewNames[i]);
        }

        public static IBaseView GetView( string viewName )
        {
            return m_views[viewName];
        } 
    }
}