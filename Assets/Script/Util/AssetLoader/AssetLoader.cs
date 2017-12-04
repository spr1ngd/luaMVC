
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

        // todo //////////////////////////////////////////////////////////////////
        // todo ***********开始程序后启动版本校验 / 可根据情况选择是否升级，比如更新皮肤，可选择不更新
        // todo 1.加载md5值表
        // todo 2.与本地md5值表进行对比，列出需要重新下载的更新项目
        // todo 3.将更新项目加入新线程进行更新/解压资源
        // todo 4.在线程队列处理完成以后，正式启动项目
    }
}