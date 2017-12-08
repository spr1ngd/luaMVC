
using System;
using System.Collections;
using System.Collections.Generic; 
using System.IO; 
using UObject = UnityEngine.Object;

namespace LuaMVC
{
    using UnityEngine;

    public interface ILuaCallback
    {
        Action<UObject> luaCallback { get; set; }
    }

    public class LoadRequest
    {
        public string assetbundleName;
        public string assetName;  
        public Type assetType;
        public Action<UObject> callback;
        public ILuaCallback luaCallback;
        public AssetBundleRequest assetRequest;
    }

    public class LoadedAsset
    {
        public AssetBundle assetbundle;
        public int dependencyCount;
    } 

    public class AssetLoader : MonoBehaviour
    {
        private IDictionary<string,AudioClip> m_audios = new Dictionary<string, AudioClip>();
        private IDictionary<string,TextAsset> m_texts = new Dictionary<string, TextAsset>();
        private IDictionary<string,Texture> m_textures = new Dictionary<string, Texture>();
        private IDictionary<string,AssetBundle> m_assets = new Dictionary<string, AssetBundle>(); 

        //private string m_assetServerPath = "http://192.168.1.113:8088/"; // todo 这个值应该也有服务器推送
        private string m_assetServerPath ; 
        private AssetBundleManifest m_manifest = null;
        private IList<LoadRequest> m_loadRequests = new List<LoadRequest>();
        private IDictionary<string, LoadedAsset> m_loadedAssets = new Dictionary<string, LoadedAsset>();
        private IDictionary<string, string[]> m_dependencies = new Dictionary<string, string[]>();
        private IDictionary<string, WWW> m_wwws = new Dictionary<string, WWW>();
        private IDictionary<string,string> m_errors = new Dictionary<string, string>();

        private void Awake()
        {
            m_assetServerPath = "file://"+Application.streamingAssetsPath + "/";
            localMD5ListPath = Application.streamingAssetsPath + "/md5list.txt";
            OnInitialize();
        }

        private void OnInitialize()
        {
            if (null == m_manifest)
            {
                LoadAsset<AssetBundleManifest>("StreamingAssets", "AssetBundleManifest", (obj) =>
                {
                    m_manifest = obj as AssetBundleManifest;
                    m_dependencies.Add("StreamingAssets", m_manifest.GetAllDependencies("StreamingAssets"));
                }, null);
            }
        }

        // todo 产生了80B的GC浪费
        private void Update()
        {
            AutoDownload();
        }

        private void AutoDownload()
        {
            IList<string> finishedList = new List<string>();
            if (m_wwws.Keys.Count <= 0)
                return; 
            foreach (KeyValuePair<string, WWW> www in m_wwws)
            {
                WWW download = www.Value;
                if (null != download.error)
                {
                    Debug.Log(www.Key +" download error :"+download.error);
                    m_errors.Add(www.Key,download.error);
                    finishedList.Add(www.Key);
                    continue;
                }
                if (download.isDone)
                { 
                    AssetBundle bundle = download.assetBundle;
                    if (null == bundle)
                    {
                        m_errors.Add(www.Key, string.Format("{0} is not a valid asset bundle.", www.Key));
                    }
                    else
                    {
                        LoadedAsset loadedAsset = new LoadedAsset {assetbundle = bundle};
                        Add2LoadedAssets(www.Key, loadedAsset);
                    }
                    finishedList.Add(www.Key);
                } 
            } 
            for (int i = 0; i < finishedList.Count; i++)
            {
                m_wwws[finishedList[i]].Dispose();
                m_wwws.Remove(finishedList[i]);
            }
        }

        private void Add2LoadedAssets( string assetPath,LoadedAsset loadedAsset )
        {
            var requestList = GetLoadRequest(assetPath);
            for (int i = 0; i < requestList.Count; i++)
            {
                var request = requestList[i];
                UObject asset = loadedAsset.assetbundle.LoadAssetAsync(request.assetName, request.assetType).asset;
                if (null != request.callback)
                    request.callback(asset);
                if (null != request.luaCallback)
                    request.luaCallback.luaCallback(asset);
            }
            m_loadedAssets.Add(assetPath,loadedAsset);
        }

        private List<LoadRequest> GetLoadRequest( string assetPath )
        {
            List<LoadRequest> requests = new List<LoadRequest>();
            for (int i = 0; i < m_loadRequests.Count; i++)
            {
                if (m_loadRequests[i].assetbundleName.Equals(assetPath))
                    requests.Add(m_loadRequests[i]);
            }
            return requests;
        }

        // todo 这个方法应该拆分为多个接口
        public void LoadAsset<T>(string assetPath, string assetName, Action<UObject> callback, ILuaCallback luaCallback) where T : UObject
        { 
            var loadedAsset = GetLoaded(assetPath);
            if (null != loadedAsset)
            { 
                UObject asset = loadedAsset.assetbundle.LoadAssetAsync(assetName, typeof(T)).asset;
                if (null != callback)
                    callback(asset);
                if (null != luaCallback)
                    luaCallback.luaCallback(asset);
            }
            else
            {
                LoadRequest loadRequest = new LoadRequest();
                loadRequest.assetbundleName = assetPath;
                loadRequest.assetType = typeof(T);
                loadRequest.assetName = assetName;
                loadRequest.callback = callback;
                loadRequest.luaCallback = luaCallback; 
                m_loadRequests.Add(loadRequest); 
                OnLoad<T>(assetPath);
            }
        }

        private void OnLoad<T>(string assetPath) where T : UObject
        {
            if (m_wwws.ContainsKey(assetPath))
                return;
            string url = m_assetServerPath + assetPath;
            WWW www = null;
            if (typeof(T) == typeof(AssetBundleManifest))
                www = new WWW(url);
            else
            {
                // todo 需要在此处进行版本区分 以及 unity版本区分
                //www = WWW.LoadFromCacheOrDownload(url,0);
                www = new WWW(url);
            } 
            m_wwws.Add(assetPath, www);
            LoadDependencies(assetPath);
        }

        private void LoadDependencies( string assetPath )
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

        private LoadedAsset GetLoaded( string assetPath )
        {
            LoadedAsset loadedAsset = null;
            m_loadedAssets.TryGetValue(assetPath, out loadedAsset);
            if (null == loadedAsset)
                return null; 
            string[] dependencies = null;
            m_dependencies.TryGetValue(assetPath, out dependencies);
            if (null == dependencies)
                return null; 
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

        public void LoadAudioClip(string audioName, Action<AudioClip> callback, params object[] objs)
        {
            if (m_audios.ContainsKey(audioName))
            {
                callback(m_audios[audioName]);
                return;
            }
            // todo 先加载依赖项
        }
        public void LoadTextAsset(string textName, Action<TextAsset> callback, params object[] objs)
        {

        }
        public void LoadTexture(string textureName, Action<Texture> callback, params object[] objs)
        {

        }
        public void LoadAssetbundle(string assetName, Action<AssetBundle> callback, params object[] objs)
        {

        }

        public void OnDestroy()
        {
            foreach (KeyValuePair<string, LoadedAsset> asset in m_loadedAssets)
                asset.Value.assetbundle.Unload(true);
        }

        #region AutomaticUpdateAssetsFromAssetServer
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
        private string localMD5ListPath;
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
                if (!isContain)
                    result.Add(item);
            }
            return result;
        }
        #endregion
    }
}