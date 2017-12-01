
using PureMVC.Patterns;
using System.Collections.Generic;
using SimpleJson;

namespace Game
{
    public class LoginMediator : Mediator
    {
        public new const string NAME = "LoginMediator";

        public LoginView LoginView
        {
            get { return (LoginView) ViewComponent; }
        }

        public LoginMediator(LoginView view) : base(NAME, view)
        {
            view.ActLogin += playerInfo =>
            {
                JsonObject json = new JsonObject();
                json.Add("username", playerInfo.username);
                json.Add("password", playerInfo.password);
                SendNotification(NotificationType.SERVICE_LOGIN, json);
            };
            view.ActRegister += () => { SendNotification(NotificationType.V2V_GAME_REGISTER); };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.SERVICE_LOGIN_SUCCESS:
                    Loom.InvokeAsync(LoginView.Close);
                    break;
                case NotificationType.SERVICE_LOGIN_FAILED:
                    SendNotification(NotificationType.MESSAGE_BUILDIN,"账户密码错误，请重新登陆");
                    break;
            }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.SERVICE_LOGIN_SUCCESS);
            notifications.Add(NotificationType.SERVICE_LOGIN_FAILED);
            return notifications;
        }
    }
}