
using System;
using System.Collections.Generic;
using Pomelo.DotNetClient;
using PureMVC.Patterns;
using SimpleJson;

namespace Game
{
    // todo 这个整合成Command或者集成到StartUpCommand中,或者是集成到pureMVC框架中，预留pomeloclinet和其他协议接口
    public class ClientStartHandler : Handler
    {
        public new const string NAME = "ClientStartHandler";
        public ClientStartHandler() : base(NAME){}
        public ClientStartHandler(IProxy proxy) : base(NAME, proxy){}

        public override void Request(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.SERVICE_CONNECTSERVER:
                    // todo 只允许在这里赋值一次
                    pomeloClient = ((ServerAddressProxy) Proxy).PomeloClient;
                    UnityEngine.Debug.Log("Connect server.");
                    pomeloClient.connect(null, data =>
                    {
                        pomeloClient.request("gate.gateHandler.queryEntry", new JsonObject(), OnQuery);
                    });
                    break;
                case NotificationType.SERVICE_DISCONNECTSERVER:

                    break;
            }
        }

        //todo  这里需要重新整理优化
        private void OnQuery(JsonObject result)
        {
            if (Convert.ToInt32(result["code"]) == 200)
            {
                pomeloClient.disconnect();
                string host = (string)result["host"];
                int port = Convert.ToInt32(result["port"]);
                pomeloClient = new PomeloClient(host, port);
                pomeloClient.connect(null, (data) =>
                {
                });
            }
        }

        public override IList<string> HandleNotification()
        {
            List<string> notifications = new List<string>();
            notifications.Add(NotificationType.SERVICE_CONNECTSERVER);
            notifications.Add(NotificationType.SERVICE_DISCONNECTSERVER);
            return notifications;
        }
    }
}