
using PureMVC.Patterns;
using System.Collections.Generic;

namespace Game
{
    public class TimeMasterMediator : Mediator
    {
        public new const string NAME = "TimeMasterMediator";
        public TimeMaster TimeMaster
        {
            get { return (TimeMaster) ViewComponent; }
        }

        public TimeMasterMediator(TimeMaster view) : base(NAME, view)
        {
            TimeMaster.ActTimeUp += () => { SendNotification(NotificationType.TIME_TIMEUP); };
            TimeMaster.ActTimeRemain += (time) => { SendNotification(NotificationType.TIME_REMAIN, time); };
            //todo 警报铃声
            TimeMaster.ActTimeWarning += () =>
            {
                //SendNotification(NotificationType.AUDIO_PLAY,"TimeUp");
            };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.TIME_ADDTIME:
                    TimeMaster.AddTime();
                    break;
                case NotificationType.TIME_REDUCETIME:
                    TimeMaster.ReduceTime();
                    break;
                case NotificationType.TIME_ADDUPPERLIMIT:
                    TimeMaster.AddUpperTime(float.Parse(notification.Body as string));
                    break;
                case NotificationType.V2V_GAME_START:
                    TimeMaster.Restart();
                    break;
            }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.TIME_ADDTIME);
            notifications.Add(NotificationType.TIME_REDUCETIME);
            notifications.Add(NotificationType.TIME_ADDUPPERLIMIT);
            notifications.Add(NotificationType.V2V_GAME_START);
            return notifications;
        }
    }
}