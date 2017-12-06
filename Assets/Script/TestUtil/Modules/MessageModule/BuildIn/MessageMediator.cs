
using System.Collections.Generic;
using LuaMVC;
using PureMVC.Patterns;

namespace Game
{
    public class MessageMediator : Mediator
    {
        public new const string NAME = "MessageMediator";

        public MessageView MessageView
        {
            get { return (MessageView)ViewComponent; }
        }

        public MessageMediator(MessageView view) : base(NAME, view)
        {

        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.MESSAGE_BUILDIN:
                    UnityEngine.Debug.Log("登录失败");
                    Loom.InvokeSync(data => { MessageView.ShowMessage(data.ToString()); }, notification.Body as string);
                    break;
                case NotificationType.MESSAGE_ANDROID:

                    break;
                case NotificationType.MESSAGE_IOS:

                    break;
            }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.MESSAGE_BUILDIN);
            notifications.Add(NotificationType.MESSAGE_ANDROID);
            notifications.Add(NotificationType.MESSAGE_IOS);
            return notifications;
        }
    }
}