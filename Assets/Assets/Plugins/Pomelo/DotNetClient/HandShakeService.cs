namespace Pomelo.DotNetClient
{
    using SimpleJson;
    using System;
    using System.Text;

    public class HandShakeService
    {
        private Action<JsonObject> callback;
        private Protocol protocol;
        public const string Type = "unity-socket";
        public const string Version = "0.3.0";

        public HandShakeService(Protocol protocol)
        {
            this.protocol = protocol;
        }

        public void ack()
        {
            this.protocol.send(PackageType.PKG_HANDSHAKE_ACK, new byte[0]);
        }

        private JsonObject buildMsg(JsonObject user)
        {
            if (user == null)
            {
                user = new JsonObject();
            }
            JsonObject obj2 = new JsonObject();
            JsonObject obj3 = new JsonObject();
            obj3["version"] = "0.3.0";
            obj3["type"] = "unity-socket";
            obj2["sys"] = obj3;
            obj2["user"] = user;
            return obj2;
        }

        internal void invokeCallback(JsonObject data)
        {
            if (this.callback != null)
            {
                this.callback(data);
            }
        }

        public void request(JsonObject user, Action<JsonObject> callback)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(this.buildMsg(user).ToString());
            this.protocol.send(PackageType.PKG_HANDSHAKE, bytes);
            this.callback = callback;
        }
    }
}

