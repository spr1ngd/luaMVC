
using System;

namespace PureMVC.Patterns.Lua
{
    public interface ILuaProxy
    {
        string NAME { get; set; }
        Action OnRegister { get; set; }
        Action OnRemove { get; set; }
    } 

    public class LuaProxy : ILuaProxy
    {
        public string NAME { get; set; }
        public Action OnRegister { get; set; }
        public Action OnRemove { get; set; }
    }
}