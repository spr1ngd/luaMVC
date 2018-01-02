 
using LuaMVC;
using UnityEngine;
using System.Collections; 

public class AssetLoaderTest : MonoBehaviour
{ 
    private void Start()
    { 
        StartCoroutine(init()); 
    }

    private IEnumerator init()
    {
        // todo 这些应该归入到框架内部 不暴露到普通逻辑中
        yield return AssetLoader.OnInitialize(); 
        
        yield return null;

        AssetLoader.LoadAssetInstantiate<Object>("dogs.unity3d", "dog1", (dog) =>
        {
            GameObject go = (GameObject)dog;
            go.name = "dog1";
            go.transform.localScale = Vector3.one;
        }); 
    } 
} 