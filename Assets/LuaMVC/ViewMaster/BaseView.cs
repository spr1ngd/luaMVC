
namespace LuaMVC
{
    using UnityEngine;

    public interface IBaseView
    {
        E_ViewType ViewName { get; set; }
        void Initialize();
        void Open();
        void Close();
        void OpenAnimation();
        void CloseAnimation();
    }

    public class BaseView : MonoBehaviour, IBaseView
    {
        public E_ViewType ViewName { get; set; }

        /// <summary>
        /// Initialize instead of Awake and Start function.Initialize function called by lauMVC.
        /// </summary>
        public virtual void Initialize()
        {
            ViewMaster.Instance.AddView(this);
        }

        public virtual void Open()
        {
            OpenAnimation();
        }

        public virtual void Close()
        {
            
        }

        public virtual void OpenAnimation()
        {
            CloseAnimation();
        }

        public virtual void CloseAnimation()
        {
            
        }
    }
}