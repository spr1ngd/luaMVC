
using UnityEditor;
using UnityEngine;

namespace LuaMVC.Editor
{
    public class QuicklyTest
    {
        [MenuItem("LuaMVC/TestButton")]
        public static void Test()
        {
            // unity 固定路径测试
            Debug.Log("Application.streamingAssetsPath : " + Application.streamingAssetsPath);
            Debug.Log("Application.dataPath : " + Application.dataPath);
            Debug.Log("Application.persistentDataPath : " + Application.persistentDataPath);
            Debug.Log("Application.temporaryCachePath : " + Application.temporaryCachePath);
        }
    }
}