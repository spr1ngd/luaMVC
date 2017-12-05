
namespace PureMVC.Patterns.Lua
{
    public interface ILuaCommnad
    {
        string CommandName { get; set; }
        HandleNotification Execute { get; set; }
    }

    public class LuaCommnad : ILuaCommnad
    {
        public string CommandName { get; set; }
        public HandleNotification Execute { get; set; }
    }
}