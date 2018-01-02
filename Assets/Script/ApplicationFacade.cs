 
using PureMVC.Patterns;
using UnityEngine;

namespace Game
{
	public class ApplicationFacade : Facade
    {
        public ApplicationFacade()
        {

        }

        public void StartUp( ProgramEntry mainUI )
		{
            Debug.Log("PureMVC framework start up...");
			SendNotification (NotificationType.START,mainUI);
		}

	    public void ShutDown()
	    {
            Debug.Log("PureMVC framewrok shut down...");
            SendNotification(NotificationType.QIUT);
	    }

		protected override void InitializeController ()
		{
			base.InitializeController ();
            RegisterCommand(NotificationType.START,new StartUpCommand());
		    RegisterCommand(NotificationType.QIUT, new ShutDownCommand());
        }
    }
}