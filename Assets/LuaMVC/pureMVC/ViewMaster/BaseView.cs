
namespace LuaMVC
{
    using UnityEngine;
    using PureMVC.Patterns;

    public interface IBaseView
    {
        E_ViewType ViewName { get; set; }
        IMediator Mediator { get; set; }
        void Initialize();
        void Open();
        void Close();
        void OpenAnimation();
        void CloseAnimation();
    }

    public class BaseView : MonoBehaviour, IBaseView
    {
        public E_ViewType ViewName { get; set; }
        public IMediator Mediator { get; set; }
        protected GameObject m_gameobject = null;
        protected Transform m_transform = null;
		protected Transform m_panel = null;
		protected bool isActive = true;

        /// <summary>
        /// Initialize instead of Awake and Start function.Initialize function called by lauMVC.
        /// </summary>
        public virtual void Initialize()
        {
            m_transform = this.transform;
            m_gameobject = m_transform.gameObject;
			m_panel = m_transform.Find ("Panel"); 
            ViewMaster.AddView(this);
        }

        public virtual void Open()
        { 
            OpenAnimation();
        }
        public virtual void Close()
        { 
            CloseAnimation();
        }
        public virtual void OpenAnimation()
        {
			m_transform.Find("Panel").gameObject.SetActive(true);
			isActive = true;
        }
        public virtual void CloseAnimation()
        {
			m_transform.Find("Panel").gameObject.SetActive(false);
			isActive = false;
        }
    }
}