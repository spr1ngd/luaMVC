
using LuaMVC;
using UnityEngine;
using XLua;

public class AssetLoaderTest : MonoBehaviour
{
    LuaEnv luaEnv = new LuaEnv();
    private void Start()
    {
        AssetLoader loader = this.GetComponent<AssetLoader>();
        loader.LoadAsset<Object>("dogs", "cu_puppy_corgi", (dog) =>
        {
            GameObject.Instantiate(dog);
        }, null);
        // cu_puppy_husky_lit
        loader.LoadAsset<Object>("dogs", "cu_puppy_husky_lit", (dog) =>
        {
            var dogGameObject = GameObject.Instantiate(dog) as GameObject;
            dogGameObject.transform.localPosition += new Vector3(0.3f, 0, 0);
        }, null);
        luaEnv.DoString("print('hello world')");
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();
    }
}
