
using System.Linq;
using Game;
using UnityEngine;
using System.Collections.Generic;

namespace LuaMVC.Editor
{
    using UnityEditor;

    public class AssetPackagerEditor : EditorWindow
    {
        private static BuildTarget currentBuildTarget = BuildTarget.StandaloneWindows64;
        private static BuildAssetBundleOptions currentBuildOptions = BuildAssetBundleOptions.ChunkBasedCompression;
        private static string assetbundleName = "AssetbundleName";
        private static int packageMode = 0;

        private void OnGUI()
        {
            EditorGUILayout.Space();
            currentBuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("选择打包平台", currentBuildTarget);
            currentBuildOptions = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup("打包选项", currentBuildOptions);
            packageMode = GUILayout.Toolbar(packageMode, new string[] {"打包全部", "打包选中"});
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

        [MenuItem("LuaMVC/Assetbundle/Tool Window")]
        private static void OpenPackagerWindow()
        {
            GetWindowWithRect<AssetPackagerEditor>(new Rect(Screen.width/2.0f,Screen.height/2.0f,325,135), true, "资源打包工具", true);
            if( null != Selection.activeObject)
                assetbundleName = Selection.activeObject.name;
        }

        public static void PackAll()
        {
            BuildPipeline.BuildAssetBundles(FilePath.normalPath , currentBuildOptions, currentBuildTarget);
            AssetDatabase.Refresh();
        }

        public static void PackSelected()
        {
            Object[] selections = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = assetbundleName;
            build.assetNames = new string[selections.Length];
            for (int i = 0; i < selections.Length; i++)
                build.assetNames[i] = AssetDatabase.GetAssetPath(selections[i]);
            IList<AssetBundleBuild> builds = new List<AssetBundleBuild>() { build };
            BuildPipeline.BuildAssetBundles(FilePath.normalPath + "Assetbundle/", builds.ToArray(), currentBuildOptions, currentBuildTarget);
            AssetDatabase.Refresh();
        }
    }
}