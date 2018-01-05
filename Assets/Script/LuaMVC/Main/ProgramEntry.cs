
using System.Collections;
using LuaMVC; 

namespace LuaMVC
{
	public class ProgramEntry : Program
	{   
		protected override void Awake()
		{ 
            LuaMVCDebug.Debug("欢迎使用LuaMVC框架.");
		}  

		protected override void Start ()
		{
			base.Start ();
		}

		protected override IEnumerator Init()
	    { 
			yield return base.Init ();
            facade = new ApplicationFacade();
            facade.StartUp(this);  
            luaFacade = new LuaApplicationFacade();  
        } 
    }
}