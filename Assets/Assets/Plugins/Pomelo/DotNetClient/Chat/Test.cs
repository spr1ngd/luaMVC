namespace Pomelo.DotNetClient.Chat
{
    using Pomelo.DotNetClient;
    using SimpleJson;
    using System;
    using System.Threading;

    internal class Test
    {
        private static PomeloClient pc;

        public static void kick()
        {
            pc.request("connector.entryHandler.onUserLeave", data => Console.WriteLine("userLeave " + data));
        }

        public static void login()
        {
            JsonObject msg = new JsonObject();
            msg["username"] = "zxg" + DateTime.Now.Millisecond;
            msg["rid"] = "public";
            pc.request("connector.entryHandler.enter", msg, data => Console.WriteLine("Login " + data));
        }

        public static void Main()
        {
            Console.WriteLine("before init");
            pc = new PomeloClient("127.0.0.1", 0xbc6);
            Console.WriteLine("before connect");
            pc.connect(null, delegate (JsonObject data) {
                Console.WriteLine("after connect " + data.ToString());
                JsonObject msg = new JsonObject();
                msg["uid"] = 1;
                pc.request("gate.gateHandler.queryEntry", msg, new Action<JsonObject>(Test.onQuery));
            });
            while (true)
            {
                string message = Console.ReadLine();
                if (message != null)
                {
                    send(message);
                }
                Thread.Sleep(100);
            }
        }

        public static void onQuery(JsonObject result)
        {
            if (Convert.ToInt32(result["code"]) == 200)
            {
                string host = (string) result["host"];
                int port = Convert.ToInt32(result["port"]);
                pc.disconnect();
                startConnect(host, port);
            }
        }

        public static void send(string message)
        {
            JsonObject msg = new JsonObject();
            msg["content"] = message;
            msg["target"] = "*";
            pc.notify("chat.chatHandler.send", msg);
        }

        private static void startConnect(string host, int port)
        {
            pc = new PomeloClient(host, port);
            pc.connect(null, delegate (JsonObject data) {
                pc.on("onChat", msg => Console.WriteLine(msg["from"] + " : " + msg["msg"]));
                pc.on("onAdd", msg => Console.WriteLine("onAdd : " + msg));
                pc.on("onLeave", msg => Console.WriteLine("onLeave : " + msg));
                pc.on("disconnect", msg => Console.WriteLine("disconnect : " + msg));
                Console.WriteLine("connect to connector " + data.ToString());
                login();
            });
        }
    }
}

