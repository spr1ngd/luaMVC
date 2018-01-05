namespace Pomelo.DotNetClient
{
    using SimpleJson;
    using System;
    using System.Net;
    using System.Net.Sockets;

    public class PomeloClient : IDisposable
    {
        private bool disposed;
        private EventManager eventManager = new EventManager();
        private Protocol protocol;
        private uint reqId = 1;
        private Socket socket;

        public PomeloClient(string host, int port)
        {
            this.initClient(host, port);
            this.protocol = new Protocol(this, this.socket);
        }

        public void connect()
        {
            this.protocol.start(null, null);
        }

        public void connect(JsonObject user)
        {
            this.protocol.start(user, null);
        }

        public void connect(Action<JsonObject> handshakeCallback)
        {
            this.protocol.start(null, handshakeCallback);
        }

        public bool connect(JsonObject user, Action<JsonObject> handshakeCallback)
        {
            try
            {
                this.protocol.start(user, handshakeCallback);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return false;
            }
        }

        public void disconnect()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                this.protocol.close();
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
                this.disposed = true;
            }
        }

        private void initClient(string host, int port)
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(host), port);
            try
            {
                this.socket.Connect(remoteEP);
            }
            catch (SocketException exception)
            {
                Console.WriteLine(string.Format("unable to connect to server: {0}", exception.ToString()));
            }
        }

        public void notify(string route, JsonObject msg)
        {
            this.protocol.send(route, msg);
        }

        public void on(string eventName, Action<JsonObject> action)
        {
            this.eventManager.AddOnEvent(eventName, action);
        }

        internal void processMessage(Message msg)
        {
            if (msg.type == MessageType.MSG_RESPONSE)
            {
                this.eventManager.InvokeCallBack(msg.id, msg.data);
            }
            else if (msg.type == MessageType.MSG_PUSH)
            {
                this.eventManager.InvokeOnEvent(msg.route, msg.data);
            }
        }

        public void request(string route, Action<JsonObject> action)
        {
            this.request(route, new JsonObject(), action);
        }

        public void request(string route, JsonObject msg, Action<JsonObject> action)
        {
            this.eventManager.AddCallBack(this.reqId, action);
            this.protocol.send(route, this.reqId, msg);
            this.reqId++;
        }
    }
}

