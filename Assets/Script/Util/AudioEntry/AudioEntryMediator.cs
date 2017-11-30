
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

        public override void OnRegister()
        {
            base.OnRegister();
            AudioEntry.OnInitialize();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            AudioEntry.OnRelease();
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.AUDIO_PLAY:
                    AudioEntry.PlayOneShot(notification.Body as string);
                    break;
                case NotificationType.AUDIO_VOLUME:
                    AudioEntry.SetBackgroundAudioVolumn(float.Parse(notification.Body as string));
                    break;
                case NotificationType.AUDIO_SOUNDEFFECTVOLUMN:
                    AudioEntry.SetSoundEffectVolumn(float.Parse(notification.Body as string));
                    break;
            }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.AUDIO_PLAY);
            notifications.Add(NotificationType.AUDIO_VOLUME);
            notifications.Add(NotificationType.AUDIO_SOUNDEFFECTVOLUMN);
            return notifications;
        }
    }
}