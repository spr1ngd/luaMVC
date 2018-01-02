
using System;
using PureMVC.Patterns;

namespace Game
{
    public class ShutDownCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            PlayerProxy playerProxy = Facade.RetrieveProxy("PlayerProxy") as PlayerProxy;
            if( null == playerProxy )
                throw new Exception("player proxy is null, please check it.");
//            Role role = playerProxy.Submit();
//            if( null != role)
//                SendNotification(NotificationType.SERVICE_SUBMIT_PLAYERINFO, role);
        }
    }
}