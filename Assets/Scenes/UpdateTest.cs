
using LuaMVC;
using UnityEngine;

public class UpdateTest : MonoBehaviour
{
    void Start()
    {
        AssetLoader al = new AssetLoader();
        al.AutomaticUpdateAssetsFromAssetServer();
    }
}
