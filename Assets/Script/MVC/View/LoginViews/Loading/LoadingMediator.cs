
using System.Collections.Generic;
using LuaMVC;
using PureMVC.Patterns;

namespace Game
{
    public class LoadingMediator : Mediator
    {
        public new const string NAME = "LoadingMediator";
        public LoadingView LoadingView
        {
            get { return (LoadingView) View; }
        }
        public LoadingMediator(IBaseView view) : base(NAME, view)   {  }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.M2V_UPDATE_PROGRESS:
                    LoadingView.SetProgress(float.Parse(notification.Body as string));
                    break;
            }
        } 
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.M2V_UPDATE_PROGRESS);
            return notifications;
        }
    }
}