
using System;
using XLua;
using UnityEngine;

namespace LuaMVC
{  
    public class LuaMonobehaviour : MonoBehaviour
    {
        private ILuaBaseView luaBaseView = null;
        private Action luaStart = null;
        private Action luaUpdate = null;
        private Action luaOnDestroy = null;
        private LuaTable scriptEnv = null;
        
        public void Init( string scriptName )  
        {  
            scriptEnv = LuaApplicationFacade.luaEnv.NewTable();
            LuaTable meta = LuaApplicationFacade.luaEnv.NewTable();
            meta.Set("__index", LuaApplicationFacade.luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            // 加载lua script
            LuaApplicationFacade.luaEnv.DoString("require " + "'" + scriptName + "'", "LuaComponent", scriptEnv);
            scriptEnv.SetInPath<MonoBehaviour>(scriptName + ".self", this); 
            if( null != this.transform.Find("Panel") )
                scriptEnv.SetInPath<GameObject>(scriptName + ".panel", this.transform.Find("Panel").gameObject);
            luaStart = scriptEnv.GetInPath<Action>(scriptName+".start");
            luaUpdate = scriptEnv.GetInPath<Action>(scriptName + ".update");
            luaOnDestroy = scriptEnv.GetInPath<Action>(scriptName + ".ondestroy");
            Action luaAwake = scriptEnv.GetInPath<Action>(scriptName+".awake");

            luaBaseView = new LuaBaseView();
            luaBaseView.ViewName = LuaApplicationFacade.luaEnv.Global.GetInPath<string>(scriptName+".ViewName");
            if (string.IsNullOrEmpty(luaBaseView.ViewName))
            {
                luaBaseView.ViewName = scriptName + "View";
                LuaMVCDebug.DebugWarning(scriptName +"not contain a viewName property,Please add it. (View脚本未添加ViewName属性)" );
            }
            luaBaseView.Initialize = LuaApplicationFacade.luaEnv.Global.GetInPath<Action>(scriptName + ".Initialize");
            luaBaseView.Open = LuaApplicationFacade.luaEnv.Global.GetInPath<Action>(scriptName + ".Open");
            luaBaseView.Close = LuaApplicationFacade.luaEnv.Global.GetInPath<Action>(scriptName + ".Close"); 
            if (null != luaAwake)
            {
                luaAwake();
                //todo 可以在这个方法中将gameobject或者是transform赋值给lua侧，减少lua侧代码量
                if( null != luaBaseView.Initialize )
                    luaBaseView.Initialize();
            }
            ViewMaster.AddView(luaBaseView);
        }
        
        #region Life cycle functions

        private void Start()
        {
            if (null != luaStart)
                luaStart();
        }
        private void Update()
        {
            if (null != luaUpdate)
                luaUpdate();
        }
        private void OnDestroy()
        {
            if (null != luaOnDestroy)
                luaOnDestroy();
            luaStart = null;
            luaUpdate = null;
            luaOnDestroy = null; 
            scriptEnv.Dispose();
        }

        #endregion
    }
}