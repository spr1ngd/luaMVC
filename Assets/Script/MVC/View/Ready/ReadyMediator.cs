
using System.Collections.Generic;
using PureMVC.Patterns;

namespace Game
{
    public class ReadyMediator : Mediator
    {
        public new const string NAME = "ReadyMediator";
        public ReadyView ReadyView
        {
            get { return (ReadyView)ViewComponent; }
        }

        public ReadyMediator(ReadyView view) : base(NAME, view)
        {
            ReadyView.ActStartGame += () => { SendNotification(NotificationType.V2V_GAME_START); };
        }

        public override void HandleNotification(INotification notification)
        { 

        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>(); 
            return notifications;
        }
    }
}