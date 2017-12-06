
using System;
using System.Collections.Generic;
using PureMVC.Patterns;
using SimpleJson;

namespace Game
{
    public class ClientQuitHandler : Handler
    {
        public new const string NAME = "ClientQuitHandler";

        public ClientQuitHandler():base(NAME)
        {

        }

        public override void Request(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.SERVICE_SUBMIT_PLAYERINFO:
                    Submit(notification);
                    break;
            }
        }

        private void Submit( INotification notification )
        {
            Role role = notification.Body as Role;
            if( null == role)
                throw  new Exception("Get role data failed.");
            JsonObject data = new JsonObject();
            data["info"] = SimpleJson.SimpleJson.SerializeObject(role);
            pomeloClient.notify("login.loginHandler.submit", data);
        }

        public override IList<string> HandleNotification()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.SERVICE_SUBMIT_PLAYERINFO);
            return notifications;
        }
    }
}