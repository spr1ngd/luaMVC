
using System.Collections.Generic;
using PureMVC.Patterns;

namespace LuaMVC
{
    public class SettingMediator : Mediator
    {
        public new const string NAME = "SettingMediator";

        public SettingMediator(IBaseView view) : base(NAME, view)
        {

        }

        public override void OnRegister()
        {
            // todo 在这个脚本里从本地读取设置记录，并还原就好
        }

        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);
        }

        public override IList<string> ListNotificationInterests()
        {
            return base.ListNotificationInterests();
        }
    }
}