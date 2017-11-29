
namespace Game
{
    public partial class NotificationType
    {
        // 系统级别命令
        public const string START = "StartUp";
        public const string QIUT = "ShutDown";
        public const string GAMEOVER = "GameOver";
        
        // MESSAGE 消息通知
        public const string MESSAGE_BUILDIN = "BuildInMessage";
        public const string MESSAGE_ANDROID = "AndroidMessage";
        public const string MESSAGE_IOS = "IOSMessage";
    }
}