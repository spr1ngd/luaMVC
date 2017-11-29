
using System.Collections.Generic;
using Pomelo.DotNetClient;
using PureMVC.Patterns;

namespace Game
{
    public class ServerAddressProxy : Proxy
    {
        public new const string NAME = "ClientAddressProxy";

        public ServerIPAddress ServerAddress;
        public ServerAddressProxy() : base(NAME)
        {
            ServerAddress = new ServerIPAddress();
        }
        public ServerAddressProxy(ServerIPAddress address) : base(NAME, address)
        {
            ServerAddress = address;
        }

        public PomeloClient PomeloClient
        {
            get { return new PomeloClient(ServerAddress.host, ServerAddress.port); }
        }

        public void SetIPAddress(string host, string port)
        {
            ServerAddress = new ServerIPAddress(host,port);
        }

        public override void OnRegister()
        {
            var configs = JsonFS.Instance.File2Object<List<IPAddress>>("ServerAddress");
#if UNITY_DEVELOPMENT
            foreach (var ipAddress in configs)
                if (ipAddress.address == "development")
                    ServerAddress = ipAddress.ipAdress;
#endif 
#if UNITY_PRODUCT
            foreach (var ipAddress in configs)
                if (ipAddress.address == "product")
                    ServerAddress = ipAddress.ipAdress;
            UnityEngine.Debug.Log("UNITY_PRODUCT");
#endif
        }
    }
}