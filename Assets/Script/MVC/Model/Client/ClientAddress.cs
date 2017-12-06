
namespace Game
{
    public class ServerIPAddress
    {
        public string host = "127.0.0.1";
        public int port = 3344;

        public ServerIPAddress(){}
        public ServerIPAddress( string host,int port )
        {
            this.host = host;
            this.port = port;
        }
        public ServerIPAddress( string host , string port )
        {
            this.host = host;
            this.port = int.Parse(port);
        }
    }

    public class IPAddress
    {
        public string address = null;
        public ServerIPAddress ipAdress = null;
    }
}