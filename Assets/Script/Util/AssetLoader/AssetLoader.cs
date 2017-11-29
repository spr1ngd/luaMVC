
using System;
using System.Collections;
using Game;

namespace LuaMVC
{
    using UnityEngine;

    public class AssetLoader
    {
        private static WWW loader = null;

        public static IEnumerator LoadAsset<T>( string url ,Action<T> callback ) where T : class 
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
                    Debug.Log("Click audio");
                }
            }
        }

        public static void LoadAudioClip( string audioPath ,Action<AudioClip> callback )
        {
            string url = FilePath.prePath + audioPath;
            Coroutiner.Instance.StartCoroutine(LoadAsset(url, callback));
        }

        public static void LoadAssetText( string textPath,Action<string> callback )
        {
            string url = FilePath.prePath + textPath;
            Coroutiner.Instance.StartCoroutine(LoadAsset(url, callback));
        }
    }
}