# luaMVC 框架(xlua+pureMVC)

## 更新说明
### LuaMVC Beta 0.1版本更新说明
#### v0.1 去除pureMVC中反射机制，整合Mediator和Command
-  v0.15 拓展pureMVC，增加Service/Handler模块
#### v0.2 xlua整合加入pureMVC
-  v0.2.1 增加LuaFacade/LuaComponent负责lua脚本加载与生命周期函数调用
-  v0.2.2 更新LuaFacade，可将Lua脚本映射到C#接口(无GC，详见xlua使用文档)
-  v0.2.5 新增LuaMediator、LuaCommand、LuaProxy、LuaHandler等，将lua脚本映射注入pureMVC框架
-  v0.2.8 将pureMVC通知机制和luaMVC通知机制整合
-  v0.2.9 LuaObserver和pureMVC.Observer整合
#### v0.3 luaMVC已有雏形，保持pureMVC编码方式，添加了热更新模块，也可用lua来编写全部的业务逻辑，后续的更新将优化热补丁的使用方式
#### v0.5 增加功能模块
-  v0.5.1 新增[ObjectPool(对象池)模块](https://github.com/ll4080333/luaMVC/blob/master/Documents/ObjectPool.md)(优化需要反复构造的对象所造成的性能消耗，比如GameObject)
-  v0.5.2 新增[AudioEntry(音频入口)模块](https://github.com/ll4080333/luaMVC/blob/master/Documents/AudioEntry.md)(一键生成配置表.json，按需加载，便捷更新)
-  v0.5.3 新增AssetLoader_Beta版
-  v0.5.4 新增协同程序管理器
-  v0.5.5 新增AssetPackager_Beta版
-  v0.5.6 新增[TimeMaster模块]()
-  v0.5.7 [TimeMaster模块]()新增功能
-  v0.5.8 更新[Packager模块]()，支持预制物/lua脚本打包和生成md5校验表
-  v0.5.9 新增[ViewMaster]()，优化中介者中拆装箱的操作
#### v0.6 新增[CommonUtil工具类]()
-  v0.6.1 新增自动校验md5更新游戏资源算法
-  v0.6.2 新增[Loom线程管理类/回调函数执行类]()
-  v0.6.3 新增[Setting 系统设置类]()
-  v0.6.4 更新AssetLoader模块，完善了ab包的读取与资源加载
 
### LuaMVC Beta 1.0 版本更新说明

#### 1.1 新增LuaBaseView，可将lua创建的view注册进入ViewMaster，方便管理 

#### 1.2 改进部分代码 
- v1.2.1 改进LuaComponent为LuaMonobehaviour，将Lua视图脚本映射到C# interface，方便框架统一管理
- v1.2.2 改进LuaAppcalitionFacade，新增了递归加载.lua文件的loader方法和直接从ab包加载.lua文件的方法

#### 1.3 新增LuaMVCConfig类型
- v 1.3.1 对luaMVC框架执行配置,配置的数据可映射到json文件,方便动态的修改
- v 1.3.2 修复AssetLoader加载assetbundle会导致CPU过度占用的Bug
#### 1.4 新增数据加密工具
- 1. 数据加密简述
- 2. MD5加密
- 3. DES加密与解密
- 4. RSA加密与解密
- 5. 完善框架配置问题
- 6. 完善ViewMaster其余接口
- 7. 修复部分由框架引起的bug
- 8. 完善发布前后路径调试繁琐的问题
