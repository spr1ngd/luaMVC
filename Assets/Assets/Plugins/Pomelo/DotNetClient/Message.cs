namespace Pomelo.DotNetClient
{
    using SimpleJson;
    using System;

    public class Message
    {
        public JsonObject data;
        public uint id;
        public string route;
        public MessageType type;

        public Message(MessageType type, uint id, string route, JsonObject data)
        {
            this.type = type;
            this.id = id;
            this.route = route;
            this.data = data;
        }
    }
}

