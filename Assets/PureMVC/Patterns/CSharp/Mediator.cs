namespace PureMVC.Patterns
{
    using System.Collections.Generic;

    public interface IMediator
    {
        void HandleNotification(INotification notification);
        IList<string> ListNotificationInterests();
        void OnRegister();
        void OnRemove();
        string MediatorName { get; }
        object ViewComponent { get; set; }
    }

    public class Mediator : Notifier, IMediator
    {
        protected string m_mediatorName;
        protected object m_viewComponent;
        public const string NAME = "Mediator";

        public Mediator() : this("Mediator", null)
        {
        }
        public Mediator(string mediatorName) : this(mediatorName, null)
        {
        }
        public Mediator(string mediatorName, object viewComponent)
        {
            this.m_mediatorName = (mediatorName != null) ? mediatorName : "Mediator";
            this.m_viewComponent = viewComponent;
        }

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

        public virtual string MediatorName
        {
            get
            {
                return this.m_mediatorName;
            }
        }

        public object ViewComponent
        {
            get
            {
                return this.m_viewComponent;
            }
            set
            {
                this.m_viewComponent = value;
            }
        }
    }
}

