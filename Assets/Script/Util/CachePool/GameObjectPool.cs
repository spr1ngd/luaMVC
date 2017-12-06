namespace LuaMVC
{
    using UnityEngine;
    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObjectPool(string name) : base(name) { }
        public override GameObject Pop()
        {
            var go = base.Pop();
            go.SetActive(true);
            return go;
        }
        public override void Push(GameObject obj)
        {
            obj.SetActive(false);
            base.Push(obj);
        }
        public override void OnRelease()
        {
            for (int i = 0; i < busyList.Count; i++)
                Object.Destroy(busyList[i]);
            for (int i = 0; i < idleList.Count; i++)
                Object.Destroy(idleList[i]);
            base.OnRelease();
        }
    }
}