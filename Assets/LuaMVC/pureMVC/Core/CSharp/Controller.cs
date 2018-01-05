
namespace PureMVC.Core
{
    using Patterns;
    using System.Collections.Generic;

    public interface IController
    {
        bool HasCommand(string notificationName);
        void RemoveCommand(string notificationName);
        void RegisterCommand(string notificationName, ICommand command);
        void RegisterObserver(string notificationName, IObserver observer);
        void RemoveObserver(string notificationName, IObserver observer);
    }

    public class Controller : IController
    {
        protected static volatile IController m_instance;
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();
        protected IView m_view;
        public static IController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new Controller();
                        }
                    }
                }
                return m_instance;
            }
        }
        private IDictionary<string, ICommand> m_commands = new Dictionary<string, ICommand>();

        protected Controller()
        {
            this.InitializeController();
        }
        protected virtual void InitializeController()
        {
            this.m_view = View.Instance;
        }
        
        public virtual bool HasCommand(string notificationName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_commands.ContainsKey(notificationName);
            }
        }
        public virtual void RemoveCommand(string notificationName)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_commands.ContainsKey(notificationName))
                {
                    RemoveObserver(notificationName, new Observer(m_commands[notificationName].Execute));
                    this.m_commands.Remove(notificationName);
                }
            }
        }
        public void RegisterCommand(string notificationName, ICommand command)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_commands.ContainsKey(notificationName))
                {
                    m_commands.Add(notificationName,command);
                    RegisterObserver(notificationName,new Observer(command.Execute));
                }
            }
        }

        public void RegisterObserver(string notificationName, IObserver observer)
        {
            Observers.Instance.RegisterObserver(notificationName, observer);
        }
        public void RemoveObserver(string notificationName, IObserver observer)
        {
            Observers.Instance.RemoveObserver(notificationName, observer);
        }
    }
}