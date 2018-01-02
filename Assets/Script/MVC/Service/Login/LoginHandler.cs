
using System;
using System.Collections.Generic; 
using PureMVC.Patterns;
using SimpleJson;
using UnityEngine;

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
                case NotificationType.SERVICE_REGISTER:
                    Register(notification);
                    break;
                case NotificationType.SERVICE_MODIFY_PASSWORD:
                    ModifyPassword(notification);
                    break;
            }
        }

        private void Login( INotification notification )
        {
            pomeloClient.request("login.loginHandler.loginServer", notification.Body as JsonObject, result =>
            { 
                if (Convert.ToInt32(result["code"]) == 200)
                {  
                    PlayerProxy.LoginSuccess(result["info"].ToString());
                }
                else if (Convert.ToInt32(result["code"]) == 404)
                {
                    PlayerProxy.LoginError();
                }
            });
        }
        private void Register( INotification notification )
        {
            pomeloClient.request("login.loginHandler.register",notification.Body as JsonObject, result =>
            {
                if (Convert.ToInt32(result["code"]) == 200)
                {
                    PlayerProxy.RegisterSuccess();
                }else if (Convert.ToInt32(result["code"]) == 404)
                {
                    PlayerProxy.RegisterError();
                }
            });
        }
        private void ModifyPassword( INotification notification )
        {
            pomeloClient.request("login.loginHandler.modifyPassword",notification.Body as JsonObject, result =>
            {
                if (Convert.ToInt32(result["code"]) == 200)
                {
                    PlayerProxy.ModifyPasswordSuccess();
                }
                else if (Convert.ToInt32(result["code"]) == 404)
                {
                    PlayerProxy.ModifyPasswordFailed();
                }
            });
        }

        // 这个结口用来主动接收服务器消息
        public override void Response()
        {
            base.Response();
        }

        public override IList<string> HandleNotification()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.SERVICE_LOGIN);
            notifications.Add(NotificationType.SERVICE_REGISTER);
            notifications.Add(NotificationType.SERVICE_MODIFY_PASSWORD);
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