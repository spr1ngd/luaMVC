
using System.Collections.Generic;
using Pomelo.DotNetClient;

namespace PureMVC.Patterns
{
    public interface IHandler
    {
        string HandlerName { get; }
        IList<string> HandleNotification();
        void Request(INotification notification);
        void Response();
        void OnRegister();
        void OnRemove();
    }

    public class Handler : IHandler
    {
        protected string m_handlerName;
        public string HandlerName
        {
            get { return m_handlerName; }
        }
        public const string NAME = "Handler";

        protected IProxy Proxy { get; set; }

        // todo 这里需要考虑如果更改为其他服务器，我们也应该考虑将IP:Port的方式封装为pomeloClient这中简单的接口，供客户端直接调用。
        private static PomeloClient m_pomeloClient = null;
        protected static PomeloClient pomeloClient
        {
            get
            {
                if( null == m_pomeloClient )
                    m_pomeloClient = new PomeloClient("127.0.0.1", 3344);
                return m_pomeloClient;
            }
            set { m_pomeloClient = value; }
        }

        public Handler(){}
        public Handler( string name )
        {
            m_handlerName = name;
        } 
        public Handler( string name , IProxy proxy )
        {
            m_handlerName = name;
            Proxy = proxy;
        }

        public virtual IList<string> HandleNotification()
        {
            return new List<string>();
        }
        public virtual void Request(INotification notification)
        {

        }
        public virtual void Response()
        {

        }

        public virtual void OnRegister()
        {

        }
        public virtual void OnRemove()
        {

        }
    }
}