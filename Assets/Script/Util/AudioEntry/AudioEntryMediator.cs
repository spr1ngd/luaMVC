
using PureMVC.Patterns;
using System.Collections.Generic;

namespace Game
{
    public class AudioEntryMediator : Mediator
    {
        public new const string NAME = "AudioEntryMediator";
        public AudioEntry AudioEntry
        {
            get { return (AudioEntry) ViewComponent; }
        }

        public AudioEntryMediator(AudioEntry entry):base(NAME,entry){}
         
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                // 打开Audio设置面板
                case NotificationType.AUDIO:
                    View.Open();
                    break;
                case NotificationType.AUDIO_PLAY: 
                    break; 
            }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.AUDIO);
            notifications.Add(NotificationType.AUDIO_PLAY); 
            return notifications;
        }
    }
}