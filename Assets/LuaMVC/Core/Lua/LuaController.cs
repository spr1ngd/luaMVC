
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Patterns.Lua;

namespace PureMVC.Core.Lua
{
    public interface ILuaController
    {
        void RegisterCommand( ILuaCommnad luaCommand);
        void RemoveCommand(string luaCommandName);
        bool HasCommnad(string luaCommandName);
        void RegisterObserver(string notificationName, IObserver luaObserver);
        void RemoveObserver(string notificationName, IObserver luaObserver);
    }

    public class LuaController : ILuaController
    {
        private static volatile ILuaController m_luaController = null;
        private static readonly object m_syncStaticRoot = new object();
        private readonly object m_syncRoot = new object();
        private IDictionary<string, IList<ILuaCommnad>> m_luaCommands = new Dictionary<string, IList<ILuaCommnad>>();
        public static ILuaController Instance
        {
            get
            {
                if (null == m_luaController)
                {
                    lock ( m_syncStaticRoot )
                    {
                        m_luaController = new LuaController();
                    }
                }
                return m_luaController;
            }
        }

        public void RegisterObserver(string notificationName, IObserver luaObserver)
        {
            Observers.Instance.RegisterObserver(notificationName, luaObserver);
        }
        public void RemoveObserver(string notificationName, IObserver luaObserver)
        {
            Observers.Instance.RemoveObserver(notificationName, luaObserver);
        }

        public void RegisterCommand( ILuaCommnad luaCommand)
        {
            lock ( m_syncRoot )
            {
                if(!m_luaCommands.ContainsKey(luaCommand.CommandName))
                    m_luaCommands[luaCommand.CommandName] = new List<ILuaCommnad>();
                m_luaCommands[luaCommand.CommandName].Add(luaCommand);
                RegisterObserver(luaCommand.CommandName, new Observer(luaCommand.Execute));
            }
        }
        public void RemoveCommand(string luaCommandName)
        {
            lock (m_syncRoot)
            {
                if (m_luaCommands.ContainsKey(luaCommandName))
                {
                    var luaCommands = m_luaCommands[luaCommandName];
                    for (int i = 0; i < luaCommands.Count; i++)
                        RemoveObserver(luaCommandName, new Observer(luaCommands[i].Execute));
                    m_luaCommands.Remove(luaCommandName);
                }
            }
        }
        public bool HasCommnad(string luaCommandName)
        {
            lock (m_syncRoot)
            {
                if (m_luaCommands.ContainsKey(luaCommandName))
                    return true;
                return false;
            }
        }
    }
}