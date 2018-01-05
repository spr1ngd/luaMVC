
using System;
using System.Collections.Generic;

namespace PureMVC.Patterns.Lua
{
    public interface ILuaMediator 
    {
        string NAME { get; set; }
        IList<string> ListNotificationInterests { get; set; }
        HandleNotification HandleNotification { get; set; }
        Action OnRegister { get; set; }
        Action OnRemove { get; set; }
    }

    public class LuaMediator : ILuaMediator
    {
        public string NAME { get; set; }
        public IList<string> ListNotificationInterests { get; set; }
        public HandleNotification HandleNotification { get; set; } 
        public Action OnRegister { get; set; }
        public Action OnRemove { get; set; }
    }
}
