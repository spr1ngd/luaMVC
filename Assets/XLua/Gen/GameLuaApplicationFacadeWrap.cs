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
    public class GameLuaApplicationFacadeWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.LuaApplicationFacade);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartUp", _m_StartUp);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShutDown", _m_ShutDown);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaMediator", _m_RegisterLuaMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaCommand", _m_RegisterLuaCommand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaHandler", _m_RegisterLuaHandler);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLuaProxy", _m_RegisterLuaProxy);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "luaEnv", _g_get_luaEnv);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "luaEnv", _s_set_luaEnv);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Game.LuaApplicationFacade __cl_gen_ret = new Game.LuaApplicationFacade();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.LuaApplicationFacade constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartUp(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.LuaApplicationFacade __cl_gen_to_be_invoked = (Game.LuaApplicationFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.StartUp(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShutDown(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.LuaApplicationFacade __cl_gen_to_be_invoked = (Game.LuaApplicationFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ShutDown(  );
                    
                    
                    
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
            
            
                Game.LuaApplicationFacade __cl_gen_to_be_invoked = (Game.LuaApplicationFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string mediatorName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RegisterLuaMediator( mediatorName );
                    
                    
                    
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
            
            
                Game.LuaApplicationFacade __cl_gen_to_be_invoked = (Game.LuaApplicationFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string commandName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RegisterLuaCommand( commandName );
                    
                    
                    
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
            
            
                Game.LuaApplicationFacade __cl_gen_to_be_invoked = (Game.LuaApplicationFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string handlerName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RegisterLuaHandler( handlerName );
                    
                    
                    
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
            
            
                Game.LuaApplicationFacade __cl_gen_to_be_invoked = (Game.LuaApplicationFacade)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string proxyName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RegisterLuaProxy( proxyName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Game.LuaApplicationFacade.luaEnv);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_luaEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Game.LuaApplicationFacade.luaEnv = (XLua.LuaEnv)translator.GetObject(L, 1, typeof(XLua.LuaEnv));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
