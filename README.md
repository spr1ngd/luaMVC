#luaMVC 框架(xlua+pureMVC)

# - v0.1 去除pureMVC中反射机制，整合Mediator和Command
- - v0.15 拓展pureMVC，增加Service/Handler模块
# - v0.2 xlua整合加入pureMVC
- - v0.2.1 增加LuaFacade/LuaComponent负责lua脚本加载与生命周期函数调用
- - v0.2.2 更新LuaFacade，可将Lua脚本映射到C#接口(无GC，详见xlua使用文档)
- - v0.2.5 新增LuaMediator、LuaCommand、LuaProxy、LuaHandler等，将lua脚本映射注入pureMVC框架
- - v0.2.8 将pureMVC通知机制和luaMVC通知机制整合
- - v0.2.9 LuaObserver和pureMVC.Observer整合
# - v0.3 luaMVC已有雏形，保持pureMVC编码方式，添加了热更新模块，也可用lua来编写全部的业务逻辑，并加载后续的更新后将优化热补丁的使用方式
# - v0.5 新增ObjectPool(对象池)模块(具体API和设计思路见对象池.markdown)
