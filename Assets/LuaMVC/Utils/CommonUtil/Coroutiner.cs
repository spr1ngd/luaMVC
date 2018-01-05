
namespace LuaMVC
{
    using UnityEngine;

    public class Coroutiner : MonoBehaviour
    {
        private static Coroutiner m_instance = null;

        public static Coroutiner Instance
        {
            get
            {
                if (null == m_instance)
                {
                    GameObject coroutiner = new GameObject("Coroutiner");
                    m_instance = coroutiner.AddComponent<Coroutiner>();
                }
                return m_instance;
            }
        }
    }
}