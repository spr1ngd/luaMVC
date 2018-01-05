
namespace LuaMVC
{
    /// <summary>
    /// 将数据抽象到json文本中进行管理，方便进行后期的管理
    /// </summary>
	public class LuaMVCConfig
    {
        // 如果是开发将这个选项设置为true，如果是发布版本设置为false 框架内部会自动划分不同的API
        public const bool IsDeveloping = true;

        // 是否进行自动资源更新，自动更新仅包含lua脚本的更新，资源的更新会在加载资源时根据hash值从服务器进行更新
        public const bool AutomaticUpdateLuaScriptsFromServer = false;

        // 服务器资源的路径 用于自动更新服务器资源 todo 这个值应该由服务器传递到客户端，或者也是直接在json中配置
        public const string StaticFileServerPath = "http://192.137.1.113:8088/StreamingAssets/";

        // 服务器ip地址：端口号
        public const string ServerIP = "10.177.130.88";
        public const int ServerPort = 34341; 
        public static string ServerIPAddress {get{return ServerIP +":"+ ServerPort;}}
    }
} 