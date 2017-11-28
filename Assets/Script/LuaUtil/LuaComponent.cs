
using System;
using XLua;
using UnityEngine;

namespace Game
{
    [CSharpCallLua]
    public delegate void LuaAwake(GameObject obj);

    public class LuaComponent : MonoBehaviour
    {
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
            luaStart = scriptEnv.GetInPath<Action>(scriptName+".start");
            luaUpdate = scriptEnv.GetInPath<Action>(scriptName + ".update");
            luaOnDestroy = scriptEnv.GetInPath<Action>(scriptName + ".ondestroy");
            LuaAwake luaAwake = scriptEnv.GetInPath<LuaAwake>(scriptName+".awake");
            if (null != luaAwake)
                luaAwake(this.gameObject);
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