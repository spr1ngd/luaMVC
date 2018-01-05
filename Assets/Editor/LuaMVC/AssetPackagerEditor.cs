
using System.Linq;
using Game;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace LuaMVC.Editor
{
    using UnityEditor;

    public class AssetPackagerEditor : EditorWindow
    {
        #region Base Packager

        private static BuildTarget currentBuildTarget = BuildTarget.StandaloneWindows64;
        private static BuildAssetBundleOptions currentBuildOptions = BuildAssetBundleOptions.ChunkBasedCompression;
        private static string assetbundleName = "AssetbundleName";
        private static int packageMode = 0;

        [MenuItem("LuaMVC/Assetbundle/Tool Window")]
        private static void OpenPackagerWindow()
        {
            GetWindowWithRect<AssetPackagerEditor>(new Rect(Screen.width / 2.0f, Screen.height / 2.0f, 325, 135), true, "资源打包工具", true);
            if (null != Selection.activeObject)
                assetbundleName = Selection.activeObject.name;
        }
        private void OnGUI()
        {
            EditorGUILayout.Space();
            currentBuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("选择打包平台", currentBuildTarget);
            currentBuildOptions = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup("打包选项", currentBuildOptions);
            packageMode = GUILayout.Toolbar(packageMode, new string[] { "打包全部", "打包选中" });
            if (packageMode.Equals(0))
            {
                if (GUILayout.Button("打包"))
                {
                    PackAll();
                    Close();
                }
                if (GUILayout.Button("取消"))
                    Close();
            }
            if (packageMode.Equals(1))
            {
                assetbundleName = EditorGUILayout.TextField("资源名称", assetbundleName);
                if (GUILayout.Button("打包"))
                {
                    PackSelected();
                    Close();
                }
                if (GUILayout.Button("取消"))
                    Close();
            }
        }

        public static void PackAll()
        {
            BuildPipeline.BuildAssetBundles(FilePath.normalPath, currentBuildOptions, currentBuildTarget);
            WriteMD5(FilePath.normalPath);
            AssetDatabase.Refresh();
        }
        public static void PackSelected()
        {
            Object[] selections = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = assetbundleName;
            build.assetNames = new string[selections.Length];
            for (int i = 0; i < selections.Length; i++)
            {
                Debug.Log(AssetDatabase.GetAssetPath(selections[i]));
                build.assetNames[i] = AssetDatabase.GetAssetPath(selections[i]);
            }
            IList<AssetBundleBuild> builds = new List<AssetBundleBuild>() { build };
            BuildPipeline.BuildAssetBundles(FilePath.normalPath + "Assetbundle/", builds.ToArray(), currentBuildOptions, currentBuildTarget);
            WriteMD5(FilePath.normalPath + "Assetbundle/");
            AssetDatabase.Refresh();
        }

        #endregion

        #region Pack for lua

        #region Menu button items
        [MenuItem("LuaMVC/Pack Assetbundle/Windows64")]
        public static void PackWindowResource()
        {
            PackAssetbundle(BuildTarget.StandaloneWindows64);
        }
        [MenuItem("LuaMVC/Pack Assetbundle/Android")]
        public static void PackAndroidResource()
        {
            PackAssetbundle(BuildTarget.Android);
        }
        [MenuItem("LuaMVC/Pack Assetbundle/iOS")]
        public static void PackIOSResource()
        {
            PackAssetbundle(BuildTarget.iOS);
        }
        [MenuItem("LuaMVC/Pack Lua Scripts/Windows64")]
        public static void PackWindowLuaScripts()
        {
            PackLua(BuildTarget.StandaloneWindows64);
        }
        [MenuItem("LuaMVC/Pack Lua Scripts/Android")]
        public static void PackAndroidLuaScripts()
        {
            PackLua(BuildTarget.Android);
        }
        [MenuItem("LuaMVC/Pack Lua Scripts/iOS")]
        public static void PackiOSLuaScripts()
        {
            PackLua(BuildTarget.iOS);
        }
        [MenuItem("LuaMVC/Pack Assetbundle And Lua Scripts/Windows64")]
        public static void PackWindowAssetbundleAndLuaScripts()
        {
            PackAssetbundleAndLuaScript(BuildTarget.StandaloneWindows64);
        }
        [MenuItem("LuaMVC/Pack Assetbundle And Lua Scripts/Android")]
        public static void PackAndroidAssetbundleAndLuaScripts()
        {
            PackAssetbundleAndLuaScript(BuildTarget.Android);
        }
        [MenuItem("LuaMVC/Pack Assetbundle And Lua Scripts/iOS")]
        public static void PackiOSAssetbundleAndLuaScripts()
        {
            PackAssetbundleAndLuaScript(BuildTarget.iOS);
        }
        #endregion

        private static List<AssetBundleBuild> buildItems = new List<AssetBundleBuild>();
        private static string prefabsPath = Application.dataPath + "/Resources/Prefabs/";
        private static string prefabsTargetPath = Application.streamingAssetsPath + "/Assetbundle/";

        private static string luaScriptPath = Application.dataPath + "/Script/Resources/"; // todo 考虑更改
        private static string luaScriptTargetPath = Application.streamingAssetsPath + "/Lua/";

        private static void PackAssetbundleAndLuaScript(BuildTarget buildTarget)
        {
            PackPrefab(buildTarget);
            PackLua(buildTarget);
        }

        // 打包ab资源
        private static void PackAssetbundle(BuildTarget buildTarget)
        {
            PackPrefab(buildTarget);
        }
        private static void PackPrefab(BuildTarget buildTarget)
        {
            //if (Directory.Exists(prefabsPath))
            //{
            //    buildItems.Clear();
            //    RecursionPrefab(prefabsPath);
            //    for (int i = 0; i < buildItems.Count; i++)
            //        BuildPipeline.BuildAssetBundles(prefabsTargetPath, new AssetBundleBuild[] { buildItems[i] }, BuildAssetBundleOptions.None, buildTarget);
            //    buildItems.Clear();
            //    AssetDatabase.Refresh();
            //}
            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath+"/", BuildAssetBundleOptions.None, buildTarget);
            AssetDatabase.Refresh();
        }
        private static void RecursionPrefab(string dirPath)
        {
            string[] filesPath = Directory.GetFiles(dirPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(filesPath[i]);
                if (fileInfo.Extension.Equals(".prefab"))
                {
                    AssetBundleBuild build = new AssetBundleBuild();
                    build.assetBundleName = fileInfo.Name.Replace(".prefab", "") + ".unity3d";
                    build.assetNames = new string[1];
                    string assetName = fileInfo.FullName.Replace('\\', '/');
                    assetName = assetName.Replace(Application.dataPath, "Assets");
                    build.assetNames[0] = assetName;
                    buildItems.Add(build);
                }
            }
            string[] childrenPath = Directory.GetDirectories(dirPath);
            for (int i = 0; i < childrenPath.Length; i++)
                RecursionPrefab(childrenPath[i]);
        }

        // 打包lua脚本
        private static void PackLua(BuildTarget buildTarget)
        {
            if (Directory.Exists(luaScriptPath))
            {
                // 分文件打包为ab资源包
                //buildItems.Clear();
                //RecursionLuaScript(luaScriptPath);
                //for (int i = 0; i < buildItems.Count; i++)
                //    BuildPipeline.BuildAssetBundles(luaScriptTargetPath, new AssetBundleBuild[] { buildItems[i] }, BuildAssetBundleOptions.None, buildTarget);
                //WriteMD5(luaScriptTargetPath);
                //buildItems.Clear();
                //AssetDatabase.Refresh();
                // 直接拷贝lua文件到目标文件夹
                RecursionCopyLuaScript(luaScriptPath);
                WriteMD5(luaScriptTargetPath);
                AssetDatabase.Refresh();
            }
        }
        private static void RecursionLuaScript(string luaPath)
        {
            string[] filesPath = Directory.GetFiles(luaPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(filesPath[i]);
                if (fileInfo.Name.Contains(".lua") || fileInfo.Name.Contains(".txt"))
                {
                    AssetBundleBuild build = new AssetBundleBuild();
                    build.assetBundleName = fileInfo.Name + ".unity3d";
                    build.assetNames = new string[1];
                    string assetName = fileInfo.FullName.Replace('\\', '/');
                    assetName = assetName.Replace(Application.dataPath, "Assets");
                    build.assetNames[0] = assetName;
                    buildItems.Add(build);
                }
            }
            string[] childrenPath = Directory.GetDirectories(luaPath);
            for (int i = 0; i < childrenPath.Length; i++)
                RecursionLuaScript(childrenPath[i]);
        }

        private static void RecursionCopyLuaScript(string luaPath)
        {
            string[] filesPath = Directory.GetFiles(luaPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(filesPath[i]);
                if ((fileInfo.Name.Contains(".lua") || fileInfo.Name.Contains(".lua.txt")) && !fileInfo.Name.Contains(".meta"))
                {
                    Debug.Log(luaScriptTargetPath + fileInfo.Name);
                    File.Copy(fileInfo.FullName, luaScriptTargetPath + fileInfo.Name, true);
                }
            }
            string[] childrenPath = Directory.GetDirectories(luaPath);
            for (int i = 0; i < childrenPath.Length; i++)
                RecursionCopyLuaScript(childrenPath[i]);
        }

        // 打包lua md5 checklist
        private static void WriteMD5(string dirPath)
        {
            using (StreamWriter write = File.AppendText(Application.dataPath + "/StreamingAssets/md5list.txt"))
            {
                RecursionWriteMD5(dirPath, write);
            }
        }
        private static void RecursionWriteMD5(string dirPath, StreamWriter write)
        {
            string[] filesPath = Directory.GetFiles(dirPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(filesPath[i]);
                // 打包 ab 包的做法
                //if (fileInfo.Extension.Contains(".unity3d") || fileInfo.Extension.Contains(".manifest") || fileInfo.Extension.Equals(""))
                //{
                //    // todo 这里考虑把路径展示出来 // 或者只记录名字直接在manifest文件中读取路径
                //    string fileName = fileInfo.Name;
                //    string md5Value = Util.md5file(filesPath[i]);
                //    write.WriteLine(fileName + "|" + md5Value);
                //}
                if ((fileInfo.Name.Contains(".lua") || fileInfo.Name.Contains(".lua.txt")) && !fileInfo.Name.Contains(".meta"))
                {
                    string fileName = fileInfo.Name;
                    string md5Value = Util.md5file(filesPath[i]);
                    write.WriteLine(fileName + "|" + md5Value);
                }
            }
            string[] childrenPath = Directory.GetDirectories(dirPath);
            for (int i = 0; i < childrenPath.Length; i++)
                RecursionWriteMD5(childrenPath[i], write);
        }

        #endregion
    }
}