
using System;
using System.Collections.Generic;
using PureMVC.Patterns;
using SimpleJson;

namespace Game
{
    public class LoginHandler : Handler
    {
        public new const string NAME = "LoginHandler";

        public LoginHandler() : base(NAME){}
        public LoginHandler(IProxy proxy) : base(NAME, proxy)
        {
            
        }
        public PlayerProxy PlayerProxy
        {
            get { return (PlayerProxy)Proxy; }
        }

        public override void Request(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.SERVICE_LOGIN:
                    Login(notification);
                    break;
            }
        }

        private void Login( INotification notification )
        {
            pomeloClient.request("login.loginHandler.loginServer", notification.Body as JsonObject, info =>
            {
                if (Convert.ToInt32(info["code"]) == 200)
                {
                    var roles = SimpleJson.SimpleJson.DeserializeObject<List<Role>>(info["info"].ToString());
                    PlayerProxy.UpdateRole(roles);
                }
                else if (Convert.ToInt32(info["code"]) == 404)
                {
                    PlayerProxy.LoginError();
                }
            });
        }

        public override void Response()
        {
            base.Response();
        }

        public override IList<string> HandleNotification()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.SERVICE_LOGIN);
            notifications.Add(NotificationType.SERVICE_REGISTER);
            notifications.Add(NotificationType.SERVICE_DELETE);
            notifications.Add(NotificationType.SERVICE_LOGOUT);
            return notifications;
        }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override void OnRemove()
        {
            base.OnRemove();
        }
    }
}