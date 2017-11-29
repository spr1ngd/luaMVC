
# LuaMVC.ObjectPool

## 什么是ObjectPool

## ObjectPool的优势

## 更新内容

- v0.1 对象池beta版
- v0.2 新增委托Func<T> New{get;set;}用于为对象池构造新的对象 

## LuaMVC.ObjectPool API详解


### ==interface== IPool

> IPool是==池==的抽象

```
public interface IPool
{
    string name { get; set; }
    void OnInitialze();
    void OnRelease();
}
```

### ==interface== IObjectPool<T>

> IObjectPool<T>中规范了对象池对外的操作接口，使用泛型可以让对象池接受各种不同类型的对象，不同类型的具体操作可根据需求在子类中重写操作接口

```
public interface IObjectPool<T> : IPool
{
    // 压入
    void Push(T obj);
    void Push(IList<T> objs);
    void Push(int count);
    // 取出
    T Pop();
    IList<T> Pop(int count);
    // 构造新的对象
    Func<T> New { get; set; }
    // 操作事件
    Action pushAction { get; set; }
    Action popAction { get; set; }
}
```

### ==Class== ObjectPool

> 实现接口内容
> - 对象取出压入操作
> - 实现IEnumerable接口，可直接遍历busyList(激活的)对象
> - 交付PoolManager由框架统一管理

```
public class ObjectPool<T> : IObjectPool<T>, IEnumerable
{
    public IList<T> busyList { get; private set; }
    public IList<T> idleList { get; private set; }
    // 实现基础操作
    
    public virtual void OnInitialze()
    {
        busyList = new List<T>();
        idleList = new List<T>();
#if ENABLE_POOL_MANAGER
        // 是否由PoolManager管理可自行选择
        PoolManager.Instance.AddPool(this);
#endif
    }
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < busyList.Count; i++)
            yield return busyList[i];
    }
}
```

### ==Class== PoolManager

> 管理所有已构造的对象池
```
public class PoolManager
{
    private IDictionary<string,IPool> m_pools = new Dictionary<string, IPool>();
    // 查找一个对象池
    // 新增一个对象池
    // 释放一个对象池
    // 释放所有对象池
}
```

### 案例应用 GameObjectPool(拓展一个游戏对象池)

> 重写部分方法实现特定的功能，在GameObject对象池中取出时需要设置为显示，压入时需要隐藏，释放时需要销毁

```
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
```

### GameObjectPool使用


```
public class ObjectPoolTest : MonoBehaviour
{
    // 声明一个对象池
    public GameObjectPool pool = null;
    public GameObject prefab = null;

    private void Start()
    {
        // 构造对象池
        pool = new GameObjectPool("testGameObject");
        // 注册popAction
        pool.popAction = () => { Debug.Log("取出了一个方块"); };
        // 注册pushAction
        pool.pushAction = () => { Debug.Log("销毁了一个方块"); };
        // 注册构造新对象的方法 - 也可用匿名函数
        pool.New += createCube;
    }

    private GameObject createCube()
    {
        return GameObject.Instantiate(prefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 取出5个对象 构造5个新对象
            pool.Pop(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 压入2个对象 将2个对象压入池中
            pool.Push(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 取出8个对象
            // 因为池中有已经压入的两个对象
            // 所以这一步操作只会新构造6个对象
            // 减少了构造时的性能消耗
            pool.Pop(8);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 释放这个对象池
            PoolManager.Instance.RemovePool("testGameObject");
        }
    }
}
```
