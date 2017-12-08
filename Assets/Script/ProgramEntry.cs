
using UnityEngine;

namespace Game
{
	public class ProgramEntry : MonoBehaviour
	{
		[HideInInspector] public HUDView HUDView = null;
		[HideInInspector] public ChessboardView ChessboardView = null;
        [HideInInspector] public LoginView LoginView = null;
        [HideInInspector] public MessageView MessageView = null;
	    [HideInInspector] public ReadyView ReadyView = null;
	    [HideInInspector] public AccountView AccountView = null; 

	    private ApplicationFacade facade = null;
	    private LuaApplicationFacade luaFacade = null;

        private void Awake()
        {
			HUDView = this.transform.Find ("Battleground/TimerScore").GetComponent<HUDView>();
		    LoginView = this.transform.Find("Login").GetComponent<LoginView>();
			ChessboardView = this.transform.Find ("Battleground/ChessboardBackground/Chessboard").GetComponent<ChessboardView> ();
		    MessageView = this.transform.Find("Message").GetComponentInChildren<MessageView>();
		    ReadyView = this.transform.Find("Ready").GetComponent<ReadyView>();
		    AccountView = this.transform.Find("Account").GetComponent<AccountView>();
            //todo 感觉这两个还可以再结合一次
            facade = new ApplicationFacade ();
			facade.StartUp (this);
            luaFacade = new LuaApplicationFacade();
            luaFacade.StartUp();
        }
	    private void OnApplicationQuit()
	    {
	        facade.ShutDown();
	        luaFacade.ShutDown();
        }
    }
}