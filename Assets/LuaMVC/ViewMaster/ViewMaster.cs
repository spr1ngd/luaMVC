
namespace LuaMVC
{
    using Game;
    using System.Collections.Generic;

    public enum E_ViewType : int
    {
        // Framewrok build-in Type
        Setting,

        // 
        Login,
        Register,
        Ready,
        Gameing,
    }

    public class ViewMaster : Singleton<ViewMaster>
    {
        private IDictionary<string,IBaseView> m_views = new Dictionary<string, IBaseView>();

        public void AddView(IBaseView view)
        {
            string viewName = view.ViewName.ToString();
            if (m_views.ContainsKey(viewName))
                m_views.Add(viewName, view);
        }
        public void AddView(IBaseView[] views)
        {
            for (int i = 0; i < views.Length; i++)
                AddView(views[i]);
        }

        public void OpenView( string viewName )
        {
            IBaseView view = null;
            if( m_views.TryGetValue(viewName,out view) )
                view.Open();
        }
        public void OpenView( string[] viewNames )
        {
            for( int i = 0 ; i < viewNames.Length;i++ )
                OpenView(viewNames[i]);
        }

        public void CloseView( string viewName )
        {
            IBaseView view = null;
            if( m_views.TryGetValue(viewName,out view) )
                view.Close();
        }
        public void CloseView(string[] viewNames)
        {
            for (int i = 0; i < viewNames.Length; i++)
                CloseView(viewNames[i]);
        }

        public void RemoveView( string viewName )
        {
            if (m_views.ContainsKey(viewName))
            {
                m_views[viewName].Close();
                m_views.Remove(viewName);
            }
        }
        public void RemoveView(string[] viewNames )
        {
            for( int i = 0 ; i < viewNames.Length;i++ )
                RemoveView(viewNames[i]);
        }

        public IBaseView GetView( string viewName )
        {
            return m_views[viewName];
        }
    }
}