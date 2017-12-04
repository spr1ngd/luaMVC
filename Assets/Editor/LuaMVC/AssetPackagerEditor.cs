
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
            AssetDatabase.Refresh();
        }

        #endregion

        #region Pack for lua

        #region MenuItems
        [MenuItem("LuaMVC/Assetbundle/Pack Window Resource")]
        public static void PackWindowResource()
        {
            PackAssetbundle(BuildTarget.StandaloneWindows64);
        }
        [MenuItem("LuaMVC/Assetbundle/Pack IOS Resource")]
        public static void PackIOSResource()
        {
            PackAssetbundle(BuildTarget.iOS);
        }
        [MenuItem("LuaMVC/Assetbundle/Pack Android Resource")]
        public static void PackAndroidResource()
        {
            PackAssetbundle(BuildTarget.Android);
        }
        #endregion

        private static List<AssetBundleBuild> buildItems = new List<AssetBundleBuild>();

        private static void PackAssetbundle(BuildTarget buildTarget)
        {
            //  1.自动打包现有的预制物
            //  2.根据选择判断是否将lua脚本打包为assetbundle包 / 或者直接拷贝代码
            //  3.生成md5值表，用于版本更新校验
            // todo 打包预制物
            string md5listPath = Application.streamingAssetsPath + "/md5list.txt";
            StreamWriter write = new StreamWriter(md5listPath);
            string prefabsPath = Application.dataPath + "/Resources/Prefabs/";
            if (Directory.Exists(prefabsPath))
            {
                buildItems.Clear();
                PackPrefab(prefabsPath);
                for (int i = 0; i < buildItems.Count; i++)
                {
                    BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath + "/Assetbundle/",
                        new AssetBundleBuild[] { buildItems[i] }, BuildAssetBundleOptions.None, buildTarget);
                }
                // write md5list file.
                WriteMD5(Application.streamingAssetsPath + "/Assetbundle", write);
                buildItems.Clear();
            }

            string luaScriptPath = Application.dataPath + "/Script/Resources/";
            if (Directory.Exists(luaScriptPath))
            {
                buildItems.Clear();
                PackLuaScript(luaScriptPath);
                for (int i = 0; i < buildItems.Count; i++)
                {
                    BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath + "/Lua/",
                        new AssetBundleBuild[] { buildItems[i] }, BuildAssetBundleOptions.None, buildTarget);
                }
                WriteMD5(Application.streamingAssetsPath + "/Lua", write);
                buildItems.Clear();
            }
            write.Dispose();
        }

        // 生成ab包
        private static void PackPrefab(string dirPath)
        {
            string[] filesPath = Directory.GetFiles(dirPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(filesPath[i]);
                if (fileInfo.Extension.Equals(".prefab"))
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
            string[] childrenPath = Directory.GetDirectories(dirPath);
            for (int i = 0; i < childrenPath.Length; i++)
                PackPrefab(childrenPath[i]);
        }

        // 导出md5值
        private static void WriteMD5(string dirPath,StreamWriter write)
        {
            string[] filesPath = Directory.GetFiles(dirPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(filesPath[i]);
                if (fileInfo.Extension.Contains(".unity3d") || fileInfo.Extension.Contains(".manifest"))
                {
                    string fileName = fileInfo.Name;
                    string md5Value = Util.md5file(filesPath[i]);
                    write.WriteLine(fileName + "|" + md5Value);
                }
            }
            string[] childrenPath = Directory.GetDirectories(dirPath);
            for (int i = 0; i < childrenPath.Length; i++)
                WriteMD5(childrenPath[i],write);
        }

        private static void PackLuaScript( string luaPath )
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
                PackLuaScript(childrenPath[i]);
        }

        #endregion
    }
}