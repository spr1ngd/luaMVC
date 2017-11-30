# luaMVC 框架(xlua+pureMVC)

## 更新说明
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
-  v0.5.3 新增AssetLoader_Bate版
-  v0.5.4 新增协同程序管理器
-  v0.5.5 新增AssetPackager_Beta版
-  v0.5.6 新增TimeMaster模块
