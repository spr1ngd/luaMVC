
namespace PureMVC.Patterns
{
    using System.Collections.Generic;
#if ENABLE_LUAMVC_VIEWMASTER
    using LuaMVC;
#endif

    public interface IMediator
    {
        void HandleNotification(INotification notification);
        IList<string> ListNotificationInterests();
        void OnRegister();
        void OnRemove();
        string MediatorName { get; }
        object ViewComponent { get; set; }
#if ENABLE_LUAMVC_VIEWMASTER
        IBaseView View { get; set; }
#endif
    }
    
    public class Mediator : Notifier, IMediator
    {
        protected string m_mediatorName;
        public virtual string MediatorName
        {
            get { return this.m_mediatorName; }
        }
        public const string NAME = "Mediator";
        public object ViewComponent { get; set; }
#if ENABLE_LUAMVC_VIEWMASTER
        public IBaseView View { get; set; }
#endif

        public Mediator() : this("Mediator", null)
        {
        }
        public Mediator(string mediatorName) : this(mediatorName, null)
        {
        }
        public Mediator(string mediatorName, object viewComponent)
        {
            this.m_mediatorName = (mediatorName != null) ? mediatorName : "Mediator";
            this.ViewComponent = viewComponent;
        }
#if ENABLE_LUAMVC_VIEWMASTER
        public Mediator( string mediatorName,IBaseView view )
        {
            this.m_mediatorName = (mediatorName != null) ? mediatorName : "Mediator";
            this.View = view;
        }
#endif

        public virtual void HandleNotification(INotification notification)
        {
        }
        public virtual IList<string> ListNotificationInterests()
        {
            return new List<string>();
        }
        public virtual void OnRegister()
        {
        }
        public virtual void OnRemove()
        {
        }
    }
}