

namespace LuaMVC
{
    // 负责记录与服务器交互的Notification.Name
    public partial class NotificationType 
    {
        // Client To Server
        public const string SERVICE_CONNECTSERVER = "ConnectServer";
        public const string SERVICE_DISCONNECTSERVER = "DisconnectServer";
        
        // 
        public const string SERVICE_REGISTER = "Register";
        public const string SERVICE_LOGIN = "Login";
        public const string SERVICE_DELETE = "Delete";
        public const string SERVICE_LOGOUT = "Logout";
        public const string SERVICE_MODIFY_PASSWORD = "ModifyPassword";

        // Login
        public const string SERVICE_LOGIN_SUCCESS = "LoginSuccess";
        public const string SERVICE_LOGIN_FAILED = "LoginFailed";
        // Register
        public const string SERVICE_REGISTER_SUCCESS = "RegisterSuccess";
        public const string SERVICE_REGISTER_FAILED = "RegisterFailed";

        // Data
        public const string SERVICE_SUBMIT_PLAYERINFO = "SubmitPlayerInfo";
    }
}