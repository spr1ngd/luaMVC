
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Game;

namespace LuaMVC
{
    using UnityEngine;

    public class AssetLoader
    {
        private static WWW loader = null;

        public static IEnumerator LoadAsset<T>(string url, Action<T> callback) where T : class
        {
            using (loader = new WWW(url))
            {
                yield return loader;
                if (null != loader.error)
                    Debug.Log(loader.error);
                if (loader.isDone)
                {
                    T obj = loader.bytes as T;
                    callback(obj);
                }
            }
        }

        public static void LoadAudioClip(string audioPath, Action<AudioClip> callback)
        {
            string url = FilePath.prePath + audioPath;
            Coroutiner.Instance.StartCoroutine(LoadAsset(url, callback));
        }

        public static void LoadAssetText(string textPath, Action<string> callback)
        {
            string url = FilePath.prePath + textPath;
            Coroutiner.Instance.StartCoroutine(LoadAsset(url, callback));
        }

        private string localMD5ListPath = Application.streamingAssetsPath + "/md5list.txt";

        public void AutomaticUpdateAssetsFromAssetServer()
        {
            // 1.加载本地md5list
            var localMD5List = GetMD5List(File.ReadAllLines(localMD5ListPath));
            Coroutiner.Instance.StartCoroutine(LoadMD5ListFromServer(localMD5List));
        }

        private IEnumerator LoadMD5ListFromServer(List<MD5ListItem> localMD5List)
        {
            // 2.加载服务器md5list
            string remoteMD5ListUrl = "http://192.168.1.113:8088/md5list.txt";
            WWW www = new WWW(remoteMD5ListUrl);
            yield return null;
            if (www.error != null)
            {
                Debug.Log("版本校验出错，请检查服务器资源和访问路劲是否正确。" + www.error);
            }
            if (www.isDone)
            {
                string[] remoteText = www.text.Split('\n');
                List<MD5ListItem> remoteMD5List = GetMD5List(remoteText);
                // 3.对比得出更新项目
                var updateList = CompareMD5(localMD5List, remoteMD5List);
                // 4.调用线程进行更新
                foreach (MD5ListItem item in updateList)
                {
                    Debug.Log(item.assetname + "需要从服务器下载");
                }
                // todo 5.使用新的md5list覆盖本地MD5list
                // File.WriteAllText(localMD5ListPath, www.text);
            }
            www.Dispose();
        }

        private List<MD5ListItem> GetMD5List(string[] fileContent)
        {
            List<MD5ListItem> result = new List<MD5ListItem>();
            for (int i = 0; i < fileContent.Length; i++)
                result.Add(new MD5ListItem(fileContent[i]));
            return result;
        }
        private List<MD5ListItem> CompareMD5(List<MD5ListItem> localList, List<MD5ListItem> remoteList)
        {
            List<MD5ListItem> result = new List<MD5ListItem>();
            for (int i = 0; i < remoteList.Count; i++)
            {
                var item = remoteList[i];
                bool isContain = false;
                for (int j = 0; j < localList.Count; j++)
                {
                    var localItem = localList[j];
                    if (localItem.assetname == item.assetname)
                    {
                        isContain = true;
                        if (!localItem.md5.Equals(item.md5))
                        {
                            result.Add(item);
                        }
                    }
                }
                if(!isContain)
                    result.Add(item);
            }
            return result;
        }

        // todo 1.加载prefab
        // todo 2.加载Image/AudioClip/Texture/Text资源文件
        // todo 3.Lua脚本加载 依赖项处理
        // todo 4.Resource/Assetbundle加载的区别
        // todo 5. 自动更新

        /// <summary>
        /// 同步更新是从本地Resource进行加载 //todo 现在就假想资源都从assetbundle里打包而来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        public void LoadSync<T>(string assetName, Action<T> callback)
        {

        }

        /// <summary>
        /// 异步加载是从服务器加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        public void LoadAsync<T>(string assetName, Action<T> callback)
        {

        }
    }

    public class MD5ListItem
    {
        public string assetname;
        public string md5;

        public MD5ListItem() { }
        public MD5ListItem(string assetName, string md5Value)
        {
            this.assetname = assetName;
            this.md5 = md5Value.Trim();
        }
        public MD5ListItem(string md5Item)
        {
            if (string.IsNullOrEmpty(md5Item))
                return;
            string[] values = md5Item.Split('|');
            assetname = values[0];
            md5 = values[1].Trim();
        }
    }
}