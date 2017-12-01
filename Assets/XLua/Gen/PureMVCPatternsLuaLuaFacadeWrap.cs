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
    public class PureMVCPatternsLuaLuaFacadeWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PureMVC.Patterns.Lua.LuaFacade);
			Utils.BeginObjectRegister(type, L, translator, 0, 21, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitializeLuaFacade", _m_InitializeLuaFacade);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NotifyObservers", _m_NotifyObservers);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitializeLuaMediator", _m_InitializeLuaMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaMediator", _m_RegisterLuaMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveLuaMediator", _m_RemoveLuaMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RetrieveLuaMediator", _m_RetrieveLuaMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasLuaMediator", _m_HasLuaMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitializeLuaCommand", _m_InitializeLuaCommand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaCommand", _m_RegisterLuaCommand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveLuaCommand", _m_RemoveLuaCommand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasLuaCommand", _m_HasLuaCommand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitializeLuaProxy", _m_InitializeLuaProxy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaProxy", _m_RegisterLuaProxy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveLuaProxy", _m_RemoveLuaProxy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RetrieveLuaProxy", _m_RetrieveLuaProxy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasLuaProxy", _m_HasLuaProxy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitializeLuaHandler", _m_InitializeLuaHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaHandler", _m_RegisterLuaHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveLuaHandler", _m_RemoveLuaHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RetrieveLuaHandler", _m_RetrieveLuaHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasLuaHandler", _m_HasLuaHandler);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 1, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SendNotification", _m_SendNotification_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "PureMVC.Patterns.Lua.LuaFacade does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitializeLuaFacade(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.InitializeLuaFacade(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NotifyObservers(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    PureMVC.Patterns.INotification notification = (PureMVC.Patterns.INotification)translator.GetObject(L, 2, typeof(PureMVC.Patterns.INotification));
                    
                    __cl_gen_to_be_invoked.NotifyObservers( notification );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendNotification_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string notificationName = LuaAPI.lua_tostring(L, 1);
                    
                    PureMVC.Patterns.Lua.LuaFacade.SendNotification( notificationName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)) 
                {
                    string notificationName = LuaAPI.lua_tostring(L, 1);
                    object body = translator.GetObject(L, 2, typeof(object));
                    
                    PureMVC.Patterns.Lua.LuaFacade.SendNotification( notificationName, body );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string notificationName = LuaAPI.lua_tostring(L, 1);
                    object body = translator.GetObject(L, 2, typeof(object));
                    string type = LuaAPI.lua_tostring(L, 3);
                    
                    PureMVC.Patterns.Lua.LuaFacade.SendNotification( notificationName, body, type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to PureMVC.Patterns.Lua.LuaFacade.SendNotification!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitializeLuaMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.InitializeLuaMediator(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterLuaMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    PureMVC.Patterns.Lua.ILuaMediator luaMediator = (PureMVC.Patterns.Lua.ILuaMediator)translator.GetObject(L, 2, typeof(PureMVC.Patterns.Lua.ILuaMediator));
                    
                    __cl_gen_to_be_invoked.RegisterLuaMediator( luaMediator );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveLuaMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaMediatorName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RemoveLuaMediator( luaMediatorName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RetrieveLuaMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaMediatorName = LuaAPI.lua_tostring(L, 2);
                    
                        PureMVC.Patterns.Lua.ILuaMediator __cl_gen_ret = __cl_gen_to_be_invoked.RetrieveLuaMediator( luaMediatorName );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasLuaMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaMediatorName = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.HasLuaMediator( luaMediatorName );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitializeLuaCommand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.InitializeLuaCommand(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterLuaCommand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    PureMVC.Patterns.Lua.ILuaCommnad luaCommand = (PureMVC.Patterns.Lua.ILuaCommnad)translator.GetObject(L, 2, typeof(PureMVC.Patterns.Lua.ILuaCommnad));
                    
                    __cl_gen_to_be_invoked.RegisterLuaCommand( luaCommand );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveLuaCommand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaCommandName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RemoveLuaCommand( luaCommandName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasLuaCommand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaCommand = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.HasLuaCommand( luaCommand );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitializeLuaProxy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.InitializeLuaProxy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterLuaProxy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    PureMVC.Patterns.Lua.ILuaProxy luaProxy = (PureMVC.Patterns.Lua.ILuaProxy)translator.GetObject(L, 2, typeof(PureMVC.Patterns.Lua.ILuaProxy));
                    
                    __cl_gen_to_be_invoked.RegisterLuaProxy( luaProxy );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveLuaProxy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaProxyName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RemoveLuaProxy( luaProxyName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RetrieveLuaProxy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaProxyName = LuaAPI.lua_tostring(L, 2);
                    
                        PureMVC.Patterns.Lua.ILuaProxy __cl_gen_ret = __cl_gen_to_be_invoked.RetrieveLuaProxy( luaProxyName );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasLuaProxy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaProxyName = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.HasLuaProxy( luaProxyName );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitializeLuaHandler(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.InitializeLuaHandler(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterLuaHandler(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    PureMVC.Patterns.Lua.ILuaHandler luaHandler = (PureMVC.Patterns.Lua.ILuaHandler)translator.GetObject(L, 2, typeof(PureMVC.Patterns.Lua.ILuaHandler));
                    
                    __cl_gen_to_be_invoked.RegisterLuaHandler( luaHandler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveLuaHandler(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaHandlerName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RemoveLuaHandler( luaHandlerName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RetrieveLuaHandler(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaHandlerName = LuaAPI.lua_tostring(L, 2);
                    
                        PureMVC.Patterns.Lua.ILuaHandler __cl_gen_ret = __cl_gen_to_be_invoked.RetrieveLuaHandler( luaHandlerName );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasLuaHandler(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PureMVC.Patterns.Lua.LuaFacade __cl_gen_to_be_invoked = (PureMVC.Patterns.Lua.LuaFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string luaHandlerName = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.HasLuaHandler( luaHandlerName );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushAny(L, PureMVC.Patterns.Lua.LuaFacade.Instance);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
