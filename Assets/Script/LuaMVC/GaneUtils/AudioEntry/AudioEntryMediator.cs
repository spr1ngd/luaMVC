
using PureMVC.Patterns;
using System.Collections.Generic;

namespace LuaMVC
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