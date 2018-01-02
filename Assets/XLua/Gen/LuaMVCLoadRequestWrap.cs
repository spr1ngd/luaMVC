#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class LuaMVCLoadRequestWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaMVC.LoadRequest);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 7, 7);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetbundleName", _g_get_assetbundleName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetName", _g_get_assetName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetType", _g_get_assetType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "action", _g_get_action);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "callback", _g_get_callback);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaCallback", _g_get_luaCallback);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetRequest", _g_get_assetRequest);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetbundleName", _s_set_assetbundleName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetName", _s_set_assetName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetType", _s_set_assetType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "action", _s_set_action);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "callback", _s_set_callback);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "luaCallback", _s_set_luaCallback);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetRequest", _s_set_assetRequest);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					LuaMVC.LoadRequest __cl_gen_ret = new LuaMVC.LoadRequest();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to LuaMVC.LoadRequest constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetbundleName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.assetbundleName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.assetName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.assetType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_action(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.action);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_callback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.callback);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaCallback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.luaCallback);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetRequest(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.assetRequest);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetbundleName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.assetbundleName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.assetName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.assetType = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_action(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.action = translator.GetDelegate<System.Action<System.Type>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_callback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.callback = translator.GetDelegate<System.Action<UnityEngine.Object>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_luaCallback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.luaCallback = (XLua.LuaFunction)translator.GetObject(L, 2, typeof(XLua.LuaFunction));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetRequest(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaMVC.LoadRequest __cl_gen_to_be_invoked = (LuaMVC.LoadRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.assetRequest = (UnityEngine.AssetBundleRequest)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundleRequest));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
