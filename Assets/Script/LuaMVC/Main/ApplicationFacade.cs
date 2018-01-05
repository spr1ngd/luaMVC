 
using PureMVC.Patterns;
using UnityEngine;

namespace LuaMVC
{
	public class ApplicationFacade : Facade
    {
        public ApplicationFacade()
        {

        }

        public void StartUp( ProgramEntry mainUI )
		{ 
            LuaMVCDebug.DebugSuccess("LuaMVC framework start up..."); 
		}

	    public void ShutDown()
	    {
            LuaMVCDebug.DebugSuccess("LuaMVC framewrok shut down..."); 
	    }

		protected override void InitializeController ()
		{
			base.InitializeController (); 
        }
    }
}