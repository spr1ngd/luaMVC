

using System;
using System.Collections.Generic;

namespace PureMVC.Patterns.Lua
{
    public interface ILuaHandler
    {
        string NAME { get; set; }
        IList<string> ListNotificationInterests { get; set; }
        HandleNotification Request { get; set; }
        Action OnRegister { get; set; }
        Action OnRemove { get; set; }
    }

    public class LuaHandler : ILuaHandler
    {
        public string NAME { get; set; }
        public IList<string> ListNotificationInterests { get; set; }
        public HandleNotification Request { get; set; }
        public Action OnRegister { get; set; }
        public Action OnRemove { get; set; }
    }
}