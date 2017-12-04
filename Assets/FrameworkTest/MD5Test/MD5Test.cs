
using LuaMVC;
using UnityEngine;

public class MD5Test : MonoBehaviour
{
    private void Awake()
    {
        string value = "Hello MD5";
        Debug.Log(Util.md5(value));
    }
}
