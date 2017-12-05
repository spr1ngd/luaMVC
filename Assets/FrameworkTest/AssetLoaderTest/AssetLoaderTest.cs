
using LuaMVC;
using UnityEngine;
using System.Collections;
using Game;

public class AssetLoaderTest : MonoBehaviour
{
    void Start()
    {
        // AssetLoader
        AssetLoader.LoadAudioClip("TestFile.txt", ac => { Debug.Log("success load text"); });
        //StartCoroutine(wwwLoad());
    }

    IEnumerator wwwLoad()
    {
        WWW www = new WWW(FilePath.prePath +  "TestFile.txt");//BZ34-1-CEPA.assetbundle
        yield return www;
        if (null != www.error)
        {
            Debug.Log(www.error);
            yield break;
        }
        if (www.isDone)
        {
            //var go = www.assetBundle.LoadAsset<GameObject>("BZ34-1-CEPA");
            //GameObject.Instantiate(go);
            Debug.Log(www.text);
        }
    }
}
