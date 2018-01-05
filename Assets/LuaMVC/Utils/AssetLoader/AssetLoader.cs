
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using XLua;
using UObject = UnityEngine.Object;

namespace LuaMVC
{
    using UnityEngine;

    [LuaCallCSharp]
    public interface ILuaCallback
    {
        Action<Object> luaCallback { get; set; }
    }
    [LuaCallCSharp]
    public class LuaCallback : ILuaCallback
    {
        public Action<UObject> luaCallback { get; set; }
    }
    [LuaCallCSharp]
    public class LoadRequest
    {
        public string assetbundleName;
        public string assetName;
        public Type assetType;
        public Action<Type> action;
        public Action<UObject> callback;
        public LuaFunction luaCallback;
        public AssetBundleRequest assetRequest;
    }
    [LuaCallCSharp]
    public class LoadedAsset
    {
        public AssetBundle assetbundle;
        public int dependencyCount;
    }

    [LuaCallCSharp]
    public class AssetLoader : MonoBehaviour
    {
        // 常量，根据需求修改
        private static string m_manifestPath = "StreamingAssets";
        private static string m_manifestName = "AssetBundleManifest";
        private static string m_assetbundleExtension = ".unity3d";
        private static string m_prefabPrefixName = "Prefabs/";

        // todo 这些值全部等待替换
        private static string m_luaScirptPath = "http://192.168.1.113:8088/StreamingAssets/Lua/";
        private static string m_localLuaScriptPath { get { return Application.streamingAssetsPath + "/Lua/"; } }
        private static string m_serverLuaScriptCheckList = "http://192.168.1.113:8088/StreamingAssets/md5list.txt";
        private static string m_localMD5ListPath { get { return Application.streamingAssetsPath + "/md5list.txt"; } }
        private static string m_assetServerPath = "http://192.168.1.113:8088/StreamingAssets/"; // 服务器资源文件夹路径 todo 这个值应该由服务器传递到客户端

        private static AssetBundleManifest m_manifest = null;
        private static IList<LoadRequest> m_loadRequests = new List<LoadRequest>();
        private static IDictionary<string, LoadedAsset> m_loadedAssets = new Dictionary<string, LoadedAsset>();
        private static IDictionary<string, string[]> m_dependencies = new Dictionary<string, string[]>();
        private static IDictionary<string, WWW> m_wwws = new Dictionary<string, WWW>();
        private static IDictionary<string, string> m_errors = new Dictionary<string, string>();

        public static IEnumerator OnInitialize()
        {
            if (!LuaMVCConfig.IsDeveloping)
            {
#if UNITY_DEVELOPMENT
#else
                if (LuaMVCConfig.AutomaticUpdateLuaScriptsFromServer)
                    yield return AutomaticUpdateLuaScripts();
                yield return LoadManifest();
#endif
            } 
            yield return null;
        }

        private static IEnumerator LoadManifest()
        {
#if UNITY_DEVELOPMENT
#else
            if (null == m_manifest)
            {
                LoadAsset<AssetBundleManifest>(m_manifestPath, m_manifestName, (obj) =>
                {
                    m_manifest = obj as AssetBundleManifest;
                    if (null == m_manifest)
                        throw new Exception("Load manifest file failed.");
                    m_dependencies.Add("StreamingAssets", m_manifest.GetAllDependencies("StreamingAssets"));
                    Debug.Log("Load manifest file successfully.");
                }, null);
            }
#endif
            yield return m_manifest;

        }

#if UNITY_DEVELOPMENT
        // 开发使用的API
        private static void Load(string assetPath, Action<UObject> callback)
        {
            var obj = Resources.Load(m_prefabPrefixName + assetPath);
            if ( null == obj )
                throw new Exception("Load" + assetPath +"failed.");
            callback(obj);
        }
        public static void LoadAsset<T>(string assetName, Action<UObject> callback) where T:UObject
        {
            Load( assetName, go =>
            {
                if (null != callback)
                    callback(go as T); 
            }); 
        } 
        public static void LoadAsset<T>(string assetFullPath, string assetName, Action<UObject> callback) where T : UObject
        {
            LoadAsset<T>(assetName, callback);
        }
        public static void LoadAssetInstantiate<T>(string assetName, Action<UObject> callback) where T : UObject
        {
            LoadAsset<T>(assetName, obj =>
            {
                GameObject go = GameObject.Instantiate(obj) as GameObject;
                callback(go);
            });
        }
        public static void LoadAssetInstantiate<T>(string assetFullPath, string assetName, Action<UObject> callback) where T : UObject
        {
            LoadAsset<T>(assetFullPath,assetName, obj =>
            {
                GameObject go = GameObject.Instantiate(obj) as GameObject;
                callback(go);
            });
        }

        public static void LuaLoadAsset(string assetName , LuaFunction luaCallback )
        {   
            Load(assetName, go =>
            {
                if (null != luaCallback)
                    luaCallback.Call(go); 
            }); 
        }
        public static void LuaLoadAsset(string assetFullPath, string assetName, LuaFunction luaCallback)
        {
            LuaLoadAsset(assetName, luaCallback);
        } 

#else
        private IList<string> finishedList = new List<string>();
        private void Update()
        { 
            InvokeWWWRequest();
        }
        private void InvokeWWWRequest()
        { 
            if (m_wwws.Keys.Count <= 0)
                return;  
            foreach (KeyValuePair<string, WWW> www in m_wwws)
            {  
                WWW download = www.Value;
                if (null != download.error)
                { 
                    m_errors.Add(www.Key, download.error);
                    finishedList.Add(www.Key); 
                }
                else
                {
                    if (download.isDone)
                    { 
                        AssetBundle bundle = download.assetBundle;
                        if (null == bundle)
                        {
                            m_errors.Add(www.Key, string.Format("{0} is not a valid asset bundle.", www.Key));
                        }
                        else
                        {
                            LoadedAsset loadedAsset = new LoadedAsset { assetbundle = bundle };
                            Add2LoadedAssets(www.Key, loadedAsset);
                        }
                        finishedList.Add(www.Key);
                    }
                }
            }
            for (int i = 0; i < finishedList.Count; i++)
            {
                m_wwws[finishedList[i]].Dispose();
                m_wwws[finishedList[i]] = null;
                m_wwws.Remove(finishedList[i]);
            }
            finishedList.Clear();
        } 

        public static void LuaLoadAsset(string assetName, LuaFunction luaCallback)
        { 
            LuaLoadAsset(assetName + m_assetbundleExtension,assetName, luaCallback);
        }
        public static void LuaLoadAsset(string assetFullName, string assetName, LuaFunction luaCallback)
        {
            LoadAsset<UObject>(assetFullName, assetName, null, luaCallback);
        }  

        public static void LoadAssetInstantiate<T>( string assetName,Action<GameObject> callback) where T : UObject
        {
            LoadAsset<T>(assetName, obj =>
            {
                GameObject go = GameObject.Instantiate(obj) as GameObject;
                if (null != callback)
                    callback(go);
            });
        }
        public static void LoadAssetInstantiate<T>(string assetFullName, string assetName, Action<GameObject> callback) where T : UObject
        {
            LoadAsset<T>(assetFullName, assetName, obj =>
            {
                GameObject go = GameObject.Instantiate(obj) as GameObject; 
                if (null != callback)
                    callback(go);
            });
        }
        public static void LoadAsset<T>(string assetName, Action<UObject> callback) where T : UObject 
        {
            LoadAsset<T>(assetName + m_assetbundleExtension, assetName, callback);
        }
        public static void LoadAsset<T>(string assetFullName, string assetName, Action<UObject> callback, LuaFunction luaCallback = null) where T : UObject 
        { 
            var loadedAsset = GetLoaded(assetFullName);
            if (null != loadedAsset)
            { 
                UObject asset = loadedAsset.assetbundle.LoadAssetAsync(assetName, typeof(T)).asset;
                if (null != callback)
                    callback(asset);
                if (null != luaCallback)
                    luaCallback.Call(asset);
            }
            else
            {
                LoadRequest loadRequest = new LoadRequest();
                loadRequest.assetbundleName = assetFullName;
                loadRequest.assetType = typeof(T);
                loadRequest.assetName = assetName; 
                loadRequest.callback = callback;
                loadRequest.luaCallback = luaCallback; 
                m_loadRequests.Add(loadRequest); 
                OnLoad<T>(assetFullName); 
            }
        }
          
        private static List<LoadRequest> GetLoadRequest(string assetPath)
        {
            List<LoadRequest> requests = new List<LoadRequest>();
            for (int i = 0; i < m_loadRequests.Count; i++)
            {
                if (m_loadRequests[i].assetbundleName.Equals(assetPath))
                    requests.Add(m_loadRequests[i]);
            }
            return requests;
        } 
        private static void Add2LoadedAssets(string assetPath, LoadedAsset loadedAsset)
        {
            var requestList = GetLoadRequest(assetPath);
            for (int i = 0; i < requestList.Count; i++)
            {
                var request = requestList[i];
                UObject asset = loadedAsset.assetbundle.LoadAssetAsync(request.assetName, request.assetType).asset; 
                if (null != request.callback)
                    request.callback(asset);
                if (null != request.luaCallback)
                    request.luaCallback.Call(asset); 
            }
            m_loadedAssets.Add(assetPath, loadedAsset);
        }
        private static void OnLoad<T>(string assetName) where T : UObject
        {
            if (m_wwws.ContainsKey(assetName))
                return;
            string url = m_assetServerPath + assetName;
            WWW www = null;
            if (typeof(T) == typeof(AssetBundleManifest))
                www = new WWW(url);
            else
            {
                if (null == m_manifest)
                    throw new Exception("Manifest file load failed.");
                www = WWW.LoadFromCacheOrDownload(url, m_manifest.GetAssetBundleHash(assetName), 0);
            }
            m_wwws.Add(assetName, www);
            LoadDependencies(assetName);
        }
        private static void LoadDependencies( string assetPath )
        {
            if( null == m_manifest)
                return;
            string[] dependencies = m_manifest.GetAllDependencies(assetPath);
            if (null == dependencies || dependencies.Length <= 0)
                return;
            m_dependencies.Add(assetPath, dependencies);
            for (int i = 0; i < dependencies.Length; i++)
                OnLoad<UObject>(dependencies[i]); 
        } 
        private static LoadedAsset GetLoaded( string assetPath )
        { 
            LoadedAsset loadedAsset = null;
            m_loadedAssets.TryGetValue(assetPath, out loadedAsset); 
            if (null == loadedAsset)
                return null; 
            string[] dependencies = null;
            m_dependencies.TryGetValue(assetPath, out dependencies);
            if (null == dependencies)
                return loadedAsset; 
            for (int i = 0; i < dependencies.Length; i++)
            {
                LoadedAsset dependencyAsset = null;
                m_loadedAssets.TryGetValue(dependencies[i], out dependencyAsset);
                if (null == dependencyAsset)
                    return null;
            }
            loadedAsset.dependencyCount++; 
            return loadedAsset;
        }  
        // 释放已加载的全部资源
        public void OnDestroy()
        {
            foreach (KeyValuePair<string, LoadedAsset> asset in m_loadedAssets)
                asset.Value.assetbundle.Unload(true);
            foreach (KeyValuePair<string, WWW> pair in m_wwws)
                pair.Value.Dispose();
        }

        /// <summary>
        /// isThorough为true,卸载该 ab 包所有资源
        /// </summary>
        /// <param name="assetFullName"></param>
        /// <param name="isThorough"></param>
        /// <param name="uploadDependenices"></param>
        public void Unload(string assetFullName, bool isThorough = false ,bool uploadDependenices = false)
        {
            if (m_loadedAssets.ContainsKey(assetFullName))
                m_loadedAssets[assetFullName].assetbundle.Unload(isThorough); 
            // 卸载依赖
            if (uploadDependenices)
            {
                if (m_dependencies.ContainsKey(assetFullName))
                {
                    var assets = m_dependencies[assetFullName];
                    for (int i = 0; i < assets.Length; i++)
                        Unload(assetFullName, isThorough, uploadDependenices);
                }
            }
        }

#region AutomaticUpdateAssetsFromAssetServer 启动自动更新，以后再优化解决

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

        public static IEnumerator AutomaticUpdateLuaScripts()
        {
            // 1.加载本地md5list
            var localMD5List = GetMD5List(File.ReadAllLines(m_localMD5ListPath));
            yield return Coroutiner.Instance.StartCoroutine(LoadMD5ListFromServer(localMD5List));
        }
        private static IEnumerator LoadMD5ListFromServer(List<MD5ListItem> localMD5List)
        {
            // 2.加载服务器md5list 
            WWW www = new WWW(m_serverLuaScriptCheckList);
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
                // todo 在这里可以对更新有个大致的统计
                for (int i = 0; i < updateList.Count; i++)
                {
                    var item = updateList[i]; 
                    WWW downloadThread = new WWW(m_luaScirptPath + item.assetname);
                    yield return downloadThread;
                    if (downloadThread.error != null)
                    {
                        Debug.Log("更新lua脚本" + item.assetname + "出错" + downloadThread.error);
                    }
                    if (downloadThread.isDone)
                        WriteLuaScript2LocalDirectory(downloadThread.text, m_localLuaScriptPath + item.assetname);
                }
                File.WriteAllText(m_localMD5ListPath, www.text);
            }
            www.Dispose();
        }
        private static void WriteLuaScript2LocalDirectory( string luaContent,string targetPath )
        {
            using (StreamWriter writer = new StreamWriter(targetPath))
            {
                writer.Write(luaContent);
            }
        }

        private static List<MD5ListItem> GetMD5List(string[] fileContent)
        {
            List<MD5ListItem> result = new List<MD5ListItem>();
            for (int i = 0; i < fileContent.Length; i++)
            {
                if(string.IsNullOrEmpty(fileContent[i]))
                    continue;
                result.Add(new MD5ListItem(fileContent[i]));
            }
            return result;
        }
        private static List<MD5ListItem> CompareMD5(List<MD5ListItem> localList, List<MD5ListItem> remoteList)
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
                if (!isContain)
                    result.Add(item);
            }
            return result;
        }

#endregion
#endif
    }
}