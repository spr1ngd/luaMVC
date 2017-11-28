
using UnityEngine;

namespace Game
{
    public class FilePath
    {
        public static string prePath
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
            }
        }

        public static string fullPath( string fileName )
        {
            return prePath + fileName;
        }

        // configuration file path
        public static string JsonConfigFilePath( string fileName )
        {
            return prePath + "Configuration/Json/" + fileName;
        }
        public static string XmlConfigFilePath( string fileName )
        {
            return prePath + "Configuration/Xml/" + fileName;
        }
        public static string ProtobufConfigFilePath( string fileName )
        {
            return prePath + "Configuration/Protobuf/" + fileName;
        }
    }
}