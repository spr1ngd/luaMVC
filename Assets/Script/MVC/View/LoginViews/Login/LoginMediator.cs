
using PureMVC.Patterns;
using System.Collections.Generic;
using LuaMVC;
using SimpleJson;
using UnityEngine;

namespace Game
{
    public class LoginMediator : Mediator
    {
        public new const string NAME = "LoginMediator";
        public LoginView LoginView
        {
            get { return (LoginView) View; }
        }
        public LoginMediator(IBaseView view) : base(NAME, view)
        { 
            
        }

        public void Login(Player playerInfo)
        {
            JsonObject json = new JsonObject();
            json.Add("username", playerInfo.username);
            json.Add("password", playerInfo.password);
            SendNotification(NotificationType.SERVICE_LOGIN, json);  
        }
        public void Register()
        {
            SendNotification(NotificationType.V2V_GAME_REGISTER);
        }
        public void TouristLogin()
        {
            // 应该由系统为它创建一个游戏id,用于保存基本的数据，后期注册再还原
            LoginView.Close();
        }

        public override void OnRegister()
        {
            base.OnRegister(); 
        }
        public override void OnRemove()
        {
            base.OnRemove();
        }
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.SERVICE_LOGIN_SUCCESS: 
                    Loom.InvokeSync(LoginView.Close);
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