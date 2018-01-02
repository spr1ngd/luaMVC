
using System.Collections.Generic;
using System.ComponentModel;
using Game;
using PureMVC.Patterns;

namespace LuaMVC
{
    public class SettingMediator : Mediator
    {
        public new const string NAME = "SettingMediator";

        public SettingMediator(IBaseView view) : base(NAME, view)
        {
            
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.SETTING:
                    View.Open();
                    break;
            }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.SETTING);
            return notifications;
        }
    }
}