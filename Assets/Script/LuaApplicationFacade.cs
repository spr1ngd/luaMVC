
using System.Collections.Generic;
using System.IO;
using System.Text;
using PureMVC.Patterns;

namespace Game
{
    using UnityEngine;
    using PureMVC.Patterns.Lua;
    using XLua;
    using System;

    [LuaCallCSharp]
    public class LuaApplicationFacade : LuaFacade
    {
        public static LuaEnv luaEnv = new LuaEnv();
        private LuaTable scriptEnv = null;
        private Action ondestroy = null;

        public LuaApplicationFacade()
        {
            StartUp();
        }
        public void StartUp()
        { 
            luaEnv.AddLoader(Loader);
            luaEnv.AddLoader(LuaPathLoader);
            this.scriptEnv = luaEnv.NewTable();
            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            scriptEnv.Set("self", this);
            luaEnv.DoString(LoadLua("LuaFacade"), "LuaFacade", scriptEnv); 
            Action awake = scriptEnv.Get<Action>("awake");
            ondestroy = scriptEnv.Get<Action>("ondestroy");
            if (null != awake)
                awake();
        }
        public void ShutDown()
        {
            if (null != ondestroy)
                ondestroy();
            scriptEnv.Dispose();
        }

        private string LoadLua(string filePath)
        {
            string fullPath = Application.dataPath + "/Script/LuaScripts/" + filePath + ".lua.txt";
            return File.ReadAllText(fullPath);
        }

        private byte[] Loader(ref string filePath)
        {
            string fullPath = Application.dataPath + "/Script/LuaScripts/" + filePath + ".lua.txt";
            return Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
        }
        private byte[] LuaPathLoader( ref string filePath )
        { 
            string fullPath = Application.streamingAssetsPath + "/Lua/" + filePath + ".lua.txt";
            return Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
        } 

        #region Lua table map to CSharp class 

        public void RegisterLuaMediator(string mediatorName)
        {
            luaEnv.DoString("require '" + mediatorName + "'"); 
            ILuaMediator mediator = new LuaMediator();
            mediator.NAME = luaEnv.Global.GetInPath<string>(mediatorName + ".NAME");
            mediator.ListNotificationInterests = luaEnv.Global.GetInPath<List<string>>(mediatorName + ".ListNotificationInterests");
            mediator.OnRegister = luaEnv.Global.GetInPath<Action>(mediatorName + ".OnRegister");
            mediator.OnRemove = luaEnv.Global.GetInPath<Action>(mediatorName + ".OnRemove");
            mediator.HandleNotification = luaEnv.Global.GetInPath<HandleNotification>(mediatorName + ".HandleNotification");
            base.RegisterLuaMediator(mediator);
        }
        public void RegisterLuaCommand( string commandName ) 
        {
            luaEnv.DoString("require '" + commandName + "'");
            ILuaCommnad command = new LuaCommnad();
            command.CommandName = luaEnv.Global.GetInPath<string>(commandName + ".NAME");
            command.Execute = luaEnv.Global.GetInPath<HandleNotification>(commandName+".Execute");
            base.RegisterLuaCommand(command);
        }
        public void RegisterLuaHandler( string handlerName )
        {
            luaEnv.DoString("require '" + handlerName + "'");
            ILuaHandler handler = new LuaHandler();
            handler.NAME = luaEnv.Global.GetInPath<string>(handlerName + ".NAME");
            handler.ListNotificationInterests = luaEnv.Global.GetInPath<List<string>>(handlerName + ".ListNotificationInterests");
            handler.OnRegister = luaEnv.Global.GetInPath<Action>(handlerName + ".OnRegister");
            handler.OnRemove = luaEnv.Global.GetInPath<Action>(handlerName + ".OnRemove");
            handler.Request = luaEnv.Global.GetInPath<HandleNotification>(handlerName + ".HandleNotification");
            base.RegisterLuaHandler(handler);
        }
        public void RegisterLuaProxy( string proxyName )
        {
            luaEnv.DoString("require '" + proxyName + "'");
            ILuaProxy proxy = new LuaProxy();
            proxy.NAME = luaEnv.Global.GetInPath<string>(proxyName + ".NAME");
            proxy.OnRegister = luaEnv.Global.GetInPath<Action>(proxyName + ".OnRegister");
            proxy.OnRemove = luaEnv.Global.GetInPath<Action>(proxyName + ".OnRemove");
            base.RegisterLuaProxy(proxy);
        }

        #endregion
    }
}