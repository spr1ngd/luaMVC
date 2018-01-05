
using UnityEngine;

namespace LuaMVC
{
    public class FilePath
    {
        public static string prePath
        {
            get
            {
#if UNITY_EDITOR 
                return "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_STANDALONE_WIN
                return "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROUD
                return "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                return Application.dataPath + "/Raw/";
#endif
                return "";
            }
        }

        public static string normalPath 
        {
            get
            {
#if UNITY_EDITOR 
                return  Application.dataPath + "/StreamingAssets/";
#elif UNITY_STANDALONE_WIN
                return "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROUD
                return "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                return Application.dataPath + "/Raw/";
#endif 
                return "";
            }
        }

        public static string fullPath( string fileName )
        {
            return prePath + fileName;
        }

        //todo 这是在pc端，可以保持正常，需要在安卓机进行调试
        // configuration file path
        public static string JsonConfigFilePath( string fileName )
        {
            return normalPath + "Configuration/Json/" + fileName;
        }
        public static string XmlConfigFilePath( string fileName )
        {
            return normalPath + "Configuration/Xml/" + fileName;
        }
        public static string ProtobufConfigFilePath( string fileName )
        {
            return normalPath + "Configuration/Protobuf/" + fileName;
        }
    }
}