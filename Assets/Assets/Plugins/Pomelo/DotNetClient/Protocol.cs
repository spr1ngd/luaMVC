namespace Pomelo.DotNetClient
{
    using SimpleJson;
    using System;
    using System.Net.Sockets;
    using System.Text;

    public class Protocol
    {
        private HandShakeService handshake;
        private HeartBeatService heartBeatService;
        private MessageProtocol messageProtocol;
        private PomeloClient pc;
        private ProtocolState state;
        private Transporter transporter;

        public Protocol(PomeloClient pc, Socket socket)
        {
            this.pc = pc;
            this.transporter = new Transporter(socket, new Action<byte[]>(this.processMessage));
            this.handshake = new HandShakeService(this);
            this.state = ProtocolState.start;
        }

        internal void close()
        {
            this.transporter.close();
            this.heartBeatService.stop();
            this.state = ProtocolState.closed;
        }

        private void processHandshakeData(JsonObject msg)
        {
            if ((!msg.ContainsKey("code") || !msg.ContainsKey("sys")) || (Convert.ToInt32(msg["code"]) != 200))
            {
                throw new Exception("Handshake error! Please check your handshake config.");
            }
            JsonObject obj2 = (JsonObject) msg["sys"];
            JsonObject dict = new JsonObject();
            if (obj2.ContainsKey("dict"))
            {
                dict = (JsonObject) obj2["dict"];
            }
            JsonObject obj4 = new JsonObject();
            JsonObject serverProtos = new JsonObject();
            JsonObject clientProtos = new JsonObject();
            if (obj2.ContainsKey("protos"))
            {
                obj4 = (JsonObject) obj2["protos"];
                serverProtos = (JsonObject) obj4["server"];
                clientProtos = (JsonObject) obj4["client"];
            }
            this.messageProtocol = new MessageProtocol(dict, serverProtos, clientProtos);
            int timeout = 0;
            if (obj2.ContainsKey("heartbeat"))
            {
                timeout = Convert.ToInt32(obj2["heartbeat"]);
            }
            this.heartBeatService = new HeartBeatService(timeout, this);
            if (timeout > 0)
            {
                this.heartBeatService.start();
            }
            this.handshake.ack();
            this.state = ProtocolState.working;
            JsonObject data = new JsonObject();
            if (msg.ContainsKey("user"))
            {
                data = (JsonObject) msg["user"];
            }
            this.handshake.invokeCallback(data);
        }

        internal void processMessage(byte[] bytes)
        {
            Package package = PackageProtocol.decode(bytes);
            if ((package.type == PackageType.PKG_HANDSHAKE) && (this.state == ProtocolState.handshaking))
            {
                Encoding.UTF8.GetString(package.body);
                JsonObject msg = (JsonObject) SimpleJson.DeserializeObject(Encoding.UTF8.GetString(package.body));
                this.processHandshakeData(msg);
                this.state = ProtocolState.working;
            }
            else if ((package.type == PackageType.PKG_HEARTBEAT) && (this.state == ProtocolState.working))
            {
                this.heartBeatService.resetTimeout();
            }
            else if ((package.type == PackageType.PKG_DATA) && (this.state == ProtocolState.working))
            {
                this.heartBeatService.resetTimeout();
                this.pc.processMessage(this.messageProtocol.decode(package.body));
            }
            else if (package.type == PackageType.PKG_KICK)
            {
                this.close();
            }
        }

        internal void send(PackageType type)
        {
            if (this.state != ProtocolState.closed)
            {
                this.transporter.send(PackageProtocol.encode(type));
            }
        }

        internal void send(PackageType type, JsonObject msg)
        {
            if (type != PackageType.PKG_DATA)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(msg.ToString());
                this.send(type, bytes);
            }
        }

        internal void send(PackageType type, byte[] body)
        {
            if (this.state != ProtocolState.closed)
            {
                byte[] buffer = PackageProtocol.encode(type, body);
                this.transporter.send(buffer);
            }
        }

        internal void send(string route, JsonObject msg)
        {
            this.send(route, 0, msg);
        }

        internal void send(string route, uint id, JsonObject msg)
        {
            if (this.state == ProtocolState.working)
            {
                byte[] body = this.messageProtocol.encode(route, id, msg);
                this.send(PackageType.PKG_DATA, body);
            }
        }

        internal void start(JsonObject user, Action<JsonObject> callback)
        {
            this.handshake.request(user, callback);
            this.transporter.start();
            this.state = ProtocolState.handshaking;
        }
    }
}

