
using Game;
using UnityEngine;

namespace SpringFrameworkTest
{
    public class ObjectPoolTest : MonoBehaviour
    {
        public GameObjectPool pool = null;
        public GameObject prefab = null;

        private void Start()
        {
            pool = new GameObjectPool("testGameObject");
            pool.popAction = () => { Debug.Log("取出了一个方块"); };
            pool.pushAction = () => { Debug.Log("销毁了一个方块"); };
            pool.New += createCube;
        }

        private GameObject createCube()
        {
            GameObject cube = GameObject.Instantiate(prefab);
            return cube;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                pool.Pop(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                pool.Push(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                pool.Pop(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                pool.Pop(7);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                pool.Push(10);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PoolManager.Instance.RemovePool("testGameObject");
            }
        }
    }
}